var ManageApp= angular.module('ManageApp', ['ui.router', 'ui.bootstrap','ngCookies','ngFileUpload'])
  .run(['$rootScope', '$state', '$stateParams', function ($rootScope, $state, $stateParams,$sceDelegateProvider) {
     //$rootScope.Url = "http://localhost:6129/";
     $rootScope.Url = "http://supermarket.chinacloudsites.cn/";

	  //定义通知插件
    toastr.options = {
      "closeButton": false,
      "debug": false,
      "newestOnTop": false,
      "progressBar": false,
      "positionClass": "toast-center",
      "preventDuplicates": true,
      "onclick": null,
      "showDuration": "300",
      "hideDuration": "300",
      "timeOut": "1000",
      "extendedTimeOut": "1000",
      "showEasing": "swing",
      "hideEasing": "linear",
      "showMethod": "fadeIn",
      "hideMethod": "fadeOut"
    }
  }])
    //拦截器
    .config(['$provide', '$httpProvider', function ($provide, $httpProvider, $rootScope, $cookieStore) {
        $httpProvider.interceptors.push(['$q', '$location', '$rootScope', '$cookieStore', function ($q, $location, $rootScope, $cookieStore, $http, $state) {
            return {
                'request': function (config) {
                    config.headers = config.headers || {};
                    if (config.url.indexOf("blob.yijianhe.net") > 0) {
                        config.headers.authorization = undefined;
                    }
                    else {
                        var authorizationData = $cookieStore.get('authorizationData');
                        if (authorizationData != "") {
                            config.headers.authorization = authorizationData;
                        }
                    }
                    var strs = new Array();
                    return config;
                },
                'responseError': function (response) {
                    if (response.status === 401) {
                        $location.url('/Login');
                        toastr.error("未授权访问,请重新登陆");
                    }
                    return $q.reject(response);
                },
                'response': function (response) {
                    return response;
                }
            };
        }
        ])
    }
    ])
.config(function($stateProvider,$urlRouterProvider,$sceDelegateProvider){
    $stateProvider
        .state('root', {
            abstract: true,
            url: '',
            views: {
                'header': {
                    templateUrl: 'templates/Header.html',
                    controller: 'HeaderController'
                },
                'main': {
                    templateUrl: 'templates/Main.html',
                    controller: 'MainController'
                },
                'SideBar': {
                    templateUrl: 'templates/SideBar.html',
                    controller: 'SideBarController'
                }
            }
        })
        //登陆
        .state('root.Login', {
            url: '/Login',
            //cache: false,
            views: {
                'SideBar@': {},
                'header@': {
                    templateUrl: 'templates/Login/Login.html',
                    controller: 'LoginCtrl'
                },
                'main@': {}
            }
        })
        //网站首页
        .state('root.Home', {
            url: '/Home',
            views: {
                'main@': {
                    templateUrl: 'templates/Main.html',
                    controller: 'MainController'
                }
            }
        })
        //商家管理
        .state('root.SysUser', {
            url: '/SysUser',
            views: {
                'main@': {
                    templateUrl: 'templates/SysUser/SysUser.html',
                    controller: 'SysUserCtrl'
                }
            }
        })
        //用户管理
        .state('root.User', {
            url: '/User',
            views: {
                'main@': {
                    templateUrl: 'templates/User/User.html',
                    controller: 'UserCtrl'
                }
            }
        })
        //商品大类
      .state('root.GoodsBigKind', {
        url: '/GoodsBigKind',
        views: {
          'main@': {
            templateUrl: 'templates/GoodsBigKind/GoodsBigKind.html',
            controller: 'GoodsBigKindCtrl'
          }
        }
      })
        //商品种类
        .state('root.GoodsKind', {
            url: '/GoodsKind',
            views: {
                'main@': {
                    templateUrl: 'templates/GoodsKind/GoodsKind.html',
                    controller: 'GoodsKindCtrl'
                }
            }
        })
        //商品列表
        .state('root.Goods', {
            url: '/Goods',
            views: {
                'main@': {
                    templateUrl: 'templates/Goods/Goods.html',
                    controller: 'GoodsCtrl'
                }
            }
        })
        ////地址管理
        //  .state('root.Address', {
        //    url: '/Address',
        //    views: {
        //      'main@': {
        //        templateUrl: 'templates/Address/Address.html',
        //        controller: 'AddressCtrl'
        //      }
        //    }
        //  })
        //订单管理
        .state('root.Order', {
            url: '/Order',
            views: {
                'main@': {
                    templateUrl: 'templates/Order/Order.html',
                    controller: 'OrderCtrl'
                }
            }
        })
        //订单详情
        .state('root.OrderDetail', {
            url: '/OrderDetail',
            params: {'OrderId': null },
            views: {
                'main@': {
                    templateUrl: 'templates/OrderDetail/OrderDetail.html',
                    controller: 'OrderDetailCtrl'
                }
            }
        })
        //抽奖列表
        .state('root.Lottery', {
            url: '/Lottery',           
            views: {
                'main@': {
                    templateUrl: 'templates/Lottery/Lottery.html',
                    controller: 'LotteryCtrl'
                }
            }
        })
        //中奖名单
        .state('root.WinList', {
            url: '/WinList',       
            views: {
                'main@': {
                    templateUrl: 'templates/WinList/WinList.html',
                    controller: 'WinListCtrl'
                }
            }
        });
	$urlRouterProvider.otherwise("/Login");

});
