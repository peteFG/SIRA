import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { SensorDataService } from 'src/app/backend/sensor-data.service';

@Component({
  selector: 'sira-info-page',
  templateUrl: './sira-info.page.html',
  styleUrls: ['./sira-info.page.scss'],
})
export class SiraInfoPage {
  constructor(
    private router: Router,
    private sensorDataService: SensorDataService
  ) {
    this.sensorDataService.loadSensorDataRange();
  }

  public onNavigateToUploadDataPage() {
    if (window.location.href.startsWith('http://localhost'))
      this.router.navigate(['/upload-data']);
  }
}
