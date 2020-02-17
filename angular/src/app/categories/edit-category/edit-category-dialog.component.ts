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
  CategoryServiceProxy,  
  CategoryDto
} from '@shared/service-proxies/service-proxies';

@Component({
  templateUrl: 'edit-category-dialog.component.html',
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
export class EditCategoryDialogComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  category: CategoryDto = new CategoryDto();

  constructor(
    injector: Injector,
    private _categoryService: CategoryServiceProxy,
    private _dialogRef: MatDialogRef<EditCategoryDialogComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) private _id: number
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this._categoryService.get(this._id).subscribe((result: CategoryDto) => {
      this.category = result;
    });
  }
 
  save(): void {
    this.saving = true;
    this._categoryService
      .update(this.category)
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
