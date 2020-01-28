$( function() {
    $( ".sortable" ).sortable({
        revert: true
    });
    $( ".sortable" ).droppable();

    const allTaskUrl = 'http://localhost:8080/tasks/all';
    const todoList = document.querySelector('#todo');
    const inprogressList = document.querySelector('#inprogress');
    const completedList = document.querySelector('#completed');

    fetch(allTaskUrl)
        .then(response => response.json())
        .then(data => {
            for (const task of data) {
                let listTaskItem = document.createElement('li');
                listTaskItem.id = task.id;
                listTaskItem.className = "draggable";

                let section = document.createElement('div');
                section.className = "card mb-3";
                section.setAttribute('style', 'max-width: 540px;');

                let cardSpace = document.createElement('div');
                cardSpace.className = "row no-gutters";

                let markerSpace = document.createElement('div');
                markerSpace.className = "col-md-4";

                let marker = document.createElement('img');
                marker.className = "card-img";
                marker.setAttribute('src', 'images/'+task.marker);

                let descriptionSpace = document.createElement('div');
                descriptionSpace.className = "col-md-8";

                let descriptionSpaceBody = document.createElement('div');
                descriptionSpaceBody.className = "card-body";

                let cardTitle = document.createElement('h5');
                cardTitle.className = "card-title";
                cardTitle.textContent = task.title;

                let cardText = document.createElement('p');
                cardText.className = "card-text";
                cardText.textContent =  task.description;

                let cardTimeSpace = document.createElement('p');
                cardTimeSpace.className = "card-text";

                let now = Date.now();
                let updatedAt = new Date(task.updatedAt);
                var elapsed = now - updatedAt; // time in milliseconds
                var elapsedSeconds = Math.floor(elapsed / 1000);
                var elapsedMins = Math.floor(elapsed / 60000);
                var elapsedHours = Math.floor(elapsedMins / 60);
                if (elapsedHours > 0) elapsed = elapsedHours + " hours ago";
                else if (elapsedMins > 0) elapsed = elapsedMins + " mins ago";
                else elapsed = elapsedSeconds + " seconds ago";

                let cardTime = document.createElement('small');
                cardTime.className = "text-muted";
                cardTime.textContent =  'Last updated ' + elapsed;

                let cardUser = document.createElement('footer');
                cardUser.className = "blockquote-footer";
                cardUser.textContent =  "Assigned to " + data.userId;

                listTaskItem.appendChild(section);
                    section.appendChild(cardSpace);
                        cardSpace.appendChild(markerSpace);
                            markerSpace.appendChild(marker);
                            markerSpace.after(descriptionSpace);
                                descriptionSpace.appendChild(descriptionSpaceBody);
                                    descriptionSpaceBody.appendChild(cardTitle);
                                        cardTitle.after(cardText);
                                            cardText.after(cardTimeSpace);
                                                cardTimeSpace.appendChild(cardTime);
                                            cardTimeSpace.after(cardUser);

                const getUserUrl = 'http://localhost:8080/users/' + task.userId;

                fetch(getUserUrl)
                    .then(response => response.json())
                    .then(data => {
                        cardUser.textContent =  "Assigned to " + data.firstName;
                })
                
                if (task.status == "todo") {
                    todoList.appendChild(listTaskItem);
                }
                else if (task.status == "in_progress" || task.status == "inprogress") {
                    inprogressList.appendChild(listTaskItem);
                }
                else if (task.status == "completed") {
                    completedList.appendChild(listTaskItem);
                }
            }
            
            $( ".draggable" ).draggable({
                connectToSortable: ".sortable",
                revert: true,
            });
            $( ".sortable" ).on('drop',function(event,ui){
                const droppedItemID = ui.draggable.attr("id");
                const dropZoneID = $(this).attr('id');
                updateTask(droppedItemID, dropZoneID);
            });        
            $("ul, li").disableSelection();
        });
    }
);

function updateTask(droppedItemID, dropZoneID) {
    const url = 'http://localhost:8080/tasks/' + droppedItemID;
    var taskData = "";

    fetch(url)
        .then(response => response.json())
        .then(data => {
            var createdAt = data.createdAt;
            var updatedAt = data.createdAt;
            var id = data.id;
            var userId = 3;
            var projectId = data.projectId;
            var title = data.title;
            var description = data.description;
            var startDate = data.startDate;
            var endDate = data.endDate;
            var status = dropZoneID;
            var marker = data.marker;

            taskData = JSON.stringify({ createdAt, updatedAt, id, userId, projectId, title, description, startDate, endDate, status, marker });
        
            fetch('http://localhost:8080/tasks/update/' + droppedItemID, {
                method: 'PUT',
                body: taskData,
                headers : { 
                    'Content-type' : 'application/json',
                    'Accept' : '*'
                }
            })
            .then((response) => { 
                console.log(response.status);  //400 in your case
                response.json().then((errorJson) => {
                    console.log(errorJson); // should return the error json
                });
            }) 
        }); 
    
    setTimeout(function() {
        window.location.reload();
    }, 1500);
}

