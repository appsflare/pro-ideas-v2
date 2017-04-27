import ko from 'knockout';
import 'knockout.validation';
import { asyncComputed } from '../utils';

class PageViewModel {
    constructor({name = '', content = '', canDelete = true}) {
        this.name = ko.observable(name);
        this.content = ko.observable(content);
        this.canDelete = ko.observable(canDelete);
        this.isEditing = ko.observable(false);
    }

    isValid() {
        return true;
    }
}

export default class PagesViewModel {

    constructor({ pages = [], actions: {savePages, finish} }) {

        ko.validation.init({
            errorElementClass: 'has-error',
            errorMessageClass: 'help-block',
            decorateInputElement: true
        });

        this.actions = { savePages, finish };
        this.isSaving = ko.observable(false);
        this._init(pages);

        this.details = ko.observable('');

        this.canAddPage = ko.computed(() => this.pages().length < 4);

    }

    _init(pages) {
        this.currentPage = ko.observable(false);
        this.pages = ko.observableArray(pages.map(p => new PageViewModel(p)));
        if (!this.pages().length) {
            this.pages.push(new PageViewModel({ name: 'Details', content: '', canDelete: false }));
        }
        this.currentPage(this.pages()[0]);


    }

    _validate() {
        return true;
    }

    isCurrentPage(page) {
        return this.currentPage() == page;
    }

    addPage() {
        if (!this.canAddPage())
        { return; }
        const newPage = new PageViewModel({ name: `New Page (${this.pages().length + 1})`, content: '' });
        this.pages.push(newPage);
        this.editPage(newPage);
    }

    removePage(page) {
        if (!page)
        { return; }

        this.pages.remove(page);
    }

    editPage(page) {
        this.currentPage(page);
    }

    deletePage(page) {
        this.pages.remove(p => p.name == page.name);
    }

    getPages() {
        return this.pages()
            .map(i => {
                const { name, content } = ko.toJS(i);
                return { name, content };
            });
    }

    savePages() {
        const { savePages} = this.actions;
        this.isSaving(true);
        return savePages(this.getPages())
            .then(res => {
                this.isSaving(false);
            }).catch(e => {
                this.isSaving(false);
            });
    }

    finish() {
        const { finish } = this.actions;
        return this.savePages().then(() => finish());
    }


}