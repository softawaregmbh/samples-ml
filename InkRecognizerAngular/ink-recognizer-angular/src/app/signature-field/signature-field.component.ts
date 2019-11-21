import { Component, ViewChild } from '@angular/core';
import { SignaturePad, Point } from 'angular2-signaturepad/signature-pad'
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-signature-field',
  templateUrl: './signature-field.component.html',
  styleUrls: ['./signature-field.component.scss']
})
export class SignatureFieldComponent {
  @ViewChild(SignaturePad, { static: false }) signaturePad: SignaturePad;

  public signaturePadOptions = {
    'minWidth': 3,
    'canvasWidth': 800,
    'canvasHeight': 400
  };
  
  constructor(private httpClient: HttpClient) { }

  public analyze(): void {
  }

  public ngAfterViewInit(): void {
  }
}
