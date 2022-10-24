import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { PagesRoutingModule } from './pages-routing.module';
import { HomePage } from './home/home.page';
import { StatsPage } from './stats/stats.page';
import { LawPage } from './law/law.page';
import { RoutePage } from './route/route.page';
import { AidPage } from './aid/aid.page';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    PagesRoutingModule
  ],
  declarations: [HomePage, StatsPage, LawPage, RoutePage, AidPage]
})
export class PagesModule {}
