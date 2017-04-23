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

            const viewModel = new RichTextEditorViewModel(params);

            const editorElement = element.querySelector('.rich-text-editor');
            editorElement.innerHTML = params.value();

            const editor = new Quill(editorElement, {
                theme: 'snow'
            });            

            viewModel.value.subscribe(newValue => {
                if (params.value() == editor.container.firstChild.innerHTML)
                { return; }
                editor.container.firstChild.innerHTML = params.value();
            });

            editor.on('text-change', () => {                
                viewModel.value(editor.container.firstChild.innerHTML);
            });

            return viewModel;
        }
    },
    template
});