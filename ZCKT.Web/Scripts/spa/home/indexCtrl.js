(function (app) {
    'use strict';

    app.controller('indexCtrl', ['$scope', 'apiService', 'notificationService',
        function ($scope, apiService, notificationService) {

            $scope.loadingItems = false;
            $scope.resultBarIsOpen = false; //是否打开查询结果栏

            $scope.searchConitions = [
                { name: '国外码', value: 'Content' },
                { name: '国内码', value: 'HomCode' },
                { name: '公司码', value: 'CompCode' },
                { name: '物料名', value: 'PartName' }
            ];
            $scope.searchKey = $scope.searchConitions[0].value;
            $scope.searchVal = '2521105009';
            $scope.searchResult = [];
            $scope.searchResultIdHints = [];  //查询结果在BOM树上的路径Id（打开哪些树节点）
            $scope.searchResultIds = [];

            $scope.selectedItem = null; //查询结果栏中选中的物料
            $scope.targetItem = null;   //显示该物料的图片及明细
            $scope.scrollToId = null;   //Tree滚动到的物料

            $scope.onSearch = onSearch;
            $scope.onSelectItem = onSelectItem;
            $scope.isActived = isActived;
            $scope.onSelectTreeNode = onSelectTreeNode;
            $scope.onCheckHint = onCheckHint;

            function onSearch() {
                if ($scope.searchVal === '') {
                    notificationService.displayWarning('请输入查询内容！');
                    return;
                }

                //查找物料
                $scope.targetItem = null;
                $scope.loadingItems = true;
                apiService.post('/api/items/search/' + $scope.username,
                    {
                        SearchKey: $scope.searchKey,
                        SearchValue: $scope.searchVal
                    },
                    function (result) {
                        var dataList = result.data;
                        $scope.searchResult = dataList;

                        if ($scope.searchResult.length >= 50)
                            notificationService.displayWarning('最多显示50个查询结果!');
                        else
                            notificationService.displayInfo('共找到 ' + $scope.searchResult.length + ' 个物料!');

                        $scope.resultBarIsOpen = true;
                        $scope.loadingItems = false;

                        //构建树及展开节点
                        $scope.searchResultIdHints = buildSearchResultIdHints();
                        $scope.searchResultIds = buildSearchResultIds();
                    },
                    function (response) {
                        $scope.loadingItems = false;
                        notificationService.displayError(response.data);
                    });
            };

            function onSelectItem(item) {
                $scope.selectedItem = item;
                if ($scope.selectedItem != null) {
                    $scope.scrollToId = $scope.selectedItem.Id;
                }
            };

            function isActived(item) {
                return $scope.selectedItem && item === $scope.selectedItem ? 'active' : '';
            };

            function onSelectTreeNode(id) {
                if (id != null && id !== '') {
                    apiService.get('/api/items/details/' + id, null,
                        function (result) {
                            $scope.targetItem = result.data;
                        },
                        function (response) {
                            notificationService.displayError(response.data);
                        });
                }
            };

            function onCheckHint(hint, item) {
                $scope.selectedItem = item;
                if (hint != null) {
                    $scope.scrollToId = hint.Id;
                }
                return false;
            }

            function buildSearchResultIdHints() {
                var result = [], hash = {};
                //iter items
                $scope.searchResult.forEach(function (val) {
                    if (val && val.Hints && Array.isArray(val.Hints)) {
                        //iter item hints
                        val.Hints.forEach(function (hint) {
                            if (!hash[hint.Id]) {
                                result.push(hint.Id);
                                hash[hint.Id] = true;
                            }
                        });
                    }
                });
                return result;
            };

            function buildSearchResultIds() {
                var result = [];
                $scope.searchResult.forEach(function (val) {
                    if (val && val.Id) {
                        result.push(val.Id);
                    }
                });
                return result;
            };

        }]);

})(angular.module('zckt'));