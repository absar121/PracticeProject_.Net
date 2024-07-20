import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Component } from '@angular/core';
import {  MatNativeDateModule } from '@angular/material/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { UserDetailComponent } from './user-detail/user-detail.component';
import { LoginComponent } from './login/login.component';
import { UserFormComponent } from './user-detail/user-form/user-form.component';
import { DatePipe, CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatToolbarModule } from '@angular/material/toolbar';
import { NgxUiLoaderModule } from "ngx-ui-loader";
import { MatInputModule } from '@angular/material/input';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { AppRoutingModule } from './users/app-routing.module';
import { ToastrModule } from 'ngx-toastr';
import { MatCardModule } from '@angular/material/card';
import { IntercepterService } from './users/intercepter.service';
import { ObservableComponent } from './components/observable/observable.component';
import { UserService } from './users/user.service';



@NgModule({
  declarations: [
        AppComponent,
        UserDetailComponent,
        UserFormComponent,
        LoginComponent,
        ObservableComponent

  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
      HttpClientModule,
      AppRoutingModule,
      FormsModule,
      CommonModule,
      FlexLayoutModule,
      MatSortModule,
      ReactiveFormsModule,
      NgxUiLoaderModule,
      ToastrModule.forRoot(), // ToastrModule added
      MatToolbarModule,
      MatPaginatorModule,
      MatButtonModule,
      MatIconModule,
      MatInputModule,
      MatFormFieldModule,
      MatTableModule,
      MatDatepickerModule,
      MatFormFieldModule,
      MatNativeDateModule,
      MatDialogModule,
      MatCardModule,
      MatButtonToggleModule,
      RouterModule.forRoot([
    ]),
    BrowserAnimationsModule
  ],
    providers: [DatePipe, {
        provide: HTTP_INTERCEPTORS,
        useClass: IntercepterService,
        multi: true
    }], 
    bootstrap: [AppComponent],

})
export class AppModule { }
