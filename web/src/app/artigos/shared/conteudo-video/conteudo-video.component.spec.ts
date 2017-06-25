import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConteudoVideoComponent } from './conteudo-video.component';

describe('ConteudoVideoComponent', () => {
  let component: ConteudoVideoComponent;
  let fixture: ComponentFixture<ConteudoVideoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConteudoVideoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConteudoVideoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
