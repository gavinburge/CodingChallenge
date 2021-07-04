import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material';
import { IGetCountryDetailResponse } from 'src/app/models/queries/get-country-detail-response';

@Component({
  selector: 'app-country-detail',
  templateUrl: './country-detail.component.html',
  styleUrls: ['./country-detail.component.scss']
})

export class CountryDetailComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public countryDetail:IGetCountryDetailResponse){  
  }

  ngOnInit() {
  }

}
