import Barba from 'barba.js';
import FadeTransition from '../../transitions/fade';


Barba.Pjax.getTransition = function () {
    return FadeTransition;
};

Barba.Pjax.start();