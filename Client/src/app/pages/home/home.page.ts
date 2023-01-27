import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage {

  constructor(private router: Router) {}
  
  public onItemClicked(route: string, reloadPage: boolean = false) {
    if(reloadPage)
      window.location.href = "/"+route;
    else
      this.router.navigate(['/'+route]);
  }
}
