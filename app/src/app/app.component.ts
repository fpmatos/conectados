import { TagsData, Tag } from './../providers/tags-data';
import { Analytics } from './../providers/analytics';
import { EnvVariables } from './environment-variables/environment-variables.token';
import { PageInterface } from './app.component';
import { Component, ViewChild, Inject } from '@angular/core';
import 'rxjs/add/operator/toPromise';

import { Events, MenuController, Nav, Platform } from 'ionic-angular';
import { SplashScreen } from '@ionic-native/splash-screen';

import { Storage } from '@ionic/storage';

import { UserData } from '../providers/user-data';


export interface PageInterface {
  title: string;
  name: string;
  icon?: string;
  logsOut?: boolean;
  index?: number;
  tabName?: string;
  tabComponent?: any;
  params?: any;
}

@Component({
  templateUrl: 'app.template.html'
})
export class AppComponent {
  // the root nav is a child of the root app component
  // @ViewChild(Nav) gets a reference to the app's root nav
  @ViewChild(Nav) nav: Nav;

  rootPage: any;
  rootParams: any;

  constructor(
    public events: Events,
    public userData: UserData,
    public menu: MenuController,
    public platform: Platform,
    public storage: Storage,
    public splashScreen: SplashScreen,
    private analytics: Analytics,
    public tagsData:  TagsData,
    @Inject(EnvVariables) public envVariables: any
  ) {

    console.log('environment config:', envVariables);

    if(this.isPreview()){
      this.rootPage = 'artigos-preview';
    }
    else{
      this.rootPage = 'artigos-lista';
      this.rootParams = {tag: 'noticias-recentes'};
    }

    console.log('rootPage:', this.rootPage);
    console.log('rootParams:', this.rootParams);

    this.listenToEvents();
    this.platformReady();
  }

  listenToEvents(){
    this.events.subscribe('page:ionViewDidLoad', (page:string) => {
      console.log('Event page:ionViewDidLoad', page);
      this.platform.ready().then(() => {
        if (this.platform.is('cordova')) {
          this.splashScreen.hide();
        }
      });
    })

    this.events.subscribe('page:forbiden', (redirect:string) => {
      console.log('Event page:forbiden', redirect);
      setTimeout(() => {
        this.nav.setRoot(redirect);
      },0);
    })
  }

  isPreview():boolean {
    var isPreview =  ! this.platform.is('cordova')
      && window.location.href.indexOf('/#/artigos-preview') > 0;

    console.log(`isPreview: ${isPreview}`);

    return isPreview;
  }

  openTagPage(tag: Tag) {
    this.nav.setRoot('artigos-lista', tag).catch((err: any) => {
      console.log(`Didn't set nav root: ${err}`);
    });
  }

  openPage(page: PageInterface) {
    if(page && page.params)
      page.params.title = page.title;

    this.nav.setRoot(page.name, page.params).catch((err: any) => {
      console.log(`Didn't set nav root: ${err}`);
    });
  }

  sair(){
    this.userData.logout();
    this.nav.setRoot('login');
  }

  platformReady() {
    this.analytics.inicializar();
  }

  isActive(page: PageInterface) {
    if (this.nav.getActive() && this.nav.getActive().name === page.name) {
      return 'primary';
    }
    return;
  }
}
