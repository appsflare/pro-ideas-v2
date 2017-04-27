import ko from 'knockout';
//import '../knockout.animate';
import { asyncComputed } from '../utils';
import KnockoutForEachCssTransition from '../../transitions/KnockoutForEachCssTransition';


export default class SearchIdeasViewModel {

    constructor({keyword, rateLimit = 500, actions: { search, like, viewComments }}) {
        this.keyword = ko.observable(keyword).extend({ rateLimit, method: "notifyWhenChangesStop" });
        this._currentPage = 1;
        const transition = new KnockoutForEachCssTransition({});
        this.collection = {
            clear() {
                this.data.removeAll();
            },
            add(data) {
                this.data.push(data);
            },
            data: ko.observableArray(),
            afterRender: transition.onAfterAdd,
            afterAdd: transition.onAfterAdd,
            beforeRemove: transition.onBeforeRemove
        };



        this.transition = transition;

        this.loading = ko.observable(false);

        this.actions = { search, like, viewComments };

        this._setupAutoSearch();

        this.like = this.like.bind(this);
        this.viewComments = this.viewComments.bind(this);
    }

    like(id, isLike) {
        return this.actions.like(id, isLike);
    }

    viewComments() {
        return this.actions.viewComments(id);
    }

    _setupAutoSearch() {
        asyncComputed(function () {
            return this.search();
        }, this);
    }

    _clearResults() {
        this.collection.clear();
    }

    _populateResults(results) {
        results.forEach(item => {
            this.collection.add(item);
        });
    }

    loadMore() {
        return this._fetchResults({
            keyword: this.keyword(),
            page: ++this._currentPage
        });
    }

    _fetchResults({keyword, page = 1, pageSize = 10, clearExisting = false}) {

        const {search} = this.actions;
        this.loading(true);
        return search({ keyword: this.keyword(), page: 1, pageSize: 10 })
            .then(results => {
                this.loading(false);
                clearExisting && this._clearResults();
                this._populateResults(results);
            })
            .catch(() => {
                this.loading(false);
            });
    }


    search() {
        this._currentPage = 1;
        this._clearResults();
        return this._fetchResults({ keyword: this.keyword(), clearExisting: true });
    }

}