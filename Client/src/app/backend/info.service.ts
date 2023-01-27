import { Injectable } from '@angular/core';
import { BackendService } from './backend.service';
import { BehaviorSubject, Observable } from 'rxjs';
import { Router } from '@angular/router';

export interface Info {
  title: string;
  category?: string;
  section?: string;
  text: string;
  icon?: string;
  action?: string;
  source?: string;
}

@Injectable({ providedIn: 'root' })
export class InfoService {
  private infoLawList = new BehaviorSubject<Info[]>([]);
  public infoLawList$ = this.infoLawList.asObservable();

  private infoFirstAidList = new BehaviorSubject<Info[]>([]);
  public infoFirstAidList$ = this.infoFirstAidList.asObservable();

  private currentInfo = new BehaviorSubject<Info>(null);
  public currentInfo$ = this.currentInfo.asObservable();

  constructor(private backendService: BackendService, private router: Router) {}

  public setCurrentInfo(info: Info) {
    this.currentInfo.next(info);
  }

  public getInfoLawList() {
    this.backendService
      .info('ListLawInfos')
      .then((res) => this.infoLawList.next(res as Info[]));
  }

  public getInfoFirstAidList() {
    this.backendService
      .info('ListFirstAidInfos')
      .then((res) => this.infoFirstAidList.next(res as Info[]));
  }

  public async setCurrentInfoFromMap(startText: string) {
    await this.backendService.info('ListLawInfos').then((res) => {
      const infos = res as Info[];
      this.infoLawList.next(infos);
      this.setCurrentInfo(
        infos.find(
          (item) =>
            (item.title as string).replace('§', '').toLowerCase().trim() ===
            (startText as string).replace('§', '').toLowerCase().trim()
        )
      );
      this.router.navigate(['/law/detail']);
    });
  }
}
