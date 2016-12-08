import { Component, Input, OnInit, OnChanges, SimpleChanges, SimpleChange }  from '@angular/core';
import { FormGroup }                 from '@angular/forms';
import { QuestionBase }              from '../Models/questionBase.model';
import { QuestionControlService }    from '../Services/questionControl.service';

@Component({
    selector: 'dynamic-form',
    templateUrl: 'app/Identity/Components/dynamicForm.component.html',
    providers: [ QuestionControlService ]
})
export class DynamicFormComponent implements OnChanges {
    @Input() questions: QuestionBase<any>[] = [];
    form: FormGroup;
    payLoad = '';
    submitted: boolean = false;
    
    constructor(private qcs: QuestionControlService) {  }

    ngOnChanges(changes: SimpleChanges){
        if(changes['questions'] != null && changes['questions'].currentValue != null){
            this.form = this.qcs.toFormGroup(this.questions);
        }
    }
    
    onSubmit() {
        this.submitted = true;
        this.payLoad = JSON.stringify(this.form.value);
    }
}