import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/publishReplay';
import 'rxjs/add/operator/share';
import 'rxjs/add/operator/retry';

import { Company } from '../models/company';
import { PagedTable } from '../models/pagedTable';
import { CompanyService } from '../services/company.service';

@Component({
  selector: 'app-list',
  templateUrl: 'list.component.html'
})

export class ListComponent implements OnInit {
  loading = false;
  failed = false;
  currentPage = 1;
  totalPage = 1;
  pageSize = 10;
  count = 0;

  pagedTable: Observable<PagedTable<Company>>;
  constructor(private companyService: CompanyService) { }

  loadTable() {
    this.loading = true;
    this.failed = false;
    this.pagedTable = this.companyService.getCompanies(this.currentPage, this.pageSize);
    this.pagedTable.subscribe(pt => {
      this.loading = false;
      this.pageSize = pt.pageSize;
      this.currentPage = pt.page;
      this.count = pt.count;
      this.totalPage = Math.ceil(pt.count / pt.pageSize);
    },
      err => {
        this.loading = false;
        this.failed = true;
      });
  }

  ngOnInit() {
    this.loadTable();
  }

  OnClickPrevious($event) {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.loadTable();
    }
  }

  OnClickNext($event) {
    if (this.currentPage < this.count) {
      this.currentPage++;
      this.loadTable();
    }
  }

}

