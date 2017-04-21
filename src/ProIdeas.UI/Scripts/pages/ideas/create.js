import ApiClient from '../../modules/api';
import ko from 'knockout';

import IdeaBasicInfoViewModel from '../../modules/ideas/ideaBasicInfoViewModel';
import BasePage from '../../basePage';

class CreatePage extends BasePage {
    constructor(client) {
        super(...arguments);
        this._client = client;
        this._model = new IdeaBasicInfoViewModel({
            actions: {
                save(idea) {
                    return this._client.createIdea(idea)
                        .then(data => {
                            document.location.href = `/ideas/images/${data.id}`;
                        });
                }
            }
        });
    }

    onReady() {
        ko.applyBindings(this._model, document.getElementById('create-idea-form'));
    }
}


const client = new ApiClient();

const page = new CreatePage(client);
page.init();