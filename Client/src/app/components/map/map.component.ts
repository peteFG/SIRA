import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Map } from 'ol';
import { OSM, Vector as VectorSource } from 'ol/source.js';
import View from 'ol/View.js';
import TileLayer from 'ol/layer/Tile.js';

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
    this.map = new Map({
      layers: [
        new TileLayer({
          source: new OSM(),
        }),
      ],
      target: document.getElementById('map'),
      view: new View({
        center: [0, 0],
        zoom: 3,
      }),
    });
    setTimeout(() => {
      this.map.updateSize();
    }, 500);
  }
}
