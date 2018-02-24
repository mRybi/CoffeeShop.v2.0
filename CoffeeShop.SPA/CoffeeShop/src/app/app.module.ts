import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { Routes, RouterModule} from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


import { AppComponent } from './app.component';
import { CoffeeService } from './_services/coffee.service';
import { HttpClientModule } from '@angular/common/http';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { MenuComponent } from './menu/menu.component';
import { CartComponent } from './cart/cart.component';
import { AlertifyService } from './_services/alertify.service';
import { routes } from './route.routing';
import { MenuDetailComponent } from './menu-detail/menu-detail.component';
import { CoffeeCardComponent } from './coffee-card/coffee-card.component';
import { CartService } from './_services/cart.service';
import { CartResolver } from './_resolvers/cart.resolver';
import { MenuResolver } from './_resolvers/menu.resolver';
import { OrderCartComponent } from './order-cart/order-cart.component';
import { OrderService } from './_services/order.service';
import { RegisterComponent } from './register/register.component';
import { AuthService } from './_services/auth.service';
import { JwtModule } from '@auth0/angular-jwt';
import { AuthGuard } from './_guard/AuthGuard';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from './_services/token.interceptor';

export function getAccessToken(): string {
  return localStorage.getItem('token');
}

export const jwtConfig = {
  tokenGetter: getAccessToken,
  whitelistedDomains: ['localhost:57066']
};


@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    MenuComponent,
    CartComponent,
    MenuDetailComponent,
    CoffeeCardComponent,
    OrderCartComponent,
    RegisterComponent
],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    HttpModule,
    RouterModule.forRoot(routes),
    JwtModule.forRoot({
      config: jwtConfig
    })
  ],
  providers: [
    CoffeeService,
    AlertifyService,
    CartService,
    CartResolver,
    MenuResolver,
    OrderService,
    AuthService,
    AuthGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
