import ApiClient from '../../modules/api';
import ko from 'knockout';

import IdeaBasicInfoViewModel from '../../modules/ideas/ideaBasicInfoViewModel';

$(function () {

    function getParameterByName(name, url) {
        if (!url) url = window.location.href;
        name = name.replace(/[\[\]]/g, "\\$&");
        var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
            results = regex.exec(url);
        if (!results) return null;
        if (!results[2]) return '';
        return decodeURIComponent(results[2].replace(/\+/g, " "));
    }

    const client = new ApiClient();

    client.getIdea(getParameterByName('id')).then(ideaDetails => {



        var viewModel = new IdeaBasicInfoViewModel({
            idea: ideaDetails,
            actions: {
                save(idea) {
                    return client.updateIdea(idea).then(data => {
                        document.location.href = `/ideas/images/${data.id}`;
                    });
                }
            }
        });

        ko.applyBindings(viewModel, document.getElementById('create-idea-form'));

    });

});