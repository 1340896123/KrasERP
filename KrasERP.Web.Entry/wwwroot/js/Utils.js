//(function ($) {
//    $.extend({
//        getClientResolution: function () {
//            console.log(arguments);
//            return {
//                width: window.innerWidth,
//                height: window.innerHeight
//            };
//        }

//    })
//})(JQuery) 

var getClientResolution = function () {
    console.log(arguments);
    return {
        width: window.innerWidth,
        height: window.innerHeight
    };
}