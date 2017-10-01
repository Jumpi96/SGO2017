angular.module('SGOApp', [])
    .controller('ObraCtrl', function ($scope, $http) {
        $scope.rubros = ['Rubro 1','Rubro 2','Rubro 3'];
        $scope.subrubros = ['Subrubro 1', 'Subrubro 2', 'Subrubro 3'];
        $scope.items = ['Item 1', 'Item 2', 'Item 3'];
        $scope.subitems = ['Subitem 1', 'Subitem 2', 'Subitem 3'];        
        $scope.unidad = "kg";
        $scope.entregado = "100 kg";
        $scope.aEntregar = "125 kg";
        $scope.movimientos = 10;
        $scope.selectedRubro = "Rubro";
        $scope.selectedSubrubro = "Subrubro";
        $scope.selectedItem = "Item";
        $scope.selectedSubitem = "Subitem";

        $scope.cargarInfoObra = function () {
            $http.get("/api/obras/9/0/0/0/0").then(function(response) {
                $scope.rubros = response.data.rubros;
                $scope.subrubros = response.data.subrubros;
                $scope.items = response.data.items;
                $scope.subitems = response.data.subitems;
                $scope.unidad = response.data.unidad;
                $scope.entregado = response.data.entregado;
                $scope.aEntregar = response.data.aEntregar;
                $scope.movimientos = response.data.movimientos;
            });

            $scope.seleccionarSubrubro = function (selected) {
                $scope.selectedSubrubro = selected;
            }

            $scope.seleccionarRubro = function (selected) {
                $scope.selectedRubro = selected;
            }

            $scope.seleccionarItem = function (selected) {
                $scope.selectedItem = selected;
            }

            $scope.seleccionarSubitem = function (selected) {
                $scope.selectedSubitem = selected;
            }
        };
    });