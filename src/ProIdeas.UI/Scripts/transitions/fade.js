﻿import Barba from 'barba.js';

const FadeTransition = Barba.BaseTransition.extend({
    start: function () {
        Promise
            .all([this.newContainerLoading, this.fadeOut()])
            .then(this.fadeIn.bind(this));
    },

    fadeOut: function () {
        return $(this.oldContainer).animate({ opacity: 0 }).promise();
    },

    fadeIn: function () {
        var _this = this;
        var $el = $(this.newContainer);

        $(this.oldContainer).hide();
        document.body.scrollTop = 0;

        $el.css({
            visibility: 'visible',
            opacity: 0
        });

        $el.animate({ opacity: 1 }, 400, function () {
            _this.done();
        });
    }
});
export default FadeTransition;