(function(){
    'use strict';

    angular.module('CodingCraft')
            .controller('SupplierDetailsController', SupplierDetailsController);

    function SupplierDetailsController($stateParams, suppliersService, productsService){
        var supplierViewModel = this;

        var supplierId = Number($stateParams.id);
        supplierViewModel.message = "";
        supplierViewModel.error = false;
        supplierViewModel.supplier = {};
        supplierViewModel.supplierOf = [];
        supplierViewModel.productsList = [];
        supplierViewModel.addProduct = addProductToSupplier;
        supplierViewModel.updateSupplierInfo = updateSupplierInfo;

        getSupplierInfo();
        getAllProductsPerSupplier();
        getAllProducts();

        function getSupplierInfo(){
            suppliersService.get(supplierId).then(
                function(response){
                    supplierViewModel.supplier = response.data;
                }
            );
        }

        function getAllProductsPerSupplier(){
            suppliersService.getAllProductsPerSupplier(supplierId).then(
                function(response){
                    supplierViewModel.supplierOf = response.data;
                }
            );

        }

        function getAllProducts(){
            productsService.getAll().then(
                function(response){
                    supplierViewModel.productsList = response.data;
                }
            );
        }

        function addProductToSupplier(product){
            var productOfSupplier = {
                productId: product.id,
                supplierId: supplierId,
                price: product.supplierPrice
            };

            suppliersService.addProductToSupplier(productOfSupplier).then(
                function(response){
                    getAllProductsPerSupplier();
                }
            );
        }

        function updateSupplierInfo(){
            suppliersService.updateSupplier(supplierViewModel.supplier).then(
                function(response){
                    supplierViewModel.error = false;
                    supplierViewModel.message = "Supplier info updated";
                },
                function (err){
                    supplierViewModel.error = true;
                    supplierViewModel.message = err.statusText;
                }
            )
        }
    }
})();
