import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import { FormsModule } from '@angular/forms';
import { AppHeaderComponent } from './app-header/app-header.component';
import { AppFooterComponent } from './app-footer/app-footer.component';

const components = [AppHeaderComponent, AppFooterComponent];

@NgModule({
  imports: [CommonModule, FormsModule, IonicModule],
  declarations: [...components],
  exports: [...components],
})
export class ComponentsModule {}
