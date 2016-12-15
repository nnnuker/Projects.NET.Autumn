angular.module("Repository", [])
    .service("TodoItemsService", ["$http", function ($http) {
        return {
            getAll: function () {
                return $http.get("/api/Items");
            },

            getById: function(id) {
                return $http.get("/api/Items/" + id);
            },

            addItem: function (item) {
                return $http.post("/api/Items/", item);
            },

            removeItem: function (id) {
                return $http.delete("/api/Items/" + id);
            },

            updateItem: function (id, item) {
                return $http.put("/api/Items/" + id, item);
        }
        };
    }]);