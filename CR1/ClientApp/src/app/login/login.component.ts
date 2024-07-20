import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { User } from '../users/user.model';
import { UserService } from '../users/user.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { NgForm, FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Local } from 'protractor/built/driverProviders';
import { ToastrService } from 'ngx-toastr';
import { MatCardModule } from '@angular/material/card';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

    constructor(private router: Router, public useService: UserService, private toastr: ToastrService,private formbuilder: FormBuilder) { }

  ngOnInit(): void {
  }
    loginform = this.formbuilder.group({
        email: this.formbuilder.control('', [Validators.required,Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$")]),
        password: this.formbuilder.control('', Validators.required)
    })
    isuservalid: boolean = false;

    submit() {
        let data: any;
        data = this.loginform.value;
        console.log(data);
        this.useService.Login([data.email, data.password]).subscribe((d) => {
            if (d == 'Failure') {
                
                this.isuservalid = false;
                this.toastr.error('Login failed ! please try again');
            }
            else  {
                console.log(d);
                this.isuservalid = true;
                this.useService.settoken(d);
                this.toastr.success('Login sucessfully');
                this.router.navigateByUrl('userdetails');
            }
            
        });
    }

   
}
