//Angular
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, LOCALE_ID } from '@angular/core';
import { FormsModule, ReactiveFormsModule  } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS  } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { CommonModule, registerLocaleData } from '@angular/common';
import localeRu from '@angular/common/locales/ru';

import { NgxSpinnerModule } from 'ngx-spinner';

//routes

//Kendo UI
import { MenusModule } from "@progress/kendo-angular-menu";
import { InputsModule } from '@progress/kendo-angular-inputs';
import { LabelModule } from '@progress/kendo-angular-label';
import { DatePickerModule } from '@progress/kendo-angular-dateinputs';
import { DateInputsModule  } from '@progress/kendo-angular-dateinputs';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { GridModule } from '@progress/kendo-angular-grid';
import { IntlModule } from "@progress/kendo-angular-intl";
import { WindowModule } from "@progress/kendo-angular-dialog";
import "@progress/kendo-angular-intl/locales/ru/all";

//Models
import { Role } from './models/role.model';

//Guards
import { DetailsGuard } from './guards/TestAtlant.guard';
import { AuthGuard } from './guards/auth.guard';

//Interceptors
import { JsonDateInterceptor } from './interceptors/api.interceptor';
import { JwtInterceptor } from './interceptors/jwt.interceptor';
import { ErrorInterceptor } from './interceptors/error.interceptor';

//Services
import { DetailService } from './services/detail.service';
import { CatalogService } from './services/catalog.service';
import { fakeBackendProvider } from './services/fake-backend';

//Components
import { AppComponent } from './app.component';
import { AddEditDetailComponent } from './components/details/add-edit-detail/add-edit-detail.component';
import { DetailComponent } from './components/details/detail.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';

//Catalogs
import { UsersComponent } from './components/users/users.component';
import { CatalogStorekeeperComponent } from './components/catalogs/catalog-storekeeper/catalog-storekeeper.component';
import { DialogCatalogStorekeeperComponent } from './components/details/dialog-catalog-storekeeper/dialog-catalog-storekeeper.component';


registerLocaleData(localeRu)

export const appRoutes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full', canActivate: [AuthGuard] },
  {
    path: 'details', component: DetailComponent, canActivate: [AuthGuard], children: [
      { path: 'add-edit-detail', component: AddEditDetailComponent, pathMatch: 'full', canActivate: [AuthGuard] }
    ]
  },
  { path: 'catalog-storekeeper', component: CatalogStorekeeperComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'users', component: UsersComponent, canActivate: [AuthGuard], data: { roles: [Role.Admin] }},
  { path: '**', redirectTo: '', canActivate: [AuthGuard] }

]

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    DetailComponent,
    AddEditDetailComponent,
    LoginComponent,
    UsersComponent,
    DialogCatalogStorekeeperComponent,
    CatalogStorekeeperComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(appRoutes),
    NgxSpinnerModule,
    InputsModule,
    BrowserAnimationsModule,
    DropDownsModule,
    GridModule,
    MenusModule,
    IntlModule,
    ReactiveFormsModule,
    LabelModule,
    DatePickerModule,
    DateInputsModule,
    WindowModule
  ],
  providers: [
    DetailService,
    CatalogService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JsonDateInterceptor,
      multi: true
    },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: LOCALE_ID, useValue: 'ru' },
    DetailsGuard,
    fakeBackendProvider
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
