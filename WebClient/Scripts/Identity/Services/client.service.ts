import { Injectable } from '@angular/core';
import { Headers, Http }    from '@angular/http';

import 'rxjs/add/operator/toPromise';

import { ClientModel }       from '../Models/client.model';
import { Urls }             from '../../Common/Constants/urls';

import { Observable } from 'rxjs/Observable';
import '../../Common/rxjs-extensions';

@Injectable()
export class ClientService {

    constructor(private http: Http) {}

    public save(model: ClientModel) {
        return this.http.post(Urls.client, model)
            .map((res) => res.json()) // ...and calling .json() on the response to return data
            .catch((error:any) => Observable.throw(error.json() || 'Server error'));
    }
}