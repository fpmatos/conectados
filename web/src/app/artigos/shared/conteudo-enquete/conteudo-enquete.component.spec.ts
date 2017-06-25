import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConteudoEnqueteComponent } from './conteudo-enquete.component';

describe('ConteudoEnqueteComponent', () => {
  let component: ConteudoEnqueteComponent;
  let fixture: ComponentFixture<ConteudoEnqueteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConteudoEnqueteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConteudoEnqueteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
