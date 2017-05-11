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
  vehicle: any = {}; //empty object, jak jest typ any zadlada ze kazde pole moze w nim byc, nawet potem zdefiniowane, i mamy tez intelisense wtedy
                     //tutaj w obiekcie vehicle, dodawane jest pole make, dzieje sie to w formularzu, gdzie zapisana jest wybrana marka

  

  constructor(
    private vehicleService : VehicleService) {}

  ngOnInit() {
    this.vehicleService.getMakes().subscribe(makes => { //blok kodu w {} dlatego ze jest to async wywolanie i console.log poza takim blokiem wykona sie wczesniej niz dane dojada do zmiennej, tylko po to, normlanie nie trzeba tego
      this.makes = makes;

    this.vehicleService.getFeatures().subscribe(features => 
      this.features = features);

      // console.log("Makes: ", this.makes);
    });
  }

  onMakeChange(){
    var selectedMake = this.makes.find(m => m.id == this.vehicle.make)
    this.models = selectedMake ? selectedMake.modele : [];

    console.log("Vehicle: ", this.vehicle);
  }

}
