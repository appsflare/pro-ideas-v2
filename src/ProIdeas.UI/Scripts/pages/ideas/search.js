import ApiClient from '../../modules/api';
import ko from 'knockout';
import '../../components/idea-card';

import SearchIdeasViewModel from '../../modules/ideas/searchIdeasViewModel';
import BasePage from '../../basePage';
import navigate from '../../modules/navigationHelper';

class SearchIdeasPage extends BasePage {
    constructor(client) {
        super(...arguments);
        this._client = client;
    }

    onReady() {

        this._viewModel = new SearchIdeasViewModel({
            keyword: '',
            actions: {

                like: (id, isLike) => {
                    return this._client.like(id, isLike);
                },

                viewComments: (id) => {
                    navigate.toIdeaDetails(id);
                    return Promise.resolve(true);
                },

                search: ({keyword, page, pageSize}) => {

                    return this._client.getIdeas({ keyword, page, pageSize });

                }
            }
        });

        ko.applyBindings(this._viewModel, document.getElementById('ideas-search-container'));

        this._viewModel.search();

        return Promise.resolve(true);
    }
}


const client = new ApiClient();
const page = new SearchIdeasPage(client);
page.init();