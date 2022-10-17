import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from '../Appservices/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit { 
  @Output() cancelRegister = new EventEmitter();
  model:any ={};

  constructor(private accountServic:AccountService) { 

  }

  ngOnInit(): void {
  }

  register(){
   this.accountServic.register(this.model).subscribe(
    response => { console.log(response),
    this.cancel();
    }),
    error => console.log(error);
  }

  cancel(){
    this.cancelRegister.emit(false);
  }
}
