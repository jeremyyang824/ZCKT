(function (app) {
    'use strict';
    app.factory('apiService',
        ['$http', '$location', 'notificationService', '$rootScope',
        function apiService($http, $location, notificationService, $rootScope) {
            return {
                get: get,
                post: post
            };

            function get(url, config, success, failure) {
                return $http.get(url, config)
                    .then(function (result) {
                        success(result);
                    })
                    .catch(function (error) {
                        debugger;
                        if (error.status === '401') {
                            notificationService.displayError('Authentication required.');
                            $rootScope.previousState = $location.path();
                            $location.path('/login');
                        } else if (failure != null) {
                            failure(error);
                        }
                    });
            };

            function post(url, data, success, failure) {
                return $http.post(url, data)
                    .then(function (result) {
                        success(result);
                    })
                    .catch(function (error) {
                        debugger;
                        if (error.status === '401') {
                            notificationService.displayError('Authentication required.');
                            $rootScope.previousState = $location.path();
                            $location.path('/login');
                        } else if (failure != null) {
                            failure(error);
                        }
                    });
            };
        }]);

})(angular.module('common.core'));