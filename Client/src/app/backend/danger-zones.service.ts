import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { BackendService } from './backend.service';

export interface DangerZone {
  xCoord: string;
  yCoord: string;
  toolTipText: string;
}

@Injectable({
  providedIn: 'root',
})
export class DangerZonesService {
  private dangerZones = new BehaviorSubject<DangerZone[]>([]);
  public dangerZones$ = this.dangerZones.asObservable();

  constructor(private backendService: BackendService) {}

  public getDangerZones() {
    this.backendService
      .dangerZone('ListDangerZones')
      .then((res) => this.dangerZones.next(res as DangerZone[]));
  }
}
