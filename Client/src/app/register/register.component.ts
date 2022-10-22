import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../appServices/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model: any = {};
  visible: boolean = true;
  changetype: boolean = true;
  // showPassword: boolean = false;
  // faHeart = faHeart;
  // faComment = faComment;

  constructor(private accountServic: AccountService, private toastr: ToastrService) {

  }

  ngOnInit(): void {
  }
  // next:respone => this.router.navigateByUrl('/mambers'),
  // error: error => {console.log(error),
  // this.toastr.show(error.error)
  register() {
    this.accountServic.register(this.model).subscribe({
      next: response => console.log(response),
      error: error => {
        console.log(error),
          this.toastr.error(error.error);
        this.cancel();
      }
    });
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
  viewpass() {
    this.visible = !this.visible;
    this.changetype = !this.changetype;
  }


}
  // showHidePassword() {
  //   this.showPassword = !this.showPassword;
  // }

// next:respone => this.router.navigateByUrl('/mambers'),
// error: error => {console.log(error),
// this.toastr.show(error.error)


// this.accountServic.register(this.model).subscribe(
//   response => {
//     console.log(response),
//       this.cancel();
//   }),
//   error => {
//     console.log(error),
//     this.toastr.error(error.error)
//   }