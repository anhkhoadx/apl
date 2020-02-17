import { Component, Injector, OnInit } from '@angular/core';
import { MatDialogRef, MatCheckboxChange } from '@angular/material';
import { finalize } from 'rxjs/operators';
import * as _ from 'lodash';
import { AppComponentBase } from '@shared/app-component-base';
import {
  CategoryServiceProxy,
  CategoryDto,
  PermissionDto
} from '@shared/service-proxies/service-proxies';

@Component({
  templateUrl: 'create-category-dialog.component.html',
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
export class CreateCategoryDialogComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  category: CategoryDto = new CategoryDto();
  permissions: PermissionDto[] = [];
  grantedPermissionNames: string[] = [];
  checkedPermissionsMap: { [key: string]: boolean } = {};
  defaultPermissionCheckedStatus = true;

  constructor(
    injector: Injector,
    private _categoryService: CategoryServiceProxy,
    private _dialogRef: MatDialogRef<CreateCategoryDialogComponent>
  ) {
    super(injector);
  }

  ngOnInit(): void {  
  } 

  save(): void {
    this.saving = true;
    const _category = new CategoryDto();
    _category.init(this.category);

    this._categoryService
      .create(_category)
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
