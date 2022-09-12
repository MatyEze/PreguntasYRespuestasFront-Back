import { Component, OnInit } from '@angular/core';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-cuestionarios',
  templateUrl: './cuestionarios.component.html',
  styleUrls: ['./cuestionarios.component.css']
})
export class CuestionariosComponent implements OnInit {
  nombreUsuario: string = 'xxx';
  constructor(private loginService: LoginService) { 
    
  }

  ngOnInit(): void {
    this.getNombreUsuario();
  }

  getNombreUsuario(): void{
    //console.log(this.loginService.getDecodeToken());
    this.nombreUsuario = this.loginService.getDecodeToken().sub;
  }
}
