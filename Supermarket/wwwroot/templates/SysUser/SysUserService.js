ManageApp.factory('SysUserService', function ($rootScope, $q, $http) {
    return {
        //获取商家
        GetSysUsers: function (obj) {
            var d = $q.defer();
            $http({
                method: 'post',
                url: $rootScope.Url + 'api/SysUser/GetSysUsers',
                data: obj
            }).then(function (data, status, headers, config) {
                d.resolve(data.data);
            }, function (data, status, headers, config) {
                d.reject(data.data);
            });
            return d.promise;
        },
        //新增商家
        AddSysUser: function (obj) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: $rootScope.Url + 'api/SysUser/AddSysUser',
                data: obj
            }).then(function (data, status, headers, config) {
                d.resolve(data.data);
            }, function (data, status, headers, config) {
                d.reject(data.data);
            });
            return d.promise;
        },
        //修改商家信息
        EditSysUser: function (obj) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: $rootScope.Url + 'api/SysUser/EditSysUser',
                data: obj
            }).then(function (data, status, headers, config) {
                d.resolve(data.data);
            }, function (data, status, headers, config) {
                d.reject(data.data);
            });
            return d.promise;
        },
        //删除角色
        DelSysUser: function (id) {
            var d = $q.defer();
            $http({
                method: 'delete',
                url: $rootScope.Url + 'api/SysUser/DelSysUser?id=' + id
            }).then(function (data, status, headers, config) {
                d.resolve(data.data);
            }, function (data, status, headers, config) {
                d.reject(data.data);
            });
            return d.promise;
        },
        //初始化密码
        InitPwd: function(id) {
            var d = $q.defer();
            $http({
                method: 'put',
                url: $rootScope.Url + 'api/SysUser/InitPwd?id=' + id
            }).then(function (data, status, headers, config) {
                d.resolve(data.data);
            }, function (data, status, headers, config) {
                d.reject(data.data);
            });
            return d.promise;
        },
        //获取某一商家信息
        GetSysUserInfo: function (id) {
            var d = $q.defer();
            $http({
                method: 'get',
                url: $rootScope.Url + 'api/SysUser/GetSysUserInfo?id=' + id
            }).then(function (data, status, headers, config) {
                d.resolve(data.data);
            }, function (data, status, headers, config) {
                d.reject(data.data);
            });
            return d.promise;
        },
        //商家修改密码
        ChangePwd: function (obj) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: $rootScope.Url + 'api/SysUser/ChangePwd',
                data: obj
            }).then(function (data, status, headers, config) {
                d.resolve(data.data);
            }, function (data, status, headers, config) {
                d.reject(data.data);
            });
            return d.promise;
        }
    }

});
