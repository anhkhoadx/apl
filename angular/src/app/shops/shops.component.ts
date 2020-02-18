import { Component, Injector } from '@angular/core';
import { MatDialog } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import {
    PagedListingComponentBase,
    PagedRequestDto
} from '@shared/paged-listing-component-base';
import {
    ShopServiceProxy,
    ShopDto,
    ShopDtoPagedResultDto
} from '@shared/service-proxies/service-proxies';
import { CreateShopDialogComponent } from './create-shop/create-shop-dialog.component';
import { EditShopDialogComponent } from './edit-shop/edit-shop-dialog.component';
import { Router } from '@angular/router';

class PagedShopsRequestDto extends PagedRequestDto {
    keyword: string;
}

@Component({
    templateUrl: './shops.component.html',
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
export class ShopsComponent extends PagedListingComponentBase<ShopDto> {
    shops: ShopDto[] = [];

    keyword = '';

    constructor(
        injector: Injector,
        private _shopsService: ShopServiceProxy,
        private _dialog: MatDialog,
        private _router: Router
    ) {
        super(injector);
    }

    list(
        request: PagedShopsRequestDto,
        pageNumber: number,
        finishedCallback: Function
    ): void {
        request.keyword = this.keyword;

        this._shopsService
            .getAll(request.keyword, request.skipCount, request.maxResultCount)
            .pipe(
                finalize(() => {
                    finishedCallback();
                })
            )
            .subscribe((result: ShopDtoPagedResultDto) => {
                this.shops = result.items;
                this.showPaging(result, pageNumber);
            });
    }

    delete(shop: ShopDto): void {
        abp.message.confirm(
            'Do you want to delete shop \'' + shop.name + '\'',
            undefined,
            (result: boolean) => {
                if (result) {
                    this._shopsService
                        .delete(shop.id)
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

    addProduct(shop: ShopDto): void {
        this.showCreateOrEditShopDialog(shop.id);
    }

    createShop(): void {
        this.showCreateOrEditShopDialog();
    }

    editShop(shop: ShopDto): void {
        this.showCreateOrEditShopDialog(shop.id);
    }

    showCreateOrEditShopDialog(id?: number): void {
        let createOrEditShopDialog;
        if (id === undefined || id <= 0) {
            createOrEditShopDialog = this._dialog.open(CreateShopDialogComponent);
        } else {
            createOrEditShopDialog = this._dialog.open(EditShopDialogComponent, {
                data: id
            });
        }

        createOrEditShopDialog.afterClosed().subscribe(result => {
            if (result) {
                this.refresh();
            }
        });
    }
}
