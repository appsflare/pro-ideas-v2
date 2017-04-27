
import BaseCssTransition from './baseCSSTransition';
export default class KnockoutForEachCssTransition extends BaseCssTransition {

    constructor(options) {
        super(options);
        this.onAfterRender = this.onAfterRender.bind(this);
        this.onAfterAdd = this.onAfterAdd.bind(this);
        this.onBeforeRemove = this.onBeforeRemove.bind(this);
    }

    onAfterRender(elements = []) {
        elements.forEach(element => this.onItemLoading({ element }));
        setTimeout(() => elements.forEach(element => this.onItemLoaded({ element })));
    }

    onAfterAdd(element, index) {
        this.onItemLoading({ element });
        this.onItemLoaded({ element });

    }

    onBeforeRemove(element, index) {
        $(element).remove();
    }




}