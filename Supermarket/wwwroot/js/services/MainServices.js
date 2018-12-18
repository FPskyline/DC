ManageApp.factory('MainServices', function($http,$q){
		return {
			GetWeChat: function (obj) {
				var d = $q.defer();
				$http({
					method: 'GET',
					url:""+obj
				}).success(function (data, status, headers, config) {
					d.resolve(data);
				}).error(function (data, status, headers, config) {
					d.reject(data);
				});
				return d.promise;
			}
		};
	}
);