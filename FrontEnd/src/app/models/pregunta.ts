import { Respuesta } from "./respuesta";

export class Pregunta{
    descripcion: string;
    listaRespuestas: Respuesta[];
    hide?: boolean;

    constructor(descripcion: string, listaRespuestas: Respuesta[]){
        this.descripcion = descripcion;
        this.listaRespuestas = listaRespuestas;
    }
}