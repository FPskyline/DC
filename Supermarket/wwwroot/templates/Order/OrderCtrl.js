ManageApp.controller('OrderCtrl', function ($scope, $rootScope, $state, $cookieStore, Upload,OrderService) {
    var SysUserId = $cookieStore.get('SysUserId');
    //分页器
    $scope.currentPage = 1;
    $scope.maxSize = 5;
     //获取订单信息
    $scope.GetAllOrder =function(){
	     var obj = {
            Page: $scope.currentPage,
            Number: 15,
			Cid:SysUserId
        };
		OrderService.GetAllOrder(obj).then(function(data){
				 
			  $scope.OrderInfos = data.bigField;
              $scope.totalItems = data.totalCount;
		})
	};
	$scope.GetAllOrder();
   //修改支付状态
    $scope.Modify =function(){
        OrderService.Change($scope.ModifyObj).then(function(data){
			$scope.GetAllOrder(); 		
		})
	 };
    $scope.Img = "";
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
  if(SysUserId == 1){
    $scope.ShowInfo = false;
  }
  //处理付款状态显示
  $scope.PayState = function (state) {
      switch (state) {
          case 1:
              return "未付款";
          case 2:
              return "已付款";
          case 3:
              return "申请退款";
          case 4:
              return "已退款";
          case 5:
              return "已发货";
          case 6:
              return "交易完成";
          case 7:
              return "已接单";
          case 8:
              return "已拒绝";
          default:
              return "未付款";
      }
  }
  //点击编辑按钮
  $scope.Change = function (obj) {
      $scope.ModifyObj = obj;
  }
  //跳转详情
  $scope.GoDetail = function (OrderInfo) {
      $state.go('root.OrderDetail', {OrderId:OrderInfo.orderId})
  }
  //接单
  $scope.OrderReceiving = function (obj, state) {
      obj.PayState = state;
      OrderService.OrderReceiving(obj).then(function (data) {
          $scope.GetAllOrder();
      })
  }
});
