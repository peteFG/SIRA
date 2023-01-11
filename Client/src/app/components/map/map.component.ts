import {
  AfterViewInit,
  Component,
  Input,
  OnChanges,
  OnInit,
  SimpleChanges,
} from '@angular/core';
import { Router } from '@angular/router';
import { Feature, Map, Overlay } from 'ol';
import { OSM, Vector, Vector as VectorSource } from 'ol/source.js';
import View from 'ol/View.js';
import TileLayer from 'ol/layer/Tile.js';
import {
  DangerZone,
  DangerZonesService,
} from 'src/app/backend/danger-zones.service';
import { RouteMappingData } from 'src/app/pages/route/route.testdata';
import { fromLonLat } from 'ol/proj.js';
import { getMarkerLayer } from './map-helper';
import * as L from 'leaflet';
import 'leaflet-routing-machine';
import {
  SensorDataCoord,
  SensorDataService,
} from 'src/app/backend/sensor-data.service';
import { InfoService } from 'src/app/backend/info.service';

export enum MapState {
  initial,
  running,
  end,
}
@Component({
  selector: 'sira-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss'],
})
export class MapComponent implements OnInit, AfterViewInit, OnChanges {
  @Input() public start: RouteMappingData;
  @Input() public end: RouteMappingData;
  @Input() public showButton: boolean = true;
  @Input() public showSpeed: boolean = true;
  @Input() public showDangerZones: boolean = true;

  public map: Map;
  public dangerZones: DangerZone[] = [];
  public speedList: SensorDataCoord[] = [];
  private paragraphCoordinates: { [key: string]: number[] } = {
    '§ 67. Fahrradstraße': [15.421096501565376, 47.0437646351746],
    '§ 68. Fahrradverkehr': [15.45038882824325, 47.07617469261417],
  };

  public currentMapState: MapState = MapState.initial;

  constructor(
    private router: Router,
    private dangerZonesService: DangerZonesService,
    private sensorDataService: SensorDataService,
    private infoService: InfoService
  ) {
    this.dangerZonesService.getDangerZones();
    this.sensorDataService.loadSensorData();
    this.dangerZonesService.dangerZones$.subscribe((zones) =>
      this.dangerZonesChanged(zones)
    );
    this.sensorDataService.speedList$.subscribe((list) =>
      this.speedListChanged(list)
    );
  }

  // Routing = https://www.liedman.net/leaflet-routing-machine/
  ngOnInit() {
    L.Icon.Default.imagePath = 'assets/marker/';
    this.initializeMap();
  }

  public ngOnChanges(changes: SimpleChanges): void {
    if ((changes['showSpeed'] || changes['showDangerZones']) && this.map) {
      this.map.setLayers([
        new TileLayer({
          source: new OSM(),
        }),
      ]);
      this.renderMapWithInfo();
    }
  }

  public ngAfterViewInit(): void {
    // const map: L.Map = L.map(document.getElementById('map'));
    // var router = new L.Routing.OSRM({serviceUrl: 'http://localhost:5000/viaroute'});
    // L.Routing.control({
    //   waypoints: [
    //     L.latLng(47.0729529, 15.4350478),
    //     L.latLng(47.0678426, 15.4356588)
    //   ],
    //   show: false
    // }).addTo(map);
    //   L.Routing.control({
    //     router: L.Routing.osrmv1({
    //         serviceUrl: `http://router.project-osrm.org/route/v1/`
    //     }),
    //     showAlternatives: true,
    //     fitSelectedRoutes: false,
    //     show: false,
    //     routeWhileDragging: true,
    //     waypoints: [
    //         L.latLng(47.0729529, 15.4350478),
    //         L.latLng(47.0678426, 15.4356588)
    //     ]
    // }).addTo(map);
    this.addStartAndEndMarkers();
  }

  private addStartAndEndMarkers() {
    if (this.start)
      this.map.addLayer(
        getMarkerLayer(
          [+this.start.yCoord, +this.start.xCoord],
          [48, 48],
          'map-marker.png',
          null,
          'marker'
        )
      );
    if (this.end)
      this.map.addLayer(
        getMarkerLayer(
          [+this.end.yCoord, +this.end.xCoord],
          [48, 48],
          'map-marker-check.png',
          null,
          'marker'
        )
      );
  }

  private initializeMap() {
    const view = new View({
      center: fromLonLat([15.4374655, 47.0709409]),
      zoom: 16,
    });
    this.map = new Map({
      layers: [
        new TileLayer({
          source: new OSM(),
        }),
      ],
      target: document.getElementById('map'),
      view,
    });
    this.renderMap();
    if (this.dangerZones && this.dangerZones.length > 0 && this.map)
      this.renderMapWithInfo();
  }

  public dangerZonesChanged(zones: DangerZone[]): void {
    this.dangerZones = zones;
    if (this.dangerZones && this.dangerZones.length > 0 && this.map)
      this.renderMapWithInfo();
  }

  public speedListChanged(list: SensorDataCoord[]) {
    this.speedList = list;
    if (this.speedList && this.speedList.length > 0 && this.map)
      this.renderMapWithInfo();
  }

  private addStaticMarkers() {
    for (const item of Object.keys(this.paragraphCoordinates)) {
      this.map.addLayer(
        getMarkerLayer(
          this.paragraphCoordinates[item],
          [24, 24],
          'paragraph.png',
          JSON.stringify({ text: item }),
          'paragraph'
        )
      );
    }
  }

  private renderMapWithInfo() {
    this.addStaticMarkers();
    if (this.showDangerZones) {
      for (const zone of this.dangerZones) {
        this.map.addLayer(
          getMarkerLayer(
            [+zone.yCoord, +zone.xCoord],
            [24, 24],
            'alert-circle.png',
            JSON.stringify(zone),
            'dangerZone'
          )
        );
      }
    }
    if (this.showSpeed) {
      const speedAvg = this.speedList
        ? this.speedList.reduce((a, b) => a + b.difference, 0) /
          this.speedList.length
        : 0;
      for (const speed of this.speedList) {
        this.map.addLayer(
          getMarkerLayer(
            [+speed.yCoord, +speed.xCoord],
            [24, 24],
            speed.difference < speedAvg
              ? 'speed/speedometer_green.png'
              : 'speed/speedometer_red.png',
            JSON.stringify(speed),
            'speed'
          )
        );
      }
    }

    if (
      (this.showDangerZones || this.showSpeed) &&
      (this.dangerZones.length > 0 || this.speedList.length > 0)
    ) {
      const container = document.getElementById('popup');
      const closer = document.getElementById('popup-closer');
      if (closer && container) {
        closer.onclick = function () {
          overlay.setPosition(undefined);
          closer.blur();
          return false;
        };
        const overlay = new Overlay({
          element: container,
          autoPan: {
            animation: {
              duration: 250,
            },
          },
        });
        this.map.addOverlay(overlay);
        this.mapClickHandler(overlay);
      }
    }
    this.renderMap();
  }

  private mapClickHandler(overlay: any) {
    const content = document.getElementById('popup-content');
    const mapInst = this.map;
    const infoS = this.infoService;
    this.map.on('singleclick', function (event) {
      const feature = mapInst.forEachFeatureAtPixel(
        event.pixel,
        function (feat, layer) {
          const customProps = feat.get('id_');
          const type = customProps['type'];
          const object = JSON.parse(customProps['objectJson']);

          let innerHtmlTooltipContent = '';
          let htmlTooltipContent = '';
          if (type === 'dangerZone') {
            if (object.toolTipText.toLowerCase().startsWith('lagergasse')) {
              htmlTooltipContent =
                "<div style='width: 200px'><img alt='Lagergasse' " +
                "src='../../../assets/dangerZones/Hergottwiesgasse.png' style='height: 100px; width: 200px;'/></div>";
            } else {
              innerHtmlTooltipContent = object.toolTipText;
            }
          } else if (type === 'paragraph') {
            infoS.setCurrentInfoFromMap(object.text);
          }
          if (type !== 'paragraph') {
            if (htmlTooltipContent) {
              content.innerHTML = htmlTooltipContent;
              overlay.setPosition(event.coordinate);
            } else if (innerHtmlTooltipContent) {
              content.innerHTML = '<div>' + innerHtmlTooltipContent + '</div>';
              overlay.setPosition(event.coordinate);
            }
          }
          return feat;
        }
      );
    });
  }

  private renderMap() {
    setTimeout(() => this.map.updateSize());
  }

  public onMapButtonClicked() {
    if (this.currentMapState === MapState.initial)
      this.currentMapState = MapState.end;
    else if (this.currentMapState === MapState.end) {
      this.router.navigate(['/route-data']);
    }
  }
}
