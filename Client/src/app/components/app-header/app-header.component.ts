import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './app-header.component.html',
  styleUrls: ['./app-header.component.scss'],
})
export class AppHeaderComponent implements OnInit {

  constructor(public router: Router) {
   }

  ngOnInit() {
  }

  public onNavigateToPage(page: string) {
    this.router.navigate(['/'+page]);
  }
}
