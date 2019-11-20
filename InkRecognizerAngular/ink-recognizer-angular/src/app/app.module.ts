import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { SignaturePadModule } from 'angular2-signaturepad';

import { AppComponent } from './app.component';
import { SignatureFieldComponent } from './signature-field/signature-field.component';

@NgModule({
  declarations: [
    AppComponent,
    SignatureFieldComponent
  ],
  imports: [
    BrowserModule, SignaturePadModule, HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
