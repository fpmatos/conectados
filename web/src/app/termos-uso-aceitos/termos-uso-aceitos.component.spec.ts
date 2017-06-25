import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TermosUsoAceitosComponent } from './termos-uso-aceitos.component';

describe('TermosUsoAceitosComponent', () => {
  let component: TermosUsoAceitosComponent;
  let fixture: ComponentFixture<TermosUsoAceitosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TermosUsoAceitosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TermosUsoAceitosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
