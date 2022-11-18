import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Feature, Map } from 'ol';
import { OSM, Vector as VectorSource } from 'ol/source.js';
import View from 'ol/View.js';
import TileLayer from 'ol/layer/Tile.js';
import { transform } from 'ol/proj.js';
import {
  DangerZone,
  DangerZonesService,
} from 'src/app/backend/danger-zones.service';
import { RouteMappingData } from 'src/app/pages/route/route.testdata';
import { Circle } from 'ol/geom';
import Style from 'ol/style/Style';
import { Coordinate } from 'ol/coordinate';
import VectorLayer from 'ol/layer/Vector';

@Component({
  selector: 'sira-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss'],
})
export class MapComponent implements OnInit {
  @Input() public start: RouteMappingData;
  @Input() public end: RouteMappingData;

  public map: Map;
  public dangerZones: DangerZone[];

  constructor(
    private router: Router,
    private dangerZonesService: DangerZonesService
  ) {
    this.dangerZonesService.getDangerZones();
    this.dangerZonesService.dangerZones$.subscribe(
      (zones) => (this.dangerZones = zones)
    );
  }

  // Routing = https://www.liedman.net/leaflet-routing-machine/

  ngOnInit() {
    const circleFeature = new Feature({
      geometry: new Circle([47.070536, 15.4385635], 150),
    });
    circleFeature.setStyle(
      new Style({
        renderer(coordinates, state) {
          const [[x, y], [x1, y1]] = coordinates as Coordinate[];
          const ctx = state.context;
          const dx = x1 - x;
          const dy = y1 - y;
          const radius = Math.sqrt(dx * dx + dy * dy);

          const innerRadius = 0;
          const outerRadius = radius * 1.4;

          const gradient = ctx.createRadialGradient(
            x,
            y,
            innerRadius,
            x,
            y,
            outerRadius
          );
          gradient.addColorStop(0, 'rgba(255,0,0,0)');
          gradient.addColorStop(0.6, 'rgba(255,0,0,0.2)');
          gradient.addColorStop(1, 'rgba(255,0,0,0.8)');
          ctx.beginPath();
          ctx.arc(x, y, radius, 0, 2 * Math.PI, true);
          ctx.fillStyle = gradient;
          ctx.fill();

          ctx.arc(x, y, radius, 0, 2 * Math.PI, true);
          ctx.strokeStyle = 'rgba(255,0,0,1)';
          ctx.stroke();
        },
      })
    );

    const view = new View({
      center: [0, 0],
      zoom: 13,
    });
    this.map = new Map({
      layers: [
        new TileLayer({
          source: new OSM(),
        }),
        new VectorLayer({
          source: new VectorSource({
            features: [circleFeature],
          }),
        }),
      ],
      target: document.getElementById('map'),
      view,
    });
    this.map
      .getView()
      .setCenter(transform([15.439504, 47.070714], 'EPSG:4326', 'EPSG:3857'));
    setTimeout(() => this.map.updateSize(), 500);
  }
}
