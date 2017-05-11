import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class VehicleService {

  constructor(private http: Http) { }
  
  getMakes() {
    return this.http.get('/api/makes')
      .map(resp => resp.json());    
  }

  getFeatures() {
    return this.http.get('/api/features')
      .map(resp => resp.json());
  }
  
}