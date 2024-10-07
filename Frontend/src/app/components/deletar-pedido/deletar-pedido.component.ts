import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-deletar-pedido',
  templateUrl: './deletar-pedido.component.html',
  styleUrls: ['./deletar-pedido.component.scss']
})
export class DeletarPedidoComponent implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<DeletarPedidoComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { }

  ngOnInit(): void {
  }

  confirmDelete(): void {
    this.dialogRef.close(true);
  }

  cancel(): void {
    this.dialogRef.close(false);
  }

}
