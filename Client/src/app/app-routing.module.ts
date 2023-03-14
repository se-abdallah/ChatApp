import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './appGuards/auth.guard';
import { HomeComponent } from './home/home.component';
import { ListsComponent } from './lists/lists.component';
import { MembersDetailComponent } from './members/members-detail/members-detail.component';
import { MembersListComponent } from './members/members-list/members-list.component';
import { MessagesComponent } from './messages/messages.component';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { PreventUnsavedChangesGuard } from './appGuards/prevent-unsaved-changes.guard';
import { MemberDetailedResolver } from './appResolver/member-detailed.resolver';
import { AdminPanelComponent } from './appAdmin/admin-panel/admin-panel.component';
import { AdminGuard } from './appGuards/admin.guard';

const routes: Routes = [
  {path:'', component:HomeComponent},
  {
    path:'',
    runGuardsAndResolvers:'always',
    canActivate:[AuthGuard],
    children:[
      // {path:'members', component:MembersListComponent,canActivate:[AuthGuard]},
      {path:'members', component:MembersListComponent},
      {path:'members/:username', component:MembersDetailComponent , resolve : {member: MemberDetailedResolver}},
      {path:'member/:edit', component:MemberEditComponent, canDeactivate :[PreventUnsavedChangesGuard]},
      {path:'lists', component:ListsComponent},
      {path:'messages', component:MessagesComponent},
      {path:'admin', component:AdminPanelComponent , canActivate:[AdminGuard]},
    ]
  },
  {path :'errors', component :TestErrorsComponent},
  {path :'not-found', component :NotFoundComponent},
  {path :'server-error', component :ServerErrorComponent},
  {path:'**', component:HomeComponent,pathMatch:'full'},
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
