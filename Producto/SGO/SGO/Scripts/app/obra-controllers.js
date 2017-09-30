angular.module('SGOApp', [])
    .controller('ObraCtrl', function ($scope, $http) {
        $scope.rubro = [];
        $scope.subrubro = [];
        $scope.item = [];
        $scope.subitem = [];        
        $scope.unidad = "kg";
        $scope.entregado = "100 kg";
        $scope.aEntregar = "125 kg";
        $scope.movimientos = 10;
    });