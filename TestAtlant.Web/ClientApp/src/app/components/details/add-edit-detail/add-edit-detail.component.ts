import { Component, EventEmitter, Inject, Output, ViewChild } from '@angular/core';
import { HttpClient, HttpEvent, HttpHandler, HttpRequest, HttpResponse } from '@angular/common/http';
import { GridDataResult, PageChangeEvent, FilterService } from "@progress/kendo-angular-grid";
import { SortDescriptor } from "@progress/kendo-data-query";
import { Observable } from 'rxjs';
import { filterBy, FilterDescriptor, CompositeFilterDescriptor } from "@progress/kendo-data-query";
import { OnInit } from '@angular/core';
import { DetailService } from '../../../services/detail.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { items } from "../contextmenu-items";
import { ContextMenuPopupEvent, ContextMenuSelectEvent, ContextMenuComponent  } from "@progress/kendo-angular-menu";
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { FormControl, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Storekeeper, DetailForm } from '../../../models/TestAtlant.model';
import { DetailComponent } from '../detail.component';

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
  selector: 'app-add-edit-detail',
  templateUrl: './add-edit-detail.component.html'
})
export class AddEditDetailComponent implements OnInit {
  public http: HttpClient;
  public baseUrl: string;

  public detailModelForm: FormGroup;

  //dialog-windows
  public isDialogSubindustryWindow: boolean = false;
  public isDialogStorekeeper: boolean = false;
  public isDialogDetailObject: boolean = false;

  constructor(
    http: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    private detailService: DetailService,
    private spinner: NgxSpinnerService,
    private router: ActivatedRoute,
    private location: Location,
    private detailComponent: DetailComponent
  ) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  

  ngOnInit(): void {
    this.detailModelForm = this.detailService.detailModel;
    this.detailService.isAddEditDetail = true;
  }

  save() {
    if (!this.detailService.isEdit) {
      this.detailService.addDetail(this.detailModelForm.value).subscribe((id: number) => {
        alert(id);
      });
    }
    else {
      this.detailService.editDetail(this.detailModelForm.value).subscribe(() => {
        alert('Деталь ' + this.detailModelForm.value.Id + ' успешно отредактирована');
        this.detailComponent.loadGridItems();
      });
    }

    this.detailService.isAddEditDetail = false;
    this.location.back();
  }

  openDialogStorekeeper() {
    this.isDialogStorekeeper = true;
  }

  closeDialogStorekeeper() {
    this.isDialogStorekeeper = false;
  }

  setStorekeeper(e: Storekeeper) {
    this.detailModelForm.controls["Storekeeper"].patchValue(e);
    this.isDialogStorekeeper = false;
  }
}
