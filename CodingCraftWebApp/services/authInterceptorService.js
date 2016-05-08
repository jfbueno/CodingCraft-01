/*Using injector, because the use of $state gives a CircularDependencyError
Hack source -> http://stackoverflow.com/a/25496219*/
(function(){
    'use strict';
    angular.module('CodingCraft')
            .factory('authInterceptorService', authInterceptorService);

    function authInterceptorService($q, $injector, localStorageService) {

        var authInterceptorServiceFactory = {};
        var _request = function (config) {
            config.headers = config.headers || {};
            var authData = localStorageService.get('authorizationData');
            if (authData) {
                config.headers.Authorization = 'Bearer ' + authData.token;
            }
            return config;
        }

        var _responseError = function (rejection) {
            if (rejection.status === 401) {
                var stateService = $injector.get('$state');
                stateService.go('not-authorized');
            }
            return $q.reject(rejection);
        }

        authInterceptorServiceFactory.request = _request;
        authInterceptorServiceFactory.responseError = _responseError;

        return authInterceptorServiceFactory;
    };
})();
