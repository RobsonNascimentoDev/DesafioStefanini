import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginatorModule  } from '@angular/material/paginator';
import { MatTableModule  } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ConsultarPedidosComponent } from './components/consultar-pedidos/consultar-pedidos.component';
import { DeletarPedidoComponent } from './components/deletar-pedido/deletar-pedido.component';
import { MatDialogModule } from '@angular/material/dialog';

@NgModule({
  declarations: [
    AppComponent,
    ConsultarPedidosComponent,
    DeletarPedidoComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    MatSortModule,
    MatPaginatorModule,
    MatTableModule,
    MatIconModule,
    MatDialogModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
