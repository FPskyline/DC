ManageApp.factory('UserService', function ($q, $rootScope, $http) {
      return {
		 //获取用户
		 GetUser:function(obj){
            var d = $q.defer();
            $http({
                method:'POST',
                url: $rootScope.Url +'/api/User/GetUser',
                data:obj
            }).then(function (data, status, headers, config) {
                d.resolve(data.data);
            },function (data, status, headers, config) {
                d.reject(data.data);
            });
            return d.promise;
        },
		 //获取编辑回显内容
		 EditUser:function(obj){
            var d = $q.defer();
            $http({
                method:'POST',
                url: $rootScope.Url +'/api/User/EditUser',
                data:obj
            }).then(function (data, status, headers, config) {
                d.resolve(data.data);
            },function (data, status, headers, config) {
                d.reject(data.data);
            });
            return d.promise;
        },
		 //确认修改信息
		 ModifyUser:function(obj){
            var d = $q.defer();
            $http({
                method:'POST',
                url: $rootScope.Url +'/api/User/ModifyUser',
                data:obj
            }).then(function (data, status, headers, config) {
                d.resolve(data.data);
            },function (data, status, headers, config) {
                d.reject(data.data);
            });
            return d.promise;
        },
	    //删除用户
		DeleteUser:function(id){
            var d = $q.defer();
            $http({
                method:'Delete',
                url: $rootScope.Url + '/api/User/DeleteUser?id=' + id
            }).then(function (data, status, headers, config) {
                d.resolve(data.data);
            },function (data, status, headers, config) {
                d.reject(data.data);
            });
            return d.promise;
        },

     }
	});