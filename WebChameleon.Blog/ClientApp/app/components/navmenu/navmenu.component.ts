import { Component } from '@angular/core';
import { DateService } from "../../services/date.service";

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css'],
    providers: [DateService]
})
export class NavMenuComponent {
    private currentYear: number;

    constructor(private service: DateService) { }

    ngOnInit() {
        this.currentYear = this.service.getCurrentYear();
    }
}
