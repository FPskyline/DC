ManageApp.controller('SysUserCtrl', function ($scope, $rootScope, $state, SysUserService) {

    //分页器
    $scope.currentPage = 1;
    $scope.maxSize = 5;
    //获取所有商家列表
    $scope.GetSysUserList = function () {
        var obj = {
            Page: $scope.currentPage,
            Number: 15
        };
        SysUserService.GetSysUsers(obj).then(function (data) {
            $scope.SysUsers = data.bigField;
            $scope.totalItems = data.totalCount;
        }, function (response) {
            toastr.error(response);
        });
    };
    $scope.GetSysUserList();
    //添加新商家
    $scope.AddSysUser = function () {
        SysUserService.AddSysUser($scope.AddSysUserObj).then(function (data) {
            toastr.success(data);
            $scope.GetSysUserList();
        }, function (reponse) {
            toastr.error(response);
        });
    }

    //点击编辑按钮，弹出modal
    $scope.EditThisSysUser = function (SysUser) {     
        $scope.EditSysUser = SysUser;
    };
    //确定修改
    $scope.SureEditSysUser = function () {
        SysUserService.EditSysUser($scope.EditSysUser).then(function (data) {
            toastr.success(data);
            $scope.GetSysUserList();
        }, function (reponse) {
            toastr.error(response);
        });
    };
    //点击删除按钮，弹出modal
    $scope.DelThisSysUser = function (SysUser) {
        $scope.sysUserId = SysUser.sysUserId;
    };
    //确定删除
    $scope.SureDelSysUser = function () {
        SysUserService.DelSysUser($scope.sysUserId).then(function (data) {
            toastr.success(data);
            $scope.GetSysUserList();
        }, function (reponse) {
            toastr.error(response);
        });
    }
    //点击初始化按钮，弹出modal
    $scope.InitPwd = function (SysUser) {
        $scope.InitsysUserId = SysUser.sysUserId;
    }
    //确定初始化
    $scope.SureInit = function () {
        SysUserService.InitPwd($scope.InitsysUserId).then(function (data) {
            toastr.success(data);
        }, function (reponse) {
            toastr.error(response);
        });
    }
});
