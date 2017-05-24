import ApiClient from '../../modules/api';
import ko from 'knockout';
import '../../components/rich-text-editor';

import IdeaBasicInfoViewModel from '../../modules/ideas/ideaBasicInfoViewModel';
import BasePage from '../../basePage';
import navigate from '../../modules/navigationHelper';

class EditPage extends BasePage {
    constructor(client) {
        super(...arguments);
        this._client = client;
    }

    onReady() {
        console.log('edit page ready');
        return this._client.getIdea($('#IdeaId').val()).then(ideaDetails => {


            var viewModel = new IdeaBasicInfoViewModel({
                idea: ideaDetails,
                actions: {
                    save: (idea) => {
                        return this._client.updateIdea(idea)
                            .then(data => {
                                navigate.toIdeaImages(idea.id);
                            });
                    }
                }
            });

            ko.applyBindings(viewModel, document.getElementById('create-idea-form'));
        });

    }
}


const client = new ApiClient();
const page = new EditPage(client);
page.init();