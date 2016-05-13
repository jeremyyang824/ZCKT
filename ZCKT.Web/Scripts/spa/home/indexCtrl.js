(function (app) {
    'use strict';

    app.controller('indexCtrl', ['$scope', 'apiService', 'notificationService',
        function ($scope, apiService, notificationService) {

            $scope.loadingItems = false;

            $scope.searchConitions = [
                { name: '国外码', value: 'Content' },
                { name: '国内码', value: 'HomCode' },
                { name: '物料名', value: 'PartName' }
            ];
            $scope.searchKey = $scope.searchConitions[0].value;
            $scope.searchVal = '';

            $scope.search = search;

            function search() {
                

            };

        }]);

})(angular.module('zckt'));