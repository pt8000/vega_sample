import { ToastyService } from 'ng2-toasty';
import { ErrorHandler, Inject, NgZone } from "@angular/core";

export class AppErrorHandler implements ErrorHandler {
    
    //ToastyService jest wstrzykniety manualnie bo Klasa AppErrorHandler inicjuje sie w app.module.ts wczesniej niz Toasty i nie wie co to jest ToastyService
    constructor(
        private ngZone: NgZone,
        @Inject(ToastyService) private toastyService: ToastyService) {
    }

    handleError(error: any): void {        
        console.log("error test");
        //uruchomione w zonie, zobacz zony w js albo 87 lekcja
        this.ngZone.run( () => { 
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