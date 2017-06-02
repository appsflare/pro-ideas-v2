import ko from 'knockout';
import 'knockout.validation';
import { asyncComputed } from '../utils';

function readURL(file) {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();
        let cancelTimer;
        reader.onload = (e) => {
            resolve(e.target.result);
            clearTimeout(cancelTimer);
        };

        if (file.name) {
            cancelTimer = setTimeout(() => reject('file reading timed out'), 1500);
            reader.readAsDataURL(file);
            return;
        }
        resolve(false);
    });
}



export default class IdeaImagesViewModel {

    constructor({ ideaId, actions: {next} }) {

        ko.validation.init({
            errorElementClass: 'has-error-field',
            errorMessageClass: 'help-block',
            decorateInputElement: true
        });

        this._ideaId = ideaId;

        this.actions = { next };
        this.isSaving = ko.observable(false);
        this._initForm();

    }

    _initForm() {
        this.form = {
            file: {
                icon: 'photo',
                value: ko.observable('')

            }
        };
        const fileUrl = `/api/ideas/${this._ideaId}/banner.png`;
        this.form.file.selectedFile = asyncComputed(function () {
            const file = this.value();
            if (!file) {
                return new Promise((resolve, reject) => {
                    resolve(`${fileUrl}?${new Date().getTime()}`);
                });
            }
            return readURL(file);
        }, this.form.file);
    }

    _validate() {
        return !!ko.unwrap(this.form.file.value);
    }

    save() {

        this.isSaving(true);
        const { next } = this.actions;

        return next(this._ideaId, this.form.file.value())
            .then(res => {
                this.isSaving(false);
                return Promise.resolve(res);
            }).catch(e => {
                this.isSaving(false);
            });
    }


}