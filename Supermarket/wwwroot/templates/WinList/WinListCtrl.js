ManageApp.controller('WinListCtrl', function ($scope, $state, $rootScope,WinListService,$stateParams,$cookieStore,UserService,Upload) {
  var NewSysUserId = $cookieStore.get('SysUserId');
  //分页器
  $scope.currentPage = 1;
  $scope.maxSize = 5;
  //打开增加Model
  $scope.addModal = function (){
    $scope.Kind = {
      Name:"",
      Index:"",
      SysUserId:NewSysUserId
    };
    //声明图片
    $scope.Img = " ";
  };
  //关闭增加model
  $scope.closeWareHouse = function(){
  };

  //增加商品种类信息
  $scope.PostKind = function(data){
    data.Img = $scope.Img;
    GoodsBigKindService.PostKind($scope.Kind).then(function(response){
      $scope.GetKind();
    });
  };
  //获取商品大类信息
  $scope.GetKind =function(){
    var obj = {
      Page: $scope.currentPage,
      Number: 15,
      Cid:NewSysUserId
    };
    GoodsBigKindService.GetKind(obj).then(function(data){
      console.log(data);
      $scope.KindInfos = data.bigField;
      $scope.totalItems = data.totalCount;
    })
  };
  $scope.GetKind();
  //删除产品大类信息
  $scope.DeleteGoodsKind = function(id){
    $scope.DeleteId = id;
  };
  //确定删除该产品种类
  $scope.ConfirmDelete = function(){
    GoodsBigKindService.DeleteGoodsKind($scope.DeleteId).then(function(data){
      $scope.GetKind();
    });
  };
  //获取选中产品种类信息
  $scope.EditGoodsKind = function (data) {
    $scope.Img = data.img;
    GoodsBigKindService.EditGoodsKind(data).then(function (data) {
      $scope.KindE = data;
      data.img = $scope.Img;
    });
  };
  //修改产品种类信息
  $scope.ModifyKind = function (data) {
    data.Img = $scope.Img;
    GoodsBigKindService.ModifyKind(data).then(function (data) {
      $scope.GetKind();
    }, function (err) {
    });
  };

  $scope.uploadFiles = function (files) {
    if (files.length == 0) {
      return;
    }
    var files = files;
    Upload.upload({
      url: $rootScope.Url + 'api/Order/PostImg',
      data: { file: files }
    }).progress(function (evt) {

    }).success(function (data) {
      console.log(data);
      $scope.Img = $rootScope.Url + 'Files/Pictures/' + data;
      //\\\$scope.HeadImg = data;
    }).error(function (data) {
    });
  };
  //权限处理
  $scope.ShowInfo = true;
  if(NewSysUserId == 1){
    $scope.ShowInfo = false;
  }



});
