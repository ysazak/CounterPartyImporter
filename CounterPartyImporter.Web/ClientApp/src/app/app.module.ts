import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { ImportComponent } from './home/import.component';
import { ListComponent } from './home/list.component';

import { CompanyService } from './services/company.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    ImportComponent,
    ListComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    HttpModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    RouterModule.forRoot([
      { path: '', component: ListComponent, pathMatch: 'full' },
      { path: 'import', component: ImportComponent, pathMatch: 'full' }
    ])
  ],
  providers: [CompanyService],
  bootstrap: [AppComponent]
})
export class AppModule { }
