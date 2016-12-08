import { Injectable }       from '@angular/core';
import { DropdownQuestion } from '../Models/dropdownQuestion.model';
import { QuestionBase }     from '../Models/questionBase.model';
import { TextboxQuestion }  from '../Models/textboxQuestion.model';

import { Headers, Http }    from '@angular/http';
import { Urls }             from '../../Common/Constants/urls';

import 'rxjs/add/operator/toPromise';

@Injectable()
export class QuestionService {

  constructor(private http: Http) {}
  
  getQuestions() {
      return this.http.get(Urls.clientForm)
            .map((res) => res.json())
            .toPromise();
  }
}