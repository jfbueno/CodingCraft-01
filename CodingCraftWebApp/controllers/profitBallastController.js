(function(){
    'use strict';

    angular.module('CodingCraft')
            .controller('ProfitBallastController', ProfitBallastController);

    function ProfitBallastController(salesService){
        var profitBallastViewModel = this;

        profitBallastViewModel.data = {};

        getData();

        function getData(){
            salesService.getProfitBallastOfCurrentMonth().then(
                function(response){
                    profitBallastViewModel.data = response.data;
                }
            );
        }
    }
})();
