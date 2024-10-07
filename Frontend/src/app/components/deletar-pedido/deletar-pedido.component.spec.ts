import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeletarPedidoComponent } from './deletar-pedido.component';

describe('DeletarPedidoComponent', () => {
  let component: DeletarPedidoComponent;
  let fixture: ComponentFixture<DeletarPedidoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeletarPedidoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeletarPedidoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
