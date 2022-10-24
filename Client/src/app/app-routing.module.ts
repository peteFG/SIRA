import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { AidPage } from './pages/aid/aid.page';
import { HomePage } from './pages/home/home.page';
import { LawPage } from './pages/law/law.page';
import { RoutePage } from './pages/route/route.page';
import { StatsPage } from './pages/stats/stats.page';

const routes: Routes = [
  {
    path: '',
    redirectTo: '/home',
    pathMatch: "full"
  },
  {
    path: 'home',
    component: HomePage
  },
  {
    path: 'aid',
    component: AidPage
  },
  {
    path: 'law',
    component: LawPage
  },
  {
    path: 'stats',
    component: StatsPage
  },
  {
    path: 'route',
    component: RoutePage
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {}
