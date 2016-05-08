(function(){
    'use strict';

    angular.module('CodingCraft')
            .service('suppliersService', suppliersService);

    function suppliersService($http, apiUrl){
        this.getAll = function () {
            return $http.get(apiUrl + 'suppliers');
        }

        this.get = function (supplierId) {
            return $http.get(apiUrl + 'suppliers/' + supplierId);
        }

        this.insertNew = function (supplier) {
            return $http.post(apiUrl + 'suppliers', supplier);
        }

        this.updateSupplier = function (supplier) {
            return $http.put(apiUrl + 'suppliers/' + supplier.id, supplier);
        }

        this.deleteSupplier = function (supplier) {
            return $http.delete(apiUrl + 'suppliers/' + supplier.id);
        }

        this.getAllProductsPerSupplier = function(supplierId){
            return $http.get(apiUrl + 'suppliers/' + supplierId + '/products');
        }

        this.addProductToSupplier = function(product){
            return $http.post(apiUrl + 'productspersupplier/', product);
        }
    }
})();
