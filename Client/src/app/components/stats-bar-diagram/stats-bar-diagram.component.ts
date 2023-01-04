import { AfterViewInit, Component, Input } from '@angular/core';
import { OvertakingDistance } from 'src/app/backend/sensor-data.service';
import {  ChartConfiguration, ChartType, Color } from 'chart.js';

@Component({
  selector: 'stats-bar-diagram',
  templateUrl: './stats-bar-diagram.component.html',
  styleUrls: ['./stats-bar-diagram.component.scss'],
})
export class StatsBarDiagramComponent implements AfterViewInit{
  @Input() public data: OvertakingDistance[];
  public lineChartData: Array<number>;
  public lineChartLabels: Array<string>;
  public labelMFL: Array<any>;

  public ngAfterViewInit(): void {
    this.lineChartData = this.data.map(item => item.amount);
    this.lineChartLabels = this.data.map(item => item.range + " m");
    this.labelMFL = [
      { data: this.lineChartData,
        label: "Anzahl der Überholungen innerorts",
        backgroundColor: ["#537953"]
      }
    ];
  }

  constructor(  ) { }

  public lineChartOptions: ChartConfiguration['options'] = {
    indexAxis: 'y',
    responsive: true,
    maintainAspectRatio: false,
    scales: {
      x: {
      },
      y: {
      }
    },
    plugins: {
      legend: {
        labels: {
            boxWidth: 25,
            font: {
                size: 16
            }
        },
        display: true,
      },
    }
  };

      public chartType: ChartType = 'bar';
      public barChartColors: any[] = [
        { backgroundColor: '#537953' }
      ];

  public chartClicked(e: any): void {
    console.log(e);
  }
  public chartHovered(e: any): void {
    console.log(e);
  }
}
