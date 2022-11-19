import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Info } from 'src/app/backend/info.service';

@Component({
  selector: 'info',
  templateUrl: './info.component.html',
  styleUrls: ['./info.component.scss'],
})
export class InfoComponent implements OnInit {
  @Input() public infoList: Info[];
  public showDetail = false;
  public clickedDetailId: string;
  constructor(public router: Router) {}

  ngOnInit() {}
}
