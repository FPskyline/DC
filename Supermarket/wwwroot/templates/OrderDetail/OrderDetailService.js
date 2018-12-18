ManageApp.factory('OrderDetailService', function ($rootScope, $q, $http) {
    return {

		//获取订单详情
		 GetOrderDetail:function(orderId){
            var d = $q.defer();
            $http({
                method:'get',
                url: $rootScope.Url + '/api/Order/GetOrderDetail?orderId=' + orderId,
            }).then(function (data, status, headers, config) {
                d.resolve(data.data);
            },function (data, status, headers, config) {
                d.reject(data.data);
            });
            return d.promise;
        },
    }
})