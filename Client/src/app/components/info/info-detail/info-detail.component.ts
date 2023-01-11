import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonFileService } from 'src/app/backend/common-files.service';
import { Info, InfoService } from 'src/app/backend/info.service';

@Component({
  selector: 'info-detail',
  templateUrl: './info-detail.component.html',
  styleUrls: ['./info-detail.component.scss'],
  providers: [CommonFileService],
})
export class InfoDetailComponent implements OnInit {
  public info: Info;
  public filteredInfo: Info;
  public filterText: string;
  constructor(
    public router: Router,
    public infoService: InfoService,
    private commonFileService: CommonFileService
  ) {
    this.infoService.currentInfo$.subscribe((inf) => {
      this.info = inf;
      console.log('### ', this.info);
      this.filteredInfo = inf;
    });
  }

  ngOnInit() {}

  public onFilterText(event) {
    this.filterText = event.target.value;
  }

  public onAccidentReportClicked() {
    this.commonFileService.downloadEmergencyReport();
  }
}
