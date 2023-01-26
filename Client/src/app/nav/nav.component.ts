<<<<<<< HEAD
import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Member } from '../appModel/member';
import { AccountService } from '../appServices/account.service';
import { MembersService } from '../appServices/members.service';
=======
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../appServices/account.service';
import { User } from '../appModel/user';
>>>>>>> 929b681754124ca02c81088fac73c6ff8f2352f6

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
<<<<<<< HEAD
  @Input()
  member!: Member ;
=======
>>>>>>> 929b681754124ca02c81088fac73c6ff8f2352f6
  model: any = {};
  visible: boolean = true;
  changetype: boolean = true;




  constructor(public accountService: AccountService, private router: Router, private toastr: ToastrService) {

<<<<<<< HEAD
    

=======
>>>>>>> 929b681754124ca02c81088fac73c6ff8f2352f6
  }

  ngOnInit(): void {

  }
  login() {
    this.accountService.login(this.model).subscribe({
<<<<<<< HEAD
      next: response => this.router.navigateByUrl('/members'),
      // error: error => {
      //   console.log(error),
        // this.toastr.error(error.error) this error are being handel by intercepter
=======
      next: response => this.router.navigateByUrl('/mambers'),
      // error: error => {
      //   console.log(error),
      //   this.toastr.error(error.error)
>>>>>>> 929b681754124ca02c81088fac73c6ff8f2352f6
      });



    // respone => {
    //   this.router.navigateByUrl('/mambers')
    //   error => {
    //     console.log(error);
    //     this.toastr.show(error.error);
    //   }})
  }
  // test(){
  //   this.toastr.success("iam fucking here");
  // }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }

<<<<<<< HEAD
  viewPass() {
=======
  viewpass() {
>>>>>>> 929b681754124ca02c81088fac73c6ff8f2352f6
    this.visible = !this.visible;
    this.changetype = !this.changetype;
  }

}

//   getCurrentUser(){
//     this.accountService.currentUser$.subscribe({
//      error : error => console.log(error)
//     }),
//     user => this.loggedIn =!!user
//   }
// }