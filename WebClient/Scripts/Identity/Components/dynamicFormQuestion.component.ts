import { Component, Input } from '@angular/core';
import { FormGroup }        from '@angular/forms';
import { QuestionBase }     from '../Models/questionBase.model';
import {ViewEncapsulation} from '@angular/core';

@Component({
    selector: 'df-question',
    templateUrl: 'app/Identity/Components/dynamicFormQuestion.component.html',
    styleUrls: ['app/Identity/Components/dynamicFormQuestion.component.css'],
    encapsulation: ViewEncapsulation.None
})
export class DynamicFormQuestionComponent {
    @Input() question: QuestionBase<any>;
    @Input() form: FormGroup;
    
    get isValid() { return this.form.controls[this.question.key].valid; }
}