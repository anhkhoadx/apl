import { Component, OnInit, Injector } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import { ProductDto, ProductServiceProxy, CategoryDto, CategoryServiceProxy } from '@shared/service-proxies/service-proxies';
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
    templateUrl: './read-product.component.html'
})
export class ReadProductComponent extends AppComponentBase implements OnInit {
    product: ProductDto = new ProductDto();
    id;
    shopId;
    category: CategoryDto = new CategoryDto();
    public isLoading: boolean;

    public constructor(
        injector: Injector,
        private _productService: ProductServiceProxy,
        private _categoryService: CategoryServiceProxy,
        private _activatedRoute : ActivatedRoute,
        private _router: Router
    ) {
        super(injector);
    }

    public ngOnInit() {
        this.isLoading = true;
        this.id =this._activatedRoute.snapshot.params['productId']; 
        this.shopId =this._activatedRoute.snapshot.params['id']; 

        this._productService.get(this.id).subscribe((result: ProductDto) => {
            this.product = result;
            this._categoryService.get(result.categoryId).subscribe((subResult: CategoryDto) => {
                this.category = subResult;
              });
          });
       
        this.doneLoading();
    }

    showProducts(): void {
        this._router.navigate([`app/shops/${this.shopId}/products`]);
    }

    private doneLoading(): void {
        this.isLoading = false;
    }
}
