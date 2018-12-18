ManageApp.factory('GoodsService', function ($q, $rootScope, $http) {
      return {
		 //获取商品大类
		 GetKind:function(obj){
            var d = $q.defer();
            $http({
                method:'POST',
                url: $rootScope.Url +'/api/Goods/GetKind',
                data:obj
            }).then(function (data, status, headers, config) {
                d.resolve(data.data);
            },function (data, status, headers, config) {
                d.reject(data.data);
            });
            return d.promise;
        },
		 //新增产品
		 PostGoods:function(obj){
            var d = $q.defer();
            $http({
                method:'POST',
                url: $rootScope.Url +'/api/Goods/PostGoods',
                data:obj
            }).then(function (data, status, headers, config) {
                d.resolve(data.data);
            },function (data, status, headers, config) {
                d.reject(data.data);
            });
            return d.promise;
        },
		//获取所有商品
		 GetGoods:function(obj){
            var d = $q.defer();
            $http({
                method:'POST',
                url: $rootScope.Url +'/api/Goods/GetGoods',
                data:obj
            }).then(function (data, status, headers, config) {
                d.resolve(data.data);
            },function (data, status, headers, config) {
                d.reject(data.data);
            });
            return d.promise;
        },
       //删除产品信息
		DeleteGoods:function(id){
            var d = $q.defer();
            $http({
                method:'Delete',
                url: $rootScope.Url + '/api/Goods/DeleteGoods?id=' + id,
            }).then(function (data, status, headers, config) {
                d.resolve(data.data);
            },function (data, status, headers, config) {
                d.reject(data.data);
            });
            return d.promise;
        },
		 //获取修改信息
          EditGoods: function (obj) {
                var d = $q.defer();
                $http({
                    method: 'POST',
                    url: $rootScope.Url + '/api/Goods/EditGoods',
                    data:obj
            }).then(function (data, status, headers, config) {
                d.resolve(data.data);
            },function (data, status, headers, config) {
                d.reject(data.data);
            });
                return d.promise;
          },
		 //确认修改信息
		 Modify:function(obj){
            var d = $q.defer();
            $http({
                method:'POST',
                url: $rootScope.Url +'/api/Goods/Modify',
                data:obj
            }).then(function (data, status, headers, config) {
                d.resolve(data.data);
            },function (data, status, headers, config) {
                d.reject(data.data);
            });
            return d.promise;
        },
        //搜索模板商品
        SearchGoods:function(name){
          var d = $q.defer();
          $http({
            method:'get',
            url: $rootScope.Url +'/api/Goods/SearchGoods?name=' + name,
          }).then(function (data, status, headers, config) {
            d.resolve(data.data);
          },function (data, status, headers, config) {
            d.reject(data.data);
          });
          return d.promise;
        },
        //添加模板商品
        PostGoodsTmp:function(obj){
          var d = $q.defer();
          $http({
            method:'post',
            url: $rootScope.Url +'/api/Goods/PostGoodsTmp',
            data: obj
          }).then(function (data, status, headers, config) {
            d.resolve(data.data);
          },function (data, status, headers, config) {
            d.reject(data.data);
          });
          return d.promise;
        },
        //获取所有商品
        SearchAllGoods:function(obj){
          var d = $q.defer();
          $http({
            method:'POST',
            url: $rootScope.Url +'/api/Goods/SearchAllGoods',
            data:obj
          }).then(function (data, status, headers, config) {
            d.resolve(data.data);
          },function (data, status, headers, config) {
            d.reject(data.data);
          });
          return d.promise;
        }
     }
	});
