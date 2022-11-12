import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { saveAs } from 'file-saver';

@Injectable({ providedIn: 'root' })
export class BackendService {
  private backendUrl = 'http://localhost:5011/api/';
  private sampleUrl = this.backendUrl + 'sample/';
  private infoUrl = this.backendUrl + 'Info/';
  private commonFilesUrl = this.backendUrl + 'CommonFiles/';
  constructor(private httpClient: HttpClient) {}

  public async sample(url: string) {
    return this.httpClient.get(this.sampleUrl + url).toPromise();
  }

  public async info(url: string) {
    return this.httpClient.get(this.infoUrl + url).toPromise();
  }

  public async downloadCommonFile(url: string, filename: string = "sira.pdf") {
    return this.httpClient.get(this.commonFilesUrl + url, { responseType: 'blob' }).subscribe(blob => {
      saveAs(blob, filename, {
         type: 'text/plain;charset=windows-1252' // --> or whatever you need here
      });
    });
  }
}
