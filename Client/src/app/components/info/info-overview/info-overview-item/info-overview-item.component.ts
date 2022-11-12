import { AfterViewInit, Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';
import { Info, InfoService } from 'src/app/backend/info.service';

@Component({
  selector: 'info-overview-item',
  templateUrl: './info-overview-item.component.html',
  styleUrls: ['./info-overview-item.component.scss'],
})
export class InfoOverviewItemComponent implements OnInit {
  @Input() public info: Info;
  constructor(public router: Router, public infoService: InfoService) {}

  ngOnInit() {
  }
}
