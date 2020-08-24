import { BASE_PATH } from './../../configs/variables';
import { Configuration } from './../../configs/configuration';
import { environment } from './../../../environments/environment';
import { Inject, Injectable, Optional } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams, HttpResponse, HttpEvent } from '@angular/common/http';
import { BattleAreaDto } from '../models/battleAreaDto';
import { TargetDto } from '../models/targetDto';
import { Observable } from 'rxjs';

@Injectable()
export class BattleshipGameService {

  protected basePath = environment.issuerUrl;
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
   * @param battleAreaDtoCoordinates
   * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
   * @param reportProgress flag to report request and response progress.
   */
  public battleshipGameAdd(battleAreaDtoCoordinates: BattleAreaDto, observe?: 'body', reportProgress?: boolean): Observable<BattleAreaDto>;
  public battleshipGameAdd(battleAreaDtoCoordinates: BattleAreaDto, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<BattleAreaDto>>;
  public battleshipGameAdd(battleAreaDtoCoordinates: BattleAreaDto, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<BattleAreaDto>>;
  public battleshipGameAdd(battleAreaDtoCoordinates: BattleAreaDto, observe: any = 'body', reportProgress: boolean = false): Observable<any> {

    if (battleAreaDtoCoordinates === null || battleAreaDtoCoordinates === undefined) {
      throw new Error('Required parameter battleAreaDtoCoordinates was null or undefined when calling battleshipGameAdd.');
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

    return this.httpClient.post<BattleAreaDto>(`${this.basePath}/api/battleship/game`,
      battleAreaDtoCoordinates,
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
   * @param target
   * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
   * @param reportProgress flag to report request and response progress.
   */
  public battleshipGameCheckHit(target: TargetDto, observe?: 'body', reportProgress?: boolean): Observable<any>;
  public battleshipGameCheckHit(target: TargetDto, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
  public battleshipGameCheckHit(target: TargetDto, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
  public battleshipGameCheckHit(target: TargetDto, observe: any = 'body', reportProgress: boolean = false): Observable<any> {

    if (target === null || target === undefined) {
      throw new Error('Required parameter target was null or undefined when calling battleshipGameCheckHit.');
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

    return this.httpClient.post<any>(`${this.basePath}/api/battleship/game/launch`,
      target,
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
   * @param id
   * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
   * @param reportProgress flag to report request and response progress.
   */
  public battleshipGameDeleteById(id: number, observe?: 'body', reportProgress?: boolean): Observable<any>;
  public battleshipGameDeleteById(id: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
  public battleshipGameDeleteById(id: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
  public battleshipGameDeleteById(id: number, observe: any = 'body', reportProgress: boolean = false): Observable<any> {

    if (id === null || id === undefined) {
      throw new Error('Required parameter id was null or undefined when calling battleshipGameDeleteById.');
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

    return this.httpClient.delete<any>(`${this.basePath}/api/battleship/game/${encodeURIComponent(String(id))}`,
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
   * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
   * @param reportProgress flag to report request and response progress.
   */
  public battleshipGameGetAll(observe?: 'body', reportProgress?: boolean): Observable<Array<BattleAreaDto>>;
  public battleshipGameGetAll(observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<BattleAreaDto>>>;
  public battleshipGameGetAll(observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<BattleAreaDto>>>;
  public battleshipGameGetAll(observe: any = 'body', reportProgress: boolean = false): Observable<any> {

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

    return this.httpClient.get<Array<BattleAreaDto>>(`${this.basePath}/api/battleship/game`,
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
   * @param id
   * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
   * @param reportProgress flag to report request and response progress.
   */
  public battleshipGameGetById(id: number, observe?: 'body', reportProgress?: boolean): Observable<BattleAreaDto>;
  public battleshipGameGetById(id: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<BattleAreaDto>>;
  public battleshipGameGetById(id: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<BattleAreaDto>>;
  public battleshipGameGetById(id: number, observe: any = 'body', reportProgress: boolean = false): Observable<any> {

    if (id === null || id === undefined) {
      throw new Error('Required parameter id was null or undefined when calling battleshipGameGetById.');
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

    return this.httpClient.get<BattleAreaDto>(`${this.basePath}/api/battleship/game/${encodeURIComponent(String(id))}`,
      {
        withCredentials: this.configuration.withCredentials,
        headers: headers,
        observe: observe,
        reportProgress: reportProgress
      }
    );
  }

}
