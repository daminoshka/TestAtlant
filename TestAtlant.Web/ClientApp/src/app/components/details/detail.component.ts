import { Component, Inject, ViewChild } from '@angular/core';
import { HttpClient, HttpEvent, HttpHandler, HttpRequest, HttpResponse } from '@angular/common/http';
import { GridDataResult, PageChangeEvent, FilterService, SelectableSettings } from "@progress/kendo-angular-grid";
import { SortDescriptor } from "@progress/kendo-data-query";
import { Observable } from 'rxjs';
import { filterBy, FilterDescriptor, CompositeFilterDescriptor } from "@progress/kendo-data-query";
import { OnInit } from '@angular/core';
import { DetailService } from '../../services/detail.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { items } from "./contextmenu-items";
import { ContextMenuPopupEvent, ContextMenuSelectEvent, ContextMenuComponent  } from "@progress/kendo-angular-menu";
import { ActivatedRoute } from '@angular/router';
import { element } from 'protractor';


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
  selector: 'detail',
  templateUrl: './detail.component.html'
})
export class DetailComponent implements OnInit {
  public items: any[] = items;

  public details: any[];

  public filter: CompositeFilterDescriptor;
  public gridDetailsData: any[];

  public gridItems: Observable<GridDataResult>;
  public pageSize: number = 50;
  public skip: number = 0;
  public sortDescriptor: SortDescriptor[] = [];
  public filterTerm: number = null;
  public isGridLoading: boolean = false;

  public selectableSettings: SelectableSettings = {
    checkboxOnly: false,
    mode: "single",
    enabled: true
  };

  public http: HttpClient;
  public baseUrl: string;

  public position: 'top' | 'bottom' | 'both' = 'top';

  @ViewChild("gridmenu", { static: false })
  public gridContextMenu: ContextMenuComponent;

  constructor(
    http: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    private detailService: DetailService,
    private spinner: NgxSpinnerService,
    private router: ActivatedRoute
  ) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  ngOnInit(): void {
    this.spinner.show();
    this.loadGridItems();
  }

  public loadGridItems(): void {
    this.detailService.getAllDetails().subscribe(result => {
      this.details = result;
      this.gridDetailsData = filterBy(this.details, this.filter);
      this.spinner.hide();
    }, error => {
        console.error(error)
        this.spinner.hide();
    });
  }

  public pageChange(event: PageChangeEvent): void {
    this.skip = event.skip;
  }
  
  public handleSortChange(descriptor: SortDescriptor[]): void {
    this.sortDescriptor = descriptor;
  }

  public filterChange(filter: CompositeFilterDescriptor): void {
    this.filter = filter;
    this.gridDetailsData = filterBy(this.details, filter);
  }

  public onSelect(e: ContextMenuSelectEvent): void {
    if (e.item.text === "Добавить") {
      alert("Добавить")
    } else {
      alert("Не добавить")
    }
  }

  public selectionChange(e: any) {
    if (e.selectedRows.length > 0) {
      this.detailService.selectedDetail = e.selectedRows[0].dataItem;
      this.detailService.detailModel.patchValue(e.selectedRows[0].dataItem);
    }
    else {
      this.detailService.detailModel.reset();
    }
  }

  private contextItem: any;

  public onCellClick(e: any): void {
    if (e.type === "contextmenu") {
      const originalEvent = e.originalEvent;

      originalEvent.preventDefault();

      this.contextItem = e.dataItem;

      this.gridContextMenu.show({
        left: originalEvent.pageX,
        top: originalEvent.pageY,
      });
    }
  }

  public deleteSelectedDetail() {
    if (!this.detailService.detailModel.valid) alert("Выделите строку для удаления")

    this.detailService.deleteDetail(this.detailService.detailModel.value).subscribe(() =>
      alert("Деталь успешно удалена"));

    var elementPos = this.gridDetailsData.map((x) => x.Id).indexOf(this.detailService.detailModel.value.Id);

    this.gridDetailsData.splice(elementPos, 1);

    if (this.gridDetailsData.length > 0) {
      this.detailService.selectedDetail = this.gridDetailsData[(this.gridDetailsData.length - 1) > elementPos ? elementPos : (this.gridDetailsData.length - 1)];
      this.detailService.detailModel.patchValue(this.detailService.selectedDetail);
    }
    else {
      this.detailService.selectedDetail = null;
    }
  }

  public iconClass({ text, items }: any): any {
    switch (text) {
      case "Добавить":
        return {
          "k-icon": true,
          "k-i-plus": true,
          "btn-success": true,
        }
      case "Редактировать":
        return {
          "k-icon": true,
          "k-i-edit": true,
          "btn-warning": true,
        }
      case "Удалить":
        return {
          "k-icon": true,
          "k-i-delete": true,
          "btn-danger": true,
        }
    }
  }
}
