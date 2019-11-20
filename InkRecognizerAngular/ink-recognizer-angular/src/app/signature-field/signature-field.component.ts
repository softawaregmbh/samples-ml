import { Component, ViewChild } from '@angular/core';
import { SignaturePad, Point } from 'angular2-signaturepad/signature-pad'
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { RecognitionResult } from '../types/recognition-result';
import { RecognitionUnit } from '../types/recognition-unit';

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
  
  public strokes: Point[][];

  private _recognitionResult : RecognitionResult;

  public get recognitionResult() : RecognitionResult {
    return this._recognitionResult;
  }
  public set recognitionResult(v : RecognitionResult) {
    this._recognitionResult = v;
    this.recognizedItems = this._recognitionResult.recognitionUnits.filter(v => v.category == "inkWord" || v.category == "inkDrawing");
  }

  public recognizedItems : RecognitionUnit[]; 

  constructor(private httpClient: HttpClient) { }

  public analyze(): void {
    const endpoint = "https://inkrecognizer-demo.cognitiveservices.azure.com";
    const subscriptionKey = "766a8a28ed424794ab59f638f9dc82ce";

    const url = `${endpoint}/inkrecognizer/v1.0-preview/recognize`;
    
    const content = { 
      "language": "de-DE",
      "strokes": this.strokes.map((points, index) => {
          return {
            "id": index,
            "points": points
          }
        })
    };

    this.httpClient.put<RecognitionResult>(
      url,
      content,
      { headers: new HttpHeaders({ 'Ocp-Apim-Subscription-Key': subscriptionKey }) })
        .toPromise()
        .then(v => this.recognitionResult = v)
        .catch(reason => console.error(reason));    
  }

  public ngAfterViewInit(): void {
    this.strokes = this.signaturePad.toData();
  }
}
