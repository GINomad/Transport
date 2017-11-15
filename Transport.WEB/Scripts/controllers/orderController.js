'use strict'
transportApp.controller('orderController', function ($rootScope, $scope, $timeout, $http) {
    $scope.cities = [];
    $scope.formModel = {
        fromCity: 0,
        toCity: 0,
        fromAddress: '',
        toAddress: '',
        fromDate: '',
        toDate: '',
        weight: 0,
        height: 0,
        width: 0,
        length: 0,
        name: ''

    }
    $http.get($rootScope.apiUrl + '/city/cities').then(function (response) {
        $scope.cities = response.data;
    })

    $scope.createOrder = function (form) {
        form.$submitted = true;

        if (form.$valid) {
            $http.post($rootScope.apiUrl + '/order/createOrder', $scope.formModel).then(function (response) {
                if (response.data) {
                    form.$setPristine();
                    $scope.formModel = {};
                }
            })
        }
    }

    
});