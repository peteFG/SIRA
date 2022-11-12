import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { InfoService } from 'src/app/backend/info.service';

@Component({
  selector: 'app-button',
  templateUrl: './app-button.control.html',
  styleUrls: ['./app-button.control.scss'],
})
export class AppButtonControl implements OnInit {
  @Input() public text: string;
  @Output() public onClick = new EventEmitter();
  constructor(public router: Router, public infoService: InfoService) {}

  ngOnInit() {
  }

  public onButtonClicked() {
    this.onClick.emit();
  }
}
