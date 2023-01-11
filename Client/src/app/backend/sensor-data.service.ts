import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { BackendService } from './backend.service';

export interface SensorDataCoord {
  xCoord: string;
  yCoord: string;
  difference: number;
  type: string;
  desc?: string;
}

export interface OvertakingDistance {
  range: string;
  amount: number;
  rangeFrom: number;
  rangeTo: number;
  type: string;
}

@Injectable({ providedIn: 'root' })
export class SensorDataService {
  private speedList = new BehaviorSubject<SensorDataCoord[]>([]);
  public speedList$ = this.speedList.asObservable();

  private altitudeList = new BehaviorSubject<SensorDataCoord[]>([]);
  public altitudeList$ = this.altitudeList.asObservable();

  private dateRange = new BehaviorSubject<Date[]>([]);
  public dateRange$ = this.dateRange.asObservable();

  private overtakingDistanceList = new BehaviorSubject<OvertakingDistance[]>(
    []
  );
  public overtakingDistanceList$ = this.overtakingDistanceList.asObservable();
  constructor(private backendService: BackendService) {}

  public uploadFile(formData: FormData) {
    return this.backendService.uploadSensorData(formData);
  }

  public loadSensorDataRange() {
    this.backendService.sensorData('LoadAllSensorDataPoints').then(() => {
      this.backendService.sensorData('GetMinimumAndMaximumDate').then((res) => {
        this.dateRange.next([new Date(res[0]), new Date(res[1])]);
      });
    });
  }

  public loadSensorData() {
    this.backendService.sensorData('LoadAllSensorDataPoints').then(() => {
      this.loadOvertakes();
      this.loadSensorDataSpeed();
    });
  }

  private loadOvertakes() {
    this.backendService.sensorData('GetOvertakes').then((result) => {
      this.overtakingDistanceList.next(
        (result as any[]).filter(
          (item) => item.type === 'OvertakingDistance'
        ) as OvertakingDistance[]
      );
    });
  }

  public loadSensorDataSpeed() {
    this.backendService
      .sensorData('GetNotableSpeedDifferences')
      .then((result) => {
        let res = (result as any[]).filter(
          (item) => item.type === 'Speed'
        ) as SensorDataCoord[];
        // res.forEach(item => {
        //   this.backendService.getStreetName(item.xCoord, item.yCoord).then(res => {
        //     // console.log("### ", (<any>res).result);
        //   });
        //   // item.desc =  await this.backendService.getStreetName(item.xCoord, item.yCoord)
        // });
        this.speedList.next(res);
      });
  }
}
