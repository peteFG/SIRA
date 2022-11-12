import { Component } from '@angular/core';
import { SensorDataService } from 'src/app/backend/sensor-data.service';

@Component({
  selector: 'upload-data-page',
  templateUrl: './upload-data.page.html',
  styleUrls: ['./upload-data.page.scss'],
  providers: [SensorDataService]
})
export class UploadDataPage {
  public fileList: File[];
  public formData: FormData;
  public uploadState: string;

  constructor(private sensorDataService: SensorDataService) {
    this.uploadState = null;
  }

  public onFileUploaded(event: any) {
    this.uploadState = null;
    const files: FileList = event.target.files;
    this.fileList = new Array<File>();
    const formData = new FormData();

    for (let i = 0; i < files.length; i++) {
      formData.append('file', files.item(i), files.item(i).name);
      this.fileList.push(files.item(i));
    }
    this.formData = formData;

  }
  public onUploadFile() {
    this.uploadState = "Daten werden hochgeladen...";
    this.sensorDataService.uploadFile(this.formData).then(response => {
      this.uploadState = response;
    });
  }
}
