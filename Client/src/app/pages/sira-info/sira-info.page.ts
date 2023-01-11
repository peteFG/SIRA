import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { SensorDataService } from 'src/app/backend/sensor-data.service';

@Component({
  selector: 'sira-info-page',
  templateUrl: './sira-info.page.html',
  styleUrls: ['./sira-info.page.scss'],
})
export class SiraInfoPage {
  public dataStart: Date;
  public dataEnd: Date;
  constructor(
    private router: Router,
    private sensorDataService: SensorDataService
  ) {
    this.sensorDataService.loadSensorDataRange();
    this.sensorDataService.dateRange$.subscribe((range) => {
      this.dataStart = range[0];
      this.dataEnd = range[1];
    });
  }

  public onNavigateToUploadDataPage() {
    if (window.location.href.startsWith('http://localhost'))
      this.router.navigate(['/upload-data']);
  }
}
