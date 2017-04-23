import ko from 'knockout';

export class SearchResultItemViewModel {
    constructor(item) {

        Object(item).keys().forEach(key => {
            this[key] = ko.observable(item[key]);
        });

    }
}

export default class SearchPageViewModel {

    constructor({keyword, actions: { search }}) {
        this.keyword = ko.observable(keyword);
        this.results = ko.observableArray();

        this.loading = ko.observable(false);

        this.actions = { search };        
    }

    _populateResults(results) {
        results.forEach(item => {
            this.results.push(new SearchResultItemViewModel(item));
        });
    }

    search() {

        const {search} = this.actions;
        this.loading(true);
        return search({ keyword: this.keyword(), page: 1, pageSize: 10 })
            .then(results => {
                this.loading(false);
                this._populateResults(results);
            })
            .catch(() => {
                this.loading(false);
            });
    }

}