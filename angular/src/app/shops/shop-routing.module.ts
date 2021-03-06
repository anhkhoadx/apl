import { ReadProductComponent } from './../products/read-product/read-product.component';
import { ProductsComponent } from './../products/product.component';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { ReadShopComponent } from './read-shop/read-shop.component';
import { AppComponent } from './../app.component';

@NgModule({
    imports: [
        RouterModule.forChild([      
            {
                path: 'shops',
                component: AppComponent,
                children: [
                    { path: ':id', component: ReadShopComponent, data: { permission: 'Pages.Shops' }, canActivate: [AppRouteGuard] },
                    { path: ':id/products', component: ProductsComponent, data: { permission: 'Pages.Shops' }, canActivate: [AppRouteGuard] },
                    { path: ':id/products/:productId', component: ReadProductComponent, data: { permission: 'Pages.Shops' }, canActivate: [AppRouteGuard] }
                ]
            }
        ])
    ],
    exports: [RouterModule]
})

export class ShopRoutingModule { }
