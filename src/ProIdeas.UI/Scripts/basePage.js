//import Barba from 'barba.js';
//import FadeTransition from './transitions/fade';
import Turbolinks from 'turbolinks';
$(function () {
    if (window.__turbo__) {
        return;
    }
    window.__turbo__ = true;

    Turbolinks.start();

    const $window = $(window),
        $body = $(document.body);

    $window
        .on('turbolinks:visit', (e) => {
            //$('.custom-scrollable').mCustomScrollbar('destroy');
            $('body').removeClass('animated fadeIn').addClass('animated fadeOut');
        })
        .on('turbolinks:before-cache', (e) => {
            $('.custom-scrollable').mCustomScrollbar('destroy');
            //$('body').removeClass('animated fadeIn').addClass('animated fadeOut');
        })
        .on('turbolinks:before-render', e => {
            $(event.data.newBody).removeClass('animated fadeOut').addClass('animated fadeIn');
        });




});

//Turbolinks would show progress bar automatically when page takes longer than 500ms to load
//https://github.com/turbolinks/turbolinks/issues/17#issuecomment-186635946
//Turbolinks.controller.adapter.hideProgressBar()
//Turbolinks.controller.adapter.showProgressBar()
$.material.init();



export default class BasePage {

    constructor() {

        //this._url = Barba.Pjax.getCurrentUrl();
    }

    //getTransition() {
    //    return FadeTransition;
    //}

    configure() {
        //Barba.Pjax.getTransition = () => this.getTransition();
    }

    init() {
        this.configure();

        $('.custom-scrollable').mCustomScrollbar({ scrollInertia: 0 });

        this.onReady()
            .then(() => {
                $.material.init();
            });

        //Barba.Dispatcher.on('newPageReady', (currentStatus, oldStatus, container) => {
        //    debugger;
        //    if (currentStatus.url == this._url)
        //    { this.onReady(); }
        //});
    }

    onReady() {

    }
}


