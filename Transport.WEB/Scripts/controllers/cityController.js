'use strict'
transportApp.controller('cityController', function ($rootScope, $scope, $timeout, $http) {
    $scope.cities = [];
    $scope.formModel = {
        from: null,
        to: '',
        distance: ''
    }
    $scope.alreadyExist = false;
    $http.get($rootScope.apiUrl + '/city/cities').then(function (response) {
        $scope.cities = response.data;
    })

    var _checkDistance = function () {
         return $http.get($rootScope.apiUrl + '/city/checkDistance?from=' + $scope.formModel.from + '&to=' + $scope.formModel.to);
    }

    $scope.setDistance = function (form) {
        form.$submitted = true;

        if (form.$valid) {
            var p = $scope.checkDistance();
                p.then(function (response) {
                if (response.data) {
                    $scope.alreadyExist = true;
                }
                else {
                    $scope.alreadyExist = false;
                    $http.post($rootScope.apiUrl + '/city/setDistance', $scope.formModel).then(function (response) {
                        if (response.data) {
                            form.$setPristine();
                            $scope.formModel = {};
                        }
                    })
                }
            })
        }
    }
    $scope.checkDistance = _checkDistance;
});