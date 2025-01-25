import { Routes } from '@angular/router';
import { LoginComponent } from './features/auth/login/login.component';
import { CarouselComponent } from './core/components/carousel/carousel.component';
import { SignupComponent } from './features/auth/signup/signup.component';
import { SearchComponent } from './features/components/search/search.component';
import { OrderComponent } from './features/components/order/order.component';
import { CheckoutComponent } from './features/components/checkout/checkout.component';
import { ReviewComponent} from './features/components/reviews/reviews.component';
import { AdminFunctionsComponent } from './features/admin/components/admin-functions/admin-functions.component';
import { AdminDashboardComponent } from './features/admin/admin.component';
import { AuthGuard } from './features/admin/components/authguard/authguard';
import { UsersComponent } from './features/admin/components/users/users.component';
import { PackagesComponent } from './features/admin/components/packages/packages.component';
import { WashersComponent } from './features/admin/components/washers/washers.component';
import { AdminLoginComponent } from './features/admin/components/admin-login/admin-login.component';

export const routes: Routes = [
    {path:'login', component: LoginComponent},
    {path: 'signup', component: SignupComponent },
    {path: '', component: CarouselComponent},
    {path:'search',component:SearchComponent},
    {path:'order',component:OrderComponent},
    {path:'checkout',component:CheckoutComponent},
    {path:'reviews',component:ReviewComponent},
      {path:'admin',component:AdminDashboardComponent},
      {path:'admin_login',component:AdminLoginComponent},
      { path: 'admin/users', component: UsersComponent },  
      { path: 'admin/packages', component: PackagesComponent },
      { path: 'admin/washers', component: WashersComponent },
      { path: 'admin/admin-functions', component: AdminFunctionsComponent },
]
