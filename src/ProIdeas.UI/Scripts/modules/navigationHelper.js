const navigationHelper = {

    toIdeaImages(id) {
        document.location.href = `/ideas/${id}/images`;
    },

    toEditIdea(id) {
        document.location.href = `/ideas/${id}/edit`;
    },

    toIdeaPages(id) {
        document.location.href = `/ideas/${id}/pages`;
    },

    toIdeaDetails(id) {
        document.location.href = `/ideas/${id}/details`;
    }

};

export default navigationHelper;