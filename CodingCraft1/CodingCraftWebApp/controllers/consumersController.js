'use strict';

function ConsumersController(consumersService) {
    var consumersVm = this;
    consumersVm.findAll = findAllConsumers();

    findAllConsumers();

    function findAllConsumers(){
        consumersService.getAllConsumers().then(
            function (payload) {
                consumersVm.allConsumers = payload.data;
            },
        function(errorPayload) {
            console.log(errorPayload);
        });
    }
}


angular.module('CodingCraft').controller('ConsumersController', ConsumersController);