import { Injectable } from '@angular/core';
import { BackendService } from './backend.service';

@Injectable()
export class SensorDataService {
  constructor(private backendService: BackendService) {}

  public uploadFile(formData: FormData) {
    return this.backendService.uploadSensorData(formData);
  }
}
