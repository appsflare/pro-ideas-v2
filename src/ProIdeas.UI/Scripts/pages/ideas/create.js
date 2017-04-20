import ApiClient from '../../modules/api';
import ko from 'knockout';
import '../../components/rich-text-editor';

import IdeaBasicInfoViewModel from '../../modules/ideas/ideaBasicInfoViewModel';

$(function () {

    const client = new ApiClient();


    var viewModel = new IdeaBasicInfoViewModel({
        actions: {
            save(idea) {
                debugger;
                return client.createIdea(idea);
            }
        }
    });

    ko.applyBindings(viewModel, document.getElementById('create-idea-form'));

});