import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Info } from 'src/app/backend/info.service';

@Component({
  selector: 'info-overview',
  templateUrl: './info-overview.component.html',
  styleUrls: ['./info-overview.component.scss'],
})
export class InfoOverviewComponent implements OnInit {
  @Input() public infoList: Info[];

  constructor(public router: Router) {}

  ngOnInit() {}
}
