import { Contact } from "./Contact";
import { KeyValuePair } from "./KeyValuePair";

//reprezentacja VehicleResource z server side, zeby byl intelisense w angularze lepiej sobie uzywac interfejsow tu
export interface Vehicle {
    id: number;
    model: KeyValuePair;
    make: KeyValuePair;
    isRegistered: boolean;
    features: KeyValuePair[];
    contact: Contact;
    lastUpdate: string;
}

