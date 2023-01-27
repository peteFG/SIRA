import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpRequest } from '@angular/common/http';
import { saveAs } from 'file-saver';
import * as Nominatim from "nominatim-browser";

@Injectable({ providedIn: 'root' })
export class BackendService {
  // private backendUrl = 'http://localhost:5011/api/';
  private backendUrl = 'http://192.168.109.65:5011/api/';
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

  public async sensorData(url: string) {
    return this.httpClient.get(this.sensorDataUrl + url).toPromise();
  }

  public async getStreetName(lat: string, lng: string) {
    const coord = [lat, lng] 
    // this.httpClient.get(`https://nominatim.openstreetmap.org/reverse?lat=${coord[0]}&lon=${coord[1]}&format=json`, {
    //   headers: {
    //     'User-Agent': 'ID of your APP/service/website/etc. v0.1'
    //   }
    // }).toPromise().then(res => console.log("### ", res))
    //https://nominatim.openstreetmap.org/reverse?format=jsonv2&lat=47.0604245&lon=15.4339965
    // Nominatim.geocode({
    //       city: "Minneapolis",
    //       state: "MN",
    //       country: "US",
    //       addressdetails: true
    //   }).then((result: Nominatim.NominatimResponse) => {
    //   console.log("### ", result);
    // })
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
