'use strict';

function NewConsumerController(consumersService) {
    this.consumer = {};

    this.insertNew = function () {
        consumersService.insertNewConsumer(this.consumer);
    }
}

angular.module('CodingCraft').controller('NewConsumerController', NewConsumerController);