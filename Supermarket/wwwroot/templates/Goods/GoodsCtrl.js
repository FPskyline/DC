 ManageApp.controller('GoodsCtrl', function ($scope, $state,$rootScope,GoodsService,$stateParams,$cookieStore,Upload,GoodsKindService) {
     var SysUserId = $cookieStore.get('SysUserId');
	//分页器
    $scope.currentPage = 1;
    $scope.maxSize = 5;
	//筛选器
/*	$scope.Kinds = [];
	$scope.Kinds[0] = "请选择";*/
	//$scope.SelectValue = 0;
	//监听select值变化
	$scope.SelectChange = function(){
		$scope.SelectValue = document.getElementById('Select_teamwork').value;
		 var obj = {
            Page: $scope.currentPage,
            Number: 10,
            GoodsKindId:$scope.SelectValue,
            Cid:SysUserId
        };
        GoodsService.GetGoods(obj).then(function(data){
			$scope.GoodsInfos = data.bigField;
            $scope.totalItems = data.totalCount;
		})
	};
	//获取select选项
    $scope.KindSelect =function(){
	     var obj = {
            Page: $scope.currentPage,
            Number: 999999,
			      Cid:SysUserId
        };
		GoodsKindService.GetKind(obj).then(function(data){
			$scope.Kinds = data.bigField;
       $scope.totalItems = data.totalCount;
		})
	};
	$scope.KindSelect();

   //打开增加Model
     $scope.addModal = function (){
         $scope.Goods = {
			 Name:"",
			 SysUserId:SysUserId,
			 GoodsKindId:""
		 };
		 //声明图片
         $scope.Img = " ";
         $scope.Img1 = " ";
    };
		//关闭增加model
	$scope.closeWareHouse = function(){
	};
    //增加商品具体信息
	$scope.PostGoods = function(data){
        data.Img = $scope.Img;
        data.Comment2 = $scope.Img1;
		GoodsService.PostGoods($scope.Goods).then(function(response){
           $scope.GetGoods();
		});
	};
   //获取商品大类信息
    $scope.GetKind =function(){
	     var obj = {
            Page: $scope.currentPage,
            Number: 999999,
			Cid:SysUserId
        };
		GoodsKindService.GetKind(obj).then(function(data){
			$scope.KindInfos = data.bigField;
            $scope.totalItems = data.totalCount;
		})
	};
	$scope.GetKind();
	//获取所有商品信息
    $scope.GetGoods =function(){
	     var obj = {
            Page: $scope.currentPage,
            Number: 15,
			Cid:SysUserId
        };
		GoodsService.GetGoods(obj).then(function(data){
			$scope.GoodsInfos = data.bigField;
            $scope.totalItems = data.totalCount;
		})
	};
	$scope.GetGoods();
	//删除产品信息
	$scope.DeleteGoods = function(id){
		$scope.DeleteId = id;
	};
	//确定删除该产品种类
	$scope.ConfirmDelete = function(){
		GoodsService.DeleteGoods($scope.DeleteId).then(function(data){
			$scope.GetGoods();
		});
	};

	//获取选中产品信息
     $scope.EditGoods = function (data) {
         $scope.Img = data.img;
         $scope.Img1 = data.comment2;
         GoodsService.EditGoods(data).then(function (data) {
             $scope.KindE = data;
             console.log($scope.KindE,"$scope.KindE");
             data.img = $scope.Img;
             data.comment2 = $scope.Img1;
         });
     };
     //修改产品信息
     $scope.Modify = function (data) {
         data.Img = $scope.Img;
         data.Comment2 = $scope.Img1;
         GoodsService.Modify(data).then(function (data) {
             $scope.GetGoods();
        }, function (err) {
        });
     };

	  //文件上传
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
      //文件上传详情图片
      $scope.uploadFiles1 = function (files) {
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
              $scope.Img1 = $rootScope.Url + 'Files/Pictures/' + data;
              //\\\$scope.HeadImg = data;
          }).error(function (data) {
          });
      };
   //权限处理
   $scope.ShowInfo = true;
   if(SysUserId == 1){
     $scope.ShowInfo = false;
   }
   //搜索模板商品
   $scope.SearchGoods = function (SearchContent) {
     GoodsService.SearchGoods(SearchContent).then(function (data) {
       $scope.SearchGoodsLists = data;
       console.log(data);
     },function (err) {

     })
   }
   //模板全选初始化
   $scope.AllChecked = false;
   //全选按钮
   $scope.ChenkAll = function () {
     if($scope.AllChecked == true){
       for(var i=0;i<$scope.SearchGoodsLists.length;i++){
         $scope.SearchGoodsLists[i].isChecked = true;
       }
     }else{
       for(var i=0;i<$scope.SearchGoodsLists.length;i++){
         $scope.SearchGoodsLists[i].isChecked = false;
       }
     }
   }
   //点击单一商品，判断是否全选
   $scope.IsChenkALl = function () {
     var num = 0;
     for(var i=0;i<$scope.SearchGoodsLists.length;i++){
      if($scope.SearchGoodsLists[i].isChecked == true){
        num++;
      }
     }
     if(num == $scope.SearchGoodsLists.length){
       $scope.AllChecked = true;
     }
     else{
       $scope.AllChecked = false;
     }
   };
   //添加模板商品
   $scope.AddTmpGoods = function (SelectTmpValue) {
     var TmpGoodsList = [];
     if(SelectTmpValue == 0){
       toastr.warning("请选择种类！");
       return;
     }
     for(var i = 0 ; i < $scope.SearchGoodsLists.length ; i++){
       if($scope.SearchGoodsLists[i].isChecked == true){
         $scope.SearchGoodsLists[i].sysUserId = SysUserId;
         $scope.SearchGoodsLists[i].goodsKindId = SelectTmpValue;
         TmpGoodsList.push($scope.SearchGoodsLists[i]);
       }
     }
     GoodsService.PostGoodsTmp(TmpGoodsList).then(function (data) {
       toastr.success(data);
       $scope.GetGoods();
     },function (err) {
       toastr.error(err);
     })
   };
   //搜索所有商品
   $scope.SearchAllGoods = function (SearchGoodsName) {
     var obj = {
       Page: $scope.currentPage,
       Number: 15,
       Cid:SysUserId,
       SearchName:SearchGoodsName
     };
     GoodsService.SearchAllGoods(obj).then(function(data){
       $scope.GoodsInfos = data.bigField;
       $scope.totalItems = data.totalCount;
     })
   }
});
