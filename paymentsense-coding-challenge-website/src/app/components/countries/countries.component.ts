import { AfterViewInit, Component, OnInit, ViewChild, } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { tap } from 'rxjs/operators';
import { CountriesApiService } from 'src/app/services/countries-api.service';
import { CountriesDataSource } from './countries.datasource';

@Component({
  selector: 'app-countries',
  templateUrl: './countries.component.html',
  styleUrls: ['./countries.component.scss']
})
export class CountriesComponent implements AfterViewInit, OnInit {

  public displayedColumns: string[] = ['name', 'flag'];
  public dataSource: CountriesDataSource;
  public isLoading = true;

  @ViewChild(MatPaginator, { read: MatPaginator, static: false }) paginator: MatPaginator;

  constructor(private countriesApiService: CountriesApiService) { }

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

  loadCountriesPage() {
    this.dataSource.loadCountries(
      this.paginator.pageIndex+1,
      this.paginator.pageSize);
  }
}
