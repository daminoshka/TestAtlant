import { Routes } from "@angular/router";
import { AddEditDetailComponent } from "../components/details/add-edit-detail/add-edit-detail.component";

export const detailsRoutes: Routes = [
  { path: 'add-edit-detail', component: AddEditDetailComponent, pathMatch: 'full' },
]
