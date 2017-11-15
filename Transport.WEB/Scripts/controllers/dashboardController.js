'use strict'
transportApp.controller('dashboardController', function ($rootScope, $scope, $timeout, $http) {
    $scope.orders = [];
    $http.get($rootScope.apiUrl + '/order/getOrders').then(function (response) {
        $scope.orders = response.data;
    })

    $scope.details = function (index) {
        location.assign($rootScope.url + 'Home/Details/'+$scope.orders[index].orderId)
    }
});