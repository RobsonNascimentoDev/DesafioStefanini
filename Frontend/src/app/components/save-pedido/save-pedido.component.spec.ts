import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SavePedidoComponent } from './save-pedido.component';

describe('SavePedidoComponent', () => {
  let component: SavePedidoComponent;
  let fixture: ComponentFixture<SavePedidoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SavePedidoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SavePedidoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
