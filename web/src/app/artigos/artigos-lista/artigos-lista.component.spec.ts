import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ArtigosListaComponent } from './artigos-lista.component';

describe('ArtigosListaComponent', () => {
  let component: ArtigosListaComponent;
  let fixture: ComponentFixture<ArtigosListaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArtigosListaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArtigosListaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
