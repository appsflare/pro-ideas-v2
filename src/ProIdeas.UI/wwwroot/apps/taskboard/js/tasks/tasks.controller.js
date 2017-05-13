(function(){
    function TaskController(TasksService){
        var tasksVm = this;
        
        // object for stages
        tasksVm.stages =
                [
                    {
                        "id": 1,
                        "name": "To Do",
                    },
                    {
                        "id": 2,
                        "name": "In Progress",
                    },
                    {
                        "id": 3,
                        "name": "Review",
                    },
                    {
                        "id": 4,
                        "name": "Done",
                    }
                ];


        tasksVm.tasks = TasksService.getTasks()
                        .then(function (response) {
                            tasksVm.tasks = response;
                        }, function (response) {
                            console.error(response.msg);
                        });

        // function for drag start
        tasksVm.dragStart = function dragStart(event, task){
            task.dragging = true;
        }

        // function for on dropping
        tasksVm.onDrop = function onDrop(data,event,stage){
            if(data && data.stage_id != stage.id){
                data.stage_id = stage.id;
                data.dragging = false;
                data.showEdit = false;
            }
        }		

        tasksVm.newTask = {};
        // function for creating new task
        tasksVm.pushTask = function pushTask(){
            tasksVm.tasks.push(tasksVm.newTask);
            tasksVm.newTask = {};
        }

        tasksVm.exportTasks = function exportTasks(){
            var x = window.open();
            x.document.open();
            x.document.write('<html><body><pre>' + JSON.stringify(JSON.parse(angular.toJson(tasksVm.tasks)), null, 2) + '</pre></body></html>');
            x.document.close();
        }

        // Toggle ActionBar
		tasksVm.toggleActionBar = function toggleActionBar(task){
            console.log("toggleActionBar()");
			task.showEdit = !task.showEdit;
		}
}

    TaskController.$inject = ['TasksService'];
 
    angular
    .module("taskboard")
    .controller('TaskController', TaskController);
})();