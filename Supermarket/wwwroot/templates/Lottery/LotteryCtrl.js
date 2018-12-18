ManageApp.controller('LotteryCtrl', function ($scope, $state, $rootScope,LotteryService,$stateParams,$cookieStore,UserService,Upload) {
  var NewSysUserId = $cookieStore.get('SysUserId');
  //分页器
  $scope.currentPage = 1;
  $scope.maxSize = 5;

  //增加奖品
  $scope.PostLottery = function (Lottery) {
      Lottery.SysUserId = NewSysUserId;
      LotteryService.CreateLottery(Lottery).then(function (data) {
          toastr.success(data);
          $scope.Lottery.Name = "";
          $scope.Lottery.Probability = "";

          $scope.GetLottery();

      }, function (response) {
          toastr.error(response);
      });
  };
  //获取奖品列表
  $scope.GetLottery =function(){
    var obj = {
      Page: $scope.currentPage,
      Number: 15,
      Cid:NewSysUserId
    };
    LotteryService.GetLottery(obj).then(function(data){
      console.log(data);
      $scope.LotteryInfos = data.bigField;
      $scope.totalItems = data.totalCount;
    })
  };
  $scope.GetLottery();
  //删除产品大类信息
  $scope.DeleteLottery = function(id){
    $scope.DeleteId = id;
  };
  //确定删除该产品种类
  $scope.ConfirmDelete = function(){
      LotteryService.DelLottery($scope.DeleteId).then(function(data){
          $scope.GetLottery();
          toastr.success(data);
      }, function (response) {
          toastr.error(response);
      });
  };
  //获取选中产品种类信息
  $scope.EditLottery = function (LotteryInfo) {
      $scope.LotteryE = LotteryInfo;
  };
  //修改产品种类信息
  $scope.ModifyLottery = function () {
  
      LotteryService.EditLottery($scope.LotteryE).then(function (data) {
          $scope.GetLottery();
          toastr.success(data);
      }, function (response) {
          toastr.error(response);
    });
  };

  //$scope.uploadFiles = function (files) {
  //  if (files.length == 0) {
  //    return;
  //  }
  //  var files = files;
  //  Upload.upload({
  //    url: $rootScope.Url + 'api/Order/PostImg',
  //    data: { file: files }
  //  }).progress(function (evt) {

  //  }).success(function (data) {
  //    console.log(data);
  //    $scope.Img = $rootScope.Url + 'Files/Pictures/' + data;
  //    //\\\$scope.HeadImg = data;
  //  }).error(function (data) {
  //  });
  //};
  //权限处理
  $scope.ShowInfo = true;
  if(NewSysUserId == 1){
    $scope.ShowInfo = false;
  }



});
