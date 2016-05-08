(function () {
    'use strict';

    
    function configFunction($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.otherwise("/products");

        $stateProvider
            .state('products', {
                url: '/products',
                templateUrl: 'views/products.html'
            })
            .state('consumers', {
                url: '/consumers',
                templateUrl: 'views/consumers.html'
            });
    }
    
    angular.module('CodingCraft')
        .config(configFunction)
        .run(function ($rootScope, $state) {
            $rootScope.$state = $state;
        });
})();

