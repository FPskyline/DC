ManageApp.controller('OrderDetailCtrl', function ($scope, $rootScope, $state, $cookieStore, OrderDetailService,$stateParams) {
    var SysUserId = $cookieStore.get('SysUserId');
    var orderId = $stateParams.OrderId;
    console.log(orderId);
     //获取订单信息
    $scope.GetOrderDetail =function(){
        OrderDetailService.GetOrderDetail(orderId).then(function(data){
				 
            $scope.OrderInfos = data;
            console.log(data)
		})
	};
    $scope.GetOrderDetail();
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
            default:
                return "未付款";
        }
    }
});
