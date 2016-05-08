(function(){
    'use strict';

    angular.module('CodingCraft')
            .service('purchasesService', purchasesService);

    function purchasesService($http, apiUrl){
        this.getAll = function(){
            return $http.get(apiUrl + 'purchases');
        }

        this.insertNew = function(purchase){
            return $http.post(apiUrl + 'purchases', purchase);
        }
    }

})();
