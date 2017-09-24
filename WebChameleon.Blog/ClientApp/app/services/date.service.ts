import { Injectable } from '@angular/core';

@Injectable()
export class DateService {
    private currentYear: number = new Date().getFullYear();

    getCurrentYear() {
        return this.currentYear;
    }
}