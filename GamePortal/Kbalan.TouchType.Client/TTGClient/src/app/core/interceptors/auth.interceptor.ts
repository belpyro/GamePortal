import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpEvent,
  HttpHandler,
  HttpRequest,
  HttpHeaders,
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { OAuthService } from 'angular-oauth2-oidc';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private oauth: OAuthService) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const token = this.oauth.getAccessToken();
    if (sessionStorage.getItem('id_token_claims_obj') !== null)
    {
      const idtoken = JSON.parse(sessionStorage.getItem('id_token_claims_obj'));
      const authReq = req.clone({
        headers: new HttpHeaders({
          'id-token':  idtoken.role,
          Authorization: `Bearer ${token}`
        })
    });
      return next.handle(authReq);
    }else{
      const authReq = req.clone({
        headers: new HttpHeaders({
          Authorization: `Bearer ${token}`
        })
    });
      return next.handle(authReq);
  }
    }
  }

