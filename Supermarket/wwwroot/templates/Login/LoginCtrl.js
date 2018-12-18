ManageApp.controller('LoginCtrl', function ($scope, $rootScope,LoginService,$state,$cookieStore){
  //生成验证码
  LoginService.Creat().then(function (data){
    $scope.img = $rootScope.Url + "api/AuthCode/Get" + "?r=" + Math.random();
  },function (response) {
  });
  //切换验证码
  $scope.ChangeCode = function(){
    LoginService.Creat().then(function (data){
      $scope.img = $rootScope.Url + "api/AuthCode/Get" + "?r=" + Math.random();
    },function (response) {
    });
  };
  //点击登陆
  $scope.Login = function(){
    LoginService.SysLogin($scope.User).then(function (data){
      $cookieStore.put('authorizationData',data);
      var strs = data.split("&");
      $cookieStore.put('SysUserId',strs[0]);
      $state.go('root.Home');
      toastr.success("登陆成功");
    },function (response) {
      $scope.ChangeCode();
      toastr.error(response);
    });
  }
});
