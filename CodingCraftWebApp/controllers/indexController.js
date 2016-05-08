(function(){
    'use strict';

    angular.module('CodingCraft')
            .controller('IndexController', IndexController);

    function IndexController(authService, $state){
        var indexVm = this;

        indexVm.logOut = function () {
            authService.logOut();
            $state.go('home');
        }

        indexVm.authentication = authService.authentication;
    }
})();
