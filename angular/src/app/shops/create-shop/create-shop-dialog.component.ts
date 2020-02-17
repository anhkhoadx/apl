import { Component, Injector, OnInit } from '@angular/core';
import { MatDialogRef, MatCheckboxChange } from '@angular/material';
import { finalize } from 'rxjs/operators';
import * as _ from 'lodash';
import { AppComponentBase } from '@shared/app-component-base';
import {
  ShopServiceProxy,
  ShopDto,
  PermissionDto,
  CreateShopDto,
} from '@shared/service-proxies/service-proxies';

@Component({
  templateUrl: 'create-shop-dialog.component.html',
  styles: [
    `
      mat-form-field {
        width: 100%;
      }
      mat-checkbox {
        padding-bottom: 5px;
      }
    `
  ]
})
export class CreateShopDialogComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  shop: ShopDto = new ShopDto();
  permissions: PermissionDto[] = [];
  grantedPermissionNames: string[] = [];
  checkedPermissionsMap: { [key: string]: boolean } = {};
  defaultPermissionCheckedStatus = true;

  constructor(
    injector: Injector,
    private _shopService: ShopServiceProxy,
    private _dialogRef: MatDialogRef<CreateShopDialogComponent>
  ) {
    super(injector);
  }

  ngOnInit(): void {  
  } 

  save(): void {
    this.saving = true;
    const shop_ = new CreateShopDto();
    shop_.init(this.shop);

    this._shopService
      .create(shop_)
      .pipe(
        finalize(() => {
          this.saving = false;
        })
      )
      .subscribe(() => {
        this.notify.info(this.l('SavedSuccessfully'));
        this.close(true);
      });
  }

  close(result: any): void {
    this._dialogRef.close(result);
  }
}
