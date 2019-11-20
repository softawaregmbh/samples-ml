import { Component, OnInit, ViewChild } from '@angular/core';
import { SignaturePad, Point } from 'angular2-signaturepad/signature-pad'
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { RecognitionResult } from '../types/recognition-result';

@Component({
  selector: 'app-signature-field',
  templateUrl: './signature-field.component.html',
  styleUrls: ['./signature-field.component.scss']
})
export class SignatureFieldComponent implements OnInit {
  @ViewChild(SignaturePad, { static: false }) signaturePad: SignaturePad;

  public signaturePadOptions: Object = {
    'minWidth': 3,
    'canvasWidth': 800,
    'canvasHeight': 400
  };

  public strokes: Point[][];

  public recognitionResult: RecognitionResult;

  get recognizedWords() {
    return this.recognitionResult.recognitionUnits.filter(v => v.category == "inkWord");
  }

  get recognizedDrawings() {
    return this.recognitionResult.recognitionUnits.filter(v => v.category == "inkDrawing");
  }

  constructor(private httpClient: HttpClient) { }

  public analyze(): void {
    var endpoint = "https://inkrecognizer-demo.cognitiveservices.azure.com";
    var subscriptionKey = "766a8a28ed424794ab59f638f9dc82ce";

    var url = `${endpoint}/inkrecognizer/v1.0-preview/recognize`;

    var content = { 
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
        .then(v => this.recognitionResult = v);    
  }

  public ngOnInit(): void {
  }

  public ngAfterViewInit(): void {
    this.strokes = this.signaturePad.toData();
  }

}
