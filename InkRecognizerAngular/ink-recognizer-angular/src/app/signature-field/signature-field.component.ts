import { Component, OnInit, ViewChild } from '@angular/core';
import { SignaturePad, Point } from 'angular2-signaturepad/signature-pad'

@Component({
  selector: 'app-signature-field',
  templateUrl: './signature-field.component.html',
  styleUrls: ['./signature-field.component.scss']
})
export class SignatureFieldComponent implements OnInit {
  @ViewChild(SignaturePad, { static: false }) signaturePad: SignaturePad;

  public signaturePadOptions: Object = { // passed through to szimek/signature_pad constructor
    'minWidth': 3,
    'canvasWidth': 800,
    'canvasHeight': 400
  };

  public points: Point[][];

  constructor() { }

  public analyze(): void {
    
  }

  public ngOnInit(): void {
  }

  public ngAfterViewInit(): void {
    this.points = this.signaturePad.toData();
  }

}
