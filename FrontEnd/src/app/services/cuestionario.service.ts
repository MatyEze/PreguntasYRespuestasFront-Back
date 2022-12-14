import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Cuetionario } from '../models/cuestionario';

@Injectable({
  providedIn: 'root'
})
export class CuestionarioService {
  tituloCuestionario?: string;
  descripcionCuestionario? :string;
  myAppUrl: string;
  myApiUrl: string;

  constructor(private http: HttpClient) { 
    this.myAppUrl = environment.endpoint;
    this.myApiUrl = '/api/Cuestionario';
  }

  guardarCuestionario(cuestionario: Cuetionario): Observable<any>{
    return this.http.post(this.myAppUrl + this.myApiUrl, cuestionario);
  }
}
