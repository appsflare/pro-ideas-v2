import ApiClient from '../../modules/api';
import ko from 'knockout';

import IdeaBasicInfoViewModel from '../../modules/ideas/ideaBasicInfoViewModel';
import BasePage from '../../basePage';

class EditPage extends BasePage {
    constructor(client) {
        super(...arguments);
        this._client = client;
    }

    onReady() {
        console.log('edit page ready');
        this._client.getIdea($('#IdeaId').val()).then(ideaDetails => {


            var viewModel = new IdeaBasicInfoViewModel({
                idea: ideaDetails,
                actions: {
                    save: (idea) => {
                        return this._client.updateIdea(idea)
                            .then(data => {
                                document.location.href = `/ideas/${idea.id}/images`;
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