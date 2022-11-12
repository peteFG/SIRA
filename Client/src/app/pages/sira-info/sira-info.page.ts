import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Info, InfoService } from 'src/app/backend/info.service';

@Component({
  selector: 'sira-info-page',
  templateUrl: './sira-info.page.html',
  styleUrls: ['./sira-info.page.scss'],
})
export class SiraInfoPage implements OnInit {
  public lawInfos: Info[] = [];
  constructor(public infoService: InfoService) {
    this.infoService.getInfoLawList();
    this.infoService.infoLawList$.subscribe((list) => (this.lawInfos = list));
  }

  ngOnInit() {}
}
