﻿import ApiClient from '../../modules/api';
import ko from 'knockout';
import '../../components/rich-text-editor';

import IdeaBasicInfoViewModel from '../../modules/ideas/ideaBasicInfoViewModel';
import BasePage from '../../basePage';
import navigate from '../../modules/navigationHelper';

class CreatePage extends BasePage {
    constructor(client) {
        super(...arguments);
        this._client = client;
        this._model = new IdeaBasicInfoViewModel({
            actions: {
                save: idea => {
                    return this._client.createIdea(idea)
                        .then(({ id }) => {                            
                            navigate.toIdeaImages(id);
                        });
                }
            }
        });
    }

    onReady() {
        ko.applyBindings(this._model, document.getElementById('create-idea-form'));
        return Promise.resolve(true);
    }
}


const client = new ApiClient();

const page = new CreatePage(client);
page.init();