import { Component, EventEmitter, Inject, Output, ViewChild } from '@angular/core';
import { HttpClient, HttpEvent, HttpHandler, HttpRequest, HttpResponse } from '@angular/common/http';
import { GridDataResult, PageChangeEvent, FilterService } from "@progress/kendo-angular-grid";
import { SortDescriptor } from "@progress/kendo-data-query";
import { Observable } from 'rxjs';
import { filterBy, FilterDescriptor, CompositeFilterDescriptor } from "@progress/kendo-data-query";
import { OnInit } from '@angular/core';
import { DetailService } from '../../../services/detail.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ActivatedRoute } from '@angular/router';
import { FormControl, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Storekeeper, DetailForm } from '../../../models/TestAtlant.model';

const flatten = (filter) => {
  const filters = filter.filters;
  if (filters) {
    return filters.reduce(
      (acc, curr) => acc.concat(curr.filters ? flatten(curr) : [curr]),
      []
    );
  }
  return [];
};


@Component({
  selector: 'dialog-catalog-storekeeper',
  templateUrl: './dialog-catalog-storekeeper.component.html'
})
export class DialogCatalogStorekeeperComponent implements OnInit {
  public http: HttpClient;
  public baseUrl: string;

  public selectedStorekeeper: Storekeeper = {
    Id: 0,
    EmployeeNumber: "",
    Name: "",
    Firstname: "",
    Lastname: "",
    Surname: ""
  };

  @Output() cancel = new EventEmitter<boolean>(false);
  @Output() returnSelectedStorekeeper: EventEmitter<Storekeeper> = new EventEmitter();

  constructor(
    http: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    private insuranceContractService: DetailService,
    private spinner: NgxSpinnerService,
    private router: ActivatedRoute
  ) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  ngOnInit(): void {

  }

  submit() {
    this.returnSelectedStorekeeper.emit(this.selectedStorekeeper);
  }

  close() {
    this.cancel.emit(false);
  }

  public setStorekeeper(obj: Storekeeper) {
    this.selectedStorekeeper = obj;
  }
}
