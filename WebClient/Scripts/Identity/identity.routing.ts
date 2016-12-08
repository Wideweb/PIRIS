import { Routes, RouterModule } from '@angular/router';

import { LoginComponent }   from './Components/login.component';
import { SignUpComponent }   from './Components/signUp.component';
import { ClientComponent }   from './Components/client.component';

import { AppStates } from '../Common/Constants/appStates';

const appRoutes: Routes = [
    {
        path: AppStates.login,
        component: LoginComponent
    },
    {
        path: AppStates.signUp,
        component: SignUpComponent
    },
    {
        path: AppStates.client,
        component: ClientComponent
    }
];

export const routing = RouterModule.forChild(appRoutes);