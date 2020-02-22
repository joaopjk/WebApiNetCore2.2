import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  // tslint:disable-next-line: component-selector
  selector:  'app-Eventos',
  templateUrl: './Eventos.component.html',
  styleUrls: ['./Eventos.component.css']
})
export class EventosComponent implements OnInit {
  eventos: any = [] ;
  imagemLargura = 50;
  imagemMargen = 2;
  mostrarImagem = false;
  constructor(private http: HttpClient ) { }

  ngOnInit() {
    this.getEventos();
  }
  alternarImagem(){
    this.mostrarImagem = !this.mostrarImagem;
  }
  getEventos() {
     this.http.get('http://localhost:5000/api/values').subscribe(
      response => {this.eventos = response; }, error  =>  {
        console.log(error);
      });
  }
}
