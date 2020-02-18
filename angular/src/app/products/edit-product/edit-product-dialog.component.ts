import { Component, Injector, Inject, OnInit, Optional, Input } from '@angular/core';
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
  PermissionDto,
  CategoryServiceProxy,
  CategoryDto,
  CategoryDtoPagedResultDto
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
  @Input() shopId: number;   
  categoryId;
  saving = false;
  product: ProductDto = new ProductDto();
  categories: CategoryDto[] = [];

  constructor(
    injector: Injector,
    private _productService: ProductServiceProxy,
    private _categoryService: CategoryServiceProxy,
    private _dialogRef: MatDialogRef<EditProductDialogComponent>,
    @Inject(MAT_DIALOG_DATA) private data: any
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this._categoryService.getAll('',  0, 50).subscribe((result: CategoryDtoPagedResultDto) => {
      this.categories = result.items;      
    });

    this._productService.get(this.data.id).subscribe((result: ProductDto) => {
      this.product = result;
      this.categoryId = result.categoryId;
    });
  }
 
  save(): void {
    this.saving = true;
    this.product.categoryId = this.categoryId;
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
