import { Component  ,OnInit} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from './Appmodel/user';
import { AccountService } from './Appservices/account.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  
  title = 'The Chat App';
  users :any;
  
  
  constructor (public accountService:AccountService){

  }
  
  ngOnInit() {
   this.setCurrentUser();
    }

  setCurrentUser(){
    const user:User =JSON.parse(localStorage.getItem('user')||'{}');
    this.accountService.setCurrentUser(user);
  }
    
  // getUsers(){
  //   this.http.get('https://localhost:7078/api/users').subscribe({
  //     next: response => this.users = response,
  //     error: error => console.log(error)
  // })}
    
  
}