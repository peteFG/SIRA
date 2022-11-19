import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpRequest } from '@angular/common/http';
import { saveAs } from 'file-saver';

@Injectable({ providedIn: 'root' })
export class BackendService {
  private backendUrl = 'http://localhost:5011/api/';
  private sampleUrl = this.backendUrl + 'sample/';
  private infoUrl = this.backendUrl + 'Info/';
  private commonFilesUrl = this.backendUrl + 'CommonFiles/';
  private sensorDataUrl = this.backendUrl + 'SensorData/';
  private dangerZoneUrl = this.backendUrl + 'DangerZone/';
  constructor(private httpClient: HttpClient) {}

  public async sample(url: string) {
    return this.httpClient.get(this.sampleUrl + url).toPromise();
  }

  public async info(url: string) {
    return this.httpClient.get(this.infoUrl + url).toPromise();
  }

  public async dangerZone(url: string) {
    return this.httpClient.get(this.dangerZoneUrl + url).toPromise();
  }

  public async downloadCommonFile(url: string, filename: string = 'sira.pdf') {
    return this.httpClient
      .get(this.commonFilesUrl + url, { responseType: 'blob' })
      .subscribe((blob) => {
        saveAs(blob, filename, {
          type: 'text/plain;charset=windows-1252',
        });
      });
  }

  public uploadSensorData(formData: FormData) {
    return new Promise<string>((resolve) => {
      this.httpClient
        .post(this.sensorDataUrl + 'ParseSensorData', formData, {
          headers: this.getUploadOptions(),
        })
        .toPromise()
        .then((response) => {
          console.log('Datei erfolgreich hochgeladen', response);
          resolve('Datei erfolgreich hochgeladen');
        })
        .catch((err) => {
          if (err.status !== 200) {
            resolve(
              'Datei konnte nicht hochgeladen werden. Siehe Konsole für die Fehlermeldung.'
            );
            console.error('Error while uploading file', err);
          } else {
            resolve('Datei erfolgreich hochgeladen');
          }
        });
    });
  }

  private getUploadOptions = (): HttpHeaders => {
    const headers = new HttpHeaders();
    headers.set('Accept', 'application/json');
    headers.delete('Content-Type');
    return headers;
  };
}
