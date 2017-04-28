import ko from 'knockout';
import template from './rich-text-editor.html';
import Quill from 'quill';


const editorConfigs = {

    all: {

        modules: {
            toolbar: [
                [{ 'header': [1, 2, false] }, { 'font': [] }],
                ['bold', 'italic', 'underline', 'strike', 'blockquote'],
                [{ 'list': 'ordered' }, { 'list': 'bullet' },
                { 'indent': '-1' }, { 'indent': '+1' }],
                ['link', 'image', 'video'],
                ['clean']
            ]
        },

        formats: ['header', 'font', 'size',
            'bold', 'italic', 'underline', 'strike', 'blockquote',
            'list', 'bullet', 'indent',
            'link', 'image', 'video']

    },

    mediaOnly: {


        modules: {
            toolbar: [
                ['image', 'video']
            ]
        },

        formats: ['image', 'video']

    },


    videoOnly: {


        modules: {
            toolbar: [
                ['video']
            ]
        },

        formats: ['video']

    },

    imageOnly: {


        modules: {
            toolbar: [
                ['image']
            ]
        },

        formats: ['image']

    },

    shortText: {

        modules: {
            toolbar: [
                ['bold', 'italic', 'underline', 'strike', 'blockquote'],
                ['link'], ['clean']
            ]
        },

        formats: ['header', 'font', 'size',
            'bold', 'italic', 'underline', 'strike', 'blockquote',
            'link']

    }



};

class RichTextEditorViewModel {
    constructor({value}) {
        this.value = value;
    }
}

ko.components.register('rich-text-editor', {
    viewModel: {
        createViewModel({value, placeholder = '', mode = "shortText"}, { element }) {

            const viewModel = new RichTextEditorViewModel({ value });

            const editorElement = element.querySelector('.rich-text-editor');
            editorElement.innerHTML = value();

            const {modules: {toolbar}, formats} = editorConfigs[mode];

            const editor = new Quill(editorElement, {
                theme: 'snow',
                placeholder,
                modules: { toolbar },
                formats
            });

            viewModel.value.subscribe(newValue => {
                if (value() == editor.container.firstChild.innerHTML)
                { return; }
                editor.container.firstChild.innerHTML = value();
            });

            editor.on('text-change', () => {
                viewModel.value(editor.container.firstChild.innerHTML);
            });

            return viewModel;
        }
    },
    template
});