import ko from 'knockout';
import template from './comments-list.html';
import moment from 'moment';

function formatComment(comment) {
    comment.commentedAgo = moment(comment.modifiedOn || comment.createdOn).fromNow();
    return comment;
}

class CommentsListViewModel {
    constructor({ideaId, client}) {
        this.ideaId = ideaId;
        this._client = client;
        this.isLoading = ko.observable(true);
        this.canLoadMore = ko.observable(true);
        this.currentPage = 1;

        this.commentText = ko.observable('');
        this.isCommentSaving = ko.observable(false);

        this.items = ko.observableArray([]);
    }

    createComment() {
        this.isCommentSaving(true);
        this._client.postIdeaComment({ ideaId: this.ideaId, content: this.commentText() })
            .then(comment => {
                this.items.unshift(formatComment(comment));
                this.commentText('');
                this.isCommentSaving(false);
            })
            .catch(e => {
                console.error(e);
                this.isCommentSaving(false);
            });
    }

    loadMore() {
        return this.canLoadMore() ? this._fetchComments(++this.currentPage) : Promise.resolve([]);
    }

    _fetchComments(page = 1) {
        this.isLoading(true);

        return this._client.getIdeaComments({ ideaId: this.ideaId, page })
            .then(comments => {
                this.canLoadMore(comments.length > 0);
                comments.map(formatComment)
                    .forEach(comment => this.items.push(comment));
                this.isLoading(false);
                return Promise.resolve(comments);
            }).catch(e => {
                console.error(e);
                this.isLoading(false);
            });
    }

    refresh() {
        return this._fetchComments(this.currentPage);
    }
}

ko.components.register('comments-list', {
    viewModel: {
        createViewModel(params, { element }) {
            const vm = new CommentsListViewModel(params);

            vm.refresh();

            return vm;
        }
    },
    template
});