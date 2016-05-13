(function (app) {
    'use strict';

    app.controller('rootCtrl', ['$scope', '$location', 'membershipService',
        function ($scope, $location, membershipService) {

            $scope.userData = {};

            $scope.logout = function () {
                membershipService.removeCredentials();
                $location.path('#/');
                $scope.displayUserInfo();
            };

            $scope.displayUserInfo = function () {
                $scope.userData.isUserLoggedIn = membershipService.isUserLoggedIn();
                if ($scope.userData.isUserLoggedIn) {
                    $scope.username = membershipService.getUserData().username;
                }
            };

            $scope.displayUserInfo();
        }]);

})(angular.module('zckt'));