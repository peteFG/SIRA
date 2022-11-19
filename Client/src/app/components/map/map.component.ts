import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Map, Overlay } from 'ol';
import { OSM, Vector as VectorSource } from 'ol/source.js';
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
import { defaultCircleRadius, getCircleFeatureStyle, getVectorLayer } from './map-helper';

@Component({
  selector: 'sira-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss'],
})
export class MapComponent implements OnInit {
  @Input() public start: RouteMappingData;
  @Input() public end: RouteMappingData;

  public map: Map;
  public dangerZones: DangerZone[] = [];

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
   this.initializeMap();
  }

  private initializeMap() {
    const view = new View({
      center: fromLonLat([15.439504, 47.070714]),
      zoom: 15,
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
  }
    
  public dangerZonesChanged(zones: DangerZone[]): void {
    this.dangerZones = zones;
    for(const zone of zones) {
      const circleFeature = getCircleFeatureStyle([+zone.yCoord,+zone.xCoord]);
      this.map.addLayer(getVectorLayer(circleFeature));
    }
    if(zones.length > 0) {
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
}
