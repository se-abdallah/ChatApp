<<<<<<< HEAD
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
=======
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
>>>>>>> 929b681754124ca02c81088fac73c6ff8f2352f6
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../appServices/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
<<<<<<< HEAD
  visible: boolean = true;
  changetype: boolean = true;
  // checked: boolean = false;
  registerForm: FormGroup = new FormGroup({});
  maxDate: Date = new Date();
  validationErrors: string[] | undefined;

  constructor(private accountServic: AccountService, private toastr: ToastrService, private fb: FormBuilder, private router: Router) {
=======
  model: any = {};
  visible: boolean = true;
  changetype: boolean = true;
  // showPassword: boolean = false;
  // faHeart = faHeart;
  // faComment = faComment;

  constructor(private accountServic: AccountService, private toastr: ToastrService) {
>>>>>>> 929b681754124ca02c81088fac73c6ff8f2352f6

  }

  ngOnInit(): void {
<<<<<<< HEAD
    this.initializeForm();
    this.maxDate.setFullYear(this.maxDate.getFullYear() - 18);
  }

  initializeForm() {
    this.registerForm = this.fb.group({
      username: ['', Validators.required],
      gender: ['male'],
      knownAs: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      jobTitle: ['', Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(8)]],
      confirmPassword: ['', [Validators.required, this.matchValues('password')]],
    })
    this.registerForm.controls['password'].valueChanges.subscribe({
      next: () => this.registerForm.controls['confirmPassword'].updateValueAndValidity()
    })
  }
  register() {
    const dob = this.getDateOnly(this.registerForm.controls['dateOfBirth'].value);
    const values = { ...this.registerForm.value, dateOfBirth: dob };
   
    this.accountServic.register(values).subscribe({
      next: () => {
        this.router.navigateByUrl('/members')
      },
      error: error => {
        this.validationErrors = error
        // this.toastr.show(error);
      }
    })
  }
  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control.value === control.parent?.get(matchTo)?.value ? null : { notMatching: true }
    }
=======
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
>>>>>>> 929b681754124ca02c81088fac73c6ff8f2352f6
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
<<<<<<< HEAD
  private getDateOnly(dob: string | undefined) {
    if (!dob) return;
    let theDob = new Date(dob);
    return new Date(theDob.setMinutes(theDob.getMinutes() - theDob.getTimezoneOffset())).toISOString().slice(0, 10);
  }
  viewPass() {
    this.visible = !this.visible;
    this.changetype = !this.changetype;
  }
}

// }
// next: () =>{
//   this.cancel();
// },
// error: error => {
//   console.log(error),
//     this.toastr.error(error.error);
//   this.cancel();
// }
// });
=======
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
>>>>>>> 929b681754124ca02c81088fac73c6ff8f2352f6
