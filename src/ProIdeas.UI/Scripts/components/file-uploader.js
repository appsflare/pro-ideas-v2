import ko from 'knockout';
import template from './file-uploader.html';


class FileUploaderViewModel {
    constructor({value, icon = "file"}) {
        this.value = value;
        this.icon = icon;
    }

    onFileSelect(e) {
        this.value(e.target.files[0]);
    }
}

ko.components.register('file-uploader', {
    viewModel: {
        createViewModel(params, { element }) {

            var viewModel = new FileUploaderViewModel(params);

            $(element).on('change', 'input:file', (e) => {                
                viewModel.onFileSelect(e);
            })

            return viewModel;
        }
    },
    template
});