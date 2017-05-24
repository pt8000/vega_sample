import * as Raven from "raven-js";
import { ToastyService } from 'ng2-toasty';
import { ErrorHandler, Inject, NgZone, isDevMode } from "@angular/core";

export class AppErrorHandler implements ErrorHandler {

    //ToastyService jest wstrzykniety manualnie bo Klasa AppErrorHandler inicjuje sie w app.module.ts wczesniej niz Toasty i nie wie co to jest ToastyService
    constructor(
        private ngZone: NgZone,
        @Inject(ToastyService) private toastyService: ToastyService) {
    }

    handleError(error: any): void {

        if (!isDevMode())
            Raven.captureException(error.originalError || error); //zapisz do sentry.io ten error
        else
            throw error; //wywal do consoli

        //uruchomione w zonie (wykonanie po kolei mimo tego ze w js wszystko chodzi async), zobacz zony w js albo 87 lekcja
        this.ngZone.run(() => {
            this.toastyService.error({
                title: 'Blad',
                msg: 'Error jakis tam',
                theme: 'bootstrap',
                showClose: true,
                timeout: 3000
            });
        });

    }
}