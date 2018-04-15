//employee controller
myapp.controller('Order-controller', function ($scope, SubService) {

    //save employee data
    $scope.save = function () {
        var Order = {
            Foods: $scope.Foods,
            Name: $scope.Name,
            MobileNumber: $scope.MobileNumber,
            Address: $scope.Address
  
        };
        var saverecords = SubService.save(Order);
        saverecords.then(function (d) {
            if (d.data.success === true) {
                alert("Order done, We will call you.");
                $scope.resetSave();
            }
            else { alert("Order was not  successfull."); }
        });

    }
    //reset controls after save operation
    $scope.resetSave = function () {
        $scope.Foods = '';
        $scope.Name = '';
        $scope.MobileNumber = '';
        $scope.Address = '';
    }
});






//Service to get data from employee mvc controller
myapp.service('SubService', function ($http) {

    //add new employee
    this.save = function (Order) {
        var request = $http({
            method: 'post',
            url: '/Home/SaveOrder',
            data: Order
        });
        return request;
    }
});

