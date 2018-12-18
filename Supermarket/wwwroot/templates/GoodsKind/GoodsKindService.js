ManageApp.factory('GoodsKindService', function ($q, $rootScope, $http) {
      return {
		 //获取用户
		 GetKind:function(obj){
            var d = $q.defer();
            $http({
                method:'POST',
                url: $rootScope.Url +'/api/GoodsKind/GetKind',
                data:obj
            }).then(function (data, status, headers, config) {
                d.resolve(data.data);
            },function (data, status, headers, config) {
                d.reject(data.data);
            });
            return d.promise;
        },
		 //新增
		 PostKind:function(obj){
            var d = $q.defer();
            $http({
                method:'POST',
                url: $rootScope.Url +'/api/GoodsKind/PostKind',
                data:obj
            }).then(function (data, status, headers, config) {
                d.resolve(data.data);
            },function (data, status, headers, config) {
                d.reject(data.data);
            });
            return d.promise;
        },
		//获取修改信息
          EditGoodsKind: function (obj) {
                var d = $q.defer();
                $http({
                    method: 'POST',
                    url: $rootScope.Url + '/api/GoodsKind/EditGoodsKind',
                    data:obj
            }).then(function (data, status, headers, config) {
                d.resolve(data.data);
            },function (data, status, headers, config) {
                d.reject(data.data);
            });
                return d.promise;
            },
		 //确认修改信息
		 ModifyKind:function(obj){
            var d = $q.defer();
            $http({
                method:'POST',
                url: $rootScope.Url +'/api/GoodsKind/ModifyKind',
                data:obj
            }).then(function (data, status, headers, config) {
                d.resolve(data.data);
            },function (data, status, headers, config) {
                d.reject(data.data);
            });
            return d.promise;
        },
	    //删除用户
		DeleteGoodsKind:function(id){
            var d = $q.defer();
            $http({
                method:'Delete',
                url: $rootScope.Url + '/api/GoodsKind/DeleteGoodsKind?id=' + id,
            }).then(function (data, status, headers, config) {
                d.resolve(data.data);
            },function (data, status, headers, config) {
                d.reject(data.data);
            });
            return d.promise;
        },
        //获取大类
        GetBigKind:function(id){
          var d = $q.defer();
          $http({
            method:'get',
            url: $rootScope.Url + '/api/GoodsKind/GetBigKind?sysuserid=' + id,
          }).then(function (data, status, headers, config) {
            d.resolve(data.data);
          },function (data, status, headers, config) {
            d.reject(data.data);
          });
          return d.promise;
        }

     }
	});
