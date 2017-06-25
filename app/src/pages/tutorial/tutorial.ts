import { Component, ViewChild } from '@angular/core';
import { IonicPage, NavController, NavParams, MenuController, Slides, Events } from 'ionic-angular';

import { UserData } from "../../providers/user-data";
import { Page } from "../page/page";

/**
 * Generated class for the TutorialPage page.
 *
 * See http://ionicframework.com/docs/components/#navigation for more info
 * on Ionic pages and navigation.
 */
@IonicPage({
  name: 'tutorial'
})
@Component({
  selector: 'page-tutorial',
  templateUrl: 'tutorial.html',
})
export class TutorialPage extends Page {
  showSkip = true;

	@ViewChild('slides') slides: Slides;

  constructor(
    public events: Events,
    public navCtrl: NavController,
    public navParams: NavParams,
    public menu: MenuController,
    public userData: UserData) {
    super(events);
  }

  startApp() {
    this.userData.setHasSeenTutorial().then(() => {
      this.navCtrl.setRoot('artigos-lista', {tag: 'noticias-recentes'});
    });
  }

  onSlideChangeStart(slider: Slides) {
    this.showSkip = !slider.isEnd();
  }

	ionViewWillEnter() {
		this.slides.update();
	}

  ionViewDidEnter() {
    super.ionViewDidEnter();
    // the root left menu should be disabled on the tutorial page
    this.menu.enable(false);
  }

  ionViewDidLeave() {
    // enable the root left menu when leaving the tutorial page
    this.menu.enable(true);
  }

}
