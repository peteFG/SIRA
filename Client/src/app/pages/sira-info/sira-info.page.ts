import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'sira-info-page',
  templateUrl: './sira-info.page.html',
  styleUrls: ['./sira-info.page.scss'],
})
export class SiraInfoPage {
  constructor(private router: Router) {
  }

  public onNavigateToUploadDataPage() {
    if(window.location.href.startsWith("http://localhost"))
      this.router.navigate(['/upload-data']);
  }
}
