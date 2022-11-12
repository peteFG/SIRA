import { Injectable } from '@angular/core';
import { BackendService } from './backend.service';
import { BehaviorSubject, Observable } from 'rxjs';

export interface Info {
  title: string;
  category?: string;
  section?: string;
  text: string;
  icon?: string;
  action?: string;
}

@Injectable({ providedIn: 'root' })
export class InfoService {
  private infoLawList = new BehaviorSubject<Info[]>([]);
  public infoLawList$ = this.infoLawList.asObservable();

  private infoFirstAidList = new BehaviorSubject<Info[]>([]);
  public infoFirstAidList$ = this.infoFirstAidList.asObservable();

  private currentInfo = new BehaviorSubject<Info>(null);
  public currentInfo$ = this.currentInfo.asObservable();

  constructor(private backendService: BackendService) {}

  public setCurrentInfo(info:Info) {
    this.currentInfo.next(info);
  }

  public getInfoLawList() {
    this.backendService
      .info('ListLawInfos')
      .then((res) => this.infoLawList.next((res as Info[])));
  }

  public getInfoFirstAidList() {
    this.backendService
      .info('ListFirstAidInfos')
      .then((res) => this.infoFirstAidList.next((res as Info[])));
  }
}
