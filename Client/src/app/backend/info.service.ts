import { Injectable } from '@angular/core';
import { BackendService } from './backend.service';
import { BehaviorSubject, Observable } from 'rxjs';

export interface Info {
  title: string;
  category?: string;
  section?: string;
  text: string;
  icon?: string;
}

@Injectable({ providedIn: 'root' })
export class InfoService {
  private infoLawList = new BehaviorSubject<Info[]>([]);
  public infoLawList$ = this.infoLawList.asObservable();

  private infoFirstAidList = new BehaviorSubject<Info[]>([]);
  public infoFirstAidList$ = this.infoFirstAidList.asObservable();

  constructor(private backendService: BackendService) {}

  public getInfoLawList() {
    this.backendService
      .info('ListInfos')
      .then((res) => this.infoLawList.next((res as Info[])));
  }

  public getInfoFirstAidList() {
    const infoList: Info[] = [];
    for (let i = 0; i < 10; i++) {
      infoList.push({
        title: 'Info ' + i,
        icon: "bag-add",
        text:
          'Lorem ipsum dolor sit amet, consetetur ' +
          'sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut ' +
          'labore et dolore magna aliquyam erat, sed diam voluptua. ' +
          'At vero eos et accusam et justo duo dolores et ea rebum. ' +
          'Stet clita kasd gubergren, no sea takimata sanctus est Lorem ' +
          'ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur ' +
          'sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore ' +
          'et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam ' +
          'et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea ' +
          'takimata sanctus est Lorem ipsum dolor sit amet. ',
      });
    }
    this.infoFirstAidList.next(infoList);
  }
}
