ManageApp.factory('OrderService', function ($rootScope, $q, $http) {
    return {
        //登陆
        SysLogin: function (obj) {
            var d = $q.defer();
            $http({
                method: 'post',
                url: $rootScope.Url + 'api/Login/Login',
                data: obj
            }).then(function (data, status, headers, config) {
                d.resolve(data.data);
            }, function (data, status, headers, config) {
                d.reject(data.data);
            });
            return d.promise;
        },
        //生成验证码
        Creat: function () {
            var d = $q.defer();
            $http({
                method: 'get',
                url: $rootScope.Url + 'api/AuthCode/Creat'
            }).then(function (data, status, headers, config) {
                d.resolve(data.data);
            }, function (data, status, headers, config) {
                d.reject(data.data);
            });
            return d.promise;
        },
		//获取订单
		 GetAllOrder:function(obj){
            var d = $q.defer();
            $http({
                method:'POST',
                url: $rootScope.Url +'/api/Order/GetAllOrder',
                data:obj
            }).then(function (data, status, headers, config) {
                d.resolve(data.data);
            },function (data, status, headers, config) {
                d.reject(data.data);
            });
            return d.promise;
        },
		 //修改订单状态
		 Change:function(obj){
            var d = $q.defer();
            $http({
                method:'POST',
                url: $rootScope.Url +'/api/Order/ModifyOrder',
                data:obj
            }).then(function (data, status, headers, config) {
                d.resolve(data.data);
            },function (data, status, headers, config) {
                d.reject(data.data);
            });
            return d.promise;
         },
         //接单状态
         OrderReceiving: function (obj) {
             var d = $q.defer();
             $http({
                 method: 'POST',
                 url: $rootScope.Url + '/api/Order/OrderReceiving',
                 data: obj
             }).then(function (data, status, headers, config) {
                 d.resolve(data.data);
             }, function (data, status, headers, config) {
                 d.reject(data.data);
             });
             return d.promise;
         },
    }
})