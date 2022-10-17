import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../Appmodel/user';
import { AccountService } from '../Appservices/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {}
 
  

  constructor(public accountService: AccountService) { }

  ngOnInit(): void {
    
  }
  login() {
    this.accountService.login(this.model).subscribe({
      next: respone => console.log(respone),
      error: error => console.log(error) 
    })
  }

  logout() {
    this.accountService.logout();
  }
}

//   getCurrentUser(){
//     this.accountService.currentUser$.subscribe({
//      error : error => console.log(error)
//     }),
//     user => this.loggedIn =!!user
//   }
// }