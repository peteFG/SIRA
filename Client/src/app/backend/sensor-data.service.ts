import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { BackendService } from './backend.service';

export interface SensorDataCoord {
  xCoord: string;
  yCoord: string;
  difference: number;
  type: string;
}

export interface OvertakingDistance {
  range: string;
  amount: number;
  rangeFrom: number;
  rangeTo: number;
  type: string;
}

@Injectable()
export class SensorDataService {
  private speedList$ = new BehaviorSubject<SensorDataCoord[]>([]);
  public speedList = this.speedList$.asObservable();

  private altitudeList$ = new BehaviorSubject<SensorDataCoord[]>([]);
  public altitudeList = this.altitudeList$.asObservable();

  private overtakingDistanceList$ = new BehaviorSubject<OvertakingDistance[]>([]);
  public overtakingDistanceList = this.overtakingDistanceList$.asObservable();
  constructor(private backendService: BackendService) {}

  public uploadFile(formData: FormData) {
    return this.backendService.uploadSensorData(formData);
  }

  public loadSensorData() {
    this.backendService.sensorData("LoadAllSensorDataPoints").then(() => {
      this.backendService.sensorData("GetNotableDifferencesAndOvertakes").then(result => {
        this.speedList$.next((result as any[]).filter(item => item.type === "Speed") as SensorDataCoord[]);
        this.altitudeList$.next((result as any[]).filter(item => item.type === "Altitude") as SensorDataCoord[]);
        this.overtakingDistanceList$.next( (result as any[]).filter(item => item.type === "OvertakingDistance") as OvertakingDistance[]);
      })
    })
  }
}
