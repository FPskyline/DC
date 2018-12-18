/**
 * Created by piao on 2017/9/4.
 */
ManageApp.factory('LotteryService', function ($q, $rootScope, $http) {
    return {
        //获取奖品列表
        GetLottery: function (obj) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: $rootScope.Url + '/api/Lottery/GetLottery',
                data: obj
            }).then(function (data, status, headers, config) {
                d.resolve(data.data);
            }, function (data, status, headers, config) {
                d.reject(data.data);
            });
            return d.promise;
        },
        //新增
        CreateLottery: function (obj) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: $rootScope.Url + '/api/Lottery/CreateLottery',
                data: obj
            }).then(function (data, status, headers, config) {
                d.resolve(data.data);
            }, function (data, status, headers, config) {
                d.reject(data.data);
            });
            return d.promise;
        },
        //修改
        EditLottery: function (obj) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: $rootScope.Url + '/api/Lottery/EditLottery',
                data: obj
            }).then(function (data, status, headers, config) {
                d.resolve(data.data);
            }, function (data, status, headers, config) {
                d.reject(data.data);
            });
            return d.promise;
        },
        //删除用户
        DelLottery: function (id) {
            var d = $q.defer();
            $http({
                method: 'Delete',
                url: $rootScope.Url + '/api/Lottery/DelLottery?id=' + id,
            }).then(function (data, status, headers, config) {
                d.resolve(data.data);
            }, function (data, status, headers, config) {
                d.reject(data.data);
            });
            return d.promise;
        },

    }
});
