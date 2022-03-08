import { Component, EventEmitter, Inject, Input, Output, ViewChild } from '@angular/core';
import { HttpClient, HttpEvent, HttpHandler, HttpRequest, HttpResponse } from '@angular/common/http';
import { GridDataResult, PageChangeEvent, FilterService, SelectableSettings } from "@progress/kendo-angular-grid";
import { SortDescriptor } from "@progress/kendo-data-query";
import { Observable } from 'rxjs';
import { filterBy, FilterDescriptor, CompositeFilterDescriptor } from "@progress/kendo-data-query";
import { OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ContextMenuPopupEvent, ContextMenuSelectEvent, ContextMenuComponent  } from "@progress/kendo-angular-menu";
import { ActivatedRoute } from '@angular/router';
import { CatalogService } from '../../../services/catalog.service';
import { cloneDeep } from "lodash"
import { Storekeeper } from '../../../models/TestAtlant.model';


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
  selector: 'catalog-storekeeper',
  templateUrl: './catalog-storekeeper.component.html'
})
export class CatalogStorekeeperComponent implements OnInit {

  public storekeeper: Storekeeper[];

  public filter: CompositeFilterDescriptor;
  public gridStorekeeperData: any[];

  public gridItems: Observable<GridDataResult>;
  public pageSize: number = 50;
  public skip: number = 0;
  public sortDescriptor: SortDescriptor[] = [];
  public filterTerm: number = null;
  public isGridLoading: boolean = false;

  public http: HttpClient;
  public baseUrl: string;

  public position: 'top' | 'bottom' | 'both' = 'top';

  @Input()
  public isDialog: boolean = false;

  @Output() returnSelectedStorekeeper: EventEmitter<Storekeeper> = new EventEmitter();
  private selectedStorekeeper: Storekeeper;

  private editedRowIndex: number;
  private editedStorekeeper: Storekeeper;

  public selectableSettings: SelectableSettings = {
    mode: "single",
    enabled: true
  };

  constructor(
    http: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    private spinner: NgxSpinnerService,
    private catalogService: CatalogService,
    private router: ActivatedRoute
  ) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  ngOnInit(): void {
    this.spinner.show();
    this.loadGridItems();
    //this.loadDropDownItems();
  }

  private loadGridItems(): void {
    this.catalogService.getAllStorekeepers().subscribe(result => {
      this.storekeeper = result;
      this.gridStorekeeperData = filterBy(cloneDeep(this.storekeeper), this.filter);
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
    this.gridStorekeeperData = filterBy(cloneDeep(this.storekeeper), filter);
  }

  public selectionChange(e: any) {
    if (e.selectedRows.length > 0) {
      this.selectedStorekeeper = cloneDeep(e.selectedRows[0].dataItem);
      this.returnSelectedStorekeeper.emit(this.selectedStorekeeper);
    }
    else {
      this.returnSelectedStorekeeper.emit({
        Id: 0,
        Name: "",
        EmployeeNumber: "",
        Firstname: "",
        Lastname: "",
        Surname: ""
      });
    }
  }

  public selectStorekeepered(obj: Storekeeper) {
    this.selectedStorekeeper = obj;
    this.returnSelectedStorekeeper.emit(obj);
  }

  public addHandler({ sender }, formInstance) {
    formInstance.reset();
    this.closeEditor(sender);

    sender.addRow({ Id: 0, Name: "" });
  }

  public editHandler({ sender, rowIndex, dataItem }) {
    this.closeEditor(sender);

    this.editedRowIndex = rowIndex;
    this.editedStorekeeper = Object.assign({}, dataItem);

    sender.editRow(rowIndex);
  }

  public cancelHandler({ sender, rowIndex }) {
    this.closeEditor(sender, rowIndex);
  }

  public saveHandler({ sender, rowIndex, dataItem, isNew }) {
    sender.closeRow(rowIndex);

    if (isNew) {
      this.catalogService.addStorekeeper(dataItem).subscribe((id) => {
        dataItem.Id = id;
        this.gridStorekeeperData.push(Object.assign({}, dataItem));
      });
    }
    else {
      this.catalogService.updateStorekeeper(dataItem).subscribe();
      let updatingObj = this.gridStorekeeperData.find((value: Storekeeper) =>
        value == dataItem);

      if (updatingObj) {
        let updatingIndex = this.gridStorekeeperData.indexOf(updatingObj);

        this.gridStorekeeperData[updatingIndex] = cloneDeep(dataItem);

        //this.catalogService.removeContractor(dataItem).subscribe();
      }
    }
  }

  public removeHandler({ dataItem }) {
    let deletingObj = this.gridStorekeeperData.find((value: Storekeeper) =>
      value == dataItem);

    if (deletingObj) {
      let detailCount = 0;

      this.catalogService.getDetailCount(deletingObj.Id).subscribe(result => {
        detailCount = result;

        if (detailCount > 0) {
          alert("У кладовщика остались детали. Вы не можете его удалить");
          return;
        }

        let deletingIndex = this.gridStorekeeperData.indexOf(deletingObj);

        this.gridStorekeeperData.splice(deletingIndex, 1);

        this.catalogService.removeStorekeeper(dataItem).subscribe();
      });      
    }
  }

  private closeEditor(grid, rowIndex = this.editedRowIndex) {
    grid.closeRow(rowIndex);
    this.resetItem(this.editedStorekeeper);
    this.editedRowIndex = undefined;
    this.editedStorekeeper = undefined;
  }

  public resetItem(dataItem: any) {
    if (!dataItem) {
      return;
    }

    let originalDataItem = this.storekeeper.find(
      (item) => item.Id === dataItem.Id
    );

    if (originalDataItem) {
      originalDataItem = this.gridStorekeeperData.find((item) => item.Id === dataItem.Id)
      originalDataItem = cloneDeep(dataItem);
    }

    //originalDataItem = cloneDeep(dataItem);
    //Object.assign(originalDataItem, dataItem);
  }

  //loadDropDownItems() {
  //  this.catalogService.getAllIdentifyDocumentTypes().subscribe((array) =>
  //    (this.identifyDocumentTypes = array)
  //  );
  //}
}
