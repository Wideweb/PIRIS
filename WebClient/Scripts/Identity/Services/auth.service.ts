import { Injectable } from '@angular/core';
import { Headers, Http }    from '@angular/http';

import 'rxjs/add/operator/toPromise';

import { LoginModel }       from '../Models/login.model';
import { SignUpModel }      from '../Models/signUp.model';
import { UserModel }        from '../Models/user.model';
import { Urls }             from '../../Common/Constants/urls';

@Injectable()
export class AuthService {

    private userInfo: UserModel;

    constructor(private http: Http) {}

    public login(model: LoginModel): Promise<UserModel> {
        return this.http.post(Urls.login, model)
            .toPromise()
            .then(this.onUserInfoReceived);
    }

    public signUp(model: SignUpModel): Promise<UserModel> {
        return this.http.post(Urls.signUp, model)
            .toPromise()
            .then(this.onUserInfoReceived);
    }

    public logOff() {
        return this.http.get(Urls.logOff)
            .toPromise()
            .then(response => this.updateUserInfo(null));
    }

    public getUserInfo() {
        return this.userInfo;
    }

    private onUserInfoReceived(response) {
        var user = response.json().data as UserModel;
        this.updateUserInfo(user);
        return user;
    }

    private updateUserInfo(data: UserModel) {
        this.userInfo = data;
    }
}