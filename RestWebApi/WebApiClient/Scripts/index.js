angular.module("TodoApp", ["Repository"])
        .controller("TodoController", ["$scope", "TodoItemsService", function ($scope, service) {

        $scope.items = [];
        $scope.title = "";
        $scope.author = "";
        $scope.body = "";

        initialize();

        $scope.addItem = function() {
            var obj = {
                Title: $scope.title,
                Author: $scope.author,
                Body: $scope.body,
                Created: Date.now(),
                Id: 0
            };

            clearParams();

            service.addItem(obj).then(function(response) {
                $scope.items.push(cast(response.data));
            });
        };

        $scope.removeItem = function (index, item) {
            service.removeItem(item.Id);
            $scope.items.splice(index, 1);
        };

        $scope.updateItem = function (item) {
            service.updateItem(item);
        };

        function clearParams() {
            $scope.author = "";
            $scope.body = "";
            $scope.title = "";
        }

        function cast(item) {
            return {
                Id: item.Id,
                Title: item.Title,
                Body: item.Body,
                Author: item.Author,
                Created: new Date(item.Created).toDateString()
            }
        }

        function initialize() {
            service.getAll().then(function (response) {
                $scope.items = [];

                if (response.data.length !== 0) {
                    for (var i = 0; i < response.data.length; i++) {
                        var obj = cast(response.data[i]);
                        $scope.items.push(obj);
                    }
                }
            });

        };


    }]);

