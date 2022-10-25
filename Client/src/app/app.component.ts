import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss'],
})
export class AppComponent {
  public appPages = [
    { title: 'Home', url: '/home', icon: 'home' },
    { title: 'Route', url: '/route', icon: 'swap-horizontal' },
    { title: 'Gesetze', url: '/law', icon: 'book' },
    { title: 'Erste Hilfe', url: '/aid', icon: 'add-circle' },
    { title: 'Statistiken', url: '/stats', icon: 'stats-chart' }
  ];
  constructor() {}
}
