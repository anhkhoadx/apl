import { Component, Injector } from '@angular/core';
import { MatDialog } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import {
    PagedListingComponentBase,
    PagedRequestDto
} from '@shared/paged-listing-component-base';
import {
    ProductServiceProxy,
    ProductDto,
    ProductDtoPagedResultDto
} from '@shared/service-proxies/service-proxies';
import { CreateProductDialogComponent } from './create-product/create-product-dialog.component';
import { EditProductDialogComponent } from './edit-product/edit-product-dialog.component';
import { Router, ActivatedRoute } from '@angular/router';

class PagedProductsRequestDto extends PagedRequestDto {
    keyword: string;
    shopId: number;
}

@Component({
    templateUrl: './product.component.html',
    animations: [appModuleAnimation()],
    styles: [
        `
          mat-form-field {
            padding: 10px;
          }
          `
    ],
    styleUrls: [
        '../app.component.css',        
    ]
})
export class ProductsComponent extends PagedListingComponentBase<ProductDto> {
    products: ProductDto[] = [];

    keyword = '';   
    shopId;

    constructor(
        injector: Injector,
        private _productsService: ProductServiceProxy,
        private _dialog: MatDialog,
        private _router: Router,
        private _activatedRoute : ActivatedRoute
    ) {
        super(injector);
    }

    list(
        request: PagedProductsRequestDto,
        pageNumber: number,
        finishedCallback: Function
    ): void {
        request.keyword = this.keyword;
        this.shopId = this._activatedRoute.snapshot.params['id'];
        request.shopId = this.shopId;
        
        this._productsService
            .getAll(request.keyword, request.shopId, request.skipCount, request.maxResultCount)
            .pipe(
                finalize(() => {
                    finishedCallback();
                })
            )
            .subscribe((result: ProductDtoPagedResultDto) => {
                this.products = result.items;
                this.showPaging(result, pageNumber);
            });
    }

    delete(product: ProductDto): void {
        abp.message.confirm(
            'Do you want to delete product \'' + product.name + '\'',
            undefined,
            (result: boolean) => {
                if (result) {
                    this._productsService
                        .delete(product.id)
                        .pipe(
                            finalize(() => {
                                abp.notify.success(this.l('SuccessfullyDeleted'));
                                this.refresh();
                            })
                        )
                        .subscribe(() => { });
                }
            }
        );
    }

    createProduct(): void {
        this.showCreateOrEditProductDialog();
    }

    editProduct(product: ProductDto): void {
        this.showCreateOrEditProductDialog(product.id);
    }

    readProduct(product: ProductDto): void {
        this._router.navigate(['read', product.id]);
    }

    showCreateOrEditProductDialog(id?: number): void {
        let createOrEditProductDialog;
        if (id === undefined || id <= 0) {
            createOrEditProductDialog = this._dialog.open(CreateProductDialogComponent, {
                data: {
                    shopId: this.shopId
                }
            });
        } else {
            createOrEditProductDialog = this._dialog.open(EditProductDialogComponent, {
                data: {
                    shopId: this.shopId,
                    id
                }
            });
        }

        createOrEditProductDialog.afterClosed().subscribe(result => {
            if (result) {
                this.refresh();
            }
        });
    }
}
