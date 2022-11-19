import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import { FormsModule } from '@angular/forms';
import { AppHeaderComponent } from './app-header/app-header.component';
import { AppFooterComponent } from './app-footer/app-footer.component';
import { MapComponent } from './map/map.component';
import { InfoOverviewComponent } from './info/info-overview/info-overview.component';
import { InfoComponent } from './info/info.component';
import { InfoDetailComponent } from './info/info-detail/info-detail.component';
import { HighlighterPipe } from '../pipes/highlighter.pipe';
import { InfoOverviewItemComponent } from './info/info-overview/info-overview-item/info-overview-item.component';
import { AppButtonControl } from './controls/app-button/app-button.control';
import { AppModalComponent } from './modal/modal.component';

const components = [
  AppHeaderComponent,
  AppFooterComponent,
  MapComponent,
  InfoOverviewComponent,
  InfoComponent,
  InfoDetailComponent,
  HighlighterPipe,
  InfoOverviewItemComponent,
  AppButtonControl,
  AppModalComponent,
];

@NgModule({
  imports: [CommonModule, FormsModule, IonicModule],
  declarations: [...components],
  exports: [...components],
})
export class ComponentsModule {}
