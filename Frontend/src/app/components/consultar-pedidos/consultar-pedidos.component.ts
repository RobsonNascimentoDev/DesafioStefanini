import { AfterViewInit, ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { PedidoService } from 'src/service/pedido.service';
import { DeletarPedidoComponent } from '../deletar-pedido/deletar-pedido.component';
import { SavePedidoComponent } from '../save-pedido/save-pedido.component';

@Component({
  selector: 'app-consultar-pedidos',
  templateUrl: './consultar-pedidos.component.html',
  styleUrls: ['./consultar-pedidos.component.scss']
})
export class ConsultarPedidosComponent implements OnInit, AfterViewInit {

  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;

  public displayedColumns: string[] = [
    'nomeCliente',
    'emailCliente',
    'pago',
    'itens',
    'valorTotal',
    'actions'
  ];

  public dataSource = new MatTableDataSource<any>();

  constructor(
    private pedidoService: PedidoService,
    private changeDetectorService: ChangeDetectorRef,
    private dialog: MatDialog) { }

  ngOnInit(): void {
    this.getAllPedidos();
  }

  ngAfterViewInit(): void {
    this.configurePaginator();
    this.changeDetectorService.detectChanges();
  }

  private configurePaginator(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  private getAllPedidos(): void {
    this.pedidoService.getPedidos().subscribe({
      next: (response) => {
        this.configurePaginator();
        this.dataSource.data = response;
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        this.changeDetectorService.detectChanges();
      },
      error: (e) => {
        console.error(e);
      }
    });
  }

  openModalSavePedido(idPedido: any): void {
    const dialogRef = this.dialog.open(SavePedidoComponent, {
      width: '35%',
      data: { idPedido: idPedido }
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.getAllPedidos();
      }
    });
  }

  openModalDeletarPedido(element: any): void {
    const dialogRef = this.dialog.open(DeletarPedidoComponent, {
      width: '30%',
      data: { nomeCliente: element.nomeCliente }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.deletarPedido(element.id);
      }
    });
  }

  deletarPedido(id: number): void {
    this.pedidoService.deletePedido(id).subscribe({
      next: (response) => {
        this.getAllPedidos();
      },
      error: (e) => {
        console.error(e);
      }
    });
  }

}
