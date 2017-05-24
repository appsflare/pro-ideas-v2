import ApiClient from '../../modules/api';
import ko from 'knockout';

import '../../components/comments-list';
import '../../components/idea-like';

import BasePage from '../../basePage';


class DetailsPage extends BasePage {
    constructor(client) {
        super(...arguments);
        this._client = client;
    }

    onReady() {
        console.log('edit page ready');
        ko.applyBindings({ client: this._client }, document.querySelector('.idea-details-page-wrap'));
        return Promise.resolve(true);
    }
}


const client = new ApiClient();
const page = new DetailsPage(client);
page.init();