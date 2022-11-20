import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SensorDataCoord } from 'src/app/backend/sensor-data.service';

@Component({
  selector: 'stats-list',
  templateUrl: './stats-list.component.html',
  styleUrls: ['./stats-list.component.scss'],
})
export class StatsListComponent implements OnInit {
  @Input() public coordList: SensorDataCoord[];
  @Input() public valueSuffix: string;
  @Input() public disclaimer: string;

  constructor(private router: Router) {
   }

  ngOnInit() {
  }

  public onNavigateToPage(page: string) {
    this.router.navigate(['/'+page]);
  }
}
