//employee controller
myapp.controller('Subscribe-controller', function ($scope, SubService) {

    //save employee data
    $scope.save = function () {
        var Subscriber = {
            FirstName: $scope.FirstName,
            LastName: $scope.LastName,
            Email: $scope.Email,
            Location: $scope.Location
  
        };
        var saverecords = SubService.save(Subscriber);
        saverecords.then(function (d) {
            if (d.data.success === true) {
                alert("Subscription successfull.");
                $scope.resetSave();
            }
            else { alert("Subscription was not  successfull."); }
        });

    }
    //reset controls after save operation
    $scope.resetSave = function () {
        $scope.FirstName = '';
        $scope.LastName = '';
        $scope.Email = '';

    }
});






//Service to get data from employee mvc controller
myapp.service('SubService', function ($http) {

    //add new employee
    this.save = function (Subscriber) {
        var request = $http({
            method: 'post',
            url: '/Home/Insert',
            data: Subscriber
        });
        return request;
    }
});

