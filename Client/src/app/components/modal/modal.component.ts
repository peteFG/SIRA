import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.scss'],
})
export class AppModalComponent implements OnInit {
  @Input()
  public trigger: string;
  @Output() public onModalClosed = new EventEmitter<void>();

  constructor() {}

  ngOnInit() {}

  public onDismissClicked(modal: any) {
    this.onModalClosed.emit();
    modal.dismiss();
  }
}
