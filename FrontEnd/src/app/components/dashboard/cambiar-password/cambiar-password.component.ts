import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-cambiar-password',
  templateUrl: './cambiar-password.component.html',
  styleUrls: ['./cambiar-password.component.css']
})
export class CambiarPasswordComponent implements OnInit {
  cambiarPass: FormGroup;
  loading: boolean = false;
  constructor(private fb: FormBuilder) {
    this.cambiarPass = fb.group({
      passActual: ['', Validators.required],
      passNueva: ['', [Validators.required, Validators.minLength(8)]],
      repetirPass: ['', Validators.required]
    }, { validator: this.checkPassword })
  }

  ngOnInit(): void {
  }

  checkPassword(group: FormGroup): any {
    const pass = group.controls['passNueva'].value;
    const checkPass = group.controls['repetirPass'].value;
    return pass === checkPass ? null : {notSame: true};
  }
}
