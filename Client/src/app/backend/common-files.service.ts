import { Injectable } from '@angular/core';
import { BackendService } from './backend.service';

@Injectable()
export class CommonFileService {
  constructor(private backendService: BackendService) {}

  public downloadEmergencyReport() {
    this.backendService
      .downloadCommonFile('DownloadCommonFile/euro', 'Unfallbericht.pdf');
  }
}
