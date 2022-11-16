import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Map } from 'ol';
import { OSM, Vector as VectorSource } from 'ol/source.js';
import View from 'ol/View.js';
import TileLayer from 'ol/layer/Tile.js';
import { transform } from 'ol/proj.js';

@Component({
  selector: 'sira-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss'],
})
export class MapComponent implements OnInit {
  public map: Map;
  constructor(private router: Router) {}

  // Routing = https://www.liedman.net/leaflet-routing-machine/

  ngOnInit() {
    const view = new View({
      center: [0, 0],
      zoom: 13,
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
    this.map
      .getView()
      .setCenter(transform([15.439504, 47.070714], 'EPSG:4326', 'EPSG:3857'));
    setTimeout(() => this.map.updateSize(), 500);
  }
}
