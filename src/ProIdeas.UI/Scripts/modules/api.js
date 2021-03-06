﻿const defaultHeaders = {

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
    delete(url) {
        return new Promise((resolve, reject) => {
            $.ajax({
                url,
                type: 'DELETE',                
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

    saveIdeaPages(id, pages) {
        return utils.put(`/api/ideas/${id}/pages`, pages);
    }

    getIdeaPages(id) {
        return utils.get(`/api/ideas/${id}/pages`);
    }

    getIdeas({keyword = "", page, pageSize = 100}) {
        let url = `/api/ideas?page=${page}&pageSize=${pageSize}`;
        if (keyword) {
            url = `/api/ideas/search?page=${page}&pageSize=${pageSize}&keyword=${keyword}`;
        }
        return utils.get(url);
    }

    getMyIdeas({keyword = "", page, pageSize = 100}) {
        const url = `/api/ideas/searchmyideas?page=${page}&pageSize=${pageSize}&keyword=${keyword}`;
        return utils.get(url);
    }

    like(ideaId, isLike) {
        return utils.put(`/api/ideas/${ideaId}/likes/${isLike}`);
    }

    getIdeaComments({ideaId, page, pageSize = 100}) {
        let url = `/api/ideas/${ideaId}/comments?page=${page}&pageSize=${pageSize}`;
        return utils.get(url);
    }

    postIdeaComment({ideaId, content}) {
        let url = `/api/ideas/${ideaId}/comments`;
        return utils.post(url, { content });
    }

    updateIdeaComment({ideaId, id, content}) {
        let url = `/api/ideas/${ideaId}/comments`;
        return utils.put(url, { id, content });
    }

    deleteComment(id) {
        let url = `/api/ideas/comments/${id}`;
        return utils.delete(url);
    }
}