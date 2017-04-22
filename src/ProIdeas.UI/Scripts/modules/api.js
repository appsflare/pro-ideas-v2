const defaultHeaders = {

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
    },

    uploadFile(url, files = []) {
        var formData = new FormData();

        files.forEach(file => {
            formData.append(file.name, file);
        });


        return new Promise((resolve, reject) => {
            $.post({
                url,
                data: formData,
                contentType: false,
                processData: false,
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
            contentType: 'application/json;charset=utf8',
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

    uploadIdeaBanner(id, file) {
        return utils.uploadFile(`/api/ideas/${id}/banner`, [file]);
    }

    getIdeas() {
        return Promise.resolve([]);
    }
}