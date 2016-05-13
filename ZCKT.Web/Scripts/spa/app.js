﻿(function () {
    'use strict';

    angular.module('zckt', ['common.core', 'common.ui'])
        .config(['$routeProvider', 
            function ($routeProvider) {
                //debugger;
                $routeProvider
                    .when("/", {
                        templateUrl: "/scripts/spa/home/index.html",
                        controller: "indexCtrl"
                    })
                    //.when("/login", {
                    //    templateUrl: "scripts/spa/account/login.html",
                    //    controller: "loginCtrl"
                    //})
                    //.when("/register", {
                    //    templateUrl: "scripts/spa/account/register.html",
                    //    controller: "registerCtrl"
                    //})
                    //.when("/customers", {
                    //    templateUrl: "scripts/spa/customers/customers.html",
                    //    controller: "customersCtrl",
                    //    resolve: { isAuthenticated: isAuthenticated }
                    //})
                    //.when("/customers/register", {
                    //    templateUrl: "scripts/spa/customers/register.html",
                    //    controller: "customersRegCtrl"
                    //})
                    //.when("/movies", {
                    //    templateUrl: "scripts/spa/movies/movies.html",
                    //    controller: "moviesCtrl"
                    //})
                    //.when("/movies/add", {
                    //    templateUrl: "scripts/spa/movies/add.html",
                    //    controller: "movieAddCtrl",
                    //    resolve: { isAuthenticated: isAuthenticated }
                    //})
                    //.when("/movies/:id", {
                    //    templateUrl: "scripts/spa/movies/details.html",
                    //    controller: "movieDetailsCtrl",
                    //    resolve: { isAuthenticated: isAuthenticated }
                    //})
                    //.when("/movies/edit/:id", {
                    //    templateUrl: "scripts/spa/movies/edit.html",
                    //    controller: "movieEditCtrl"
                    //})
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
            $location.path('/login');
        }
    }

})();