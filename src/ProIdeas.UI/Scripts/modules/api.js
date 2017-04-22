const defaultHeaders = {
    'content-type': 'application/json;charset=utf8'
};
const utils = {
    get(url) {
        return new Promise((resolve, reject) => {
            $.get({
                url,
                success: resolve,
                error: reject
            });
        });
    },
    put(url, data) {
        return new Promise((resolve, reject) => {
            $.ajax({
                url,
                type: 'PUT',
                data: JSON.stringify(data),
                success: resolve,
                error: reject
            });
        });
    },

    post(url, data) {
        return new Promise((resolve, reject) => {
            $.post({
                url,                
                data: JSON.stringify(data),
                success: resolve,
                error: reject
            });
        });
    }
};

export default class ApiClient {

    constructor() {
        $.ajaxSetup({
            headers: defaultHeaders,
            dataType: 'json'
        });
    }

    getIdea(id) {
        return utils.get(`/api/ideas/${id}`);
    }

    createIdea(idea) {

        return utils.post('/api/ideas', idea);
    }

    updateIdea(idea) {

        return utils.put(`/api/ideas/${idea.id}`, idea);
    }

    getIdeas() {
        return Promise.resolve([]);
    }
}