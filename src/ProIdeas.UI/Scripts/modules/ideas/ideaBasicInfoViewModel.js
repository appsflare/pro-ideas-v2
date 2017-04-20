import ko from 'knockout';

export default class IdeaBasicInfoViewModel {

    constructor({ idea = {}, actions: {save} }) {
        this.actions = { save };
        this.isSaving = ko.observable(false);
        this._initForm(idea);
    }

    _initForm({title = '', description = '', isFundingRequired = false, fundingRequirement = ''}) {
        this.form = {
            title: ko.observable(title),
            description: ko.observable(description),
            isFundingRequired: ko.observable(isFundingRequired),
            fundingRequirement: ko.observable(fundingRequirement)
        };
    }

    _validate() {
        const formData = ko.toJS(this.form);

        const part1valid = formData.title && formData.description;

        return part1valid && (formData.isFundingRequired ? !!formData.fundingRequirement : true);



    }

    save() {
        this.isSaving(true);
        const { save } = this.actions;
        return save(ko.toJS(this.form))
            .then(res => {
                this.isSaving(false);
                return res;
            }).catch(e => {
                this.isSaving(false);
            });
    }


}