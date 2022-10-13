import { Component  ,OnInit} from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  
  title = 'client';
  users :any;
  
  
  constructor (private http:HttpClient)
  {
    
  }
  ngOnInit() {
    this.http.get('https://localhost:7078/api/users').subscribe({
      next: response => this.users = response,
      error: error => console.log(error)
    })
    // this.userService.getUser().subscribe(result=>{ this.userList =result});
  }
  
  


  // getUsers(){
  //   //  this.http.get('https://localhost:7112/api/users').subscribe(data=>{
  //   //   console.log(data)});
  //   this.http.get('https://localhost:7112/api/users').subscribe(response =>
    
  //   { this.users= response;} ,error => 
   
  //   {console.log(error);});
  // }
  
 
 }