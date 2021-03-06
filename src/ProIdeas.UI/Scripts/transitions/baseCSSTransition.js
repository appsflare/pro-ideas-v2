﻿export default class BaseCSSTransition{
    static get defaultOptions() {
        return {
            name: 'transition',
            transitionDuration: 750,
            staggerDelay: 25,
            updateTransitionDuration: 250,
            removeTransitionDuration: 500,
            transitionClass: 'animated',
            prepareClass: 'start',
            loadClass: "zoomInRight",
            removeClass: "zoomOutRight",
            updateClass: "pulse"
        };
    }

    constructor(options) {                

        this.options = Object.assign(BaseCSSTransition.defaultOptions, options);
        
        this._queue = [];
    }

    _enQueue() {
        this._queue.push(this._queue.length);
    }

    _deQueue() {
        return this._queue.shift();
    }

    onItemLoading({element}) {
        let $elt = $(element);
        const {transitionClass, prepareClass} = this.options;
        $elt.addClass(transitionClass);
        $elt.addClass(prepareClass);
        this._enQueue();
    }

    onItemLoaded({element}) {
        let $elt = $(element);
        const {loadClass, staggerDelay, transitionClass, prepareClass, transitionDuration} = this.options;
        setTimeout(() => {
            $elt.removeClass(prepareClass);
            $elt.addClass(loadClass);
            setTimeout(() => {
                $elt.removeClass(loadClass);
                $elt.removeClass(transitionClass);
            }, transitionDuration);
        }, staggerDelay * this._deQueue());
    }

    onItemUpdated({element}) {
        let $elt = $(element);
        const {transitionClass, updateClass, updateTransitionDuration} = this.options;
        $elt.addClass(transitionClass);
        $elt.addClass(updateClass);
        setTimeout(() => {
            $elt.removeClass(transitionClass);
            $elt.removeClass(updateClass);
        }, updateTransitionDuration);

    }

    onItemRemoved({element}) {
        let $elt = $(element);
        const {transitionClass, removeClass, removeTransitionDuration} = this.options;
        $elt.addClass(transitionClass);
        $elt.addClass(removeClass);
        setTimeout(() => {
            $elt.remove();
        }, removeTransitionDuration);

    }
}