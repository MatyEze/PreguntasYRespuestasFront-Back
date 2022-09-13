import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Pregunta } from 'src/app/models/pregunta';
import { Respuesta } from 'src/app/models/respuesta';
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
  @Output() enviarPregunta = new EventEmitter<Pregunta>();

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

  agregarPregunta(): void{
    //se obtiene el titulo pregunta
    const descripcionPregunta = this.nuevaPregunta.get('titulo')?.value;
    //se obtiene el array de respuestas
    const arrayRespuestas = this.nuevaPregunta.get('respuestas')?.value;
    //creamos array de respuestas
    const arrayRta: Respuesta[] = [];
                          //        aca le digo que descripcion es string y index es number (infer)
    arrayRespuestas.forEach((element: { descripcion: string; }, index: number) => {
      
      const respuesta: Respuesta = new Respuesta(element.descripcion, false);
      if (this.rtaCorrecta === index) { 
        respuesta.esCorrecta = true;
      }
      arrayRta.push(respuesta);
    });

    const pregunta: Pregunta = new Pregunta(descripcionPregunta, arrayRta);

    console.log(pregunta);
    this.enviarPregunta.emit(pregunta);
    this.reset();
  }

  reset(): void{
    this.nuevaPregunta.reset();
    this.getRespuestas.clear();
    this.preguntasPorDefecto();
  }
}
