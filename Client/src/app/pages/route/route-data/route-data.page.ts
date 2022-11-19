import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {
  DangerZone,
  DangerZonesService,
} from 'src/app/backend/danger-zones.service';

@Component({
  selector: 'route-data-page',
  templateUrl: './route-data.page.html',
  styleUrls: ['./route-data.page.scss'],
})
export class RouteDataPage implements OnInit {
  constructor() {}


  ngOnInit() {}
}
