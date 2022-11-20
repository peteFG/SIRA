import { AfterViewInit, Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Feature, Map, Overlay } from 'ol';
import { OSM, Vector, Vector as VectorSource } from 'ol/source.js';
import View from 'ol/View.js';
import TileLayer from 'ol/layer/Tile.js';
import { toLonLat } from 'ol/proj.js';
import {
  DangerZone,
  DangerZonesService,
} from 'src/app/backend/danger-zones.service';
import { RouteMappingData } from 'src/app/pages/route/route.testdata';
import { toStringHDMS } from 'ol/coordinate';
import {fromLonLat} from 'ol/proj.js';
import { defaultCircleRadius, getCircleFeatureStyle, getMarkerLayer, getVectorLayer } from './map-helper';
import * as L from 'leaflet';
import 'leaflet-routing-machine';
import { Icon, Style } from 'ol/style';
import VectorLayer from 'ol/layer/Vector';
import Point from 'ol/geom/Point';

export enum MapState {
  initial,
  running,
  end
}
@Component({
  selector: 'sira-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss'],
})
export class MapComponent implements OnInit, AfterViewInit {
  @Input() public start: RouteMappingData;
  @Input() public end: RouteMappingData;

  public map: Map;
  public dangerZones: DangerZone[] = [];

  public currentMapState: MapState = MapState.initial;

  constructor(
    private router: Router,
    private dangerZonesService: DangerZonesService
  ) {
    this.dangerZonesService.getDangerZones();
    this.dangerZonesService.dangerZones$.subscribe(
      (zones) => (this.dangerZonesChanged(zones))
    );
  }

  // Routing = https://www.liedman.net/leaflet-routing-machine/
  ngOnInit() {
    L.Icon.Default.imagePath = "assets/marker/";
   this.initializeMap();
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
    this.addMarkers();
  }


  private addMarkers() {
    this.map.addLayer(getMarkerLayer([+this.start.yCoord, +this.start.xCoord]));
    this.map.addLayer(getMarkerLayer([+this.end.yCoord, +this.end.xCoord], "map-marker-check.png"));
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
    if(this.dangerZones && this.dangerZones.length > 0 && this.map)
      this.renderMapWithDangerZones();
  }
    
  public dangerZonesChanged(zones: DangerZone[]): void {
    this.dangerZones = zones;
    if(this.dangerZones && this.dangerZones.length > 0 && this.map)
      this.renderMapWithDangerZones();
  }

  private renderMapWithDangerZones() {
    for(const zone of this.dangerZones) {
      const circleFeature = getCircleFeatureStyle([+zone.yCoord,+zone.xCoord]);
      this.map.addLayer(getVectorLayer(circleFeature));
    }
    if(this.dangerZones.length > 0) {
      const container = document.getElementById('popup');
      const closer = document.getElementById('popup-closer');
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
    this.renderMap();
  }


  private mapClickHandler(overlay: any) {
    const content = document.getElementById('popup-content');
    const zones = this.dangerZones;
    this.map.on('singleclick', function (evt) {
      const coordinate = evt.coordinate;
      let text = "";
      for(const zone of zones) {
        // Check if click is in one danger zone
        const dist = fromLonLat([+zone.yCoord, +zone.xCoord]);
        const y = dist[0];
        const x = dist[1];
        if((y-defaultCircleRadius) < coordinate[0] && (y+defaultCircleRadius) > coordinate[0]
        && (x-defaultCircleRadius) < coordinate[1] && (x+defaultCircleRadius) > coordinate[1]) {
          text = zone.toolTipText;
          break;
        }
      }
      if(text) {
        content.innerHTML = '<div>'+text+'</div>';
        overlay.setPosition(coordinate);
      }
      
    });
  }

  private renderMap() {
    setTimeout(() => this.map.updateSize());
  }

  public onMapButtonClicked() {
    if(this.currentMapState === MapState.initial)
      this.currentMapState = MapState.end;
    else if(this.currentMapState === MapState.end) {
      this.router.navigate(['/route-data']);
    }
  }
}
