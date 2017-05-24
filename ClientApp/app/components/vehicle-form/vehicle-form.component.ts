import * as _ from 'underscore';
import { Vehicle } from './../../models/Vehicle';
import { SaveVehicle } from './../../models/SaveVehicle';
import { ToastyModule, ToastyService } from 'ng2-toasty';
import { VehicleService } from './../../services/vehicle.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import { Observable } from "rxjs/Observable";
import 'rxjs/add/Observable/forkJoin';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {

  makes: any[]; //init variable
  models: any[];
  features: any[];
  vehicle: SaveVehicle = {
    id: 0,
    makeId: 0,
    modelId: 0,
    isRegistered: false,
    features: [],
    contact: {
      name: '',
      phone: '',
      email: ''
    }
  }; //empty object, jak jest typ any zadlada ze kazde pole moze w nim byc, nawet potem zdefiniowane, i mamy tez intelisense wtedy
  //tutaj w obiekcie vehicle, dodawane jest pole make, dzieje sie to w formularzu, gdzie zapisana jest wybrana marka



  constructor(
    private route: ActivatedRoute, //do odczytu jaki jest id w url podany
    private router: Router, //do przkierowania jak ktos poda zla id w url
    private vehicleService: VehicleService,
    private toastyService: ToastyService) {

    route.params.subscribe(p => {
      this.vehicle.id = +p['id']; //+ robi konwersje do int
    });

  }

  ngOnInit() {

    var sources = [
      this.vehicleService.getMakes(), 
      this.vehicleService.getFeatures()
    ];

    //jak jest id vehicla, znaczy edycja, znaczy pobierz dane calego auta i je pokaz
    if(this.vehicle.id) //jak true, tzn ze nie 0
      sources.push(this.vehicleService.getVehicle(this.vehicle.id));

    //zeby wyciagnac dane do formularza (dropdown listy i features checkboxy) robimy to odpalajac rownolegle requesty
    //zastepujemy oddzielne niezsynchronizowane wywolania co byly ponizej w jeden blok Observable, gdzie kolejnosc ma znaczenie > otrzymujemy obiekt ze wszystkimi rezultatami
    Observable.forkJoin(sources)
      .subscribe(data => {
        this.makes = data[0],
        this.features = data[1]
        
        if(this.vehicle.id) {
          this.setVehicle(data[2]);
          this.populateModels();              
        }
          //this.vehicle = data[2]
      }, err => {
        if (err.status == 404) {
          this.router.navigate(['/not-found']);
        }
    });

    //edycja auta
    // this.vehicleService.getVehicle(this.vehicle.id)
    //   .subscribe(v => {
    //     this.vehicle = v
    //   }/*, err => {
    //     if (err.status == 404) {
    //       this.router.navigate(['/not-found']);
    //     }
    //   }*/);

    // this.vehicleService.getMakes().subscribe(makes => { //blok kodu w {} dlatego ze jest to async wywolanie i console.log poza takim blokiem wykona sie wczesniej niz dane dojada do zmiennej, tylko po to, normlanie nie trzeba tego
    //   this.makes = makes;
    //   // console.log("Makes: ", this.makes);
    // });

    // this.vehicleService.getFeatures().subscribe(features =>
    //   this.features = features);
  }

  private setVehicle(v: Vehicle) {
      this.vehicle.id = v.id;
      this.vehicle.makeId = v.make.id;
      this.vehicle.modelId = v.model.id;
      this.vehicle.isRegistered = v.isRegistered;
      this.vehicle.contact = v.contact;
      this.vehicle.features = _.pluck(v.features, 'id'); //underscore wyciaganie id do arraya
  }
  
  onMakeChange() {
    this.populateModels();
    delete this.vehicle.modelId; //czyszczenie modelu przy zmianie marki, czysci tez formularz
  }

  private populateModels() {
    var selectedMake = this.makes.find(m => m.id == this.vehicle.makeId)
    this.models = selectedMake ? selectedMake.modele : [];
  }

  onFeatureToggle(featureId, $event) {
    if ($event.target.checked)
      this.vehicle.features.push(featureId); //push musi byn na zainicjalowanym obiekcie, temu features jest w vehiclu zadeklarowane na gorze
    else {
      var index = this.vehicle.features.indexOf(featureId);
      this.vehicle.features.splice(index, 1);
    }
  }

  submit() {
    if(this.vehicle.id) {
      this.vehicleService.update(this.vehicle)
        .subscribe(x => {
          this.toastyService.success({
            title: 'sukces',
            msg: 'vehikul zapisany',
            theme: 'bootstrap',
            showClose: true,
            timeout: 3000
          })
        });
    }
    else {
      this.vehicleService.create(this.vehicle)
      .subscribe(x => console.log(x));
    }
  }

  delete() {
    if(confirm("ryli, na pewno?")) {
      this.vehicleService.delete(this.vehicle.id)
        .subscribe(x => {
          this.router.navigate(['/list']);
        });
    }
  }

}
