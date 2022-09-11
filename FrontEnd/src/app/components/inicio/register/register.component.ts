import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  register: FormGroup;
  constructor(private fb: FormBuilder) {
    this.register = fb.group({
      usuario: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(8)]],
      repetirPassword: ['']
    }, { validator: this.checkPassword })
  }

  ngOnInit(): void {
  }

  registrarUser(): void {
    console.log(this.register);
  }
  
  checkPassword(group: FormGroup): any {
    const pass = group.controls['password'].value;
    const checkPass = group.controls['repetirPassword'].value;
    return pass === checkPass ? null : {notSame: true};
  }
}
