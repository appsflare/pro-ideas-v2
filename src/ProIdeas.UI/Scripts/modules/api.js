
function post(url, data) {
    return new Promise((resolve, reject) => {

        $.post({
            url: '/api/ideas',
            data: JSON.stringify(idea),
            success: resolve,
            error: reject
        });
    });
}
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

        return post('/api/ideas', idea);
    }

    getIdeas() {
        return Promise.resolve([]);
    }
}