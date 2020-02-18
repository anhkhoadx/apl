import { CategoryDto, CategoryServiceProxy, CategoryDtoPagedResultDto } from './../../../shared/service-proxies/service-proxies';
import { Component, Injector, OnInit, Input, Optional, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { finalize } from 'rxjs/operators';
import * as _ from 'lodash';
import { AppComponentBase } from '@shared/app-component-base';
import {
  ProductServiceProxy,
  ProductDto,
  PermissionDto,
  CreateProductDto,
} from '@shared/service-proxies/service-proxies';
import { request } from 'http';
import { ActivatedRoute } from '@angular/router';

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
  @Input() shopId: number;
  saving = false;
  categoryId;
  product: ProductDto = new ProductDto();  
  categories: CategoryDto[] = [];

  constructor(
    injector: Injector,
    private _productService: ProductServiceProxy,
    private _categoryService: CategoryServiceProxy,
    private _activatedRoute : ActivatedRoute,
    private _dialogRef: MatDialogRef<CreateProductDialogComponent>,
    @Inject(MAT_DIALOG_DATA) private data: any
  ) {
    super(injector);
  }

  ngOnInit(): void {  
    this._categoryService.getAll('',  0, 50).subscribe((result: CategoryDtoPagedResultDto) => {
      this.categories = result.items;
    });
  } 

  save(): void {
    this.saving = true;
    const product = new CreateProductDto();
    product.init(this.product);
    product.categoryId = this.categoryId;
    product.shopId = this.data.shopId;
    this._productService
      .create(product)
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
