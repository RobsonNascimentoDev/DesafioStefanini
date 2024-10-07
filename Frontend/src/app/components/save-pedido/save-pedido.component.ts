import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup, FormArray, Validators } from '@angular/forms';
import { PedidoService } from 'src/service/pedido.service';

@Component({
  selector: 'app-save-pedido',
  templateUrl: './save-pedido.component.html',
  styleUrls: ['./save-pedido.component.scss']
})
export class SavePedidoComponent implements OnInit {

  pedidoForm!: FormGroup;

  constructor(
    private pedidoService: PedidoService,
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<SavePedidoComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { }

  ngOnInit(): void {
    this.initializeForm();

    if (this.data.idPedido) {
      this.getPedidoId();
    }
  }

  initializeForm(): void {
    this.pedidoForm = this.fb.group({
      id: [0, Validators.required],
      nomeCliente: ['', Validators.required],
      emailCliente: ['', [Validators.required, Validators.email]],
      dataCriacao: [new Date(), Validators.required],
      pago: [false],
      itens: this.fb.array([this.createItem()])
    });
  }

  createItem(): FormGroup {
    return this.fb.group({
      id: [0],
      idPedido: [0],
      idProduto: [0],
      quantidade: [0, Validators.required],
      produto: this.fb.group({
        id: [0, Validators.required],
        nomeProduto: ['', Validators.required],
        valor: [0, Validators.required]
      })
    });
  }

  get itens(): FormArray {
    return this.pedidoForm.get('itens') as FormArray;
  }

  addItem(): void {
    this.itens.push(this.createItem());
  }

  removeItem(index: number): void {
    this.itens.removeAt(index);
  }

  getPedidoId(): void {
    this.pedidoService.getPedidoById(this.data.idPedido).subscribe({
      next: (response) => {
        this.pedidoForm.patchValue(response);

        this.pedidoForm.setControl('itens', this.fb.array(
          response.itens.map((item: any) => this.fb.group({
            id: item.id,
            idPedido: item.idPedido,
            idProduto: item.idProduto,
            quantidade: [item.quantidade],
            produto: this.fb.group({
              id: [item.produto.id],
              nomeProduto: [item.produto.nomeProduto],
              valor: [item.produto.valor]
            })
          }))
        ));
      },
      error: (e) => {
        console.error(e);
      }
    });
  }

  savePedido(): void {
    this.data.idPedido ? this.updatePedido() : this.createPedido();
  }

  createPedido(): void {
    const pedido = this.pedidoForm.value;
    this.pedidoService.createPedido(pedido)
      .subscribe({
        next: (response) => {
          this.dialogRef.close(true);
        },
        error: (e) => {
          console.error(e);
        }
      });
  }

  updatePedido(): void {
    const pedido = this.pedidoForm.value;
    this.pedidoService.updatePedido(this.data.idPedido, pedido)
      .subscribe({
        next: (response) => {
          this.dialogRef.close(true);
        },
        error: (e) => {
          console.error(e);
        }
      });
  }
}
