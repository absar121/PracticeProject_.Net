import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { User } from './user.model';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from '../../environments/environment';
import { Local } from 'protractor/built/driverProviders';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import Swal from 'sweetalert2/dist/sweetalert2.js';
import jwt_decode from 'jwt-decode';
//import { UserDetailComponent } from '../user-detail/user-detail.component';

//import { text } from 'stream/consumers';

@Injectable({
  providedIn: 'root'
})
export class UserService {
    exclusive = new Subject<boolean>();
    constructor(private myhttp: HttpClient, private router: Router, private toastr: ToastrService) { }
    currentuser: BehaviorSubject<any> = new BehaviorSubject(null);
    //clearTimeout:any;
    jwtHelperService = new JwtHelperService();
    userurl: string = environment.baseurl + 'api/Users';
    role: string='admin';
    //urll: string = environment.baseurl;
    listuser: User[] = [];

    userdata: User = new User(); //single object for post /inset data
    
    saveUser(userdata: any) {

        return this.myhttp.post(this.userurl, userdata); //hit form data on post api to save
    }
 
    updateUser(data) {
        return this.myhttp.put(`${this.userurl}/${data.userid}`, data); //hit form data on put api to save
    }

    getUser(): Observable<User[]> {
        return this.myhttp.get<User[]>(this.userurl);
    }

    deleteUser(userid: number) {
        return this.myhttp.delete(`${this.userurl}/${userid}`);
    }

    Login(logininfo: Array<string>) {
        return this.myhttp.post(this.userurl +'/login', {
            email: logininfo[0],
            password: logininfo[1]

        },
            {
                responseType: 'text',

            }); //hit form data on post api to save
    }
    settoken(token: string) {
        localStorage.setItem("acess_token", token);
        //localStorage.setItem("refresh token", token.refreshtoken);
        this.loadcurrentuser();
        //let date = new Date().getTime();
        //let expdate = new Date
        this.autologout(100000);
    }

    autologout(expdate: number) {
        setTimeout(() => {
            //this.ud.logout();
            this.removetoken();
            //this.router.navigateByUrl('/login');
        }, expdate);
    }

    loadcurrentuser() {
        const token = localStorage.getItem("acess_token");
        const userinfo = token != null ? this.jwtHelperService.decodeToken(token) : null;
        console.log(userinfo);
        const data = userinfo ? {
            email: userinfo.email,
            password: userinfo.password,
            role:userinfo.role
        } : null;
        this.role = userinfo.role;
        this.currentuser.next(data);
    }

    isloggedin(): boolean {
        return localStorage.getItem("acess_token") ? true : false;
    }

    removetoken() {
        //this.toastr.success('Login Session Expires ');
        //Swal.fire('Session Expires, Session Expires Again login');
        alert('session expires');
        setTimeout(() => {
            localStorage.removeItem("acess_token");
        }, 10000);
       
        this.router.navigateByUrl('/login');
    }

    gettoken() {
        return localStorage.getItem("acess_token");
    }

       // verifytoken(req,res,next) {

      //   if (!req.headers.authorization) {
        //     return res.status(401).send('unauthorized request');
         //}
         //let token = req.headers.authorization.split('')[1];
         //if (token == null) {
           //  return res.status(401).send('unauthorized request');
         //}
         //let payload = this.verify(token, 'secret key');

    //}




}

