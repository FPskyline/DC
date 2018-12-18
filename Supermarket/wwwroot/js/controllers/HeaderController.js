ManageApp.controller('HeaderController', function ($scope, $rootScope, $state, SysUserService, $cookieStore) {
    var SysUserId = $cookieStore.get('SysUserId');
    console.log(SysUserId);
    if(SysUserId == undefined){
      toastr.error("请登陆!");
      $state.go("root.Login", {}, { reload: true });
    }
    //获取当前登录用户信息（名字）
    SysUserService.GetSysUserInfo(SysUserId).then(function (data) {
        $scope.SysUser = data;
    }, function (err) {
        toastr.error(err);
    });
    //修改密码
    $scope.SureCHangePwd = function () {
        var obj = {
            SysUserId: SysUserId,
            Pwd: $scope.ChangePwd.OldPwd,
            NewPwd: $scope.ChangePwd.NewPwd,
            SurePwd: $scope.ChangePwd.SureNewPwd
        };
        SysUserService.ChangePwd(obj).then(function (data) {
            toastr.success(data);
            $state.go("root.Login", {}, { reload: true });
        }, function (err) {
            toastr.error(err);
        });
    };
    $('#changePwd').modal({
        backdrop: false,
        show: false
    });
    //用户注销
    $scope.LogOut = function () {
        $cookieStore.remove('authorizationData');
        $cookieStore.remove('SysUserId');
        toastr.success("注销成功，请重新登陆");
        $state.go("root.Login", {}, { reload: true });
    };
    $scope.GoHome = function () {
        $state.go("root.Home", {}, { reload: true });
    }

});
