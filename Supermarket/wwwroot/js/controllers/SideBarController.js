/**
 * Created by zhisheng on 2017/4/18.
 */
ManageApp.controller('SideBarController', function($scope,$rootScope,$state,$cookieStore){
  var SysUserId = $cookieStore.get('SysUserId');
  $scope.SysUserShow = false;
  $scope.UserShow = false;
  if (SysUserId == 1){
    $scope.SysUserShow = true;
    $scope.UserShow = true;
  }
});
