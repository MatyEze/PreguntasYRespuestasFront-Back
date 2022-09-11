import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Usuario } from '../../../models/usuario'

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  login: FormGroup;
  loading: boolean = false;
  constructor(private fb: FormBuilder,
              private toastr: ToastrService,
              private router: Router) {
    this.login = this.fb.group({
      usuario: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  ngOnInit(): void {
  }

  log(): void {

    const usuario: Usuario = {
      nombreUsuario: this.login.value.usuario,
      password: this.login.value.password
    }

    console.log(usuario);

    if (usuario.nombreUsuario === "msellanes" && usuario.password === "admin123") {
      this.login.reset();
      this.router.navigate(['/dashboard']);
    } else {
      this.toastr.error("Usuario o Contraseña Invalidos", "ERROR!");
      this.login.reset();
    }

  }
}
