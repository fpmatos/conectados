import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConteudoImagemComponent } from './conteudo-imagem.component';

describe('ConteudoImagemComponent', () => {
  let component: ConteudoImagemComponent;
  let fixture: ComponentFixture<ConteudoImagemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConteudoImagemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConteudoImagemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
