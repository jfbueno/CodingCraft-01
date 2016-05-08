(function() {
    'use strict';

    angular
        .module('CodingCraft', ['ui.router', 'LocalStorageModule', 'growlNotifications'])
        .constant('apiUrl', 'http://localhost:60806/api/')
        .constant('apiBaseUrl', 'http://localhost:60806/')
        .config(function($httpProvider){
            $httpProvider.interceptors.push('authInterceptorService');
        })
        .run(function(authService){
            authService.fillAuthData();
        });
})();
