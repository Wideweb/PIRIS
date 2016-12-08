import { Component } from '@angular/core';

import { AppStates } from './Common/Constants/AppStates';

@Component({
    selector: 'my-app',
    templateUrl: 'app/app.component.html',
    styleUrls: ['app/app.component.css']
})
export class AppComponent {

    appStates: any;

    constructor() {
        this.appStates = AppStates;
    }
}