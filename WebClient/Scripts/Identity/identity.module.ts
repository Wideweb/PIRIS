import { NgModule }         from '@angular/core';
import { BrowserModule }    from '@angular/platform-browser';
import { HttpModule }       from '@angular/http';
import { FormsModule, ReactiveFormsModule }      from '@angular/forms';
import { CommonModule }      from '@angular/common';

import { LoginComponent }   from './Components/login.component';
import { SignUpComponent }  from './Components/signUp.component';
import { ClientComponent }  from './Components/client.component';
import { DynamicFormComponent }  from './Components/dynamicForm.component';
import { DynamicFormQuestionComponent }  from './Components/dynamicFormQuestion.component';

import { AuthService }      from './Services/auth.service';
import { ClientService }      from './Services/client.service';
import { QuestionService }      from './Services/question.service';
import { QuestionControlService }      from './Services/questionControl.service';

import { routing }          from './identity.routing';

import { PirisCommonModule } from '../Common/common.module'

@NgModule({
    imports:      [BrowserModule, HttpModule, FormsModule, routing, PirisCommonModule, CommonModule, ReactiveFormsModule],
    declarations: [LoginComponent, SignUpComponent, ClientComponent, DynamicFormComponent, DynamicFormQuestionComponent],
    providers:    [AuthService, ClientService, QuestionControlService, QuestionService]
})
export class IdentityModule { }