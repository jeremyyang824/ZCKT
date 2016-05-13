(function (app) {
    'use strict';

    app.directive('bomTree', ['apiService', 'notificationService',
        function (apiService, notificationService) {
            return {
                restrict: 'E',
                replace: true,
                templateUrl: '/scripts/spa/directives/bomTree.html',
                scope: {
                    username: '@',
                    loadTree: '&'
                },
                link: function ($scope, iElem, iAttrs, ctrl) {

                    var $tree = $(iElem).find('.bom-tree-content');
                    var treeInitializedBefore = false;
                    var inTreeChangeEvent = false;

                    $scope.$watch('username', function () {
                        if ($scope.username == null) {
                            return;
                        }

                        buildTree();
                    });

                    function buildTree() {
                        if (treeInitializedBefore) {
                            $tree.jstree('destroy');
                        }
                        treeInitializedBefore = true;

                        apiService.get('/api/items/roots/',
                            {
                                params: {
                                    username: $scope.username
                                }
                            },
                            //success
                            function (result) {
                                var rootData = createTreeNode(result.data);
                                initializeTree(rootData);
                            },
                            //failure
                            function (response) {
                                notificationService.displayError(response.data);
                            });
                    };

                    function createTreeNode(itemData) {
                        if (!itemData)
                            return null;

                        if (Array.isArray(itemData)) {
                            return itemData.map(function (val, idx) {
                                return createTreeNode(val);
                            });
                        } else {
                            return {
                                id: itemData.Id,
                                text: itemData.FullName,
                                children: ['']
                        };
                        }
                    }

                    function initializeTree(rootData) {
                        if (!rootData || !Array.isArray(rootData))
                            return;

                        $tree.jstree({
                            "core": {
                                data: function (node, callback) {
                                    if (node.id === '#') {
                                        callback.call(this, rootData);
                                    } else {
                                        //ajax components
                                        debugger;
                                    }
                                }
                            },
                            "plugins": ['wholerow']
                        });

                        $tree.on("open_node.jstree", function (e, data) {
                            //debugger;
                        });
                    }
                },
                controller: function ($scope) {
                    $scope.doTest = function () { };
                }
            };
        }]);

})(angular.module('common.ui'));