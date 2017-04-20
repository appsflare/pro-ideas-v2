import ApiClient from '../../modules/api';
import ko from 'knockout';

import IdeaBasicInfoViewModel from '../../modules/ideas/ideaBasicInfoViewModel';

$(function () {

    const client = new ApiClient();


    var viewModel = new IdeaBasicInfoViewModel({
        actions: {
            save(idea) {
                return client.createIdea(idea).then(data => {
                    document.location.href = `/ideas/images/${data.id}`;
                });
            }
        }
    });

    ko.applyBindings(viewModel, document.getElementById('create-idea-form'));

});