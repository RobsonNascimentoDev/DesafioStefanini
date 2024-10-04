import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PedidoService {
  
  private apiUrl = environment.apiUrl + '/Pedidos';

  constructor(private http: HttpClient) {}

  getPedidos(): Observable<any[]> {
    return this.http.get<any>(this.apiUrl);
  }

  createPedido(pedido: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, pedido, {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    });
  }

  getPedidoById(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }

  updatePedido(id: number, pedido: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${id}`, pedido, {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    });
  }

  deletePedido(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
