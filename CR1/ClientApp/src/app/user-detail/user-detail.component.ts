import { DatePipe } from '@angular/common';
import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { User } from '../users/user.model';
import { UserService } from '../users/user.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { NgForm, FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { NgxUiLoaderService } from "ngx-ui-loader";


@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.css']
})


export class UserDetailComponent implements OnInit {
   // exclusive: Boolean = false;
    displayedColumns: string[] = ['userid', 'name', 'cnic', 'dob', 'age', 'action'];
    dataSource: MatTableDataSource<any>;
    actionbtn: string = "Save";
    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort) sort: MatSort;
    constructor(private router: Router, private ngxService: NgxUiLoaderService, private toastr: ToastrService,public useService: UserService, public datepipe: DatePipe, private formbuilder: FormBuilder) { }
    
    ngOnInit(): void {
        this.getallproduct();
       // this.useService.exclusive.subscribe(res => {
          //  this.exclusive = res;
       // })
    }
    getallproduct() {
        this.ngxService.start(); // start foreground spinner of the master loader with 'default' taskId
        // Stop the foreground loading after 5s
        setTimeout(() => {
            this.ngxService.stop(); // stop foreground spinner of the master loader with 'default' taskId
        }, 3000);
        this.useService.getUser().subscribe(data => {
            this.useService.listuser = data;
            this.dataSource = new MatTableDataSource(data);
            this.dataSource.paginator = this.paginator;
            this.dataSource.sort = this.sort;
        });
    }
    userform = this.formbuilder.group({

        userid: this.formbuilder.control(''),
        Name: this.formbuilder.control('', Validators.required),
        Age: this.formbuilder.control('', Validators.required),
        Cnic: this.formbuilder.control('', Validators.required),
        dob: this.formbuilder.control('', Validators.required)
    })

    submit() {
        let data: any;
        data = this.userform.value;
        if (this.userform.value.userid == '') {
            delete data.userid;
            this.useService.saveUser(data).subscribe(d => {
                this.refreshdata();
                this.resetform();
                this.toastr.success('Data Save Sucessfully');
                this.getallproduct();
            });
        }
        else {
            debugger
            this.useService.updateUser(data).subscribe(d => {
                this.refreshdata();
                this.resetform();
                this.toastr.success('Update Sucessfully');
                this.getallproduct();
            });
        }

    }

    resetform() {
        this.userform.reset();
        this.actionbtn = "Save";
        //this.useService.userdata = new User();

    }
    refreshdata() {
        this.useService.getUser().subscribe(data => {
            this.useService.listuser = data;
        });
    }
    applyFilter(event: Event) {
        const filterValue = (event.target as HTMLInputElement).value;
        this.dataSource.filter = filterValue.trim().toLowerCase();

        if (this.dataSource.paginator) {
            this.dataSource.paginator.firstPage();
        }
    }
    
    edituser(row: any) {
        if (this.useService.role == 'admin') {
            let dp = this.datepipe.transform(row.dob, 'yyyy-MM-dd');
            row.dob = dp;
            this.useService.userdata = row;
            let address = {
                userid: row.userid,
                Name: row.name,
                Cnic: row.cnic,
                Age: row.age,
                dob: row.dob
            };
            this.userform.patchValue(address);
            this.actionbtn = "update";
        }
        else {
            this.toastr.error('Sorry! You dont Have Acess');
        }
        
           }

    deleteuser(userid: number) {
        if (this.useService.role == 'admin') {
            if (confirm('are you really want to delete this record?')) {
                this.useService.deleteUser(userid).subscribe(data => {
                    this.toastr.success('Deleted Sucessfully');
                    //to refresh data after deleted
                    this.getallproduct();
                }, error => {
                    this.toastr.error('Error! Not Deleted');
                });
            }
        }
        else {
            this.toastr.error('Sorry! You dont Have Acess');
        }
        
    }

    logout() {
        this.toastr.success('Login Session Expires ');
        this.useService.removetoken();
    }

}
