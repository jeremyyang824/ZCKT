(function (app) {
    'use strict';

    app.directive('collapseBar', [
        function () {
            return {
                restrict: 'E',
                replace: true,
                transclude: true,
                template: '<div class="resultRegion">'
                        + '<div class="resultRegion-content" ng-transclude></div>'
                        + '<div class="resultRegion-toggler" ng-click="toggle()"><i class="fa fa-chevron-left fa-fw"></i></div>'
                        + '</div>',
                scope: {
                    isOpen: '='
                },
                link: function ($scope, iElem, iAttrs) {

                    var $toggler = $(iElem).find('.resultRegion-toggler');
                    var $content = $toggler.prev('.resultRegion-content');
                    var $container = $toggler.parent();

                    $scope.$watch('isOpen', function () {
                        if ($scope.isOpen == null) {
                            return;
                        }
                        toggleCollapse();
                    });

                    $scope.toggle = function() {
                        $scope.isOpen = !$scope.isOpen;
                        toggleCollapse();
                    };

                    function toggleCollapse() {
                        if ($scope.isOpen) {
                            $toggler.removeClass('resultRegion-toggler-close').addClass('resultRegion-toggler-open');
                            $toggler.children('i.fa').removeClass('fa-chevron-right').addClass('fa-chevron-left');
                            $content.removeClass('resultRegion-content-close').addClass('resultRegion-content-open');
                            $container.removeClass('resultRegion-close').addClass('resultRegion-open');
                        } else {
                            $toggler.removeClass('resultRegion-toggler-open').addClass('resultRegion-toggler-close');
                            $toggler.children('i.fa').removeClass('fa-chevron-left').addClass('fa-chevron-right');
                            $content.removeClass('resultRegion-content-open').addClass('resultRegion-content-close');
                            $container.removeClass('resultRegion-open').addClass('resultRegion-close');
                        }
                    }
                }
            }
        }]);

})(angular.module('common.ui'));