import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable()
export class UtilityService {

    private _router: Router;

    constructor(router: Router) {
        this._router = router;
    }

    navigate(path: string) {
        this._router.navigate([path]);
    }

    navigateToHome() {
        this.navigate('/');
    }

    navigateToBlog() {
        this.navigate('/blog');
    }

    removeHTMLtags(text: string) {
        return text ? String(text).replace(/<[^>]+>/gm, '').replace(/&nbsp;/gi, '').trim() : '';
    }
}