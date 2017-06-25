import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ArtigosDetalheComponent } from './artigos-detalhe.component';

describe('ArtigosDetalheComponent', () => {
  let component: ArtigosDetalheComponent;
  let fixture: ComponentFixture<ArtigosDetalheComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArtigosDetalheComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArtigosDetalheComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
