const utils = {
    post(url, data) {
        return new Promise((resolve, reject) => {

            $.post({
                url: '/api/ideas',
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
            headers: {
                'content-type': 'application/json;charset=utf8'
            },
            dataType: 'json'
        });
    }

    

    createIdea(idea) {

        return utils.post('/api/ideas', idea);
    }

    getIdeas() {
        return Promise.resolve([]);
    }
}