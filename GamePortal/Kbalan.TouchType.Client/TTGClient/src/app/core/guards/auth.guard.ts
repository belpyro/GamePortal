
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { OAuthService } from 'angular-oauth2-oidc';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private oauth: OAuthService, private router: Router) {
  }
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    const url: string = state.url;
    return this.checkUserRole(next, url);
  }

  checkUserRole(route: ActivatedRouteSnapshot, url: any): boolean {
    if (this.oauth.hasValidAccessToken()) {
      const user  = JSON.parse(sessionStorage.getItem('id_token_claims_obj'));
      const userRole = user.role;
      if (route.data.role && route.data.role.indexOf(userRole) === -1) {
        this.router.navigate(['home']);
        return false;
      }
      return true;
    }
    this.router.navigate(['entry/login']);
    return false;
  }
}
