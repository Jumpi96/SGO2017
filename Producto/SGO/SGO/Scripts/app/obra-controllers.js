angular.module('SGOApp', [])
    .controller('ObraCtrl', function ($scope, $http) {
        $scope.idObra = 0;
        $scope.rubros = ['Rubro 1','Rubro 2','Rubro 3'];
        $scope.subrubros = ['Subrubro 1', 'Subrubro 2', 'Subrubro 3'];
        $scope.items = ['Item 1', 'Item 2', 'Item 3'];
        $scope.subitems = ['Subitem 1', 'Subitem 2', 'Subitem 3'];        
        $scope.unidad = "kg";
        $scope.entregadoGrafico = 100;
        $scope.entregado = "100 kg";
        $scope.aEntregarGrafico = 125;
        $scope.aEntregar = "125 kg";
        $scope.movimientos = 10;
        $scope.selectedRubro = "Rubro";
        $scope.selectedSubrubro = "Subrubro";
        $scope.selectedItem = "Item";
        $scope.selectedSubitem = "Subitem";
        $scope.selectedEnPesos = true;

        $scope.cargarInfoObra = function (idObra) {
            $scope.idObra = idObra;
            var params = {
                id: idObra,
                rubro: 0,//$scope.selectedRubro,
                subrubro: 0,//$scope.selectedSubrubro,
                item: 0,//$scope.selectedItem,
                subitem: 0,//$scope.selectedSubitem,
                enPesos: $scope.selectedEnPesos ? 1 : 0
            };
            var route = params.id + "/" + params.rubro + "/" + params.subrubro
                + "/" + params.item + "/" + params.subitem + "/" + params.enPesos;
            $http.get("/api/Obras/" + route).then(function(response) {
                $scope.rubros = response.data.rubros;
                $scope.subrubros = response.data.subrubros;
                $scope.items = response.data.items;
                $scope.subitems = response.data.subitems;
                $scope.unidad = response.data.unidad;
                $scope.entregadoGrafico = response.data.entregado;
                $scope.entregado = $scope.GetMontoYUnidad($scope.entregadoGrafico,
                                                   $scope.unidad);
                $scope.aEntregarGrafico = response.data.aEntregar;
                $scope.aEntregar = $scope.GetMontoYUnidad($scope.aEntregarGrafico,
                                                   $scope.unidad);
                $scope.movimientos = response.data.movimientos;
                $scope.SetGrafico();
            });

            $scope.seleccionarRubro = function (selected) {
                $scope.selectedRubro = selected;
            };

            $scope.seleccionarSubrubro = function (selected) {
                $scope.selectedSubrubro = selected;
            };

            $scope.seleccionarItem = function (selected) {
                $scope.selectedItem = selected;
            };

            $scope.seleccionarSubitem = function (selected) {
                $scope.selectedSubitem = selected;
            };

            $scope.GetMontoYUnidad = function (numero, unidad) {
                if ($scope.selectedEnPesos) {
                    return unidad + " " + numero.toLocaleString(undefined, { maximumFractionDigits: 2 });
                } else {
                    return numero.toLocaleString(undefined, { maximumFractionDigits: 2 }) + " " + unidad;
                }
            };
            $scope.SetGrafico = function () {
                if ($scope.aEntregarGrafico <= $scope.entregadoGrafico) {
                    CrearGraficoBarras([$scope.aEntregarGrafico * 100 / $scope.entregadoGrafico, 100]);
                }
                else {
                    var data = [{ "estado": "Entregado", "valor": $scope.entregadoGrafico }, { "estado": "No entregado", "valor": $scope.aEntregarGrafico }];
                    CrearGraficoTorta(data);
                }
            };
        };
    });