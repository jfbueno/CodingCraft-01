(function(){
    'use strict';

    angular.module('CodingCraft')
            .controller('signupController', function ($state, $timeout, authService) {

        var signUpVm = this;

        signUpVm.savedSuccessfully = false;
        signUpVm.message = "";

        signUpVm.registration = {
            userName: "",
            password: "",
            confirmPassword: "",
            email: ""
        };

        signUpVm.signUp = function () {
            authService.saveRegistration(signUpVm.registration).then(function (response) {
                signUpVm.savedSuccessfully = true;
                signUpVm.message = "User has been registered successfully, you will be redicted to login page in 2 seconds.";
                startTimer();
            },
             function (response) {
                 var errors = [];
                 for (var key in response.data.modelState) {
                     for (var i = 0; i < response.data.modelState[key].length; i++) {
                         errors.push(response.data.modelState[key][i]);
                     }
                 }
                 signUpVm.message = "Failed to register user due to:" + errors.join(' ');
             });
        };

        var startTimer = function () {
            var timer = $timeout(function () {
                $timeout.cancel(timer);
                $state.go('login');
            }, 2000);
        }
    });
})();
