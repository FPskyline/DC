ManageApp.factory('LoginService', function($rootScope,$q,$http){
  return {
    //登陆
    SysLogin: function (obj) {
      var d = $q.defer();
      $http({
        method:'post',
        url: $rootScope.Url +'api/Login/Login',
        data:obj
      }).then(function (data, status, headers, config) {
        d.resolve(data.data);
      },function (data, status, headers, config) {
        d.reject(data.data);
      });
      return d.promise;
    },
    //生成验证码
    Creat: function () {
      var d = $q.defer();
      $http({
        method:'get',
        url:$rootScope.Url + 'api/AuthCode/Creat'
      }).then(function (data, status, headers, config) {
        d.resolve(data.data);
      },function (data, status, headers, config) {
        d.reject(data.data);
      });
      return d.promise;
    }
  }

});
