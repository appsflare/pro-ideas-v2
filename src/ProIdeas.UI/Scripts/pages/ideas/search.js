import ApiClient from '../../modules/api';
import ko from 'knockout';

import SearchPageViewModel from '../../modules/ideas/searchPageViewModel';
import BasePage from '../../basePage';

class SearchIdeasPage extends BasePage {
    constructor(client) {
        super(...arguments);
        this._client = client;
    }

    onReady() {
        console.log('edit page ready');



        this._viewModel = new SearchPageViewModel({
            keyword: ideaDetails,
            actions: {

                search: ({keyword, page, pageSize}) => {

                    return this._client.searchIdeas({ keyword, page, pageSize });

                }
            }
        });

        ko.applyBindings(this._viewModel, document.getElementById('search-container'));

        this._viewModel.search();

    }
}


const client = new ApiClient();
const page = new SearchIdeasPage(client);
page.init();