import { IonicModule } from 'ionic-angular';
import { NgModule } from '@angular/core';

import { SafeResourceUrlPipe } from './safe-resource-url.pipe';
import { SafeUrlPipe } from './safe-url.pipe';
import { SafeHtmlPipe } from './safe-html.pipe';

@NgModule({
  declarations: [
    SafeHtmlPipe,
    SafeUrlPipe,
    SafeResourceUrlPipe
  ],
  imports:[
    IonicModule
  ],
  exports: [
    SafeHtmlPipe,
    SafeUrlPipe,
    SafeResourceUrlPipe
  ]
})
export class SafePipesModule { }
