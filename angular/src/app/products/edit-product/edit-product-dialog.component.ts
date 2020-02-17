import { Component, Injector, Inject, OnInit, Optional } from '@angular/core';
import {
  MatDialogRef,
  MAT_DIALOG_DATA,
  MatCheckboxChange
} from '@angular/material';
import { finalize } from 'rxjs/operators';
import * as _ from 'lodash';
import { AppComponentBase } from '@shared/app-component-base';
import {
  ProductServiceProxy,  
  ProductDto,
  PermissionDto
} from '@shared/service-proxies/service-proxies';

@Component({
  templateUrl: 'edit-product-dialog.component.html',
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
export class EditProductDialogComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  product: ProductDto = new ProductDto();

  constructor(
    injector: Injector,
    private _productService: ProductServiceProxy,
    private _dialogRef: MatDialogRef<EditProductDialogComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) private _id: number
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this._productService.get(this._id).subscribe((result: ProductDto) => {
      this.product = result;
    });
  }
 
  save(): void {
    this.saving = true;
    this._productService
      .update(this.product)
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
