import { AfterViewInit, Component, Input } from '@angular/core';
import { OvertakingDistance } from 'src/app/backend/sensor-data.service';
import {  ChartConfiguration, ChartType, Color } from 'chart.js';

@Component({
  selector: 'stats-pie-diagram',
  templateUrl: './stats-pie-diagram.component.html',
  styleUrls: ['./stats-pie-diagram.component.scss'],
})
export class StatsPieDiagramComponent implements AfterViewInit{
  @Input() public data: OvertakingDistance[];
  public lineChartData: Array<number>;
  public lineChartLabels: Array<string>;
  public labelMFL: Array<any>;

  public ngAfterViewInit(): void {
    const overMin = this.data.filter(item => item.rangeTo > 150).length;
    this.lineChartData = [overMin, (this.data.length - overMin)];
    this.lineChartLabels = ["Über 1.5m", "Unter 1.5m"];

    this.labelMFL = [
      { data: this.lineChartData,
        label: "Anzahl der Überholungen",
        backgroundColor: ["#537953", "#833737"]
      }
    ];
  }

  constructor(  ) { }

  public lineChartOptions: ChartConfiguration['options'] = {
    responsive: true,
    maintainAspectRatio: false,
    plugins: {
      legend: {
        display: true,
      },
    }
  };

  public chartType: ChartType = 'pie';
  public chartClicked(e: any): void {
    console.log(e);
  }
  public chartHovered(e: any): void {
    console.log(e);
  }
}
