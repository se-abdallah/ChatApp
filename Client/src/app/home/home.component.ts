<<<<<<< HEAD
=======
import { HttpClient } from '@angular/common/http';
>>>>>>> 929b681754124ca02c81088fac73c6ff8f2352f6
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
<<<<<<< HEAD
  users: any;

  constructor() {
  }

  ngOnInit(): void {
  }

  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }
=======
  users:any;

  constructor() { 
    
  }

  ngOnInit(): void {
    
  }

  registerToggle(){
    this.registerMode =!this.registerMode;
  }
  
   cancelRegisterMode(event:boolean){
    this.registerMode =event;
   }
>>>>>>> 929b681754124ca02c81088fac73c6ff8f2352f6
}