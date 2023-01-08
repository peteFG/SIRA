import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OvertakingDistance, SensorDataCoord, SensorDataService } from 'src/app/backend/sensor-data.service';

@Component({
  selector: 'stats-page',
  templateUrl: './stats.page.html',
  styleUrls: ['./stats.page.scss']
})
export class StatsPage implements OnInit {
  public selValue: string = "dangerZones";

  public speedList: SensorDataCoord[] = [];
  public altitudeList: SensorDataCoord[] = [];
  public overtakingDistanceList: OvertakingDistance[] = [];

  public isDangerZoneChecked: boolean = true;
  public isSpeedChecked: boolean = true;

  constructor(private sensorDataService: SensorDataService) { 
    this.sensorDataService.loadSensorData();
    this.sensorDataService.altitudeList$.subscribe(list => this.altitudeList = list);
    this.sensorDataService.speedList$.subscribe(list => this.speedList = list);
    this.sensorDataService.overtakingDistanceList$.subscribe(list => this.overtakingDistanceList = list);
  }

  ngOnInit() {
  }

  public onSelValueChanged(event: any) {
    if(event && event.detail) {
      this.selValue = event.detail.value;
    }
  }

}
