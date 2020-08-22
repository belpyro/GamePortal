import { Inject, Injectable, Optional } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams, HttpResponse, HttpEvent } from '@angular/common/http';
import { CustomHttpUrlEncodingCodec } from './encoder';
import { } from 'rxjs/Observable';
import { LoginDto2 } from '../models/loginDto';
import { NewUserDto2 } from '../models/newUserDto';
import { BASE_PATH, COLLECTION_FORMATS } from './variables';
import { Configuration } from '../configs/configuration';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';

@Injectable()
export class UserService {

  protected basePath = environment.backendUrl;
  public defaultHeaders = new HttpHeaders();
  public configuration = new Configuration();

  constructor(protected httpClient: HttpClient, @Optional() @Inject(BASE_PATH) basePath: string, @Optional() configuration: Configuration) {
    if (basePath) {
      this.basePath = basePath;
    }
    if (configuration) {
      this.configuration = configuration;
      this.basePath = basePath || configuration.basePath || this.basePath;
    }
  }

  /**
   * @param consumes string[] mime-types
   * @return true: consumes contains 'multipart/form-data', false: otherwise
   */
  private canConsumeForm(consumes: string[]): boolean {
    const form = 'multipart/form-data';
    for (const consume of consumes) {
      if (form === consume) {
        return true;
      }
    }
    return false;
  }

  /**
   *
   *
   * @param userId
   * @param token
   * @param newPassword
   * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
   * @param reportProgress flag to report request and response progress.
   */
  public userChangePassword(userId: string, token: string, newPassword: string, observe?: 'body', reportProgress?: boolean): Observable<any>;
  public userChangePassword(userId: string, token: string, newPassword: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
  public userChangePassword(userId: string, token: string, newPassword: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
  public userChangePassword(userId: string, token: string, newPassword: string, observe: any = 'body', reportProgress: boolean = false): Observable<any> {

    if (userId === null || userId === undefined) {
      throw new Error('Required parameter userId was null or undefined when calling userChangePassword.');
    }

    if (token === null || token === undefined) {
      throw new Error('Required parameter token was null or undefined when calling userChangePassword.');
    }

    if (newPassword === null || newPassword === undefined) {
      throw new Error('Required parameter newPassword was null or undefined when calling userChangePassword.');
    }

    let queryParameters = new HttpParams({ encoder: new CustomHttpUrlEncodingCodec() });
    if (userId !== undefined && userId !== null) {
      queryParameters = queryParameters.set('userId', <any> userId);
    }
    if (token !== undefined && token !== null) {
      queryParameters = queryParameters.set('token', <any> token);
    }
    if (newPassword !== undefined && newPassword !== null) {
      queryParameters = queryParameters.set('newPassword', <any> newPassword);
    }

    let headers = this.defaultHeaders;

    // to determine the Accept header
    let httpHeaderAccepts: string[] = [
      'application/json'
    ];
    const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
    if (httpHeaderAcceptSelected != undefined) {
      headers = headers.set('Accept', httpHeaderAcceptSelected);
    }

    // to determine the Content-Type header
    const consumes: string[] = [
      'application/json'
    ];

    return this.httpClient.put<any>(`${this.basePath}/api/battleship/Users/changepass`,
      null,
      {
        params: queryParameters,
        withCredentials: this.configuration.withCredentials,
        headers: headers,
        observe: observe,
        reportProgress: reportProgress
      }
    );
  }

  /**
   *
   *
   * @param userId
   * @param token
   * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
   * @param reportProgress flag to report request and response progress.
   */
  public userConfirmEmail(userId: string, token: string, observe?: 'body', reportProgress?: boolean): Observable<any>;
  public userConfirmEmail(userId: string, token: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
  public userConfirmEmail(userId: string, token: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
  public userConfirmEmail(userId: string, token: string, observe: any = 'body', reportProgress: boolean = false): Observable<any> {

    if (userId === null || userId === undefined) {
      throw new Error('Required parameter userId was null or undefined when calling userConfirmEmail.');
    }

    if (token === null || token === undefined) {
      throw new Error('Required parameter token was null or undefined when calling userConfirmEmail.');
    }

    let queryParameters = new HttpParams({ encoder: new CustomHttpUrlEncodingCodec() });
    if (userId !== undefined && userId !== null) {
      queryParameters = queryParameters.set('userId', <any> userId);
    }
    if (token !== undefined && token !== null) {
      queryParameters = queryParameters.set('token', <any> token);
    }

    let headers = this.defaultHeaders;

    // to determine the Accept header
    let httpHeaderAccepts: string[] = [
      'application/json'
    ];
    const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
    if (httpHeaderAcceptSelected != undefined) {
      headers = headers.set('Accept', httpHeaderAcceptSelected);
    }

    // to determine the Content-Type header
    const consumes: string[] = [
      'application/json'
    ];

    return this.httpClient.put<any>(`${this.basePath}/api/battleship/Users/confirmemail`,
      null,
      {
        params: queryParameters,
        withCredentials: this.configuration.withCredentials,
        headers: headers,
        observe: observe,
        reportProgress: reportProgress
      }
    );
  }

  /**
   *
   *
   * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
   * @param reportProgress flag to report request and response progress.
   */
  public userDelete(observe?: 'body', reportProgress?: boolean): Observable<any>;
  public userDelete(observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
  public userDelete(observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
  public userDelete(observe: any = 'body', reportProgress: boolean = false): Observable<any> {

    let headers = this.defaultHeaders;

    // to determine the Accept header
    let httpHeaderAccepts: string[] = [
      'application/json'
    ];
    const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
    if (httpHeaderAcceptSelected != undefined) {
      headers = headers.set('Accept', httpHeaderAcceptSelected);
    }

    // to determine the Content-Type header
    const consumes: string[] = [
      'application/json'
    ];

    return this.httpClient.delete<any>(`${this.basePath}/api/battleship/Users`,
      {
        withCredentials: this.configuration.withCredentials,
        headers: headers,
        observe: observe,
        reportProgress: reportProgress
      }
    );
  }

  /**
   *
   *
   * @param model
   * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
   * @param reportProgress flag to report request and response progress.
   */
  public userLogin(model: LoginDto2, observe?: 'body', reportProgress?: boolean): Observable<any>;
  public userLogin(model: LoginDto2, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
  public userLogin(model: LoginDto2, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
  public userLogin(model: LoginDto2, observe: any = 'body', reportProgress: boolean = false): Observable<any> {

    if (model === null || model === undefined) {
      throw new Error('Required parameter model was null or undefined when calling userLogin.');
    }

    let headers = this.defaultHeaders;

    // to determine the Accept header
    let httpHeaderAccepts: string[] = [
      'application/json'
    ];
    const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
    if (httpHeaderAcceptSelected != undefined) {
      headers = headers.set('Accept', httpHeaderAcceptSelected);
    }

    // to determine the Content-Type header
    const consumes: string[] = [
      'application/json'
    ];
    const httpContentTypeSelected: string | undefined = this.configuration.selectHeaderContentType(consumes);
    if (httpContentTypeSelected != undefined) {
      headers = headers.set('Content-Type', httpContentTypeSelected);
    }

    return this.httpClient.post<any>(`${this.basePath}/api/battleship/Users/login`,
      model,
      {
        withCredentials: this.configuration.withCredentials,
        headers: headers,
        observe: observe,
        reportProgress: reportProgress
      }
    );
  }

  /**
   *
   *
   * @param model
   * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
   * @param reportProgress flag to report request and response progress.
   */
  public userRegister(model: NewUserDto2, observe?: 'body', reportProgress?: boolean): Observable<any>;
  public userRegister(model: NewUserDto2, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
  public userRegister(model: NewUserDto2, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
  public userRegister(model: NewUserDto2, observe: any = 'body', reportProgress: boolean = false): Observable<any> {

    if (model === null || model === undefined) {
      throw new Error('Required parameter model was null or undefined when calling userRegister.');
    }

    let headers = this.defaultHeaders;

    // to determine the Accept header
    let httpHeaderAccepts: string[] = [
      'application/json'
    ];
    const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
    if (httpHeaderAcceptSelected != undefined) {
      headers = headers.set('Accept', httpHeaderAcceptSelected);
    }

    // to determine the Content-Type header
    const consumes: string[] = [
      'application/json'
    ];
    const httpContentTypeSelected: string | undefined = this.configuration.selectHeaderContentType(consumes);
    if (httpContentTypeSelected != undefined) {
      headers = headers.set('Content-Type', httpContentTypeSelected);
    }

    return this.httpClient.post<any>(`${this.basePath}/api/battleship/Users/register`,
      model,
      {
        withCredentials: this.configuration.withCredentials,
        headers: headers,
        observe: observe,
        reportProgress: reportProgress
      }
    );
  }

  /**
   *
   *
   * @param email
   * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
   * @param reportProgress flag to report request and response progress.
   */
  public userResetPassword(email: string, observe?: 'body', reportProgress?: boolean): Observable<any>;
  public userResetPassword(email: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
  public userResetPassword(email: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
  public userResetPassword(email: string, observe: any = 'body', reportProgress: boolean = false): Observable<any> {

    if (email === null || email === undefined) {
      throw new Error('Required parameter email was null or undefined when calling userResetPassword.');
    }

    let headers = this.defaultHeaders;

    // to determine the Accept header
    let httpHeaderAccepts: string[] = [
      'application/json'
    ];
    const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
    if (httpHeaderAcceptSelected != undefined) {
      headers = headers.set('Accept', httpHeaderAcceptSelected);
    }

    // to determine the Content-Type header
    const consumes: string[] = [
      'application/json'
    ];
    const httpContentTypeSelected: string | undefined = this.configuration.selectHeaderContentType(consumes);
    if (httpContentTypeSelected != undefined) {
      headers = headers.set('Content-Type', httpContentTypeSelected);
    }

    return this.httpClient.put<any>(`${this.basePath}/api/battleship/Users/resetpass`,
      email,
      {
        withCredentials: this.configuration.withCredentials,
        headers: headers,
        observe: observe,
        reportProgress: reportProgress
      }
    );
  }

}
