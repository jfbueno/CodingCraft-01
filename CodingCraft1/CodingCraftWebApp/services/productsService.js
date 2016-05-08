(function () {
    'use strict';

    angular
    .module('CodingCraft')
    .service('productsService', productsService);

    function productsService($http, apiUrl) {
        this.getAllProducts = function () {
            return $http.get(apiUrl + 'api/products');
        }

        this.insertNewProduct = function (product) {
            return $http.post(apiUrl + 'api/products', product);
        }

        this.deleteProduct = function (product) {
            return $http.delete(apiUrl + 'api/product/' + product.id);
        }
    }
}());
