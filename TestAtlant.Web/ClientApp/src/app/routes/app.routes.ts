import { Routes } from "@angular/router";
import { HomeComponent } from "../components/home/home.component";
import { DetailComponent } from "../components/details/detail.component";

import { detailsRoutes } from "./details.routes";

export const appRoutes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'details', component: DetailComponent, children: detailsRoutes },
]
