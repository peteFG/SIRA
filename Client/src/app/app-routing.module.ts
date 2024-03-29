import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { AidPage } from './pages/aid/aid.page';
import { LawPage } from './pages/law/law.page';
import { RouteDataPage } from './pages/route/route-data/route-data.page';
import { RoutePage } from './pages/route/route.page';
import { SiraInfoPage } from './pages/sira-info/sira-info.page';
import { StatsPage } from './pages/stats/stats.page';
import { InfoDetailPage } from './pages/subpages/info-detail/info-detail.page';
import { UploadDataPage } from './pages/upload-data/upload-data.page';

const routes: Routes = [
  {
    path: 'home',
    loadChildren: () => import('./pages/pages.module').then( m => m.PagesModule)
  },
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full'
  },
  {
    path: 'law',
    component: LawPage,
    loadChildren: () => import('./pages/pages.module').then( m => m.PagesModule)
  },
  {
    path: 'law/detail',
    component: InfoDetailPage,
    loadChildren: () => import('./pages/pages.module').then( m => m.PagesModule)
  },{
    path: 'aid/detail',
    component: InfoDetailPage,
    loadChildren: () => import('./pages/pages.module').then( m => m.PagesModule)
  },
  {
    path: 'aid',
    component: AidPage,
    loadChildren: () => import('./pages/pages.module').then( m => m.PagesModule)
  },
  {
    path: 'route',
    component: RoutePage,
    loadChildren: () => import('./pages/pages.module').then( m => m.PagesModule),
    
  },
  {
    path: 'stats',
    component: StatsPage,
    loadChildren: () => import('./pages/pages.module').then( m => m.PagesModule)
  },
  {
    path: 'info',
    component: SiraInfoPage,
    loadChildren: () => import('./pages/pages.module').then( m => m.PagesModule)
  },
  {
    path: 'upload-data',
    component: UploadDataPage,
    loadChildren: () => import('./pages/pages.module').then( m => m.PagesModule)
  },
  {
    path: 'route-data',
    component: RouteDataPage,
    loadChildren: () => import('./pages/pages.module').then( m => m.PagesModule)
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
