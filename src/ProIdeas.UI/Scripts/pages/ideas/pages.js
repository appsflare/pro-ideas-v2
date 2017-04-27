import ApiClient from '../../modules/api';
import ko from 'knockout';
import '../../components/rich-text-editor';

import PagesViewModel from '../../modules/ideas/pagesViewModel';
import BasePage from '../../basePage';
import navigate from '../../modules/navigationHelper';

class IdeaPagesPage extends BasePage {
    constructor(client) {
        super(...arguments);
        this._client = client;
    }

    onReady() {
        console.log('edit page ready');
        return this._client.getIdea($('#IdeaId').val())
            .then(({id, pages}) => {


                var viewModel = new PagesViewModel({
                    pages,
                    actions: {
                        savePages: pagestoSave => {
                            //TODO: call api
                            return this._client.saveIdeaPages(id, pagestoSave);
                        },
                        finish: () => {                            
                            navigate.toIdeaDetails(id);
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