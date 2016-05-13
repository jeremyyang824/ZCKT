(function (app) {
    'use strict';

    app.directive('topBar', [function () {
        return {
            restrict: 'E',
            replace: true,
            templateUrl: '/scripts/spa/directives/topBar.html'
        };
    }]);

})(angular.module('common.ui'));