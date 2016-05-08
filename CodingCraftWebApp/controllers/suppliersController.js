(function(){
    'use strict';

    angular.module('CodingCraft').controller('SuppliersController', SuppliersController);

    function SuppliersController(suppliersService){
        var suppliersViewModel = this;

        suppliersViewModel.allSuppliers = [];
        suppliersViewModel.newSupplier = {};
        suppliersViewModel.getAllSuppliers = getAllSuppliers;
        suppliersViewModel.insertNewSupplier = insertNewSupplier;

        getAllSuppliers();

        function getAllSuppliers(){
            suppliersService.getAll().then(
                function(payload){
                    suppliersViewModel.allSuppliers = payload.data;
                },
                function(errorPayload){
                    //TODO: handle errors here
                }
            );
        }

        function insertNewSupplier(){
            suppliersService.insertNew(suppliersViewModel.newSupplier).then(
                function(response){
                    if(response.status == 201){ //created
                        suppliersViewModel.allSuppliers.push(response.data);
                        suppliersViewModel.newSupplier = {};
                    }else{
                        //TODO: handle errors herer
                    }
                },
                function(errorResponse){
                    //TODO: handle errors herer
                }
            );
        }
    }
})();
