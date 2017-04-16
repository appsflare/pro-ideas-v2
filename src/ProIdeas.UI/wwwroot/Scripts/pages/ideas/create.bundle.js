define(function () { 'use strict';

var classCallCheck = function (instance, Constructor) {
  if (!(instance instanceof Constructor)) {
    throw new TypeError("Cannot call a class as a function");
  }
};

var createClass = function () {
  function defineProperties(target, props) {
    for (var i = 0; i < props.length; i++) {
      var descriptor = props[i];
      descriptor.enumerable = descriptor.enumerable || false;
      descriptor.configurable = true;
      if ("value" in descriptor) descriptor.writable = true;
      Object.defineProperty(target, descriptor.key, descriptor);
    }
  }

  return function (Constructor, protoProps, staticProps) {
    if (protoProps) defineProperties(Constructor.prototype, protoProps);
    if (staticProps) defineProperties(Constructor, staticProps);
    return Constructor;
  };
}();

var ApiClient = function () {
    function ApiClient() {
        classCallCheck(this, ApiClient);
    }

    createClass(ApiClient, [{
        key: "getIdeas",
        value: function getIdeas() {
            return Promise.resolve([]);
        }
    }]);
    return ApiClient;
}();

var client = new ApiClient();

client.getIdeas().then(function (ideas) {
    console.log();
});

});
//# sourceMappingURL=create.bundle.js.map
