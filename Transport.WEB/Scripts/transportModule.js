///_references.js
var transportApp = angular.module("transportApp", []);
transportApp.run(function ($rootScope, $location) {
    $rootScope.apiUrl = $location.protocol() + "://" + location.host + "/api";
    $rootScope.url = $location.protocol() + "://" + location.host + "/";
});