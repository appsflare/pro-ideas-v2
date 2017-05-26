import ko from 'knockout';
import 'knockout.validation';

export default class IdeaBasicInfoViewModel {

    constructor({ idea = {}, actions: { save } }) {

        ko.validation.init({
            errorElementClass: 'has-error-field',
            errorMessageClass: 'help-block',
            decorateInputElement: true
        });

        this.actions = { save };
        this.isSaving = ko.observable(false);
        this._initForm(idea);

    }

    _initForm({ id, title = '', description = '', isFundingRequired = false, fundingRequirement = '' }) {
        this.form = ko.validatedObservable({
            id,
            title: ko.observable(title).extend({ required: true, maxLength: 100 }),
            description: ko.observable(description).extend({ required: true, maxLength: 500 }),
            isFundingRequired: ko.observable(isFundingRequired).extend({ required: true }),
            fundingRequirement: ko.observable(fundingRequirement).extend({ required: false })
        });
    }

    _validate() {

        if (!this.form.isValid()) {
            this.form.errors.showAllMessages();
            return false;
        }
        return true;
    }

    save() {
        if (!this._validate())
        { return Promise.reject('Validation failed'); }

        const form = ko.toJS(this.form);

        form.isFundingRequired = !!(form.fundingRequirement && form.fundingRequirement.length > 0);

        this.isSaving(true);
        const { save } = this.actions;
        return save(form)
            .then(res => {
                this.isSaving(false);
                return Promise.resolve(res);
            }).catch(e => {
                this.isSaving(false);
            });
    }


}