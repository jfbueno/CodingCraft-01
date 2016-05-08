(function(){
    'use strict';

    angular.module('CodingCraft')
            .controller('ListSalesController', ListSalesController);

    function ListSalesController(salesService){
        var salesViewModel = this;

        salesViewModel.sales = [];
        salesViewModel.sendPaymentReminders = sendPaymentReminders;

        getAllSales();

        function getAllSales(){
            salesService.getAll().then(
                function(response){
                    salesViewModel.sales = response.data;
                }
            );
        }

        function sendPaymentReminders(){
            salesService.sendPaymentReminders().then(
                function(response){
                    if(response.status == 204){

                    }
                }
            )
        }
    }
})();
