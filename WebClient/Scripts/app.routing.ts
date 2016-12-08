import { Routes, RouterModule }  from '@angular/router';
import { AppStates } from './Common/Constants/appStates';

const appRoutes: Routes = [
    {
        path: '',
        redirectTo: AppStates.notFound,
        pathMatch: 'full'
    }
];

export const appRoutingProviders: any[] = [

];

export const routing = RouterModule.forRoot(appRoutes);