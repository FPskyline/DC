var app = angular.module('myApp', []);
app.controller('myCtrl', function($scope,$http) {
	//获取本期中奖名单
	var GetWinners = function(){
			$http({
				method: 'get',
				url: 'https://supermarket.chinacloudsites.cn/api/Winner/GetWinners'						
			}).then(function (data) {						
				$scope.WinnerLists = data.data;					
			}, function (data) {
				console.log(data.data);
			});			
	};
	GetWinners();
});