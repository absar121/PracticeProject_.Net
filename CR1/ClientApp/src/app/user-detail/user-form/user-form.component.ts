
import { Component, OnInit,Input } from '@angular/core';
import { NgForm, FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { UserService } from '../../users/user.service';
import { UserDetailComponent } from '../user-detail.component';

@Component({
  selector: 'app-user-form',
  templateUrl: './user-form.component.html',
  styleUrls: ['./user-form.component.css']
})
export class UserFormComponent implements OnInit {
   // debugger
   // @Input() hero: any = {};

    ngOnInit(): void {


    }


    constructor(public useservice: UserService, private formbuilder: FormBuilder) {
    }
  

}