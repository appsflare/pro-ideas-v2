import Barba from 'barba.js';
import FadeTransition from './transitions/fade';


export default class BasePage {

    constructor() {

        this._url = Barba.Pjax.getCurrentUrl();
    }

    getTransition() {
        return FadeTransition;
    }

    configure() {
        Barba.Pjax.getTransition = () => this.getTransition();
    }

    init() {
        this.configure();
        Barba.Pjax.start();
        this.onReady();

        Barba.Dispatcher.on('newPageReady', (currentStatus, oldStatus, container) => {            
            if (currentStatus.url == this._url)
            { this.onReady(); }
        });
    }

    onReady() {

    }
}


