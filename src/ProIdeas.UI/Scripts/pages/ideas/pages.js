import ApiClient from '../../modules/api';
import ko from 'knockout';
import '../../components/rich-text-editor';

import PagesViewModel from '../../modules/ideas/pagesViewModel';
import BasePage from '../../basePage';

class IdeaPagesPage extends BasePage {
    constructor(client) {
        super(...arguments);
        this._client = client;
    }

    onReady() {
        console.log('edit page ready');
        this._client.getIdea($('#IdeaId').val()).then(ideaDetails => {


            var viewModel = new PagesViewModel({
                idea: ideaDetails,
                actions: {
                    savePages: pages => {
                        //TODO: call api
                        return Promise.resolve(pages);
                    },
                    finish: () => {
                        document.location.href = `/ideas/${ideaDetails.id}/details`;
                    }
                }
            });

            ko.applyBindings(viewModel, document.getElementById('idea-pages'));
        });

    }
}


const client = new ApiClient();
const page = new IdeaPagesPage(client);
page.init();