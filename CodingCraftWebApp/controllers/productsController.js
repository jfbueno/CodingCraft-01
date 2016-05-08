(function(){
    'use strict';

    angular.module('CodingCraft')
            .controller('ProductsController', ProductsController);


    function ProductsController(productsService){
        var productsViewModel = this;

        productsViewModel.newProduct = {};
        productsViewModel.allProducts = [];
        productsViewModel.getAllProducts = getAllProducts;
        productsViewModel.insertNewProduct = insertNewProduct;

        getAllProducts();

        function getAllProducts(){
            productsService.getAll().then(
                function(payload){
                    productsViewModel.allProducts = payload.data;
                }, function(errorResponse){
                    //TODO: HANDLE ERRORS HERE
                }
            );
        }

        function insertNewProduct(){
            productsService.insertNew(productsViewModel.newProduct).then(
                function(response){
                    if(response.status == 201){//201 -> created
                        productsViewModel.allProducts.push(response.data);
                        productsViewModel.newProduct = {};
                    }
                },
                function(errorResponse){
                    //TODO: HANDLE ERRORS HERE
                }
            );

        }
    }
})();
