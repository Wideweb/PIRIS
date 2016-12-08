import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule }   from '@angular/router';
import { FormsModule } from '@angular/forms';

import { IdentityModule } from './Identity/identity.module';
import { PirisCommonModule } from './Common/common.module';

import { AppComponent }  from './app.component';
import { routing }  from './app.routing';

@NgModule({
    imports: [BrowserModule, IdentityModule, PirisCommonModule, RouterModule, routing, FormsModule],
    declarations: [AppComponent],
    bootstrap: [AppComponent]
})
export class AppModule { }