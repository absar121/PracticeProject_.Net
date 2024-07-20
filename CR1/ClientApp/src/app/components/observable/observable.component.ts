import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { of,from,map, interval,fromEvent, toArray, Subscription, debounceTime } from 'rxjs';
import { UserService } from '../../users/user.service';

@Component({
  selector: 'app-observable',
  templateUrl: './observable.component.html',
  styleUrls: ['./observable.component.css']
})
export class ObservableComponent implements OnInit,AfterViewInit {

    constructor(private useservice: UserService) { }
    @ViewChild('addbtn') addbtn: ElementRef;
    @ViewChild('myinput') myinput: ElementRef;
    sub1: Subscription;
    msg1;
    reqdata=null;
    ngOnInit(): void {
       // this.useservice.exclusive.next(true);
        const obaaj = of(['absar', 'ahmad', 'ahmaaad']);
        obaaj.pipe(toArray()).subscribe(data => {
            console.log(data);
            this.print2(data);
        })

        const broadcast = interval(1000);
        this.sub1 = broadcast.pipe(
            map(data=>'absar'+data)
        )
            .subscribe(res => {
                this.msg1 = res;
            })
        setTimeout(() => {
            this.sub1.unsubscribe();
        }, 10000);
    }

    ngAfterViewInit() {
        let count = 1;
        fromEvent(this.addbtn.nativeElement, 'click').subscribe(res => {
           let countval = 'absar' + count++;
            this.print(countval);
        })

        const searchitem = fromEvent<any>(this.myinput.nativeElement, 'keyup').pipe(
            map(event => event.target.value),
            debounceTime(1000)
        );

        searchitem.subscribe(res => {
            console.log(res);
            this.reqdata = res;
        })
    }

    print(val) {
        let el = document.createElement('li');
        el.innerText = val;
        document.getElementById('licontainer').appendChild(el);   
    }

     print2(val) {
          let el = document.createElement('li');
         el.innerText = val;
         document.getElementById('licontainer2').appendChild(el);
      }




}
