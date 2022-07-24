import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CarPoolsComponent } from './car-pools/car-pools.component';
import { LoginComponent } from './login/login.component';
import { MyCarPoolsComponent } from './my-car-pools/my-car-pools.component';
import { RegisterComponent } from './register/register.component';

const routes: Routes = [
  {path: '', redirectTo:'login',pathMatch:'full'},
  {path: "register", component: RegisterComponent},
  {path: "login", component: LoginComponent},
  {path: "carpools", component: CarPoolsComponent},
  {path: "mycarpools", component: MyCarPoolsComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
