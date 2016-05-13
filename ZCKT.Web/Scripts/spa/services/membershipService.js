(function (app) {
    'use strict';

    app.factory('membershipService',
        ['apiService', 'notificationService', '$http', '$base64', '$cookieStore', '$rootScope',
        function (apiService, notificationService, $http, $base64, $cookieStore, $rootScope) {
            return {
                login: login,
                saveCredentials: saveCredentials,
                removeCredentials: removeCredentials,
                isUserLoggedIn: isUserLoggedIn,
                getUserData: getUserData
            };

            function login(loginDto, completed) {
                //apiService.post('/api/account/authenticate', loginDto,
                //    completed,
                //    function (response) {
                //        notificationService.displayError(response.data);
                //    });
            };

            function saveCredentials(user) {
                var membershipData = $base64.encode(user.username + ':' + user.password);
                $rootScope.userData = {
                    username: user.username,
                    authdata: membershipData,
                    islogged: true
                };
                $http.defaults.headers.common['Authorization'] = 'Basic ' + membershipData;
                $cookieStore.put('userData', $rootScope.userData);
            };

            function removeCredentials() {
                $rootScope.userData = {};
                $cookieStore.remove('repository');
                $http.defaults.headers.common.Authorization = '';
            };

            function isUserLoggedIn() {
                return !!$rootScope.userData.islogged;
            };

            function getUserData() {
                if (!isUserLoggedIn())
                    return null;
                return $rootScope.userData;
            }

        }]);


})(angular.module('common.core'));