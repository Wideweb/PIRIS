import { Component } from '@angular/core';
import { ClientService } from '../Services/client.service';
import { ClientModel } from '../Models/client.model';
import { Urls }             from '../../Common/Constants/urls';
import { QuestionService } from '../Services/question.service';

@Component({
    selector: 'login',
    templateUrl: 'app/Identity/Components/client.component.html',
    styleUrls: ['app/Identity/Components/client.component.css']
})
export class ClientComponent { 
    
    model: ClientModel;
    questions: any[];
    serverErrors: any;

    constructor(private clientService: ClientService, service: QuestionService){
        this.model = new ClientModel();
        service.getQuestions().then(it => {
            this.questions = it;
            console.log(it);
        });
    }

    onSubmit(clientForm){
        this.serverErrors = [];

        if (!clientForm.valid) {
            return
        }

        this.clientService.save(this.model).subscribe(
            response => {
                // Emit list event
                console.log(response)
            }, 
            err => this.serverErrors = Object.keys(err).map((key)=>{ return err[key]}).reduce((p, c) => p.concat(c)));
    }
}