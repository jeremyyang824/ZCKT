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
                    selectNodeId: '@'
                },
                link: function ($scope, iElem, iAttrs, ctrl) {

                    var $tree = $(iElem).find('.bom-tree-content');
                    var $container = $tree.closest('.bomtree');

                    var treeInitializedBefore = false;
                    var inTreeChangeEvent = false;

                    $scope.$watch('username', function () {
                        if ($scope.username == null) {
                            return;
                        }
                        buildTree();
                    });

                    //监控并滚动到指定元素
                    $scope.$watch('selectNode', function (newVal, oldVal) {
                        if ($scope.selectNode == null || $scope.selectNode === '')
                            return;

                        var scrollTo = $(".bomtree .jstree li[id=" + $scope.selectNode + "]");
                        if (scrollTo.length) {
                            $container.animate({
                                scrollTop: scrollTo.offset().top - $container.offset().top + $container.scrollTop()
                            });
                        }
                    });

                    //构建/重建BOM树
                    function buildTree() {
                        if (treeInitializedBefore) {
                            $tree.jstree('destroy');
                        }
                        treeInitializedBefore = true;

                        $tree.jstree({
                            "core": {
                                data: function (node, callback) {
                                    if (node.id === '#') {
                                        loadRootData(function (data) {
                                            var rootNodes = createTreeNode(data);
                                            callback.call(this, rootNodes);
                                        });
                                    } else {
                                        loadComponentData(node.id, function (data) {
                                            var components = createTreeNode(data);
                                            callback.call(this, components);
                                        });
                                    }
                                }
                            },
                            "plugins": ['wholerow', "types"]
                        });

                        $tree.on("open_node.jstree", function (e, data) {
                            //debugger;
                        });
                        $tree.on("hover_node.jstree", function (e, data) {
                            /*
                            var nodeId = data.node.id;
                            $(".bomtree .jstree li[id=" + nodeId + "]").tooltip('show');
                            */
                        });
                        $tree.on("dehover_node.jstree", function (e, data) {
                            /*
                            var nodeId = data.node.id;
                            $(".bomtree .jstree li[id=" + nodeId + "]").tooltip('destroy');
                            */
                        });

                    };

                    //根据PartItemDto创建jsTreeNode格式
                    function createTreeNode(itemData) {
                        if (!itemData)
                            return null;

                        if (Array.isArray(itemData)) {
                            return itemData.map(function (val, idx) {
                                return createTreeNode(val);
                            });
                        } else {
                            /*
                            var itemRemark = "<div class='tooltip-align-left'>";
                            itemRemark += "国内码：" + (itemData.HomCode == null ? "" : itemData.HomCode) + "<br/>";
                            itemRemark += "国外码：" + (itemData.ItemCode == null ? "" : itemData.ItemCode) + "<br/>";
                            itemRemark += "公司码：" + (itemData.CompCode == null ? "" : itemData.CompCode) + "<br/>";
                            itemRemark += "零件名：" + (itemData.PartName == null ? "" : itemData.PartName) + "<br/>";
                            itemRemark += "</div>";
                            */
                            var itemRemark = "";
                            itemRemark += "国内码：" + (itemData.HomCode == null ? "" : itemData.HomCode) + "\r\n";
                            itemRemark += "国外码：" + (itemData.ItemCode == null ? "" : itemData.ItemCode) + "\r\n";
                            itemRemark += "公司码：" + (itemData.CompCode == null ? "" : itemData.CompCode) + "\r\n";
                            itemRemark += "零件名：" + (itemData.PartName == null ? "" : itemData.PartName) + "\r\n";

                            return {
                                id: itemData.Id,
                                text: itemData.FullName,
                                children: itemData.ChildCount > 0,
                                state: {
                                    "opened": (itemData.ChildCount > 0) && false
                                },
                                li_attr: {
                                    "title": itemRemark
                                }
                                /*
                                li_attr: {
                                    "data-toggle": "tooltip",
                                    "data-html": true,
                                    "data-placement": "top",
                                    "title": itemRemark
                                }*/
                            };
                        }
                    };

                    //根据当前用户获取根数据
                    function loadRootData(callback) {
                        apiService.get('/api/items/roots/' + $scope.username, null,
                            //success
                            function (result) {
                                if (callback && typeof callback == 'function')
                                    callback(result.data);
                            },
                            //failure
                            function (response) {
                                notificationService.displayError(response.data);
                            });
                    };

                    //根据父ID获取子项数据
                    function loadComponentData(pid, callback) {
                        if (pid == null) {
                            notificationService.displayError('pid can not be empty!');
                        }

                        apiService.get('/api/items/components/' + pid, null,
                            //success
                            function (result) {
                                if (callback && typeof callback == 'function')
                                    callback(result.data);
                            },
                            //failure
                            function (response) {
                                notificationService.displayError(response.data);
                            });
                    };

                }
            };
        }]);

})(angular.module('common.ui'));