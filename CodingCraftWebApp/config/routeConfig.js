(function () {
    'use strict';

    angular.module('CodingCraft')
            .config(configFunction);

    function configFunction($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.otherwise("/home");

        $stateProvider
            .state('home', {
                url: '/home',
                templateUrl: 'views/home.html',
                controller: 'HomeController as homeVm'
            })
            .state('login', {
                url: '/login',
                templateUrl: 'views/login.html',
                controller: 'LoginController as loginVm'
            })
            .state('signup', {
                url: '/signup',
                templateUrl: 'views/signup.html',
                controller: 'signupController as signUpVm'
            })
            .state('products', {
                abstract: true,
                url: '/products',
                templateUrl: 'views/shared/content.html'
            })
            .state('products.list', {
                url: '/list',
                templateUrl: 'views/products/list.html',
                controller: 'ProductsController as productsVm'
            })
            .state('products.edit', {
                url: '/edit/:id',
                templateUrl: 'views/products/edit.html',
                controller: 'EditProductController as productViewModel'
            })
            .state('consumers', {
                url: '/consumers',
                templateUrl: 'views/consumers.html',
                controller: 'ConsumersController as consumersViewModel'
            })
            .state('suppliers', {
                abstract: true,
                url: '/suppliers',
                templateUrl: 'views/shared/content.html'
            })
            .state('suppliers.list', {
                url: '/list',
                templateUrl: 'views/suppliers/list.html',
                controller: 'SuppliersController as suppliersViewModel'
            })
            .state('suppliers.details', {
                url: '/details/:id',
                templateUrl: 'views/suppliers/details.html',
                controller: 'SupplierDetailsController as supplierViewModel'
            })
            .state('sales', {
                url: '/sales',
                abstract: true,
                templateUrl: 'views/shared/content.html'
            })
            .state('sales.list', {
                url: '/list',
                templateUrl: 'views/sales/list.html',
                controller: 'ListSalesController as salesViewModel'
            })
            .state('sales.new', {
                url: '/new',
                templateUrl: 'views/sales/new.html',
                controller: 'SaleController as saleViewModel'
            })
            .state('profit-ballast', {
                url: '/profit-ballast',
                templateUrl: 'views/profit-ballast.html',
                controller: 'ProfitBallastController as profitBallastViewModel'
            })
            .state('purchases', {
                abstract: true,
                url: '/purchases',
                templateUrl: 'views/shared/content.html'
            })
            .state('purchases.list', {
                url: '/list',
                templateUrl: 'views/purchases/list.html',
                controller: 'ListPurchasesController as purchasesViewModel'
            })
            .state('purchases.new', {
                url: '/new',
                templateUrl: 'views/purchases/new.html',
                controller: 'NewPurchaseController as newPurchaseViewModel'
            });
        }
})();
