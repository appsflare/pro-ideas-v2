//import Barba from 'barba.js';
//import FadeTransition from './transitions/fade';
import Turbolinks from 'turbolinks';
$(function () { Turbolinks.start(); });

//Turbolinks would show progress bar automatically when page takes longer than 500ms to load
//https://github.com/turbolinks/turbolinks/issues/17#issuecomment-186635946
//Turbolinks.controller.adapter.hideProgressBar()
//Turbolinks.controller.adapter.showProgressBar()
$.material.init();



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


