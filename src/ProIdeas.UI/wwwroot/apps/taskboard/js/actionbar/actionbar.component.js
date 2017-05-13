(function(){
    angular
    .module("taskboard")
     .directive("actionbar", function() {
        return {
            restrict: 'E',
            scope: {
                task: '=',
                tasks: '='
            },
            controller: 'ActionBarController',
            controllerAs: 'actionBarVm',
            templateUrl: 'js/actionbar/actionbar.html'
        };
    })
})();
