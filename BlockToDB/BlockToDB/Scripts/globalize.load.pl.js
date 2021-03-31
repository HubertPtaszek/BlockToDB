(function (factory) {
	if ( typeof define === "function" && define.amd ) {

		// AMD. Register as an anonymous module.
		define([ "jquery",	
            "cldr",
            "../globalize",
			"cldr/event" ], factory );
	} else if (typeof exports === "object") {

	    // Node, CommonJS
	    module.exports = factory(require("jquery"), require("cldrjs"), require("../globalize"));
	    // Browser globals
	}
	else {	  
	    factory(jQuery, Cldr, Globalize);
	}
}(function ($) {
        $.holdReady(true);
$.when(
        $.getJSON("/Content/localizationJson/pl/ca-gregorian.json"),
        $.getJSON("/Content/localizationJson/pl/numbers.json"),
        $.getJSON("/Content/localizationJson/pl/currencies.json"),
        $.getJSON("/Content/localizationJson/likelySubtags.json"),
        $.getJSON("/Content/localizationJson/timeData.json"),
        $.getJSON("/Content/localizationJson/weekData.json"),
        $.getJSON("/Content/localizationJson/currencyData.json"),
        $.getJSON("/Content/localizationJson/numberingSystems.json")
        )
    .then(function () {
        //The following code converts the got results into an array
        return [].slice.apply(arguments, [0]).map(function (result) {;
            return result[0];
        });
    }).then(
        Globalize.load //loads data held in each array item to Globalize
    ).then(function () {
        //initialize your application here
        Globalize.locale('pl');
        $.holdReady(false);
    });
    }));

//$.ajax({
//    url: '/Content/localizationJson/pl/ca-gregorian.json',
//    type: 'GET',
//    async: false,
//    success: function (data) {
//        Globalize.load(data);
//    }
//});
//$.ajax({
//    url: '/Content/localizationJson/pl/numbers.json',
//    type: 'GET',
//    async: false,
//    success: function (data) {
//        Globalize.load(data);
//    }
//});
//$.ajax({
//    url: '/Content/localizationJson/pl/currencies.json',
//    type: 'GET',
//    async: false,
//    success: function (data) {
//        Globalize.load(data);
//    }
//});
//$.ajax({
//    url: '/Content/localizationJson/likelySubtags.json',
//    type: 'GET',
//    async: false,
//    success: function (data) {
//        Globalize.load(data);
//    }
//});
//$.ajax({
//    url: '/Content/localizationJson/timeData.json',
//    type: 'GET',
//    async: false,
//    success: function (data) {
//        Globalize.load(data);
//    }
//});
//$.ajax({
//    url: '/Content/localizationJson/weekData.json',
//    type: 'GET',
//    async: false,
//    success: function (data) {
//        Globalize.load(data);
//    }
//});
//$.ajax({
//    url: '/Content/localizationJson/currencyData.json',
//    type: 'GET',
//    async: false,
//    success: function (data) {
//        Globalize.load(data);
//    }
//});

//$.ajax({
//    url: '/Content/localizationJson/numberingSystems.json',
//    type: 'GET',
//    async: false,
//    success: function (data) {
//        Globalize.load(data);
//    }
//});

//Globalize.locale('pl');