import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { ReadProductComponent } from './read-product/read-product.component';
import { AppComponent } from './../app.component';

@NgModule({
    imports: [
        RouterModule.forChild([      
            {
                path: 'products',
                component: AppComponent,
                children: [
                    { path: 'detail/:id', component: ReadProductComponent, data: { permission: 'Pages.Products' }, canActivate: [AppRouteGuard] }
                ]
            }
        ])
    ],
    exports: [RouterModule]
})

export class ProductRoutingModule { }
