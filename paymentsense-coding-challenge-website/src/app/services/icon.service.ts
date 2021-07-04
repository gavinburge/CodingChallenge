import { Injectable } from "@angular/core";
import { IconDefinition } from "@fortawesome/fontawesome-svg-core";
import { faInfo } from "@fortawesome/free-solid-svg-icons";

@Injectable({
    providedIn: 'root'
  })

  export class IconService {
    constructor() {}
  
    get info(): IconDefinition {
        return faInfo;
    }
  }