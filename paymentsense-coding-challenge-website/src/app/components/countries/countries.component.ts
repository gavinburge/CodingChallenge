import { trigger } from '@angular/animations';
import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild, } from '@angular/core';
import { MatDialog } from '@angular/material';
import { MatPaginator } from '@angular/material/paginator';
import { Subject } from 'rxjs';
import { takeUntil, tap } from 'rxjs/operators';
import { IGetCountryDetailQuery } from 'src/app/models/queries/get-country-detail-query';
import { CountriesApiService } from 'src/app/services/countries-api.service';
import { IconService } from 'src/app/services/icon.service';
import { CountryDetailComponent } from '../country-detail/country-detail.component';
import { CountriesDataSource } from './countries.datasource';

@Component({
  selector: 'app-countries',
  templateUrl: './countries.component.html',
  styleUrls: ['./countries.component.scss'],
})
export class CountriesComponent implements AfterViewInit, OnInit, OnDestroy {

  public displayedColumns: string[] = ['name', 'flag', 'details'];
  public dataSource: CountriesDataSource;
  public isLoading = true;
  private unsubscribe$ = new Subject<void>();

  @ViewChild(MatPaginator, { read: MatPaginator, static: false }) paginator: MatPaginator;

  constructor(
    private countriesApiService: CountriesApiService,
    private dialog: MatDialog,
    public iconService : IconService) { }

  ngAfterViewInit(): void {
    this.paginator.page
      .pipe(
        tap(() => this.loadCountriesPage())
      )
      .subscribe();
  }

  ngOnInit() {
    this.dataSource = new CountriesDataSource(this.countriesApiService);
    this.dataSource.loadCountries(1, 5);
    this.isLoading = false;
  }

  ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  loadCountriesPage() {
    this.dataSource.loadCountries(
      this.paginator.pageIndex + 1,
      this.paginator.pageSize);
  }

  detailsClick(countryName) {
    let request: IGetCountryDetailQuery = {
      countryName: countryName
    }

    this.countriesApiService
      .getCountryDetail(request)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(response => {

        //Open MatDialog and load component dynamically  
        const dialogRef = this.dialog.open(CountryDetailComponent, {
          data: response.data
        });

        //Need to subscribe afterClosed event of MatDialog  
        dialogRef.afterClosed().subscribe();

      });
  }

  detailIcon(){
    return this.iconService.info;
  }
}
