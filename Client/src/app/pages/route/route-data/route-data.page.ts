import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { dataCheckValues } from './route-data.testdata';

export interface DataCheckState {
  state: boolean;
  text: string;
  value: string;
}

@Component({
  selector: 'route-data-page',
  templateUrl: './route-data.page.html',
  styleUrls: ['./route-data.page.scss'],
})
export class RouteDataPage implements OnInit {
  public dataCheckValues = dataCheckValues;
  public isAllChecked: boolean = false;
  public isSaveStateChecked: boolean = false;
  public buttonText: string = "Ohne Auswahl fortfahren";
  constructor(private router: Router) {}

  ngOnInit() {
    const item = localStorage.getItem("dataCheckValues");
    if(item) {
      this.dataCheckValues = JSON.parse(item);
      this.setButtonText();
    }
  }

  public onModalClosed() {
    this.router.navigate(['/home']);
  }

  public onAllStateChanged() {
    if(this.isAllChecked)
      this.dataCheckValues.forEach(val => val.state = true);
      this.setButtonText();
    }

  public onCheckboxStateChanged() {
    this.isAllChecked = this.dataCheckValues.filter(val => !val.state).length === 0;
    this.setButtonText();
  }

  public setButtonText() {
    this.buttonText = this.dataCheckValues.filter(val => val.state).length > 0 ? "Daten übermitteln": "Ohne Auswahl fortfahren";
  }

  public onSendData() {
    if(this.isSaveStateChecked) {
      localStorage.setItem("dataCheckValues", JSON.stringify(this.dataCheckValues));
    }
  }
}
