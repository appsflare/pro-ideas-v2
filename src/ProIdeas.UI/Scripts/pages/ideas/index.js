import ApiClient from '../../modules/api';
import knockout from 'knockout';



var client = new ApiClient();

client.getIdeas()
    .then(ideas => {
        console.log();
    });