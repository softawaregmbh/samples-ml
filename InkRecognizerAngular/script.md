# Video Script

* Neues Service (Preview): Ink Recognizer
* Sample auf Microsoft-Demo-Seite zeigen

* OCR vs. Ink Recognition
* Möglichkeit in UWP, Ink Recognition am Client durchzuführen (InkCanvas)
* Neu: Als Service, das per REST konsumiert werden kann

## Angular App
* Ausgangsbasis: App mit Signature Pad + Analyze-Button
* Hinweis auf Signature Pad / GitHub und angular2-signaturepad

```ts
public strokes: Point[][];

public ngAfterViewInit(): void {
    this.strokes = this.signaturePad.toData();
}
```

## Cognitive Services
* Key + Endpoint im Portal zeigen

```ts
const endpoint = "https://inkrecognizer-demo.cognitiveservices.azure.com";
const subscriptionKey = "766a8a28ed424794ab59f638f9dc82ce";
const url = `${endpoint}/inkrecognizer/v1.0-preview/recognize`;
```

* Recognize-Methode in API zeigen (PUT)

```ts
this.httpClient.put(
    url,
    //content,
    { headers: new HttpHeaders({ 'Ocp-Apim-Subscription-Key': subscriptionKey }) })
```

* JSON-Content bauen
```ts
var content = { 
      "language": "de-DE",
      "strokes": this.strokes.map((points, index) => {
          return {
            "id": index,
            "points": points
          }
        })
    };
```

* RecognitionResult-Typings zeigen
```ts
this.httpClient.put<RecognitionResult>(...)
        .toPromise()
        .then(v => {
            this.recognitionResult = v;
            console.log(this.recognitionResult.recognitionUnits.length);
        })
        .catch(reason => console.error(reason));


private _recognitionResult : RecognitionResult;

public get recognitionResult() : RecognitionResult {
  return this._recognitionResult;
}
public set recognitionResult(v : RecognitionResult) {
  this._recognitionResult = v;
}

```

## Bereiche einzeichnen
* Filtern auf inkWord und inkDrawing

```ts
public recognizedItems : RecognitionUnit[]; 

// public set recognitionResult(v : RecognitionResult) {
this.recognizedItems = this._recognitionResult.recognitionUnits.filter(v => v.category == "inkWord" || v.category == "inkDrawing");
```

* Anzeige erweitern
* signature-field.component.scss zeigen (position)

```html
<div *ngFor="let item of recognizedItems" 
        class="recognized-area" 
        [style.left.px]="item.boundingRectangle.topX" 
        [style.top.px]="item.boundingRectangle.topY"
        [style.width.px]="item.boundingRectangle.width"
        [style.height.px]="item.boundingRectangle.height">
    <span class="recognized-description">
        {{ item.recognizedText || item.recognizedObject }}
    </span>
</div>
```

* Hinweis auf SourceCode