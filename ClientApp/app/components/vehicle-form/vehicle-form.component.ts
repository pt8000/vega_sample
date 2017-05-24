import { ToastyModule, ToastyService } from 'ng2-toasty';
import { VehicleService } from './../../services/vehicle.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {

  makes: any[]; //init variable
  models: any[];
  features: any[];
  vehicle: any = {
    features: [],
    contact: {}
  }; //empty object, jak jest typ any zadlada ze kazde pole moze w nim byc, nawet potem zdefiniowane, i mamy tez intelisense wtedy
  //tutaj w obiekcie vehicle, dodawane jest pole make, dzieje sie to w formularzu, gdzie zapisana jest wybrana marka



  constructor(
    private vehicleService: VehicleService,
    private toastyService: ToastyService) { }

  ngOnInit() {
    this.vehicleService.getMakes().subscribe(makes => { //blok kodu w {} dlatego ze jest to async wywolanie i console.log poza takim blokiem wykona sie wczesniej niz dane dojada do zmiennej, tylko po to, normlanie nie trzeba tego
      this.makes = makes;
      // console.log("Makes: ", this.makes);
    });

    this.vehicleService.getFeatures().subscribe(features =>
      this.features = features);

  }

  onMakeChange() {
    var selectedMake = this.makes.find(m => m.id == this.vehicle.makeId)
    this.models = selectedMake ? selectedMake.modele : [];

    delete this.vehicle.modelId; //czyszczenie modelu przy zmianie marki

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
    this.vehicleService.create(this.vehicle)
      .subscribe(x => console.log(x));
  }
}
