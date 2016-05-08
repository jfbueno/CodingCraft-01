(function () {
    'use strict';

    angular.module('CodingCraft')
            .service('productsService', productsService);

    function productsService($http, apiUrl) {
        this.getAll = function () {
            return $http.get(apiUrl + 'products');
        }

        this.get = function(id){
            return $http.get(apiUrl + 'products/' + id);
        }

        this.insertNew = function (product) {
            return $http.post(apiUrl + 'products', product);
        }

        this.updateProduct = function(product){
            return $http.put(apiUrl + 'products/' + product.id, product);
        }

        this.deleteProduct = function (product) {
            return $http.delete(apiUrl + 'product/' + product.id);
        }
    }
}());
