import { Component, Injector, OnInit } from '@angular/core';
import { MatDialogRef, MatCheckboxChange } from '@angular/material';
import { finalize } from 'rxjs/operators';
import * as _ from 'lodash';
import { AppComponentBase } from '@shared/app-component-base';
import {
  ProductServiceProxy,
  ProductDto,
  PermissionDto,
  CreateProductDto,
} from '@shared/service-proxies/service-proxies';

@Component({
  templateUrl: 'create-product-dialog.component.html',
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
export class CreateProductDialogComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  product: ProductDto = new ProductDto();
  permissions: PermissionDto[] = [];
  grantedPermissionNames: string[] = [];
  checkedPermissionsMap: { [key: string]: boolean } = {};
  defaultPermissionCheckedStatus = true;

  constructor(
    injector: Injector,
    private _productService: ProductServiceProxy,
    private _dialogRef: MatDialogRef<CreateProductDialogComponent>
  ) {
    super(injector);
  }

  ngOnInit(): void {  
  } 

  save(): void {
    this.saving = true;
    const product_ = new CreateProductDto();
    product_.init(this.product);

    this._productService
      .create(product_)
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
