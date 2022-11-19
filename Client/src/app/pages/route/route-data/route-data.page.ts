import { Component, OnInit } from '@angular/core';
import { dataCheckValues } from './route-data.testdata';

export interface DataCheckState {
  state: boolean;
  text: string;
  value: string;
}

@Component({
  selector: 'route-data-page',
  templateUrl: './route-data.page.html',
  styleUrls: ['./route-data.page.scss'],
})
export class RouteDataPage implements OnInit {
  public dataCheckValues = dataCheckValues;
  constructor() {}


  ngOnInit() {}
}
