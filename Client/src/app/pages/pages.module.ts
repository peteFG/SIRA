import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import { FormsModule } from '@angular/forms';

import { HomePageRoutingModule } from './pages-routing.module';
import { ComponentsModule } from '../components/components.module';
import { HomePage } from './home/home.page';
import { LawPage } from './law/law.page';
import { RoutePage } from './route/route.page';
import { AidPage } from './aid/aid.page';
import { StatsPage } from './stats/stats.page';
import { InfoDetailPage } from './subpages/info-detail/info-detail.page';
import { SiraInfoPage } from './sira-info/sira-info.page';
import { UploadDataPage } from './upload-data/upload-data.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    HomePageRoutingModule,
    ComponentsModule,
  ],
  declarations: [
    HomePage,
    LawPage,
    RoutePage,
    AidPage,
    LawPage,
    StatsPage,
    InfoDetailPage,
    SiraInfoPage,
    UploadDataPage,
  ],
})
export class PagesModule {}
