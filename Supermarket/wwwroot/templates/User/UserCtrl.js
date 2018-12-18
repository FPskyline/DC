 ManageApp.controller('UserCtrl', function ($scope, $state,$rootScope,UserService,$stateParams) {
    //分页器
    $scope.currentPage = 1;
    $scope.maxSize = 5;
 
 
   //打开增加Model
     $scope.addModal = function () {
         $scope.User = { Name:"",};
    }
	//关闭增加model
	$scope.closeWareHouse = function(){
	}
    //用户信息初始化
	$scope.User = {
		name:"",
	};
 //获取用户信息   
   $scope.GetUser =function(){
	     var obj = {
            Page: $scope.currentPage,
            Number: 15
        };
		UserService.GetUser(obj).then(function(data){
            $scope.UserInfos = data.bigField;
           // $scope.UserInfo.name = UserInfo.name.Replace(memberDto.Account, @"\p{Cs}", "");
            $scope.totalItems = data.totalCount;
		})
	};
    $scope.GetUser();
	//删除用户信息
	$scope.DeleteUser = function(id){
		$scope.DeleteId = id;
	};
	//确定删除该用户
	$scope.confirmDelete = function(){
		UserService.DeleteUser($scope.DeleteId).then(function(data){
			$scope.GetUser(); 		
		});
	};
 

});