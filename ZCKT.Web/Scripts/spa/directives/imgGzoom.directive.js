(function (app) {
    'use strict';

    app.directive('imgGzoom', [
        function () {
            return {
                restrict: 'E',
                replace: true,
                template: '<div class="zoom">'
                        + '<img class="zoom-image" alt="零件图片" />'
                        + '</div>',
                scope: {
                    src: '@'
                },
                link: function ($scope, iElem, iAttrs) {

                    var initializedBefore = false;
                    var $zoom = $(iElem);
                    var $img = $zoom.find('.zoom-image');

                    $scope.$watch('src', function () {
                        if ($scope.src == null || $scope.src === '') {
                            return;
                        }

                        if (initializedBefore) {
                            $zoom.trigger('zoom.destroy');
                        }
                        initializedBefore = true;

                        $img.attr('src', $scope.src);
                        $zoom.zoom({
                            url: $scope.src,
                            callback: function () {
                                autoResize($img);
                            }
                        });
                    });

                    function autoResize($image) {
                        debugger;
                        var maxw = $zoom.parent().parent().width() - 12,
                            maxh = $zoom.parent().height(),
                            w = $image.width(),
                            h = $image.height();
                        var wRatio = maxw / w;
                        var hRatio = maxh / h;
                        var ratio = wRatio <= hRatio ? wRatio : hRatio;

                        if (ratio < 1) {
                            $image.width(w * ratio);
                            $image.height(h * ratio);
                        }
                    };
                }
            };
        }]);
})(angular.module('common.ui'));