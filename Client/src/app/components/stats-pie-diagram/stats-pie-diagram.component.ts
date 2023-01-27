import { AfterViewInit, Component, Input } from '@angular/core';
import { OvertakingDistance } from 'src/app/backend/sensor-data.service';
import { ChartConfiguration, ChartType, Color } from 'chart.js';

@Component({
  selector: 'stats-pie-diagram',
  templateUrl: './stats-pie-diagram.component.html',
  styleUrls: ['./stats-pie-diagram.component.scss'],
})
export class StatsPieDiagramComponent implements AfterViewInit {
  @Input() public data: OvertakingDistance[];
  public lineChartData: Array<number>;
  public lineChartLabels: Array<string>;
  public labelMFL: Array<any>;

  public ngAfterViewInit(): void {
    const overMin = this.data.filter((item) => item.rangeTo > 150)
    .reduce((sum, current) => sum + current.amount, 0);
    const underMin = this.data.filter((item) => item.rangeTo < 150)
    .reduce((sum, current) => sum + current.amount, 0);
    this.lineChartData = [overMin, underMin];
    this.lineChartLabels = ['Über 1.5m', 'Unter 1.5m'];

    this.labelMFL = [
      {
        data: this.lineChartData,
        label: 'Anzahl der Überholungen',
        backgroundColor: ['#537953', '#d91811'],
      },
    ];
  }

  constructor() {}

  public lineChartOptions: ChartConfiguration['options'] = {
    responsive: true,
    maintainAspectRatio: false,
    plugins: {
      legend: {
        reverse: true,
        display: true,
        labels: {
          boxWidth: 25,
          font: {
            size: 20,
          },
        },
      },
    },
  };

  public chartType: ChartType = 'pie';
  public chartClicked(e: any): void {
    console.log(e);
  }
  public chartHovered(e: any): void {
    console.log(e);
  }
}
