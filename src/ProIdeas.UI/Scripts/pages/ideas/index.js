import ApiClient from '../../modules/api';


var client = new ApiClient();

client.getIdeas()
    .then(ideas => {
        console.log();
    });