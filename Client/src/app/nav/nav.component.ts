import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../appServices/account.service';
import { User } from '../appModel/user';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  visible: boolean = true;
  changetype: boolean = true;




  constructor(public accountService: AccountService, private router: Router, private toastr: ToastrService) {

  }

  ngOnInit(): void {

  }
  login() {
    this.accountService.login(this.model).subscribe({
      next: response => this.router.navigateByUrl('/mambers'),
      // error: error => {
      //   console.log(error),
      //   this.toastr.error(error.error)
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

  viewpass() {
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