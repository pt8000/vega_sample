import { SaveVehicle } from './../models/SaveVehicle';
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

  create(vehicle) {
    return this.http.post('/api/vehicles/new', vehicle)
      .map(resp => resp.json());
  }

  getVehicle(id){
    return this.http.get('/api/vehicles/' + id)
      .map(resp => resp.json());
  }

  update(vehicle: SaveVehicle){
    return this.http.put('/api/vehicles/' + vehicle.id, vehicle)
      .map(resp => resp.json());
  }

  delete(id) {
    return this.http.delete('/api/vehicles/' + id)
      .map(resp => resp.json());
  }
}
