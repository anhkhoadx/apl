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
  ShopServiceProxy,  
  ShopDto,
  PermissionDto
} from '@shared/service-proxies/service-proxies';

@Component({
  templateUrl: 'edit-shop-dialog.component.html',
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
export class EditShopDialogComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  shop: ShopDto = new ShopDto();

  constructor(
    injector: Injector,
    private _shopService: ShopServiceProxy,
    private _dialogRef: MatDialogRef<EditShopDialogComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) private _id: number
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this._shopService.get(this._id).subscribe((result: ShopDto) => {
      this.shop = result;
    });
  }
 
  save(): void {
    this.saving = true;
    this._shopService
      .update(this.shop)
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
