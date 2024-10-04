import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ConsultarPedidosComponent } from './components/consultar-pedidos/consultar-pedidos.component';

const routes: Routes = [
  { path: 'consultar-pedido', component: ConsultarPedidosComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
