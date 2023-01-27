import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-footer',
  templateUrl: './app-footer.component.html',
  styleUrls: ['./app-footer.component.scss'],
})
export class AppFooterComponent implements OnInit {
  constructor(public router: Router) {}

  ngOnInit() {}

  public onItemClicked(route: string, reloadPage: boolean = false) {
    if(reloadPage)
      window.location.href = "/"+route;
    else
      this.router.navigate(['/'+route]);
  }
}
