import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UsuarioService } from 'src/app/services/usuario.service';

@Component({
  selector: 'app-cambiar-password',
  templateUrl: './cambiar-password.component.html',
  styleUrls: ['./cambiar-password.component.css']
})
export class CambiarPasswordComponent implements OnInit {
  cambiarPass: FormGroup;
  loading: boolean = false;
  constructor(private fb: FormBuilder,
              private toastr: ToastrService,
              private router: Router,
              private usuarioService: UsuarioService) {
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

  cambiarPassword(): void{
    const cambiarPassword: any = {
      passwordAnterior: String = this.cambiarPass.value.passActual,
      nuevaPassword: String = this.cambiarPass.value.passNueva
    }
    //console.log(cambiarPassword);
    this.loading = true;
    this.usuarioService.cambiarPassword(cambiarPassword).subscribe(data => {
      console.log(data);
      this.toastr.success("La contraseña se a cambiado exitosamente", "Contraseña cambiada");
      this.router.navigate(['/dashboard']);
    }, error =>{
      console.log(error);
      this.toastr.error(error.error.message, "ERROR!");
      this.cambiarPass.reset();
      this.loading = false;
    })
  }
}
