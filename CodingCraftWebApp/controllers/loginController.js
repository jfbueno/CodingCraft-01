(function(){
    'use strict';

    angular.module('CodingCraft')
            .controller('LoginController', LoginController);

    function LoginController($state, authService){
        var loginVm = this;

        loginVm.data = {
            userName: "",
            password: ""
        }

        loginVm.message = "";

        loginVm.login = function(){
            authService.login(loginVm.data).then(function (response) {
                $state.go('sales.new');
            },
             function (err) {
                 loginVm.message = err.error_description;
             });
        }
    }
})();
