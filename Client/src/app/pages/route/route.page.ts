import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {
  DangerZone,
  DangerZonesService,
} from 'src/app/backend/danger-zones.service';
import { mapValues } from './route.testdata';

@Component({
  selector: 'route-page',
  templateUrl: './route.page.html',
  styleUrls: ['./route.page.scss'],
})
export class RoutePage implements OnInit {
  constructor(private dangerZonesService: DangerZonesService) {}

  public coordValues = mapValues;

  ngOnInit() {}
}
