import ko from 'knockout';
import 'knockout.validation';

class PageViewModel {
    constructor({name = '', content = ''}) {
        this.name = ko.observable(name);
        this.content = ko.observable(content);

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

        this.canAddPage = ko.computed(() => this.pages().length < 4);

    }

    _init(pages) {
        this.currentPage = ko.observable(false);
        this.pages = ko.observableArray(pages.map(p => new PageViewModel(p)));
        if (this.pages().length) {
            this.currentPage(this.pages()[0]);
        }

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