import { Routes, RouterModule } from '@angular/router';

import { ForbiddenComponent }   from './Components/forbidden.component';
import { NotFoundComponent }   from './Components/notFound.component';
import { ServerErrorComponent }   from './Components/serverError.component';
import { UnauthorizedComponent }   from './Components/unauthorized.component';

import { AppStates } from './Constants/appStates';

const appRoutes: Routes = [
    {
        path: AppStates.forbidden,
        component: ForbiddenComponent
    },
    {
        path: AppStates.notFound,
        component: NotFoundComponent
    },
    {
        path: AppStates.serverError,
        component: ServerErrorComponent
    },
    {
        path: AppStates.unauthorized,
        component: UnauthorizedComponent
    }
];

export const routing = RouterModule.forChild(appRoutes);