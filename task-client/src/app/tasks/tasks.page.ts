import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { TaskService, Task } from '../services/task.service';
import { Dialog } from '@capacitor/dialog';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.page.html',
  styleUrls: ['./tasks.page.scss'],
  standalone: false
})
export class TasksPage implements OnInit {
  tasks: Task[] = [];
  incompleteTasks: Task[] = [];
  completedTasks: Task[] = [];

  constructor(private taskService: TaskService, private cdr: ChangeDetectorRef) {}

  ngOnInit() {
    this.loadTasks();
  }

  loadTasks() {
    this.taskService.getTasks().subscribe((tasks) => {
      this.tasks = tasks;
      this.incompleteTasks = tasks.filter(task => !task.completed);
      this.completedTasks = tasks.filter(task => task.completed);
    });
  }

  async addTask() {
    console.log('addTask function called');
  
    const { value, cancelled } = await Dialog.prompt({
      title: 'Add New Task',
      message: 'Enter Task Title in Box Below',
      inputPlaceholder: 'Task name',
      okButtonTitle: 'Add',
      cancelButtonTitle: 'Cancel'
    });
    console.log('Prompt result:', { value, cancelled });
  
    if (!cancelled && value) {
      console.log('Creating task with value:', value);
      this.createTask(value);
    } else {
      console.log('Task creation cancelled');
    }
  }

  createTask(title: string) {
    const newTask: Task = { taskId: 0, title, completed: false };
    this.taskService.createTask(newTask).subscribe(() => {
      console.log('Task created:', title);
      this.loadTasks();
    }, error => {
      console.error('Error creating task:', error);
    });
  }

  toggleTaskCompletion(task: Task) {
    const updatedTask = { ...task, completed: !task.completed };
  
    this.taskService.updateTask(task.taskId, updatedTask).subscribe(
      (updatedTaskFromServer) => {
        console.log('Updated task from server:', updatedTaskFromServer);
  
        const index = this.tasks.findIndex(t => t.taskId === updatedTaskFromServer.taskId);
        if (index !== -1) {
          this.tasks[index] = updatedTaskFromServer;
        }
  
        this.refreshTaskLists();
        this.cdr.detectChanges(); // 🔥 Force UI to refresh
      },
      (error) => {
        console.error('Error updating task:', error);
      }
    );
  }
  
  deleteTask(taskId: number) {
    this.taskService.deleteTask(taskId).subscribe(() => {
      console.log('Task deleted:', taskId);
      this.tasks = this.tasks.filter(task => task.taskId !== taskId);
      this.refreshTaskLists();
    }, error => {
      console.error('Error deleting task:', error);
    });
  }
  
  refreshTaskLists() {
    this.incompleteTasks = this.tasks.filter(task => !task.completed);
    this.completedTasks = this.tasks.filter(task => task.completed);
  }
}