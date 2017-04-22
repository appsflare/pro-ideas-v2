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

    constructor({ idea = {}, actions: {save} }) {

        ko.validation.init({
            errorElementClass: 'has-error',
            errorMessageClass: 'help-block',
            decorateInputElement: true
        });

        this.actions = { save };
        this.isSaving = ko.observable(false);
        this._initForm(idea);

        this.canAddPage = ko.computed(() => this.pages().length < 4);

    }

    _initForm({pages = []}) {
        this.currentPage = ko.observable(false);
        this.pages = ko.observableArray(pages.map(p => new PageViewModel(p)));

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

    savePages() {
        const { savePages} = this.actions;
        this.isSaving(true);
        savePages(this.pages())
            .then(res => {
                this.isSaving(false);
            }).catch(e => {
                this.isSaving(false);
            });
    }

    finish() {
        const { finish } = this.actions;
        return finish(form);
    }


}