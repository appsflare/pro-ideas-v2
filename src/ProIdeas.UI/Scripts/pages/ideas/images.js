import ApiClient from '../../modules/api';
import ko from 'knockout';
import '../../components/file-uploader';

import IdeaImagesViewModel from '../../modules/ideas/ideaImagesViewModel';

import BasePage from '../../basePage';
import navigate from '../../modules/navigationHelper';

class ImagesPage extends BasePage {
    constructor(client) {
        super(...arguments);
        this._client = client;
    }

    onReady() {

        var viewModel = new IdeaImagesViewModel({
            ideaId: $('#IdeaId').val(),
            actions: {
                next: (id, file) => {
                    if (file) {
                        return this._client.uploadIdeaBanner(id, file)
                            .then(data => {
                                navigate.toIdeaPages(id);
                            })
                            .catch(e => {
                                console.error(e);
                            });
                    }

                    navigate.toTeamDetails(id);
                    return Promise.resolve(true);
                }
            }
        });

        ko.applyBindings(viewModel, document.getElementById('idea-image-upload-form'));

        return Promise.resolve(true);
    }
}


const client = new ApiClient();
const page = new ImagesPage(client);
page.init();