(function(){
    'use strict';

    angular.module('CodingCraft')
            .service('salesService', salesService);

    function salesService($http, apiUrl){
        this.newSale = function(sale){
            return $http.post(apiUrl + 'sales', sale);
        }

        this.getAll = function(){
            return $http.get(apiUrl + 'sales');
        }

        this.sendPaymentReminders = function(){
            return $http.post(apiUrl + 'sales/send-payment-reminders');
        }

        this.getProfitBallastOfCurrentMonth = function(){
            return $http.get(apiUrl + 'sales/profit-ballast-details')
        }
    }
})();
