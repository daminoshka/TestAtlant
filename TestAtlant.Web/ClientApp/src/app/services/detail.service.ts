import { Component, Inject, Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpEvent, HttpHandler, HttpHeaders, HttpRequest, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DetailForm } from '../models/TestAtlant.model';
import { FormGroup } from '@angular/forms';

@Injectable()
export class DetailService implements OnInit {

  public http: HttpClient;
  public baseUrl: string;

  public detailModel: FormGroup = new DetailForm().model;
  public selectedDetail: any;

  public isAddEditDetail: boolean = false;

  public isEdit = false;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  ngOnInit(): void {
  }
  
  getAllDetails(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + 'Detail/GetAllDetails');
  }

  addDetail(detail: any) {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });
    const options = { headers: headers };

    return this.http.post<number>(this.baseUrl + 'Detail/AddDetail', detail, options);
  }

  editDetail(detail: any) {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });
    const options = { headers: headers };

    return this.http.post<any>(this.baseUrl + 'Detail/UpdateDetail', detail, options);
  }

  deleteDetail(detail: any) {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });
    const options = { headers: headers };

    return this.http.post<any>(this.baseUrl + 'Detail/DeleteDetail', detail, options);
  }

}
