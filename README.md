# IonicAngularFinal
A Full Stack application witha a Backend REST API using C#.  The Front End is Ionic Angular.  This project creates a task list divided between Incomplete Tasks and Completed Tasks.  Tasks can be added, checked complete or incomplete, and deleted if necessary.

Project Setup
Navigate into the ionic-course directory with your command prompt/terminal.

Create a new folder in this directory called final-project and navigate into it:

mkdir final-project
cd final-project
Create a new folder in final-project called task-api. You will create your backend application in this directory.

In the final-project folder, generate a new Ionic Angular project with the following command:

ionic start task-client blank --type=angular --capacitor

Requirements
Back-End
Create a Back-End REST API using C# or NodeJS, with the following requirements:

The API should have a Task model that includes the following properties:

TaskId: unique identifier for the task
Title: string that contains the task title
Completed: boolean that indicates whether the task is completed
The API needs to provide, at a minimum, the following routes:

GET /tasks: returns a list of all tasks
POST /tasks: creates a new task
PUT /tasks/:id: updates an existing task
DELETE /tasks/:id: deletes an existing task
You can use any form of data persistence (in-memory, SQL, or NoSQL) to store the tasks.

Note: We recommend you test your back-end API using PostMan (or Swagger) to test and document your API endpoints. This will make it easier to integrate your Ionic web client.

Front-End
Create an Ionic Angular front-end application that consumes your back-end API and provides a user interface for managing tasks. All HTTP requests to your API should be managed in an Angular service.

Your app should include the following features:

Task List Screen
Lists all of the incomplete tasks at the top of the page and the completed tasks at the bottom of the page.
Include a button on the screen that allows the user to add a new task.
Create Task Dialog
The Add Task button should open a prompt dialog that allows the user to enter a new task name.
When the task is submitted, the dialog should be dismissed and the new task should be sent to the backend.
The task list should be refreshed to display the new task.
Mark Task Complete/Incomplete
When a task checkbox is tapped, the task should be toggled complete/incomplete in the back-end, and the change should be reflected in the UI.
Delete Task
Users can slide an item in the list to delete the Task (use the ion-item-sliding component).
When an item is deleted, the operation should be sent to the back-end and the task list should be refreshed to display the updated list.
Use a custom theme and styling to make the app visually appealing.
