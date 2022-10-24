import { Component } from '@angular/core';
@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss'],
})
export class AppComponent {
  public appPages = [
    { title: 'Home', url: '/home', icon: 'home' },
    { title: 'Route finden', url: '/route', icon: 'home' },
    { title: 'Gesetze', url: '/law', icon: 'home' },
    { title: 'Erste Hilfe', url: '/aid', icon: 'home' },
    { title: 'Statistiken', url: '/stats', icon: 'home' },
  ];
  constructor() {}
}
