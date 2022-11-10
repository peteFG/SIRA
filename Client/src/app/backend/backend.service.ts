import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({ providedIn: 'root' })
export class BackendService {
  private backendUrl = 'http://localhost:5011/api/';
  private sampleUrl = this.backendUrl + 'sample/';
  private infoUrl = this.backendUrl + 'Info/';
  constructor(private httpClient: HttpClient) {}

  public async sample(url: string) {
    return this.httpClient.get(this.sampleUrl + url).toPromise();
  }

  public async info(url: string) {
    return this.httpClient.get(this.infoUrl + url).toPromise();
  }
}
