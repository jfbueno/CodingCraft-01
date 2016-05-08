(function(){
    'use strict';

    angular.module('CodingCraft')
            .controller('ListPurchasesController', ListPurchasesController);

    function ListPurchasesController(purchasesService){
        var purchasesViewModel = this;

        purchasesViewModel.purchases = [];

        getAllPurchases();

        function getAllPurchases(){
            purchasesService.getAll().then(
                function(response){
                    purchasesViewModel.purchases = response.data;
                }
            );
        }
    }
})();
