import { AfterViewInit, ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { PedidoService } from 'src/service/pedido.service';

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
    private changeDetectorService: ChangeDetectorRef) { }

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

        console.log(response)
      },
      error: (e) => {
        console.error(e);
      }
    });
  }

  createPedido(): void {
    console.log("createPedido")
  }

  updatePedido(pedido: any): void {
    console.log("updatePedido")
  }

  deletePedido(pedido: any): void {
    console.log("deletePedido")
  }

}
