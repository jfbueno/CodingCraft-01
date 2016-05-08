(function(){
    'use strict';

    angular.module('CodingCraft')
            .controller('EditProductController', EditProductController);

    function EditProductController($stateParams, productsService){
        var productViewModel = this;
        var productId = Number($stateParams.id);


        productViewModel.message = "";
        productViewModel.error = false;
        productViewModel.product = {};
        productViewModel.saveChanges = saveChanges;
        productViewModel.showNotification = false;

        findProduct();

        function findProduct(){
            productsService.get(productId).then(
                function(response){
                    productViewModel.product = response.data;
                }
            );
        }

        function saveChanges(){
            productsService.updateProduct(productViewModel.product).then(
                function(response){
                    productViewModel.error = false;
                    productViewModel.message = 'Product edited';
                },
                function(errorResponse){
                    productViewModel.error = true;
                    productViewModel.message = errorResponse.status + ' ' + errorResponse.statusText;
                }
            );
        }
    }
})();
