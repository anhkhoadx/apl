import { Component, OnInit, Injector } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import { ShopDto, ShopServiceProxy } from '@shared/service-proxies/service-proxies';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormControl, FormGroupDirective, NgForm } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material';

export class FormGroupErrorStateMatcher implements ErrorStateMatcher {
    constructor(private formGroup: FormGroup) { }

    public isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
        return control && control.dirty && control.touched && this.formGroup && this.formGroup.errors && this.formGroup.errors.areEqual;
    }
}

@Component({
    animations: [appModuleAnimation()],
    templateUrl: './read-shop.component.html'
})
export class ReadShopComponent extends AppComponentBase implements OnInit {

    shop: ShopDto = new ShopDto();
    id;
    public isLoading: boolean;

    public constructor(
        injector: Injector,
        private _shopService: ShopServiceProxy,
        private _activatedRoute : ActivatedRoute,
        private _router: Router
    ) {
        super(injector);
    }

    public ngOnInit() {
        this.isLoading = true;
        this.id =this._activatedRoute.snapshot.params['id']; 
        this._shopService.get(this.id).subscribe((result: ShopDto) => {
            this.shop = result;
          });
       
        this.doneLoading();
    }

    back(): void {
       this._router.navigate(['app/shops']);
    }

    private doneLoading(): void {
        this.isLoading = false;
    }
}
