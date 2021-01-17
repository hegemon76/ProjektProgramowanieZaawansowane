import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CategoriesListComponent } from './categories/categories-list/categories-list.component';
import { HomeComponent } from './home/home.component';
import { CompletedTasksComponent } from './todoitems/completed-tasks/completed-tasks.component';
import { TaskListComponent } from './todoitems/task-list/task-list.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'categories', component: CategoriesListComponent},
  {path: 'tasks', component: TaskListComponent},
  {path: 'completed-tasks', component: CompletedTasksComponent},
  {path: '**', component: HomeComponent, pathMatch:'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
