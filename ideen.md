## Video-Ideen


## 
* int statt float?

## DataView
* .Preview()-Methode

## Model in Azure speichern

## OpenML

## Trainingsdaten aus einer Datei laden

## RegressionSimple1

* .NET Core
* Microsoft.ML NuGet
* OpenML: wine_review (https://www.openml.org/d/42074)
* ArffTools NuGet
* WineReview-Klasse und Extension Method für Mapping

### Pipeline
* Exception: Column 'Price' has values of Double -> Features nimmt den Datentyp der ersten Spalte (String)
    * Strings zu Zahlen normalisieren

```cs
var predictions = model.Transform(trainingData);
preview = predictions.Preview(50);
```
Preview hat nun eine Score-Spalte


* SDCA: abgeleitet von StocasticTrainerBase
    * IEstimator hat Fit-Methode


## Regression
## Schritte
* Nur Sqft Living/Lot -> Plot
* Year Built hinzugefügt, zunächst Mean, dann Binning (besser!), dann Categorize
* Poison-Trainer

## Was hat funktioniert..
* NormalizeBinning für Sqft/Lot


## Was hat nicht funktioniert...
* NormalizeBinningSupervised für SqftLiving/Lot

0,56
244839,92

0,89
121155,71