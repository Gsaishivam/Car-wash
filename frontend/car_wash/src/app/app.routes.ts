import { Routes } from '@angular/router';
import { CategoryListComponent } from './features/category/category-list/category-list.component';
import { LoginComponent } from './features/auth/login/login.component';
import { CarouselComponent } from './core/components/carousel/carousel.component';
import { SignupComponent } from './features/auth/signup/signup.component';


export const routes: Routes = [
    {   path: 'admin/categories',component: CategoryListComponent},
    {   path:'login', component: LoginComponent},
    { path: 'signup', component: SignupComponent },
    { path: '', component: CarouselComponent}
]
