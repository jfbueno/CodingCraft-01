(function(){
    'use strict';

    angular.module('CodingCraft')
            .controller('UnauthorizedPageController', UnauthorizedPageController);

    function UnauthorizedPageController(localStorageService){
        var unauthorizedViewModel = this;
        unauthorizedViewModel.message = "";
        unauthorizedViewModel.isAuth = true;

        getAuthorizationData();

        function getAuthorizationData(){
            var authData = localStorageService.get('authorizationData');
            if(authData == null){
                unauthorizedViewModel.isAuth = false;
                unauthorizedViewModel.message = "You're not logged in.";
            } else {
                unauthorizedViewModel.isAuth = true;
                unauthorizedViewModel.message = "You're not authorized to see this page.";
            }
        }
    }
})();
