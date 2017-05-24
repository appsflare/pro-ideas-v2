import ko from 'knockout';
import template from './idea-card.html';


class IdeaCardViewModel {
    constructor({idea, like, viewComments}) {
        Object.keys(idea).forEach(key => {
            this[key] = ko.observable(idea[key]);
        });

        this.actions = {
            like: () => like(this.id(), true).then(stats => this.likes(stats.likes)),
            dislike: () => like(this.id(), false).then(stats => this.likes(stats.disLikes)),
            viewComments: () => viewComments(this.id())
        };

        this.banner = `/api/ideas/${idea.id}/banner.png`;
        this.detailsUrl = `/ideas/${idea.id}/details`;
        this.ownerProfileUrl= `/profile/${idea.ownerId}`;
    }

    updateStats({likes, disLikes}) {
        this.likes(likes);
        this.disLikes(likes);
    }
}

ko.components.register('idea-card', {
    viewModel: {
        createViewModel(params, { element }) {
            return new IdeaCardViewModel(params);
        }
    },
    template: template.substr(1) //To remove the white-space
});