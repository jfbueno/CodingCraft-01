(function(){
    'use strict';

    angular.module('CodingCraft')
            .controller('SaleController', SaleController);

    function SaleController(productsService, salesService, localStorageService){
        var saleViewModel = this;

        saleViewModel.consumersList = [];
        saleViewModel.productsList = [];

        saleViewModel.sale = {};
        saleViewModel.sale.items = [];
        saleViewModel.totalCost = 0;

        saleViewModel.addProduct = addProductToSale;
        saleViewModel.finishSale = finishSale;

        saleViewModel.message = "";
        saleViewModel.error = false;

        getAllProducts();

        function getAllProducts(){
            productsService.getAll().then(
                function(response){
                    saleViewModel.productsList = response.data;
                },
                function(errorResponse){

                }
            );
        }

        function addProductToSale(product){
            var saleItem = {
                productId: product.id,
                description: product.description,
                quantity: product.saleQuantity,
                totalCost: product.salePrice * product.saleQuantity
            };

            saleViewModel.sale.items.push(saleItem);
            saleViewModel.totalCost += saleItem.totalCost;
        }

        function finishSale(){
            salesService.newSale(saleViewModel.sale).then(
                function(response){
                    console.log(response);
                    saleViewModel.error = false;
                    saleViewModel.message = "Enjoy your snack =)";
                },
                function(errorResponse){
                    saleViewModel.error = true;
                    saleViewModel.message = "Something went wrong.\n" + errorResponse.statusText;
                }
            );
        }
    }
})();
