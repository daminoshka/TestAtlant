import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, RouterStateSnapshot, Router, CanActivate } from "@angular/router";
import { DetailComponent } from "../components/details/detail.component";


@Injectable()
export class DetailsGuard {
  private firstNavigation = true;
  constructor(private router: Router) { }
  canActivate(route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
    if (this.firstNavigation) {
      this.firstNavigation = false;
      if (route.component !== DetailComponent) {
        this.router.navigateByUrl("/details");
        return false;
      }
    }
    return true;
  }
}
