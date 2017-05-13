(function(){
    angular
    .module("taskboard")
    .factory('TasksService', function ($http, $q) {
        return {
            getTasks: function() {
                var deferred = $q.defer();
                $http.get('http://www.mocky.io/v2/5916f9b80f0000fc0791fafa')
                    .then(function(response) {
                        deferred.resolve(response.data);
                    }, function(response) {
                        // something went wrong
                        deferred.reject(response.data);
                    });
           
                return deferred.promise;
            }
        };
    });
})();