import ko from 'knockout';
import template from './rich-text-editor.html';
import Quill from 'quill';

class RichTextEditorViewModel {
    constructor({value}) {
        this.value = value;
    }
}

ko.components.register('rich-text-editor', {
    viewModel: {
        createViewModel(params, { element }) {

            var viewModel = new RichTextEditorViewModel(params);

            var editor = new Quill(element.querySelector('.rich-text-editor'), {
                theme: 'snow'
            });


            editor.setText(params.value());

            viewModel.value.subscribe(newValue => {
                if (params.value() == editor.getText())
                { return; }
                editor.setText(params.value());
            });

            editor.on('editor-change', () => {
                viewModel.value(editor.getText());
            });

            return viewModel;
        }
    },
    template
});