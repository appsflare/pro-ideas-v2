import ko from 'knockout';
import template from './idea-like.html';

class IdeaLikeViewModel {
    constructor({like, ideaId, client}) {
        var self = this;
        self.LikeCount = ko.observable(like);
        self.actions = {
            like: () => {
                client.like(ideaId, true).then(data => {
                    self.LikeCount(data.likes);
                });
            }
        }
    }
}

ko.components.register('idea-like', {
    viewModel: {
        createViewModel(params, { element }) {
            return new IdeaLikeViewModel(params);
        }
    },
    template
});
