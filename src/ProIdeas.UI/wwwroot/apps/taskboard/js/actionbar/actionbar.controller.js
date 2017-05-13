(function(){
    angular
    .module("taskboard")
    .controller('ActionBarController', ActionBarController);

    function ActionBarController($element){
        var actionBarVm = this;
        var cardTitle = $element.siblings(".card-title");
        
		// Edit task
		actionBarVm.editTask = function editTask(task){
			task.contenteditable = true;
			cardTitle.attr("contenteditable", "true").focus();
			task.editMode = true;
		}

		// Delete task
		actionBarVm.deleteTask = function deleteTask(task, tasks){
			task.editMode = false;
				
			var idx = tasks.indexOf(task);
			tasks.splice(idx, 1);
		}

		// Update task
		actionBarVm.updateTask = function updateTask(task){
			task.editMode = false;
			cardTitle.attr("contenteditable", "false");
		}

		// Cancel task
		actionBarVm.cancelEditTask = function cancelEditTask(task){
			task.editMode = false;
			cardTitle.attr("contenteditable", "false");
		}
    }
})();
