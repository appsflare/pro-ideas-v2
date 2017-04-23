import Turbolinks from 'turbolinks';

const navigationHelper = {

    toIdeaImages(id) {
        Turbolinks.visit(`/ideas/${id}/images`);
    },

    toEditIdea(id) {
        Turbolinks.visit(`/ideas/${id}/edit`);
    },

    toIdeaPages(id) {
        Turbolinks.visit(`/ideas/${id}/pages`);
    },

    toIdeaDetails(id) {
        Turbolinks.visit(`/ideas/${id}/details`);
    }

};

export default navigationHelper;