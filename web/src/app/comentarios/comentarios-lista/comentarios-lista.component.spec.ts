import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ComentariosListaComponent } from './comentarios-lista.component';

describe('ComentariosListaComponent', () => {
  let component: ComentariosListaComponent;
  let fixture: ComponentFixture<ComentariosListaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ComentariosListaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ComentariosListaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
