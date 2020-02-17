import { Component, Injector } from '@angular/core';
import { MatDialog } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import {
    PagedListingComponentBase,
    PagedRequestDto
} from '@shared/paged-listing-component-base';
import {
    CategoryServiceProxy,
    CategoryDto,
    CategoryDtoPagedResultDto
} from '@shared/service-proxies/service-proxies';
import { CreateCategoryDialogComponent } from './create-category/create-category-dialog.component';
import { EditCategoryDialogComponent } from './edit-category/edit-category-dialog.component';
import { Router } from '@angular/router';

class PagedCategoriesRequestDto extends PagedRequestDto {
    keyword: string;
}

@Component({
    templateUrl: './categories.component.html',
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
export class CategoriesComponent extends PagedListingComponentBase<CategoryDto> {
    categories: CategoryDto[] = [];

    keyword = '';

    constructor(
        injector: Injector,
        private _categoryService: CategoryServiceProxy,
        private _dialog: MatDialog,
        private _router: Router
    ) {
        super(injector);
    }

    list(
        request: PagedCategoriesRequestDto,
        pageNumber: number,
        finishedCallback: Function
    ): void {
        request.keyword = this.keyword;

        this._categoryService
            .getAll(request.keyword, request.skipCount, request.maxResultCount)
            .pipe(
                finalize(() => {
                    finishedCallback();
                })
            )
            .subscribe((result: CategoryDtoPagedResultDto) => {
                this.categories = result.items;
                this.showPaging(result, pageNumber);
            });
    }

    delete(category: CategoryDto): void {
        abp.message.confirm(
            'Do you want to delete category \'' + category.name + '\'',
            undefined,
            (result: boolean) => {
                if (result) {
                    this._categoryService
                        .delete(category.id)
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

    createCategory(): void {
        this.showCreateOrEditCategoryDialog();
    }

    editCategory(category: CategoryDto): void {
        this.showCreateOrEditCategoryDialog(category.id);
    }

    showCreateOrEditCategoryDialog(id?: number): void {
        let createOrEditCategoryDialog;
        if (id === undefined || id <= 0) {
            createOrEditCategoryDialog = this._dialog.open(CreateCategoryDialogComponent);
        } else {
            createOrEditCategoryDialog = this._dialog.open(EditCategoryDialogComponent, {
                data: id
            });
        }

        createOrEditCategoryDialog.afterClosed().subscribe(result => {
            if (result) {
                this.refresh();
            }
        });
    }
}
