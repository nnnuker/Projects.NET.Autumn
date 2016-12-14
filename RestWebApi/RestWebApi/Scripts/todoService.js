angular.module("Repository", [])
    .service('TodoItemsService', ['$http', function ($http) {
        return {
            getAll: function () {
                return $http.get("http://localhost:55891/Home/GetJson");
            },

            addImage: function (imageUrl) {
                return $http.post("http://localhost:55891/Home/AddImage", { imageUrl: imageUrl });
            },

            removeImage: function (removeId) {
                return $http.post("http://localhost:55891/Home/RemoveImage", { id: removeId });
            }
        };
    }]);