import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MenuComponent } from './menu/menu.component';
import { CartComponent } from './cart/cart.component';
import { MenuDetailComponent } from './menu-detail/menu-detail.component';
import { CartResolver } from './_resolvers/cart.resolver';
import { MenuResolver } from './_resolvers/menu.resolver';
import { OrderCartComponent } from './order-cart/order-cart.component';
import { RegisterComponent } from './register/register.component';
import { AuthGuard } from './_guard/AuthGuard';


export const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'menu', component: MenuComponent, resolve: {coffees: MenuResolver} },
  { path: 'menu/:id', component: MenuDetailComponent },
  { path: 'register', component: RegisterComponent},
  { path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
  { path: 'cart', component: CartComponent, resolve: {coffeesInCart: CartResolver} },
  { path: 'order', component: OrderCartComponent},
]},

  { path: '**', redirectTo: 'home', pathMatch: 'full'}
];

