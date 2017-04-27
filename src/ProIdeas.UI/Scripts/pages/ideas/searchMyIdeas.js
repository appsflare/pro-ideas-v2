import ApiClient from '../../modules/api';
import ko from 'knockout';

import SearchIdeasViewModel from '../../modules/ideas/searchIdeasViewModel';
import BasePage from '../../basePage';

class SearchMyIdeasPage extends BasePage {
    constructor(client) {
        super(...arguments);
        this._client = client;
    }

    onReady() {      
        
        this._viewModel = new SearchIdeasViewModel({
            keyword: '',
            actions: {

                search: ({keyword, page, pageSize}) => {

                    return this._client.getMyIdeas({ keyword, page, pageSize });

                }
            }
        });

        ko.applyBindings(this._viewModel, document.getElementById('ideas-search-container'));

        this._viewModel.search();

        return Promise.resolve(true);
    }
}


const client = new ApiClient();
const page = new SearchMyIdeasPage(client);
page.init();