// General application level utilities - revealing module
// requires:   jquery, jquery.cookie
appUtilities = function () {
    // private variables
    var _version = '1.00';
    var _canUseCookies;

    // private methods

    // public methods
    getVersion = function () {
        return _version;
    }

    // top level routine ... test cookies feature
    // and retain the results
    function areCookiesEnabled() {
        var r = false;       
        if (isDataEmpty(_canUseCookies)) {
            $.cookie('test_cookie', true, { path: '/' });
            if ($.cookie('test_cookie')) {
                r = true;
                $.cookie('test_cookie', null);
            }            
            _canUseCookies = r;
        }
        return _canUseCookies;
    }

    // check for undefined, null, or empty javascript object
    isDataEmpty = function (testData) {
        return (typeof testData === "undefined" || testData === null || testData === "");
    }

    // check for a jQuery element
    doesElementExist = function (testElement) {
        return testElement.length > 1;
    }

    // handle errors for json requests
    // TODO - this is for deprecated error ajax calls ... needs refactored
    commonErrorHandler = function (request, status, error) {
        if (request.status == 404) {
            alert("ajax error: connection problem with the server");
        }
        else if (request.status == 302) {
            alert("ajax error: session timeout occured.  Login to the system again");
        }
        else if (request.status == 500) {
            alert("ajax error: internal server error servicing your request");
        }
        else {
            alert("ajax error: session expired ... login again to continue");
        }
    }

    // handle errors for ajax html requests
    // TODO - this is for deprecated error ajax calls ... needs refactored
    commonHtmlErrorHandler = function (request, status, error) {
        if (request.status == 404) {
            alert("ajax error: connection problem with the server");
        }
        else if (request.status == 302) {
            alert("ajax error: session timeout occured.  Login to the system again");
        }
        else if (request.status == 500) {
            alert("ajax error: internal server error servicing your request");
        }
        else {
            alert("unknown error");
        }
    }

    // declared public api
    return {
        areCookiesEnabled : areCookiesEnabled,
        getVersion: getVersion,
        doesElementExist: doesElementExist,
        isDataEmpty: isDataEmpty,
        commonErrorHandler: commonErrorHandler,
        commonHtmlErrorHandler: commonHtmlErrorHandler
    }
}();