import { Component, OnInit } from '@angular/core';
import { Info, InfoService } from 'src/app/backend/info.service';

@Component({
  selector: 'aid-page',
  templateUrl: './aid.page.html',
  styleUrls: ['./aid.page.scss'],
})
export class AidPage implements OnInit {

  public firstAidInfos: Info[] = [];
  constructor(public infoService: InfoService) {
    this.infoService.getInfoFirstAidList();
    this.infoService.infoFirstAidList$.subscribe((list) => (this.firstAidInfos = list));
  }

  ngOnInit() {}

}
