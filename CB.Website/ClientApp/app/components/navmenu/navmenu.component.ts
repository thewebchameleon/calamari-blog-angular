import { Component } from '@angular/core';

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css'],
})
export class NavMenuComponent {
    private currentYear: number;

    constructor() { }

    ngOnInit() {
        this.currentYear = new Date().getFullYear();
    }
}
