import { Analytics } from './../providers/analytics';
import { PerfilData } from './../providers/perfil-data';
import { AceitesTermosUsoData } from './../providers/aceites-termos-uso-data';
import { EnvironmentsModule } from './environment-variables/environment-variables.module';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { NgModule, ErrorHandler } from '@angular/core';

import { IonicApp, IonicModule, IonicErrorHandler } from 'ionic-angular';

import { InAppBrowser } from '@ionic-native/in-app-browser';
import { SplashScreen } from '@ionic-native/splash-screen';
import { GoogleAnalytics } from "@ionic-native/google-analytics";

import { IonicStorageModule } from '@ionic/storage';

import { AppComponent } from './app.component';
import { HeilbaumPhotoswipeModule } from "heilbaum-ionic-photoswipe/dist";

import { UserData } from '../providers/user-data';
import { ArtigosData } from '../providers/artigos-data';
import { TagsData } from '../providers/tags-data';
import { HttpClientService } from "../providers/http-client";

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    IonicModule.forRoot(AppComponent),
    IonicStorageModule.forRoot(),
    HeilbaumPhotoswipeModule,
    EnvironmentsModule
  ],
  bootstrap: [IonicApp],
  entryComponents: [
    AppComponent,
  ],
  providers: [
    { provide: ErrorHandler, useClass: IonicErrorHandler },
    UserData,
    ArtigosData,
    HttpClientService,
    TagsData,
    InAppBrowser,
    SplashScreen,
    AceitesTermosUsoData,
    PerfilData,
    GoogleAnalytics,
    Analytics
  ]
})
export class AppModule { }
