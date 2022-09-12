import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Usuario } from '../models/usuario';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  myAppUrl: string;
  myApiUrl: string;

  constructor(private http: HttpClient) { 
    this.myAppUrl = environment.endpoint;
    this.myApiUrl = '/api/Login';
  }

  login(usuario: Usuario): Observable<any>{
    return this.http.post(this.myAppUrl + this.myApiUrl, usuario);
  }

  setLocalStorage(data: any): void{
    localStorage.setItem('token', data);
  }

  removeLocalStorage(): void{
    localStorage.clear();
  }

  getDecodeToken(): any{
    const helper = new JwtHelperService();
    //const token = JSON.parse();
    return helper.decodeToken(localStorage.getItem('token')!);
  }

  /* getNombreUsuario(): any{  
    return localStorage.getItem('token');
  } */
}
