import { EnvVariables } from '../../app/environment-variables/environment-variables.token';
import { Component, Inject } from '@angular/core';
import { IonicPage, MenuController } from 'ionic-angular';

import { Subject } from "rxjs/Subject";
/**
 * Generated class for the ArtigosPreviewPage page.
 *
 * See http://ionicframework.com/docs/components/#navigation for more info
 * on Ionic pages and navigation.
 */
@IonicPage({
  name: 'artigos-preview'
})
@Component({
  selector: 'page-artigos-preview',
  templateUrl: 'artigos-preview.html',
})
export class ArtigosPreviewPage {
  artigo$ = new Subject<any>();

  constructor(public menuCtrl: MenuController, @Inject(EnvVariables) public envVariables: any) {
    console.log('Preview CMS url: ', envVariables.cmsServer);
  }

  ionViewDidLoad() {
    this.menuCtrl.swipeEnable(false, 'left');
    this.menuCtrl.enable(false, 'left');

    window.addEventListener('message', (event) => {
        // IMPORTANT: Check the origin of the data!
        if (event.origin.indexOf(this.envVariables.cmsServer) == 0) {
            // Read and elaborate the received data
            this.artigo$.next(event.data);
        }
    });
    window.parent.postMessage("ionViewDidLoad", this.envVariables.cmsServer);
  }

}
