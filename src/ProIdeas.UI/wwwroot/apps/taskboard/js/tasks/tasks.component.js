(function(){
    angular.module("taskboard")
     .directive("tasks", function() {
        return {
            restrict: 'E',
            scope: {},
            controller: 'TaskController',
            controllerAs: 'tasksVm',
            templateUrl: 'js/tasks/tasks.html'
        };
    })
})();
