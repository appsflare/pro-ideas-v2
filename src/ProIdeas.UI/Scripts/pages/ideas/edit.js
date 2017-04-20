import ApiClient from '../../modules/api';
import ko from 'knockout';

import IdeaBasicInfoViewModel from '../../modules/ideas/ideaBasicInfoViewModel';

$(function () {

    const client = new ApiClient();

    client.getIdea($('#IdeaId').val()).then(ideaDetails => {



        var viewModel = new IdeaBasicInfoViewModel({
            idea: ideaDetails,
            actions: {
                save(idea) {
                    return client.updateIdea(idea)
                        .then(data => {
                            document.location.href = `/ideas/images/${data.id}`;
                        });
                }
            }
        });

        ko.applyBindings(viewModel, document.getElementById('create-idea-form'));

    });

});