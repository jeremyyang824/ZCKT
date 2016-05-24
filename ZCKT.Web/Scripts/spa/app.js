(function () {
    'use strict';

    angular.module('zckt', ['common.core', 'common.ui'])
        .config(['$routeProvider', 
            function ($routeProvider) {
                //debugger;
                $routeProvider
                    .when("/", {
                        templateUrl: "/scripts/spa/home/index.html",
                        controller: "indexCtrl",
                        resolve: { isAuthenticated: isAuthenticated }
                    })
                    .when("/unauthenticated", {
                        templateUrl: "/scripts/spa/home/unauthenticated.html"
                    })
                    .otherwise({ redirectTo: "/" });
            }])
        .run(['$rootScope', '$location', '$cookieStore', '$http',
            function run($rootScope, $location, $cookieStore, $http) {
                //debugger;
                //当页面刷新时，从cookie恢复用户信息
                $rootScope.userData = $cookieStore.get('userData') || {};
                if ($rootScope.userData.authdata) {
                    $http.defaults.headers.common['Authorization'] = $rootScope.userData.authdata;
                };
            }]);


    //检测是否有登录信息
    isAuthenticated.$inject = ['membershipService', '$rootScope', '$location'];
    function isAuthenticated(membershipService, $rootScope, $location) {
        if (!membershipService.isUserLoggedIn()) {
            $rootScope.previousState = $location.path();
            $location.path('/unauthenticated');
        }
    }

})();