(function () {
    'use strict';

    angular
    .module('CodingCraft')
    .service('consumersService', consumersService);

    function consumersService($http, apiUrl) {
        this.getAllConsumers = function() {
            return $http.get(apiUrl + 'api/consumers');
        }

        this.insertNewConsumer = function (consumer) {
            return $http.post(apiUrl + 'api/consumers', consumer);
        }

        this.deleteConsumer = function(consumer) {
            return $http.delete(apiUrl + 'api/consumer/' + consumer.id);
        }
    }
}());
