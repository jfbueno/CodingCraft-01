(function(){
    'use strict';

    angular.module('CodingCraft')
            .controller('NewPurchaseController', NewPurchaseController);

    function NewPurchaseController(purchasesService, suppliersService){
        var newPurchaseViewModel = this;
        var _purchaseDate = new Date();
        var _paymentDate = new Date();
        _paymentDate.setMonth(_paymentDate.getMonth() + 1);
        _paymentDate.setDate(1);

        newPurchaseViewModel.totalCost = 0;
        newPurchaseViewModel.purchase = {};
        newPurchaseViewModel.purchase.purchaseDate = _purchaseDate;
        newPurchaseViewModel.purchase.payday = _paymentDate;
        newPurchaseViewModel.purchase.items = [];
        newPurchaseViewModel.purchase.supplier = {};
        newPurchaseViewModel.suppliersList = [];
        newPurchaseViewModel.productsList = [];
        newPurchaseViewModel.getProductsPerSupplier = getAllProductsPerSupplier;
        newPurchaseViewModel.addItemToPurchase = addItemToPurchase;
        newPurchaseViewModel.finishPurchase = finishPurchase;

        newPurchaseViewModel.message = "";
        newPurchaseViewModel.error = false;


        getAllSuppliers();

        function getAllSuppliers(){
            suppliersService.getAll().then(
                function(response){
                    newPurchaseViewModel.suppliersList = response.data;
                }
            );
        }

        function getAllProductsPerSupplier(){
            suppliersService.getAllProductsPerSupplier(newPurchaseViewModel.purchase.supplier.id).then(
                function(response){
                    newPurchaseViewModel.productsList = response.data;
                }
            );
        }

        function addItemToPurchase(product){
            var purchaseItem = {
                productId: product.id,
                productDescription: product.description,
                unitPrice: product.price,
                quantity: product.purchaseQuantity,
                totalCost: product.price * product.purchaseQuantity
            }

            newPurchaseViewModel.purchase.items.push(purchaseItem);
            newPurchaseViewModel.totalCost += purchaseItem.totalCost;
        }

        function finishPurchase(){
            purchasesService.insertNew(newPurchaseViewModel.purchase).then(
                function(response){
                    newPurchaseViewModel.message = 'Purchase registred';
                    newPurchaseViewModel.error = false;
                },
                function(errorResponse){
                    newPurchaseViewModel.message = errorResponse.statusText;
                    newPurchaseViewModel.error = true;
                }
            );
        }


    }
})();
