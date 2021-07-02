import { Component, OnInit, } from '@angular/core';
import { CountriesApiService } from 'src/app/services/countries-api.service';
import { CountriesDataSource } from './countries.datasource';

@Component({
  selector: 'app-countries',
  templateUrl: './countries.component.html',
  styleUrls: ['./countries.component.scss']
})
export class CountriesComponent implements OnInit {

  public displayedColumns: string[] = ['name'];
  public dataSource: CountriesDataSource;
  public isLoading = true;

  constructor(private countriesApiService: CountriesApiService) { }

  ngOnInit() {
    this.dataSource = new CountriesDataSource(this.countriesApiService);
    this.dataSource.loadCountries();
    this.isLoading = false;
  }
}
