//import Barba from 'barba.js';
//import FadeTransition from './transitions/fade';
import Turbolinks from 'turbolinks';
Turbolinks.start();
Turbolinks.controller.adapter.showProgressBar()

export default class BasePage {

    constructor() {

        //this._url = Barba.Pjax.getCurrentUrl();
    }

    getTransition() {
        return FadeTransition;
    }

    configure() {
        //Barba.Pjax.getTransition = () => this.getTransition();
    }

    init() {
        this.configure();
        
        this.onReady();

        //Barba.Dispatcher.on('newPageReady', (currentStatus, oldStatus, container) => {
        //    debugger;
        //    if (currentStatus.url == this._url)
        //    { this.onReady(); }
        //});
    }

    onReady() {

    }
}


