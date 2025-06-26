import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http'; // Importa HttpClientModule

interface ComisionReport {
  vendedor: string;
  montoTotal: number;
  comision: number;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  standalone: true,
  imports: [
    FormsModule,
    CommonModule,
    HttpClientModule // ✅ Agregado aquí
  ]
})
export class AppComponent {
  fechaInicio: string = '';
  fechaFin: string = '';
  comisiones: ComisionReport[] = [];
  cargando: boolean = false;

  constructor(private http: HttpClient) {}

  onSubmit() {
    if (!this.fechaInicio || !this.fechaFin) {
      alert('Por favor selecciona ambas fechas.');
      return;
    }

    this.cargando = true;

    const url = `http://localhost:5140/Comision/filtrar?fechaInicio=${this.fechaInicio}&fechaFin=${this.fechaFin}`;

    this.http.get<ComisionReport[]>(url).subscribe({
      next: (data) => {
        console.log('Datos recibidos:', data); // Verifica en consola
        this.comisiones = data;
        this.cargando = false;
      },
      error: (err) => {
        console.error('Error al obtener datos:', err);
        alert('Error al obtener los datos');
        this.cargando = false;
      }
    });
  }
}