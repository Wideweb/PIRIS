import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { ForbiddenComponent } from './Components/forbidden.component';
import { NotFoundComponent } from './Components/notFound.component';
import { ServerErrorComponent } from './Components/serverError.component';
import { UnauthorizedComponent } from './Components/unauthorized.component';

import { PirisSelectComponent } from './ComponentsForm/pirisSelect.component';

import { routing } from './common.routing';

@NgModule({
    imports: [BrowserModule, routing, FormsModule, HttpModule],
    declarations: [ForbiddenComponent, NotFoundComponent, ServerErrorComponent, UnauthorizedComponent, PirisSelectComponent],
    exports: [PirisSelectComponent]
})
export class PirisCommonModule { }