import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';

import { ClientViewComponent } from './components/client-viewer/client-viewer.component';
import { RollupComponent } from "./components/rollup/rollup.component";
import { ErrorRollupComponent } from "./components/error-rollup/error-rollup.component";

@NgModule({
  declarations: [
    AppComponent,
    ClientViewComponent,
    RollupComponent,
    ErrorRollupComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    RouterModule.forRoot([
      {path: 'client-viewer', component: ClientViewComponent}
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
