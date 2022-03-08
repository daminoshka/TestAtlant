import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient, HttpEvent, HttpHandler, HttpHeaders, HttpRequest, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Storekeeper, DetailForm } from '../models/TestAtlant.model';

@Injectable()
export class CatalogService {

  public http: HttpClient;
  public baseUrl: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  getAllStorekeepers(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + 'Storekeeper/GetAllStorekeepers');
  }

  /**
   * Добавление кладовщика
   */
  addStorekeeper(contractor: Storekeeper) {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });
    const options = { headers: headers };

    return this.http.post<any>(this.baseUrl + 'Storekeeper/AddStorekeeper', contractor, options);
  }

  /**
   * Обновление кладовщика
   */
  updateStorekeeper(contractor: Storekeeper) {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });
    const options = { headers: headers };

    return this.http.post<any>(this.baseUrl + 'Storekeeper/UpdateStorekeeper', contractor, options);
  }

  /**
   * Удаление кладовщика
   */
  removeStorekeeper(storekeeper: Storekeeper) {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });
    const options = { headers: headers };

    return this.http.post<any>(this.baseUrl + 'Storekeeper/DeleteStorekeeper', storekeeper, options);
  }

  /**
   * Получение количества деталей кладовщика
   */
  getDetailCount(storekeeperId: number) {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });
    const options = { headers: headers };

    return this.http.get<number>(`${this.baseUrl}Storekeeper/GetDetailCount?storekeeperId=${storekeeperId}`, options);
  }
}
