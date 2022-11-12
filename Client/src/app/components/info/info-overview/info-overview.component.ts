import { AfterViewInit, Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';
import { Info, InfoService } from 'src/app/backend/info.service';

@Component({
  selector: 'info-overview',
  templateUrl: './info-overview.component.html',
  styleUrls: ['./info-overview.component.scss'],
})
export class InfoOverviewComponent implements OnInit, OnChanges {
  @Input() public infoList: Info[];
  public filteredInfoList: Info[] = [];
  constructor(public router: Router, public infoService: InfoService) {}

  ngOnInit() {
  }

  public ngOnChanges(changes: SimpleChanges): void {
    if(changes["infoList"]) {
      if(this.infoList && this.infoList.length > 0)
        this.filteredInfoList = this.infoList;
  }
  }

  public filterList(event) {
    const query = event.target.value.toLowerCase();
    this.filteredInfoList = this.infoList.filter(item => item.title.toLowerCase().includes(query));
  }

  public onItemClicked(info: Info) {
    if(info.action == "EmergencyCall")
      return;
    this.infoService.setCurrentInfo(info);
    this.router.navigate([this.router.url+"/detail"]);
    
  }
}
