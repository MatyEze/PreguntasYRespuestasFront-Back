import { Pregunta } from "./pregunta";

export class Cuetionario{
    id?: number;
    nombre: string;
    descripcion: string;
    fechaCreacion: Date;
    listaPreguntas: Pregunta[];

    constructor(nombre: string, descripcion: string, fechaCreacion: Date, listaPreguntas: Pregunta[]){
        this.nombre = nombre;
        this.descripcion = descripcion;
        this.fechaCreacion = fechaCreacion;
        this.listaPreguntas = listaPreguntas;
    }
}