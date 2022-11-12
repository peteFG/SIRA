import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Info, InfoService } from 'src/app/backend/info.service';

@Component({
  selector: 'law-page',
  templateUrl: './law.page.html',
  styleUrls: ['./law.page.scss'],
})
export class LawPage implements OnInit {
  public lawInfos: Info[] = [];
  constructor(public infoService: InfoService) {
    this.infoService.getInfoLawList();
    this.infoService.infoLawList$.subscribe((list) => (this.lawInfos = list));
  }

  ngOnInit() {}
}
