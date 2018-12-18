/**
 * Created by piao on 2017/9/4.
 */
ManageApp.factory('WinListService', function ($q, $rootScope, $http) {
  return {
    //获取用户
    GetKind:function(obj){
      var d = $q.defer();
      $http({
        method:'POST',
        url: $rootScope.Url +'/api/GoodsBigKind/GetKind',
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
        url: $rootScope.Url +'/api/GoodsBigKind/PostKind',
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
        url: $rootScope.Url + '/api/GoodsBigKind/EditGoodsKind',
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
        url: $rootScope.Url +'/api/GoodsBigKind/ModifyKind',
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
        url: $rootScope.Url + '/api/GoodsBigKind/DeleteGoodsKind?id=' + id,
      }).then(function (data, status, headers, config) {
        d.resolve(data.data);
      },function (data, status, headers, config) {
        d.reject(data.data);
      });
      return d.promise;
    },

  }
});
