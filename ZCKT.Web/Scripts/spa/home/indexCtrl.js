(function (app) {
    'use strict';

    app.controller('indexCtrl', ['$scope', 'apiService', 'notificationService',
        function ($scope, apiService, notificationService) {

            $scope.loadingItems = false;
            $scope.resultBarIsOpen = false;

            $scope.searchConitions = [
                { name: '国外码', value: 'Content' },
                { name: '国内码', value: 'HomCode' },
                { name: '物料名', value: 'PartName' }
            ];
            $scope.searchKey = $scope.searchConitions[0].value;
            $scope.searchVal = '';
            $scope.searchResult = [];

            $scope.selectedItem = null;

            $scope.onSearch = onSearch;
            $scope.onSelectItem = onSelectItem;
            $scope.isActived = isActived;

            function onSearch() {
                if ($scope.searchVal === '') {
                    notificationService.displayWarning('请输入查询内容！');
                    return;
                }

                //查找物料
                apiService.post('/api/items/search/' + $scope.username,
                    {
                        SearchKey: $scope.searchKey,
                        SearchValue: $scope.searchVal
                    },
                    function (result) {
                        $scope.searchResult = result.data;
                        notificationService.displayInfo('共找到 ' + $scope.searchResult.length + ' 个物料!');

                        $scope.resultBarIsOpen = true;
                    },
                    function (response) {
                        notificationService.displayError(response.data);
                    });
            };

            function onSelectItem(item) {
                $scope.selectedItem = item;
            };

            function isActived(item) {
                return $scope.selectedItem && item === $scope.selectedItem ? 'active' : '';
            };

        }]);

})(angular.module('zckt'));