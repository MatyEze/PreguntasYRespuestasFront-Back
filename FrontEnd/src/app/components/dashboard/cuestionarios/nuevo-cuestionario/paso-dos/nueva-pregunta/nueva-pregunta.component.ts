import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Pregunta } from 'src/app/models/pregunta';
import { CuestionarioService } from 'src/app/services/cuestionario.service';

@Component({
  selector: 'app-nueva-pregunta',
  templateUrl: './nueva-pregunta.component.html',
  styleUrls: ['./nueva-pregunta.component.css']
})
export class NuevaPreguntaComponent implements OnInit {
  nuevaPregunta: FormGroup;
  pregunta?: Pregunta;
  rtaCorrecta = 0;

  constructor(private fb: FormBuilder,
              private router: Router,
              private toastr: ToastrService,
              private cuestionarioService: CuestionarioService) { 
    this.nuevaPregunta = this.fb.group({
      titulo: ['', Validators.required],
      respuestas: this.fb.array([])
    })
            }

  ngOnInit(): void {
    this.preguntasPorDefecto();
  }

  get getRespuestas(): FormArray{
    const formArray = this.nuevaPregunta.get('respuestas') as FormArray
    return formArray;
  }

  agregarRespuesta(): void{
    this.getRespuestas.push(this.fb.group({
      descripcion: ['', Validators.required],
      esCorrecta: 0
    }));
  }

  eliminarRespuesta(index: number): void{
    if (this.getRespuestas.length === 2) {
      this.toastr.error("La pregunta debe tener minimo dos respuestas", "No se puede eliminar!");
    } else {
      this.getRespuestas.removeAt(index);
    }
  }

  preguntasPorDefecto(): void{
    this.agregarRespuesta();
    this.agregarRespuesta();
  }

  setRespuestaValida(index: number): void{
    this.rtaCorrecta = index;
    console.log(this.rtaCorrecta);
  }
}
