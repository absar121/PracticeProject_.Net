
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from '../login/login.component';
import { NotFoundComponent } from '../not-found/not-found.component';
import { UserDetailComponent } from '../user-detail/user-detail.component';
import { ObservableComponent } from '../components/observable/observable.component';
import { HttpClientModule } from '@angular/common/http';
import { AuthGuard } from './auth.guard';

const routes: Routes = [
    {
        path: 'login',
        component: LoginComponent
    },
    {
        path: 'userdetails',
        component: UserDetailComponent,
        canActivate: [AuthGuard]    },
    {
        path: '',
        redirectTo: 'login',
        pathMatch:'full'
    },
    {
        path: 'observable',
        component: ObservableComponent,
        pathMatch: 'full'
    },

    {
        path: '**',
        component: NotFoundComponent
    }

];

@NgModule({
    imports: [RouterModule.forRoot(routes), HttpClientModule], 
    exports: [RouterModule]
    })

export class AppRoutingModule {

}