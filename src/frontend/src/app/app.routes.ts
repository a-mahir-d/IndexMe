import { Routes } from '@angular/router';
import { Home } from './shared/components/home/home';
import { Register } from './features/auth/register/register';
import { Login } from './features/auth/login/login';
import { Features } from './shared/components/features/features';
import { Dashboard } from './features/users/dashboard/dashboard';

export const routes: Routes = [
    { path: '', component: Home },
    { path: 'features', component: Features },
    { path: 'register', component: Register },
    { path: 'login', component: Login },
    { path: 'dashboard', component: Dashboard },
];
