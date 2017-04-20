import ApiClient from '../../modules/api';
import ko from 'knockout';

var client = new ApiClient();

client.getIdeas()
    .then(ideas => {
        console.log();
    });