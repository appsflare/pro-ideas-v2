this[''] = this[''] || {};
this['']['/Scripts/pages/ideas/create'] = this['']['/Scripts/pages/ideas/create'] || {};
(function () {
'use strict';

var _typeof = typeof Symbol === "function" && typeof Symbol.iterator === "symbol" ? function (obj) {
  return typeof obj;
} : function (obj) {
  return obj && typeof Symbol === "function" && obj.constructor === Symbol && obj !== Symbol.prototype ? "symbol" : typeof obj;
};











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









var inherits = function (subClass, superClass) {
  if (typeof superClass !== "function" && superClass !== null) {
    throw new TypeError("Super expression must either be null or a function, not " + typeof superClass);
  }

  subClass.prototype = Object.create(superClass && superClass.prototype, {
    constructor: {
      value: subClass,
      enumerable: false,
      writable: true,
      configurable: true
    }
  });
  if (superClass) Object.setPrototypeOf ? Object.setPrototypeOf(subClass, superClass) : subClass.__proto__ = superClass;
};











var possibleConstructorReturn = function (self, call) {
  if (!self) {
    throw new ReferenceError("this hasn't been initialised - super() hasn't been called");
  }

  return call && (typeof call === "object" || typeof call === "function") ? call : self;
};

var defaultHeaders = {};
var utils = {
    get: function get$$1(url) {
        return new Promise(function (resolve, reject) {
            $.get({
                url: url,
                success: resolve,
                error: reject
            });
        });
    },
    put: function put(url, data) {
        return new Promise(function (resolve, reject) {
            $.ajax({
                url: url,
                type: 'PUT',
                data: JSON.stringify(data),
                success: resolve,
                error: reject
            });
        });
    },
    post: function post(url, data) {
        return new Promise(function (resolve, reject) {
            $.post({
                url: url,
                data: JSON.stringify(data),
                success: resolve,
                error: reject
            });
        });
    },
    delete: function _delete(url) {
        return new Promise(function (resolve, reject) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: resolve,
                error: reject
            });
        });
    },
    uploadFile: function uploadFile(url) {
        var files = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : [];

        var formData = new FormData();

        files.forEach(function (file) {
            formData.append(file.name, file);
        });

        return new Promise(function (resolve, reject) {
            $.post({
                url: url,
                data: formData,
                contentType: false,
                processData: false,
                success: resolve,
                error: reject
            });
        });
    }
};

var ApiClient = function () {
    function ApiClient() {
        classCallCheck(this, ApiClient);

        $.ajaxSetup({
            headers: defaultHeaders,
            contentType: 'application/json;charset=utf8',
            dataType: 'json'
        });
    }

    createClass(ApiClient, [{
        key: 'getIdea',
        value: function getIdea(id) {
            return utils.get('/api/ideas/' + id);
        }
    }, {
        key: 'createIdea',
        value: function createIdea(idea) {

            return utils.post('/api/ideas', idea);
        }
    }, {
        key: 'updateIdea',
        value: function updateIdea(idea) {

            return utils.put('/api/ideas/' + idea.id, idea);
        }
    }, {
        key: 'uploadIdeaBanner',
        value: function uploadIdeaBanner(id, file) {
            return utils.uploadFile('/api/ideas/' + id + '/banner', [file]);
        }
    }, {
        key: 'saveIdeaPages',
        value: function saveIdeaPages(id, pages) {
            return utils.put('/api/ideas/' + id + '/pages', pages);
        }
    }, {
        key: 'getIdeaPages',
        value: function getIdeaPages(id) {
            return utils.get('/api/ideas/' + id + '/pages');
        }
    }, {
        key: 'getIdeas',
        value: function getIdeas(_ref) {
            var _ref$keyword = _ref.keyword,
                keyword = _ref$keyword === undefined ? "" : _ref$keyword,
                page = _ref.page,
                _ref$pageSize = _ref.pageSize,
                pageSize = _ref$pageSize === undefined ? 100 : _ref$pageSize;

            var url = '/api/ideas?page=' + page + '&pageSize=' + pageSize;
            if (keyword) {
                url = '/api/ideas/search?page=' + page + '&pageSize=' + pageSize + '&keyword=' + keyword;
            }
            return utils.get(url);
        }
    }, {
        key: 'getMyIdeas',
        value: function getMyIdeas(_ref2) {
            var _ref2$keyword = _ref2.keyword,
                keyword = _ref2$keyword === undefined ? "" : _ref2$keyword,
                page = _ref2.page,
                _ref2$pageSize = _ref2.pageSize,
                pageSize = _ref2$pageSize === undefined ? 100 : _ref2$pageSize;

            var url = '/api/ideas/searchmyideas?page=' + page + '&pageSize=' + pageSize + '&keyword=' + keyword;
            return utils.get(url);
        }
    }, {
        key: 'like',
        value: function like(ideaId, isLike) {
            return utils.put('/api/ideas/' + ideaId + '/likes/' + isLike);
        }
    }, {
        key: 'getIdeaComments',
        value: function getIdeaComments(_ref3) {
            var ideaId = _ref3.ideaId,
                page = _ref3.page,
                _ref3$pageSize = _ref3.pageSize,
                pageSize = _ref3$pageSize === undefined ? 100 : _ref3$pageSize;

            var url = '/api/ideas/' + ideaId + '/comments?page=' + page + '&pageSize=' + pageSize;
            return utils.get(url);
        }
    }, {
        key: 'postIdeaComment',
        value: function postIdeaComment(_ref4) {
            var ideaId = _ref4.ideaId,
                content = _ref4.content;

            var url = '/api/ideas/' + ideaId + '/comments';
            return utils.post(url, { content: content });
        }
    }, {
        key: 'updateIdeaComment',
        value: function updateIdeaComment(_ref5) {
            var ideaId = _ref5.ideaId,
                id = _ref5.id,
                content = _ref5.content;

            var url = '/api/ideas/' + ideaId + '/comments';
            return utils.put(url, { id: id, content: content });
        }
    }, {
        key: 'deleteComment',
        value: function deleteComment(id) {
            var url = '/api/ideas/comments/' + id;
            return utils.delete(url);
        }
    }]);
    return ApiClient;
}();

var commonjsGlobal = typeof window !== 'undefined' ? window : typeof global !== 'undefined' ? global : typeof self !== 'undefined' ? self : {};

function commonjsRequire () {
	throw new Error('Dynamic requires are not currently supported by rollup-plugin-commonjs');
}



function createCommonjsModule(fn, module) {
	return module = { exports: {} }, fn(module, module.exports), module.exports;
}

var knockout = createCommonjsModule(function (module, exports) {
  /*!
   * Knockout JavaScript library v3.4.2
   * (c) The Knockout.js team - http://knockoutjs.com/
   * License: MIT (http://www.opensource.org/licenses/mit-license.php)
   */

  (function () {
    (function (n) {
      var x = this || (0, eval)("this"),
          t = x.document,
          M = x.navigator,
          u = x.jQuery,
          H = x.JSON;(function (n) {
        "function" === typeof undefined && undefined.amd ? undefined(["exports", "require"], n) : n(module.exports || exports);
      })(function (N, O) {
        function J(a, c) {
          return null === a || (typeof a === "undefined" ? "undefined" : _typeof(a)) in R ? a === c : !1;
        }function S(b, c) {
          var d;return function () {
            d || (d = a.a.setTimeout(function () {
              d = n;b();
            }, c));
          };
        }function T(b, c) {
          var d;return function () {
            clearTimeout(d);d = a.a.setTimeout(b, c);
          };
        }function U(a, c) {
          c && c !== E ? "beforeChange" === c ? this.Ob(a) : this.Ja(a, c) : this.Pb(a);
        }function V(a, c) {
          null !== c && c.k && c.k();
        }function W(a, c) {
          var d = this.Mc,
              e = d[s];e.T || (this.ob && this.Oa[c] ? (d.Sb(c, a, this.Oa[c]), this.Oa[c] = null, --this.ob) : e.s[c] || d.Sb(c, a, e.t ? { $: a } : d.yc(a)), a.Ha && a.Hc());
        }function K(b, c, d, e) {
          a.d[b] = { init: function init(b, g, h, l, m) {
              var k, r;a.m(function () {
                var q = g(),
                    p = a.a.c(q),
                    p = !d !== !p,
                    A = !r;if (A || c || p !== k) A && a.xa.Ca() && (r = a.a.wa(a.f.childNodes(b), !0)), p ? (A || a.f.fa(b, a.a.wa(r)), a.hb(e ? e(m, q) : m, b)) : a.f.za(b), k = p;
              }, null, { i: b });return { controlsDescendantBindings: !0 };
            } };a.h.va[b] = !1;a.f.aa[b] = !0;
        }var a = "undefined" !== typeof N ? N : {};a.b = function (b, c) {
          for (var d = b.split("."), e = a, f = 0; f < d.length - 1; f++) {
            e = e[d[f]];
          }e[d[d.length - 1]] = c;
        };a.H = function (a, c, d) {
          a[c] = d;
        };a.version = "3.4.2";a.b("version", a.version);a.options = { deferUpdates: !1, useOnlyNativeEvents: !1 };a.a = function () {
          function b(a, b) {
            for (var c in a) {
              a.hasOwnProperty(c) && b(c, a[c]);
            }
          }function c(a, b) {
            if (b) for (var c in b) {
              b.hasOwnProperty(c) && (a[c] = b[c]);
            }return a;
          }function d(a, b) {
            a.__proto__ = b;return a;
          }function e(b, c, d, e) {
            var m = b[c].match(r) || [];a.a.r(d.match(r), function (b) {
              a.a.ra(m, b, e);
            });b[c] = m.join(" ");
          }var f = { __proto__: [] } instanceof Array,
              g = "function" === typeof Symbol,
              h = {},
              l = {};h[M && /Firefox\/2/i.test(M.userAgent) ? "KeyboardEvent" : "UIEvents"] = ["keyup", "keydown", "keypress"];h.MouseEvents = "click dblclick mousedown mouseup mousemove mouseover mouseout mouseenter mouseleave".split(" ");b(h, function (a, b) {
            if (b.length) for (var c = 0, d = b.length; c < d; c++) {
              l[b[c]] = a;
            }
          });var m = { propertychange: !0 },
              k = t && function () {
            for (var a = 3, b = t.createElement("div"), c = b.getElementsByTagName("i"); b.innerHTML = "\x3c!--[if gt IE " + ++a + "]><i></i><![endif]--\x3e", c[0];) {}return 4 < a ? a : n;
          }(),
              r = /\S+/g;return { gc: ["authenticity_token", /^__RequestVerificationToken(_.*)?$/], r: function r(a, b) {
              for (var c = 0, d = a.length; c < d; c++) {
                b(a[c], c);
              }
            }, o: function o(a, b) {
              if ("function" == typeof Array.prototype.indexOf) return Array.prototype.indexOf.call(a, b);for (var c = 0, d = a.length; c < d; c++) {
                if (a[c] === b) return c;
              }return -1;
            }, Vb: function Vb(a, b, c) {
              for (var d = 0, e = a.length; d < e; d++) {
                if (b.call(c, a[d], d)) return a[d];
              }return null;
            }, Na: function Na(b, c) {
              var d = a.a.o(b, c);0 < d ? b.splice(d, 1) : 0 === d && b.shift();
            }, Wb: function Wb(b) {
              b = b || [];for (var c = [], d = 0, e = b.length; d < e; d++) {
                0 > a.a.o(c, b[d]) && c.push(b[d]);
              }return c;
            }, ib: function ib(a, b) {
              a = a || [];for (var c = [], d = 0, e = a.length; d < e; d++) {
                c.push(b(a[d], d));
              }return c;
            }, Ma: function Ma(a, b) {
              a = a || [];for (var c = [], d = 0, e = a.length; d < e; d++) {
                b(a[d], d) && c.push(a[d]);
              }return c;
            }, ta: function ta(a, b) {
              if (b instanceof Array) a.push.apply(a, b);else for (var c = 0, d = b.length; c < d; c++) {
                a.push(b[c]);
              }return a;
            }, ra: function ra(b, c, d) {
              var e = a.a.o(a.a.Bb(b), c);0 > e ? d && b.push(c) : d || b.splice(e, 1);
            }, la: f, extend: c, $a: d, ab: f ? d : c, D: b, Ea: function Ea(a, b) {
              if (!a) return a;var c = {},
                  d;for (d in a) {
                a.hasOwnProperty(d) && (c[d] = b(a[d], d, a));
              }return c;
            }, rb: function rb(b) {
              for (; b.firstChild;) {
                a.removeNode(b.firstChild);
              }
            }, nc: function nc(b) {
              b = a.a.W(b);for (var c = (b[0] && b[0].ownerDocument || t).createElement("div"), d = 0, e = b.length; d < e; d++) {
                c.appendChild(a.ba(b[d]));
              }return c;
            }, wa: function wa(b, c) {
              for (var d = 0, e = b.length, m = []; d < e; d++) {
                var k = b[d].cloneNode(!0);m.push(c ? a.ba(k) : k);
              }return m;
            }, fa: function fa(b, c) {
              a.a.rb(b);if (c) for (var d = 0, e = c.length; d < e; d++) {
                b.appendChild(c[d]);
              }
            }, uc: function uc(b, c) {
              var d = b.nodeType ? [b] : b;if (0 < d.length) {
                for (var e = d[0], m = e.parentNode, k = 0, f = c.length; k < f; k++) {
                  m.insertBefore(c[k], e);
                }k = 0;for (f = d.length; k < f; k++) {
                  a.removeNode(d[k]);
                }
              }
            }, Ba: function Ba(a, b) {
              if (a.length) {
                for (b = 8 === b.nodeType && b.parentNode || b; a.length && a[0].parentNode !== b;) {
                  a.splice(0, 1);
                }for (; 1 < a.length && a[a.length - 1].parentNode !== b;) {
                  a.length--;
                }if (1 < a.length) {
                  var c = a[0],
                      d = a[a.length - 1];for (a.length = 0; c !== d;) {
                    a.push(c), c = c.nextSibling;
                  }a.push(d);
                }
              }return a;
            }, wc: function wc(a, b) {
              7 > k ? a.setAttribute("selected", b) : a.selected = b;
            }, cb: function cb(a) {
              return null === a || a === n ? "" : a.trim ? a.trim() : a.toString().replace(/^[\s\xa0]+|[\s\xa0]+$/g, "");
            }, sd: function sd(a, b) {
              a = a || "";return b.length > a.length ? !1 : a.substring(0, b.length) === b;
            }, Rc: function Rc(a, b) {
              if (a === b) return !0;if (11 === a.nodeType) return !1;if (b.contains) return b.contains(3 === a.nodeType ? a.parentNode : a);if (b.compareDocumentPosition) return 16 == (b.compareDocumentPosition(a) & 16);for (; a && a != b;) {
                a = a.parentNode;
              }return !!a;
            }, qb: function qb(b) {
              return a.a.Rc(b, b.ownerDocument.documentElement);
            }, Tb: function Tb(b) {
              return !!a.a.Vb(b, a.a.qb);
            }, A: function A(a) {
              return a && a.tagName && a.tagName.toLowerCase();
            }, Zb: function Zb(b) {
              return a.onError ? function () {
                try {
                  return b.apply(this, arguments);
                } catch (c) {
                  throw a.onError && a.onError(c), c;
                }
              } : b;
            }, setTimeout: function (_setTimeout) {
              function setTimeout(_x, _x2) {
                return _setTimeout.apply(this, arguments);
              }

              setTimeout.toString = function () {
                return _setTimeout.toString();
              };

              return setTimeout;
            }(function (b, c) {
              return setTimeout(a.a.Zb(b), c);
            }), dc: function dc(b) {
              setTimeout(function () {
                a.onError && a.onError(b);throw b;
              }, 0);
            }, q: function q(b, c, d) {
              var e = a.a.Zb(d);d = k && m[c];if (a.options.useOnlyNativeEvents || d || !u) {
                if (d || "function" != typeof b.addEventListener) {
                  if ("undefined" != typeof b.attachEvent) {
                    var f = function f(a) {
                      e.call(b, a);
                    },
                        l = "on" + c;b.attachEvent(l, f);a.a.G.qa(b, function () {
                      b.detachEvent(l, f);
                    });
                  } else throw Error("Browser doesn't support addEventListener or attachEvent");
                } else b.addEventListener(c, e, !1);
              } else u(b).bind(c, e);
            }, Fa: function Fa(b, c) {
              if (!b || !b.nodeType) throw Error("element must be a DOM node when calling triggerEvent");var d;"input" === a.a.A(b) && b.type && "click" == c.toLowerCase() ? (d = b.type, d = "checkbox" == d || "radio" == d) : d = !1;if (a.options.useOnlyNativeEvents || !u || d) {
                if ("function" == typeof t.createEvent) {
                  if ("function" == typeof b.dispatchEvent) d = t.createEvent(l[c] || "HTMLEvents"), d.initEvent(c, !0, !0, x, 0, 0, 0, 0, 0, !1, !1, !1, !1, 0, b), b.dispatchEvent(d);else throw Error("The supplied element doesn't support dispatchEvent");
                } else if (d && b.click) b.click();else if ("undefined" != typeof b.fireEvent) b.fireEvent("on" + c);else throw Error("Browser doesn't support triggering events");
              } else u(b).trigger(c);
            }, c: function c(b) {
              return a.I(b) ? b() : b;
            }, Bb: function Bb(b) {
              return a.I(b) ? b.p() : b;
            }, fb: function fb(b, c, d) {
              var k;c && ("object" === _typeof(b.classList) ? (k = b.classList[d ? "add" : "remove"], a.a.r(c.match(r), function (a) {
                k.call(b.classList, a);
              })) : "string" === typeof b.className.baseVal ? e(b.className, "baseVal", c, d) : e(b, "className", c, d));
            }, bb: function bb(b, c) {
              var d = a.a.c(c);if (null === d || d === n) d = "";var e = a.f.firstChild(b);!e || 3 != e.nodeType || a.f.nextSibling(e) ? a.f.fa(b, [b.ownerDocument.createTextNode(d)]) : e.data = d;a.a.Wc(b);
            }, vc: function vc(a, b) {
              a.name = b;if (7 >= k) try {
                a.mergeAttributes(t.createElement("<input name='" + a.name + "'/>"), !1);
              } catch (c) {}
            }, Wc: function Wc(a) {
              9 <= k && (a = 1 == a.nodeType ? a : a.parentNode, a.style && (a.style.zoom = a.style.zoom));
            }, Sc: function Sc(a) {
              if (k) {
                var b = a.style.width;a.style.width = 0;a.style.width = b;
              }
            }, nd: function nd(b, c) {
              b = a.a.c(b);c = a.a.c(c);for (var d = [], e = b; e <= c; e++) {
                d.push(e);
              }return d;
            }, W: function W(a) {
              for (var b = [], c = 0, d = a.length; c < d; c++) {
                b.push(a[c]);
              }return b;
            }, bc: function bc(a) {
              return g ? Symbol(a) : a;
            }, xd: 6 === k,
            yd: 7 === k, C: k, ic: function ic(b, c) {
              for (var d = a.a.W(b.getElementsByTagName("input")).concat(a.a.W(b.getElementsByTagName("textarea"))), e = "string" == typeof c ? function (a) {
                return a.name === c;
              } : function (a) {
                return c.test(a.name);
              }, k = [], m = d.length - 1; 0 <= m; m--) {
                e(d[m]) && k.push(d[m]);
              }return k;
            }, kd: function kd(b) {
              return "string" == typeof b && (b = a.a.cb(b)) ? H && H.parse ? H.parse(b) : new Function("return " + b)() : null;
            }, Gb: function Gb(b, c, d) {
              if (!H || !H.stringify) throw Error("Cannot find JSON.stringify(). Some browsers (e.g., IE < 8) don't support it natively, but you can overcome this by adding a script reference to json2.js, downloadable from http://www.json.org/json2.js");
              return H.stringify(a.a.c(b), c, d);
            }, ld: function ld(c, d, e) {
              e = e || {};var k = e.params || {},
                  m = e.includeFields || this.gc,
                  f = c;if ("object" == (typeof c === "undefined" ? "undefined" : _typeof(c)) && "form" === a.a.A(c)) for (var f = c.action, l = m.length - 1; 0 <= l; l--) {
                for (var g = a.a.ic(c, m[l]), h = g.length - 1; 0 <= h; h--) {
                  k[g[h].name] = g[h].value;
                }
              }d = a.a.c(d);var r = t.createElement("form");r.style.display = "none";r.action = f;r.method = "post";for (var n in d) {
                c = t.createElement("input"), c.type = "hidden", c.name = n, c.value = a.a.Gb(a.a.c(d[n])), r.appendChild(c);
              }b(k, function (a, b) {
                var c = t.createElement("input");
                c.type = "hidden";c.name = a;c.value = b;r.appendChild(c);
              });t.body.appendChild(r);e.submitter ? e.submitter(r) : r.submit();setTimeout(function () {
                r.parentNode.removeChild(r);
              }, 0);
            } };
        }();a.b("utils", a.a);a.b("utils.arrayForEach", a.a.r);a.b("utils.arrayFirst", a.a.Vb);a.b("utils.arrayFilter", a.a.Ma);a.b("utils.arrayGetDistinctValues", a.a.Wb);a.b("utils.arrayIndexOf", a.a.o);a.b("utils.arrayMap", a.a.ib);a.b("utils.arrayPushAll", a.a.ta);a.b("utils.arrayRemoveItem", a.a.Na);a.b("utils.extend", a.a.extend);a.b("utils.fieldsIncludedWithJsonPost", a.a.gc);a.b("utils.getFormFields", a.a.ic);a.b("utils.peekObservable", a.a.Bb);a.b("utils.postJson", a.a.ld);a.b("utils.parseJson", a.a.kd);a.b("utils.registerEventHandler", a.a.q);a.b("utils.stringifyJson", a.a.Gb);a.b("utils.range", a.a.nd);a.b("utils.toggleDomNodeCssClass", a.a.fb);a.b("utils.triggerEvent", a.a.Fa);a.b("utils.unwrapObservable", a.a.c);a.b("utils.objectForEach", a.a.D);a.b("utils.addOrRemoveItem", a.a.ra);a.b("utils.setTextContent", a.a.bb);a.b("unwrap", a.a.c);Function.prototype.bind || (Function.prototype.bind = function (a) {
          var c = this;if (1 === arguments.length) return function () {
            return c.apply(a, arguments);
          };var d = Array.prototype.slice.call(arguments, 1);return function () {
            var e = d.slice(0);e.push.apply(e, arguments);return c.apply(a, e);
          };
        });a.a.e = new function () {
          function a(b, g) {
            var h = b[d];if (!h || "null" === h || !e[h]) {
              if (!g) return n;h = b[d] = "ko" + c++;e[h] = {};
            }return e[h];
          }var c = 0,
              d = "__ko__" + new Date().getTime(),
              e = {};return { get: function get$$1(c, d) {
              var e = a(c, !1);return e === n ? n : e[d];
            }, set: function set$$1(c, d, e) {
              if (e !== n || a(c, !1) !== n) a(c, !0)[d] = e;
            }, clear: function clear(a) {
              var b = a[d];return b ? (delete e[b], a[d] = null, !0) : !1;
            }, J: function J() {
              return c++ + d;
            } };
        }();a.b("utils.domData", a.a.e);a.b("utils.domData.clear", a.a.e.clear);a.a.G = new function () {
          function b(b, c) {
            var e = a.a.e.get(b, d);e === n && c && (e = [], a.a.e.set(b, d, e));return e;
          }function c(d) {
            var e = b(d, !1);if (e) for (var e = e.slice(0), l = 0; l < e.length; l++) {
              e[l](d);
            }a.a.e.clear(d);a.a.G.cleanExternalData(d);if (f[d.nodeType]) for (e = d.firstChild; d = e;) {
              e = d.nextSibling, 8 === d.nodeType && c(d);
            }
          }var d = a.a.e.J(),
              e = { 1: !0, 8: !0, 9: !0 },
              f = { 1: !0, 9: !0 };return { qa: function qa(a, c) {
              if ("function" != typeof c) throw Error("Callback must be a function");b(a, !0).push(c);
            }, tc: function tc(c, e) {
              var f = b(c, !1);f && (a.a.Na(f, e), 0 == f.length && a.a.e.set(c, d, n));
            }, ba: function ba(b) {
              if (e[b.nodeType] && (c(b), f[b.nodeType])) {
                var d = [];a.a.ta(d, b.getElementsByTagName("*"));for (var l = 0, m = d.length; l < m; l++) {
                  c(d[l]);
                }
              }return b;
            }, removeNode: function removeNode(b) {
              a.ba(b);b.parentNode && b.parentNode.removeChild(b);
            }, cleanExternalData: function cleanExternalData(a) {
              u && "function" == typeof u.cleanData && u.cleanData([a]);
            } };
        }();
        a.ba = a.a.G.ba;a.removeNode = a.a.G.removeNode;a.b("cleanNode", a.ba);a.b("removeNode", a.removeNode);a.b("utils.domNodeDisposal", a.a.G);a.b("utils.domNodeDisposal.addDisposeCallback", a.a.G.qa);a.b("utils.domNodeDisposal.removeDisposeCallback", a.a.G.tc);(function () {
          var b = [0, "", ""],
              c = [1, "<table>", "</table>"],
              d = [3, "<table><tbody><tr>", "</tr></tbody></table>"],
              e = [1, "<select multiple='multiple'>", "</select>"],
              f = { thead: c, tbody: c, tfoot: c, tr: [2, "<table><tbody>", "</tbody></table>"], td: d, th: d, option: e, optgroup: e },
              g = 8 >= a.a.C;a.a.na = function (c, d) {
            var e;if (u) {
              if (u.parseHTML) e = u.parseHTML(c, d) || [];else {
                if ((e = u.clean([c], d)) && e[0]) {
                  for (var k = e[0]; k.parentNode && 11 !== k.parentNode.nodeType;) {
                    k = k.parentNode;
                  }k.parentNode && k.parentNode.removeChild(k);
                }
              }
            } else {
              (e = d) || (e = t);var k = e.parentWindow || e.defaultView || x,
                  r = a.a.cb(c).toLowerCase(),
                  q = e.createElement("div"),
                  p;p = (r = r.match(/^<([a-z]+)[ >]/)) && f[r[1]] || b;r = p[0];p = "ignored<div>" + p[1] + c + p[2] + "</div>";"function" == typeof k.innerShiv ? q.appendChild(k.innerShiv(p)) : (g && e.appendChild(q), q.innerHTML = p, g && q.parentNode.removeChild(q));for (; r--;) {
                q = q.lastChild;
              }e = a.a.W(q.lastChild.childNodes);
            }return e;
          };a.a.Eb = function (b, c) {
            a.a.rb(b);c = a.a.c(c);if (null !== c && c !== n) if ("string" != typeof c && (c = c.toString()), u) u(b).html(c);else for (var d = a.a.na(c, b.ownerDocument), e = 0; e < d.length; e++) {
              b.appendChild(d[e]);
            }
          };
        })();a.b("utils.parseHtmlFragment", a.a.na);a.b("utils.setHtml", a.a.Eb);a.N = function () {
          function b(c, e) {
            if (c) if (8 == c.nodeType) {
              var f = a.N.pc(c.nodeValue);null != f && e.push({ Qc: c, hd: f });
            } else if (1 == c.nodeType) for (var f = 0, g = c.childNodes, h = g.length; f < h; f++) {
              b(g[f], e);
            }
          }var c = {};return { yb: function yb(a) {
              if ("function" != typeof a) throw Error("You can only pass a function to ko.memoization.memoize()");var b = (4294967296 * (1 + Math.random()) | 0).toString(16).substring(1) + (4294967296 * (1 + Math.random()) | 0).toString(16).substring(1);c[b] = a;return "\x3c!--[ko_memo:" + b + "]--\x3e";
            }, Bc: function Bc(a, b) {
              var f = c[a];if (f === n) throw Error("Couldn't find any memo with ID " + a + ". Perhaps it's already been unmemoized.");try {
                return f.apply(null, b || []), !0;
              } finally {
                delete c[a];
              }
            }, Cc: function Cc(c, e) {
              var f = [];b(c, f);for (var g = 0, h = f.length; g < h; g++) {
                var l = f[g].Qc,
                    m = [l];e && a.a.ta(m, e);a.N.Bc(f[g].hd, m);l.nodeValue = "";l.parentNode && l.parentNode.removeChild(l);
              }
            }, pc: function pc(a) {
              return (a = a.match(/^\[ko_memo\:(.*?)\]$/)) ? a[1] : null;
            } };
        }();a.b("memoization", a.N);a.b("memoization.memoize", a.N.yb);a.b("memoization.unmemoize", a.N.Bc);a.b("memoization.parseMemoText", a.N.pc);a.b("memoization.unmemoizeDomNodeAndDescendants", a.N.Cc);a.Z = function () {
          function b() {
            if (e) for (var b = e, c = 0, m; g < e;) {
              if (m = d[g++]) {
                if (g > b) {
                  if (5E3 <= ++c) {
                    g = e;a.a.dc(Error("'Too much recursion' after processing " + c + " task groups."));break;
                  }b = e;
                }try {
                  m();
                } catch (k) {
                  a.a.dc(k);
                }
              }
            }
          }function c() {
            b();g = e = d.length = 0;
          }var d = [],
              e = 0,
              f = 1,
              g = 0;return { scheduler: x.MutationObserver ? function (a) {
              var b = t.createElement("div");new MutationObserver(a).observe(b, { attributes: !0 });return function () {
                b.classList.toggle("foo");
              };
            }(c) : t && "onreadystatechange" in t.createElement("script") ? function (a) {
              var b = t.createElement("script");b.onreadystatechange = function () {
                b.onreadystatechange = null;t.documentElement.removeChild(b);b = null;a();
              };t.documentElement.appendChild(b);
            } : function (a) {
              setTimeout(a, 0);
            }, Za: function Za(b) {
              e || a.Z.scheduler(c);d[e++] = b;return f++;
            }, cancel: function cancel(a) {
              a -= f - e;a >= g && a < e && (d[a] = null);
            }, resetForTesting: function resetForTesting() {
              var a = e - g;g = e = d.length = 0;return a;
            }, rd: b };
        }();a.b("tasks", a.Z);a.b("tasks.schedule", a.Z.Za);a.b("tasks.runEarly", a.Z.rd);a.Aa = { throttle: function throttle(b, c) {
            b.throttleEvaluation = c;var d = null;return a.B({ read: b, write: function write(e) {
                clearTimeout(d);
                d = a.a.setTimeout(function () {
                  b(e);
                }, c);
              } });
          }, rateLimit: function rateLimit(a, c) {
            var d, e, f;"number" == typeof c ? d = c : (d = c.timeout, e = c.method);a.gb = !1;f = "notifyWhenChangesStop" == e ? T : S;a.Wa(function (a) {
              return f(a, d);
            });
          }, deferred: function deferred(b, c) {
            if (!0 !== c) throw Error("The 'deferred' extender only accepts the value 'true', because it is not supported to turn deferral off once enabled.");b.gb || (b.gb = !0, b.Wa(function (c) {
              var e,
                  f = !1;return function () {
                if (!f) {
                  a.Z.cancel(e);e = a.Z.Za(c);try {
                    f = !0, b.notifySubscribers(n, "dirty");
                  } finally {
                    f = !1;
                  }
                }
              };
            }));
          }, notify: function notify(a, c) {
            a.equalityComparer = "always" == c ? null : J;
          } };var R = { undefined: 1, "boolean": 1, number: 1, string: 1 };a.b("extenders", a.Aa);a.zc = function (b, c, d) {
          this.$ = b;this.jb = c;this.Pc = d;this.T = !1;a.H(this, "dispose", this.k);
        };a.zc.prototype.k = function () {
          this.T = !0;this.Pc();
        };a.K = function () {
          a.a.ab(this, D);D.ub(this);
        };var E = "change",
            D = { ub: function ub(a) {
            a.F = { change: [] };a.Qb = 1;
          }, Y: function Y(b, c, d) {
            var e = this;d = d || E;var f = new a.zc(e, c ? b.bind(c) : b, function () {
              a.a.Na(e.F[d], f);e.Ka && e.Ka(d);
            });e.ua && e.ua(d);
            e.F[d] || (e.F[d] = []);e.F[d].push(f);return f;
          }, notifySubscribers: function notifySubscribers(b, c) {
            c = c || E;c === E && this.Kb();if (this.Ra(c)) {
              var d = c === E && this.Fc || this.F[c].slice(0);try {
                a.l.Xb();for (var e = 0, f; f = d[e]; ++e) {
                  f.T || f.jb(b);
                }
              } finally {
                a.l.end();
              }
            }
          }, Pa: function Pa() {
            return this.Qb;
          }, Zc: function Zc(a) {
            return this.Pa() !== a;
          }, Kb: function Kb() {
            ++this.Qb;
          }, Wa: function Wa(b) {
            var c = this,
                d = a.I(c),
                e,
                f,
                g,
                h;c.Ja || (c.Ja = c.notifySubscribers, c.notifySubscribers = U);var l = b(function () {
              c.Ha = !1;d && h === c && (h = c.Mb ? c.Mb() : c());var a = f || c.Ua(g, h);f = e = !1;
              a && c.Ja(g = h);
            });c.Pb = function (a) {
              c.Fc = c.F[E].slice(0);c.Ha = e = !0;h = a;l();
            };c.Ob = function (a) {
              e || (g = a, c.Ja(a, "beforeChange"));
            };c.Hc = function () {
              c.Ua(g, c.p(!0)) && (f = !0);
            };
          }, Ra: function Ra(a) {
            return this.F[a] && this.F[a].length;
          }, Xc: function Xc(b) {
            if (b) return this.F[b] && this.F[b].length || 0;var c = 0;a.a.D(this.F, function (a, b) {
              "dirty" !== a && (c += b.length);
            });return c;
          }, Ua: function Ua(a, c) {
            return !this.equalityComparer || !this.equalityComparer(a, c);
          }, extend: function extend(b) {
            var c = this;b && a.a.D(b, function (b, e) {
              var f = a.Aa[b];"function" == typeof f && (c = f(c, e) || c);
            });return c;
          } };a.H(D, "subscribe", D.Y);a.H(D, "extend", D.extend);a.H(D, "getSubscriptionsCount", D.Xc);a.a.la && a.a.$a(D, Function.prototype);a.K.fn = D;a.lc = function (a) {
          return null != a && "function" == typeof a.Y && "function" == typeof a.notifySubscribers;
        };a.b("subscribable", a.K);a.b("isSubscribable", a.lc);a.xa = a.l = function () {
          function b(a) {
            d.push(e);e = a;
          }function c() {
            e = d.pop();
          }var d = [],
              e,
              f = 0;return { Xb: b, end: c, sc: function sc(b) {
              if (e) {
                if (!a.lc(b)) throw Error("Only subscribable things can act as dependencies");
                e.jb.call(e.Lc, b, b.Gc || (b.Gc = ++f));
              }
            }, w: function w(a, d, e) {
              try {
                return b(), a.apply(d, e || []);
              } finally {
                c();
              }
            }, Ca: function Ca() {
              if (e) return e.m.Ca();
            }, Va: function Va() {
              if (e) return e.Va;
            } };
        }();a.b("computedContext", a.xa);a.b("computedContext.getDependenciesCount", a.xa.Ca);a.b("computedContext.isInitial", a.xa.Va);a.b("ignoreDependencies", a.wd = a.l.w);var F = a.a.bc("_latestValue");a.O = function (b) {
          function c() {
            if (0 < arguments.length) return c.Ua(c[F], arguments[0]) && (c.ia(), c[F] = arguments[0], c.ha()), this;a.l.sc(c);return c[F];
          }
          c[F] = b;a.a.la || a.a.extend(c, a.K.fn);a.K.fn.ub(c);a.a.ab(c, B);a.options.deferUpdates && a.Aa.deferred(c, !0);return c;
        };var B = { equalityComparer: J, p: function p() {
            return this[F];
          }, ha: function ha() {
            this.notifySubscribers(this[F]);
          }, ia: function ia() {
            this.notifySubscribers(this[F], "beforeChange");
          } };a.a.la && a.a.$a(B, a.K.fn);var I = a.O.md = "__ko_proto__";B[I] = a.O;a.Qa = function (b, c) {
          return null === b || b === n || b[I] === n ? !1 : b[I] === c ? !0 : a.Qa(b[I], c);
        };a.I = function (b) {
          return a.Qa(b, a.O);
        };a.Da = function (b) {
          return "function" == typeof b && b[I] === a.O || "function" == typeof b && b[I] === a.B && b.$c ? !0 : !1;
        };a.b("observable", a.O);a.b("isObservable", a.I);a.b("isWriteableObservable", a.Da);a.b("isWritableObservable", a.Da);a.b("observable.fn", B);a.H(B, "peek", B.p);a.H(B, "valueHasMutated", B.ha);a.H(B, "valueWillMutate", B.ia);a.ma = function (b) {
          b = b || [];if ("object" != (typeof b === "undefined" ? "undefined" : _typeof(b)) || !("length" in b)) throw Error("The argument passed when initializing an observable array must be an array, or null, or undefined.");b = a.O(b);a.a.ab(b, a.ma.fn);return b.extend({ trackArrayChanges: !0 });
        };
        a.ma.fn = { remove: function remove(b) {
            for (var c = this.p(), d = [], e = "function" != typeof b || a.I(b) ? function (a) {
              return a === b;
            } : b, f = 0; f < c.length; f++) {
              var g = c[f];e(g) && (0 === d.length && this.ia(), d.push(g), c.splice(f, 1), f--);
            }d.length && this.ha();return d;
          }, removeAll: function removeAll(b) {
            if (b === n) {
              var c = this.p(),
                  d = c.slice(0);this.ia();c.splice(0, c.length);this.ha();return d;
            }return b ? this.remove(function (c) {
              return 0 <= a.a.o(b, c);
            }) : [];
          }, destroy: function destroy(b) {
            var c = this.p(),
                d = "function" != typeof b || a.I(b) ? function (a) {
              return a === b;
            } : b;this.ia();
            for (var e = c.length - 1; 0 <= e; e--) {
              d(c[e]) && (c[e]._destroy = !0);
            }this.ha();
          }, destroyAll: function destroyAll(b) {
            return b === n ? this.destroy(function () {
              return !0;
            }) : b ? this.destroy(function (c) {
              return 0 <= a.a.o(b, c);
            }) : [];
          }, indexOf: function indexOf(b) {
            var c = this();return a.a.o(c, b);
          }, replace: function replace(a, c) {
            var d = this.indexOf(a);0 <= d && (this.ia(), this.p()[d] = c, this.ha());
          } };a.a.la && a.a.$a(a.ma.fn, a.O.fn);a.a.r("pop push reverse shift sort splice unshift".split(" "), function (b) {
          a.ma.fn[b] = function () {
            var a = this.p();this.ia();this.Yb(a, b, arguments);
            var d = a[b].apply(a, arguments);this.ha();return d === a ? this : d;
          };
        });a.a.r(["slice"], function (b) {
          a.ma.fn[b] = function () {
            var a = this();return a[b].apply(a, arguments);
          };
        });a.b("observableArray", a.ma);a.Aa.trackArrayChanges = function (b, c) {
          function d() {
            if (!e) {
              e = !0;l = b.notifySubscribers;b.notifySubscribers = function (a, b) {
                b && b !== E || ++h;return l.apply(this, arguments);
              };var c = [].concat(b.p() || []);f = null;g = b.Y(function (d) {
                d = [].concat(d || []);if (b.Ra("arrayChange")) {
                  var e;if (!f || 1 < h) f = a.a.lb(c, d, b.kb);e = f;
                }c = d;f = null;h = 0;
                e && e.length && b.notifySubscribers(e, "arrayChange");
              });
            }
          }b.kb = {};c && "object" == (typeof c === "undefined" ? "undefined" : _typeof(c)) && a.a.extend(b.kb, c);b.kb.sparse = !0;if (!b.Yb) {
            var e = !1,
                f = null,
                g,
                h = 0,
                l,
                m = b.ua,
                k = b.Ka;b.ua = function (a) {
              m && m.call(b, a);"arrayChange" === a && d();
            };b.Ka = function (a) {
              k && k.call(b, a);"arrayChange" !== a || b.Ra("arrayChange") || (l && (b.notifySubscribers = l, l = n), g.k(), e = !1);
            };b.Yb = function (b, c, d) {
              function k(a, b, c) {
                return m[m.length] = { status: a, value: b, index: c };
              }if (e && !h) {
                var m = [],
                    l = b.length,
                    g = d.length,
                    G = 0;switch (c) {case "push":
                    G = l;case "unshift":
                    for (c = 0; c < g; c++) {
                      k("added", d[c], G + c);
                    }break;case "pop":
                    G = l - 1;case "shift":
                    l && k("deleted", b[G], G);break;case "splice":
                    c = Math.min(Math.max(0, 0 > d[0] ? l + d[0] : d[0]), l);for (var l = 1 === g ? l : Math.min(c + (d[1] || 0), l), g = c + g - 2, G = Math.max(l, g), n = [], s = [], w = 2; c < G; ++c, ++w) {
                      c < l && s.push(k("deleted", b[c], c)), c < g && n.push(k("added", d[w], c));
                    }a.a.hc(s, n);break;default:
                    return;}f = m;
              }
            };
          }
        };var s = a.a.bc("_state");a.m = a.B = function (b, c, d) {
          function e() {
            if (0 < arguments.length) {
              if ("function" === typeof f) f.apply(g.sb, arguments);else throw Error("Cannot write a value to a ko.computed unless you specify a 'write' option. If you wish to read the current value, don't pass any parameters.");
              return this;
            }a.l.sc(e);(g.V || g.t && e.Sa()) && e.U();return g.M;
          }"object" === (typeof b === "undefined" ? "undefined" : _typeof(b)) ? d = b : (d = d || {}, b && (d.read = b));if ("function" != typeof d.read) throw Error("Pass a function that returns the value of the ko.computed");var f = d.write,
              g = { M: n, da: !0, V: !0, Ta: !1, Hb: !1, T: !1, Ya: !1, t: !1, od: d.read, sb: c || d.owner, i: d.disposeWhenNodeIsRemoved || d.i || null, ya: d.disposeWhen || d.ya, pb: null, s: {}, L: 0, fc: null };e[s] = g;e.$c = "function" === typeof f;a.a.la || a.a.extend(e, a.K.fn);a.K.fn.ub(e);a.a.ab(e, z);d.pure ? (g.Ya = !0, g.t = !0, a.a.extend(e, Y)) : d.deferEvaluation && a.a.extend(e, Z);a.options.deferUpdates && a.Aa.deferred(e, !0);g.i && (g.Hb = !0, g.i.nodeType || (g.i = null));g.t || d.deferEvaluation || e.U();g.i && e.ca() && a.a.G.qa(g.i, g.pb = function () {
            e.k();
          });return e;
        };var z = { equalityComparer: J, Ca: function Ca() {
            return this[s].L;
          }, Sb: function Sb(a, c, d) {
            if (this[s].Ya && c === this) throw Error("A 'pure' computed must not be called recursively");this[s].s[a] = d;d.Ia = this[s].L++;d.pa = c.Pa();
          }, Sa: function Sa() {
            var a,
                c,
                d = this[s].s;for (a in d) {
              if (d.hasOwnProperty(a) && (c = d[a], this.oa && c.$.Ha || c.$.Zc(c.pa))) return !0;
            }
          }, gd: function gd() {
            this.oa && !this[s].Ta && this.oa(!1);
          }, ca: function ca() {
            var a = this[s];return a.V || 0 < a.L;
          }, qd: function qd() {
            this.Ha ? this[s].V && (this[s].da = !0) : this.ec();
          }, yc: function yc(a) {
            if (a.gb && !this[s].i) {
              var c = a.Y(this.gd, this, "dirty"),
                  d = a.Y(this.qd, this);return { $: a, k: function k() {
                  c.k();d.k();
                } };
            }return a.Y(this.ec, this);
          }, ec: function ec() {
            var b = this,
                c = b.throttleEvaluation;c && 0 <= c ? (clearTimeout(this[s].fc), this[s].fc = a.a.setTimeout(function () {
              b.U(!0);
            }, c)) : b.oa ? b.oa(!0) : b.U(!0);
          }, U: function U(b) {
            var c = this[s],
                d = c.ya,
                e = !1;if (!c.Ta && !c.T) {
              if (c.i && !a.a.qb(c.i) || d && d()) {
                if (!c.Hb) {
                  this.k();return;
                }
              } else c.Hb = !1;c.Ta = !0;try {
                e = this.Vc(b);
              } finally {
                c.Ta = !1;
              }c.L || this.k();return e;
            }
          }, Vc: function Vc(b) {
            var c = this[s],
                d = !1,
                e = c.Ya ? n : !c.L,
                f = { Mc: this, Oa: c.s, ob: c.L };a.l.Xb({ Lc: f, jb: W, m: this, Va: e });c.s = {};c.L = 0;f = this.Uc(c, f);this.Ua(c.M, f) && (c.t || this.notifySubscribers(c.M, "beforeChange"), c.M = f, c.t ? this.Kb() : b && this.notifySubscribers(c.M), d = !0);e && this.notifySubscribers(c.M, "awake");return d;
          }, Uc: function Uc(b, c) {
            try {
              var d = b.od;return b.sb ? d.call(b.sb) : d();
            } finally {
              a.l.end(), c.ob && !b.t && a.a.D(c.Oa, V), b.da = b.V = !1;
            }
          }, p: function p(a) {
            var c = this[s];(c.V && (a || !c.L) || c.t && this.Sa()) && this.U();return c.M;
          }, Wa: function Wa(b) {
            a.K.fn.Wa.call(this, b);this.Mb = function () {
              this[s].da ? this.U() : this[s].V = !1;return this[s].M;
            };this.oa = function (a) {
              this.Ob(this[s].M);this[s].V = !0;a && (this[s].da = !0);this.Pb(this);
            };
          }, k: function k() {
            var b = this[s];!b.t && b.s && a.a.D(b.s, function (a, b) {
              b.k && b.k();
            });b.i && b.pb && a.a.G.tc(b.i, b.pb);b.s = null;b.L = 0;b.T = !0;b.da = !1;b.V = !1;b.t = !1;b.i = null;
          } },
            Y = { ua: function ua(b) {
            var c = this,
                d = c[s];if (!d.T && d.t && "change" == b) {
              d.t = !1;if (d.da || c.Sa()) d.s = null, d.L = 0, c.U() && c.Kb();else {
                var e = [];a.a.D(d.s, function (a, b) {
                  e[b.Ia] = a;
                });a.a.r(e, function (a, b) {
                  var e = d.s[a],
                      l = c.yc(e.$);l.Ia = b;l.pa = e.pa;d.s[a] = l;
                });
              }d.T || c.notifySubscribers(d.M, "awake");
            }
          }, Ka: function Ka(b) {
            var c = this[s];c.T || "change" != b || this.Ra("change") || (a.a.D(c.s, function (a, b) {
              b.k && (c.s[a] = { $: b.$, Ia: b.Ia, pa: b.pa }, b.k());
            }), c.t = !0, this.notifySubscribers(n, "asleep"));
          }, Pa: function Pa() {
            var b = this[s];b.t && (b.da || this.Sa()) && this.U();return a.K.fn.Pa.call(this);
          } },
            Z = { ua: function ua(a) {
            "change" != a && "beforeChange" != a || this.p();
          } };a.a.la && a.a.$a(z, a.K.fn);var P = a.O.md;a.m[P] = a.O;z[P] = a.m;a.bd = function (b) {
          return a.Qa(b, a.m);
        };a.cd = function (b) {
          return a.Qa(b, a.m) && b[s] && b[s].Ya;
        };a.b("computed", a.m);a.b("dependentObservable", a.m);a.b("isComputed", a.bd);a.b("isPureComputed", a.cd);a.b("computed.fn", z);a.H(z, "peek", z.p);a.H(z, "dispose", z.k);a.H(z, "isActive", z.ca);a.H(z, "getDependenciesCount", z.Ca);a.rc = function (b, c) {
          if ("function" === typeof b) return a.m(b, c, { pure: !0 });b = a.a.extend({}, b);b.pure = !0;return a.m(b, c);
        };a.b("pureComputed", a.rc);(function () {
          function b(a, f, g) {
            g = g || new d();a = f(a);if ("object" != (typeof a === "undefined" ? "undefined" : _typeof(a)) || null === a || a === n || a instanceof RegExp || a instanceof Date || a instanceof String || a instanceof Number || a instanceof Boolean) return a;var h = a instanceof Array ? [] : {};g.save(a, h);c(a, function (c) {
              var d = f(a[c]);switch (typeof d === "undefined" ? "undefined" : _typeof(d)) {case "boolean":case "number":case "string":case "function":
                  h[c] = d;break;case "object":case "undefined":
                  var k = g.get(d);h[c] = k !== n ? k : b(d, f, g);}
            });return h;
          }function c(a, b) {
            if (a instanceof Array) {
              for (var c = 0; c < a.length; c++) {
                b(c);
              }"function" == typeof a.toJSON && b("toJSON");
            } else for (c in a) {
              b(c);
            }
          }function d() {
            this.keys = [];this.Lb = [];
          }a.Ac = function (c) {
            if (0 == arguments.length) throw Error("When calling ko.toJS, pass the object you want to convert.");return b(c, function (b) {
              for (var c = 0; a.I(b) && 10 > c; c++) {
                b = b();
              }return b;
            });
          };a.toJSON = function (b, c, d) {
            b = a.Ac(b);return a.a.Gb(b, c, d);
          };d.prototype = { save: function save(b, c) {
              var d = a.a.o(this.keys, b);0 <= d ? this.Lb[d] = c : (this.keys.push(b), this.Lb.push(c));
            }, get: function get$$1(b) {
              b = a.a.o(this.keys, b);return 0 <= b ? this.Lb[b] : n;
            } };
        })();a.b("toJS", a.Ac);a.b("toJSON", a.toJSON);(function () {
          a.j = { u: function u(b) {
              switch (a.a.A(b)) {case "option":
                  return !0 === b.__ko__hasDomDataOptionValue__ ? a.a.e.get(b, a.d.options.zb) : 7 >= a.a.C ? b.getAttributeNode("value") && b.getAttributeNode("value").specified ? b.value : b.text : b.value;case "select":
                  return 0 <= b.selectedIndex ? a.j.u(b.options[b.selectedIndex]) : n;default:
                  return b.value;}
            }, ja: function ja(b, c, d) {
              switch (a.a.A(b)) {case "option":
                  switch (typeof c === "undefined" ? "undefined" : _typeof(c)) {case "string":
                      a.a.e.set(b, a.d.options.zb, n);"__ko__hasDomDataOptionValue__" in b && delete b.__ko__hasDomDataOptionValue__;b.value = c;break;default:
                      a.a.e.set(b, a.d.options.zb, c), b.__ko__hasDomDataOptionValue__ = !0, b.value = "number" === typeof c ? c : "";}break;case "select":
                  if ("" === c || null === c) c = n;for (var e = -1, f = 0, g = b.options.length, h; f < g; ++f) {
                    if (h = a.j.u(b.options[f]), h == c || "" == h && c === n) {
                      e = f;break;
                    }
                  }if (d || 0 <= e || c === n && 1 < b.size) b.selectedIndex = e;break;default:
                  if (null === c || c === n) c = "";b.value = c;}
            } };
        })();a.b("selectExtensions", a.j);a.b("selectExtensions.readValue", a.j.u);a.b("selectExtensions.writeValue", a.j.ja);a.h = function () {
          function b(b) {
            b = a.a.cb(b);123 === b.charCodeAt(0) && (b = b.slice(1, -1));var c = [],
                d = b.match(e),
                r,
                h = [],
                p = 0;if (d) {
              d.push(",");for (var A = 0, y; y = d[A]; ++A) {
                var v = y.charCodeAt(0);if (44 === v) {
                  if (0 >= p) {
                    c.push(r && h.length ? { key: r, value: h.join("") } : { unknown: r || h.join("") });r = p = 0;h = [];continue;
                  }
                } else if (58 === v) {
                  if (!p && !r && 1 === h.length) {
                    r = h.pop();continue;
                  }
                } else 47 === v && A && 1 < y.length ? (v = d[A - 1].match(f)) && !g[v[0]] && (b = b.substr(b.indexOf(y) + 1), d = b.match(e), d.push(","), A = -1, y = "/") : 40 === v || 123 === v || 91 === v ? ++p : 41 === v || 125 === v || 93 === v ? --p : r || h.length || 34 !== v && 39 !== v || (y = y.slice(1, -1));h.push(y);
              }
            }return c;
          }var c = ["true", "false", "null", "undefined"],
              d = /^(?:[$_a-z][$\w]*|(.+)(\.\s*[$_a-z][$\w]*|\[.+\]))$/i,
              e = RegExp("\"(?:[^\"\\\\]|\\\\.)*\"|'(?:[^'\\\\]|\\\\.)*'|/(?:[^/\\\\]|\\\\.)*/w*|[^\\s:,/][^,\"'{}()/:[\\]]*[^\\s,\"'{}()/:[\\]]|[^\\s]", "g"),
              f = /[\])"'A-Za-z0-9_$]+$/,
              g = { "in": 1, "return": 1, "typeof": 1 },
              h = {};return { va: [], ga: h, Ab: b, Xa: function Xa(e, m) {
              function k(b, e) {
                var m;if (!A) {
                  var l = a.getBindingHandler(b);if (l && l.preprocess && !(e = l.preprocess(e, b, k))) return;if (l = h[b]) m = e, 0 <= a.a.o(c, m) ? m = !1 : (l = m.match(d), m = null === l ? !1 : l[1] ? "Object(" + l[1] + ")" + l[2] : m), l = m;l && g.push("'" + b + "':function(_z){" + m + "=_z}");
                }p && (e = "function(){return " + e + " }");f.push("'" + b + "':" + e);
              }m = m || {};var f = [],
                  g = [],
                  p = m.valueAccessors,
                  A = m.bindingParams,
                  y = "string" === typeof e ? b(e) : e;a.a.r(y, function (a) {
                k(a.key || a.unknown, a.value);
              });g.length && k("_ko_property_writers", "{" + g.join(",") + " }");return f.join(",");
            }, fd: function fd(a, b) {
              for (var c = 0; c < a.length; c++) {
                if (a[c].key == b) return !0;
              }return !1;
            }, Ga: function Ga(b, c, d, e, f) {
              if (b && a.I(b)) !a.Da(b) || f && b.p() === e || b(e);else if ((b = c.get("_ko_property_writers")) && b[d]) b[d](e);
            } };
        }();a.b("expressionRewriting", a.h);a.b("expressionRewriting.bindingRewriteValidators", a.h.va);a.b("expressionRewriting.parseObjectLiteral", a.h.Ab);a.b("expressionRewriting.preProcessBindings", a.h.Xa);a.b("expressionRewriting._twoWayBindings", a.h.ga);a.b("jsonExpressionRewriting", a.h);a.b("jsonExpressionRewriting.insertPropertyAccessorsIntoJson", a.h.Xa);(function () {
          function b(a) {
            return 8 == a.nodeType && g.test(f ? a.text : a.nodeValue);
          }function c(a) {
            return 8 == a.nodeType && h.test(f ? a.text : a.nodeValue);
          }function d(a, d) {
            for (var e = a, f = 1, l = []; e = e.nextSibling;) {
              if (c(e) && (f--, 0 === f)) return l;l.push(e);b(e) && f++;
            }if (!d) throw Error("Cannot find closing comment tag to match: " + a.nodeValue);return null;
          }function e(a, b) {
            var c = d(a, b);return c ? 0 < c.length ? c[c.length - 1].nextSibling : a.nextSibling : null;
          }var f = t && "\x3c!--test--\x3e" === t.createComment("test").text,
              g = f ? /^\x3c!--\s*ko(?:\s+([\s\S]+))?\s*--\x3e$/ : /^\s*ko(?:\s+([\s\S]+))?\s*$/,
              h = f ? /^\x3c!--\s*\/ko\s*--\x3e$/ : /^\s*\/ko\s*$/,
              l = { ul: !0, ol: !0 };a.f = { aa: {}, childNodes: function childNodes(a) {
              return b(a) ? d(a) : a.childNodes;
            }, za: function za(c) {
              if (b(c)) {
                c = a.f.childNodes(c);for (var d = 0, e = c.length; d < e; d++) {
                  a.removeNode(c[d]);
                }
              } else a.a.rb(c);
            }, fa: function fa(c, d) {
              if (b(c)) {
                a.f.za(c);for (var e = c.nextSibling, f = 0, l = d.length; f < l; f++) {
                  e.parentNode.insertBefore(d[f], e);
                }
              } else a.a.fa(c, d);
            }, qc: function qc(a, c) {
              b(a) ? a.parentNode.insertBefore(c, a.nextSibling) : a.firstChild ? a.insertBefore(c, a.firstChild) : a.appendChild(c);
            }, kc: function kc(c, d, e) {
              e ? b(c) ? c.parentNode.insertBefore(d, e.nextSibling) : e.nextSibling ? c.insertBefore(d, e.nextSibling) : c.appendChild(d) : a.f.qc(c, d);
            }, firstChild: function firstChild(a) {
              return b(a) ? !a.nextSibling || c(a.nextSibling) ? null : a.nextSibling : a.firstChild;
            }, nextSibling: function nextSibling(a) {
              b(a) && (a = e(a));return a.nextSibling && c(a.nextSibling) ? null : a.nextSibling;
            }, Yc: b, vd: function vd(a) {
              return (a = (f ? a.text : a.nodeValue).match(g)) ? a[1] : null;
            }, oc: function oc(d) {
              if (l[a.a.A(d)]) {
                var k = d.firstChild;if (k) {
                  do {
                    if (1 === k.nodeType) {
                      var f;f = k.firstChild;var g = null;if (f) {
                        do {
                          if (g) g.push(f);else if (b(f)) {
                            var h = e(f, !0);h ? f = h : g = [f];
                          } else c(f) && (g = [f]);
                        } while (f = f.nextSibling);
                      }if (f = g) for (g = k.nextSibling, h = 0; h < f.length; h++) {
                        g ? d.insertBefore(f[h], g) : d.appendChild(f[h]);
                      }
                    }
                  } while (k = k.nextSibling);
                }
              }
            } };
        })();a.b("virtualElements", a.f);a.b("virtualElements.allowedBindings", a.f.aa);a.b("virtualElements.emptyNode", a.f.za);a.b("virtualElements.insertAfter", a.f.kc);a.b("virtualElements.prepend", a.f.qc);a.b("virtualElements.setDomNodeChildren", a.f.fa);(function () {
          a.S = function () {
            this.Kc = {};
          };a.a.extend(a.S.prototype, { nodeHasBindings: function nodeHasBindings(b) {
              switch (b.nodeType) {case 1:
                  return null != b.getAttribute("data-bind") || a.g.getComponentNameForNode(b);case 8:
                  return a.f.Yc(b);default:
                  return !1;}
            }, getBindings: function getBindings(b, c) {
              var d = this.getBindingsString(b, c),
                  d = d ? this.parseBindingsString(d, c, b) : null;return a.g.Rb(d, b, c, !1);
            }, getBindingAccessors: function getBindingAccessors(b, c) {
              var d = this.getBindingsString(b, c),
                  d = d ? this.parseBindingsString(d, c, b, { valueAccessors: !0 }) : null;return a.g.Rb(d, b, c, !0);
            }, getBindingsString: function getBindingsString(b) {
              switch (b.nodeType) {case 1:
                  return b.getAttribute("data-bind");case 8:
                  return a.f.vd(b);default:
                  return null;}
            }, parseBindingsString: function parseBindingsString(b, c, d, e) {
              try {
                var f = this.Kc,
                    g = b + (e && e.valueAccessors || ""),
                    h;if (!(h = f[g])) {
                  var l,
                      m = "with($context){with($data||{}){return{" + a.h.Xa(b, e) + "}}}";l = new Function("$context", "$element", m);h = f[g] = l;
                }return h(c, d);
              } catch (k) {
                throw k.message = "Unable to parse bindings.\nBindings value: " + b + "\nMessage: " + k.message, k;
              }
            } });a.S.instance = new a.S();
        })();a.b("bindingProvider", a.S);(function () {
          function b(a) {
            return function () {
              return a;
            };
          }function c(a) {
            return a();
          }function d(b) {
            return a.a.Ea(a.l.w(b), function (a, c) {
              return function () {
                return b()[c];
              };
            });
          }function e(c, e, k) {
            return "function" === typeof c ? d(c.bind(null, e, k)) : a.a.Ea(c, b);
          }function f(a, b) {
            return d(this.getBindings.bind(this, a, b));
          }function g(b, c, d) {
            var e,
                k = a.f.firstChild(c),
                f = a.S.instance,
                m = f.preprocessNode;if (m) {
              for (; e = k;) {
                k = a.f.nextSibling(e), m.call(f, e);
              }k = a.f.firstChild(c);
            }for (; e = k;) {
              k = a.f.nextSibling(e), h(b, e, d);
            }
          }function h(b, c, d) {
            var e = !0,
                k = 1 === c.nodeType;k && a.f.oc(c);if (k && d || a.S.instance.nodeHasBindings(c)) e = m(c, null, b, d).shouldBindDescendants;e && !r[a.a.A(c)] && g(b, c, !k);
          }function l(b) {
            var c = [],
                d = {},
                e = [];a.a.D(b, function X(k) {
              if (!d[k]) {
                var f = a.getBindingHandler(k);f && (f.after && (e.push(k), a.a.r(f.after, function (c) {
                  if (b[c]) {
                    if (-1 !== a.a.o(e, c)) throw Error("Cannot combine the following bindings, because they have a cyclic dependency: " + e.join(", "));
                    X(c);
                  }
                }), e.length--), c.push({ key: k, jc: f }));d[k] = !0;
              }
            });return c;
          }function m(b, d, e, k) {
            var m = a.a.e.get(b, q);if (!d) {
              if (m) throw Error("You cannot apply bindings multiple times to the same element.");a.a.e.set(b, q, !0);
            }!m && k && a.xc(b, e);var g;if (d && "function" !== typeof d) g = d;else {
              var h = a.S.instance,
                  r = h.getBindingAccessors || f,
                  p = a.B(function () {
                (g = d ? d(e, b) : r.call(h, b, e)) && e.Q && e.Q();return g;
              }, null, { i: b });g && p.ca() || (p = null);
            }var s;if (g) {
              var t = p ? function (a) {
                return function () {
                  return c(p()[a]);
                };
              } : function (a) {
                return g[a];
              },
                  u = function u() {
                return a.a.Ea(p ? p() : g, c);
              };u.get = function (a) {
                return g[a] && c(t(a));
              };u.has = function (a) {
                return a in g;
              };k = l(g);a.a.r(k, function (c) {
                var d = c.jc.init,
                    k = c.jc.update,
                    f = c.key;if (8 === b.nodeType && !a.f.aa[f]) throw Error("The binding '" + f + "' cannot be used with virtual elements");try {
                  "function" == typeof d && a.l.w(function () {
                    var a = d(b, t(f), u, e.$data, e);if (a && a.controlsDescendantBindings) {
                      if (s !== n) throw Error("Multiple bindings (" + s + " and " + f + ") are trying to control descendant bindings of the same element. You cannot use these bindings together on the same element.");
                      s = f;
                    }
                  }), "function" == typeof k && a.B(function () {
                    k(b, t(f), u, e.$data, e);
                  }, null, { i: b });
                } catch (m) {
                  throw m.message = 'Unable to process binding "' + f + ": " + g[f] + '"\nMessage: ' + m.message, m;
                }
              });
            }return { shouldBindDescendants: s === n };
          }function k(b) {
            return b && b instanceof a.R ? b : new a.R(b);
          }a.d = {};var r = { script: !0, textarea: !0, template: !0 };a.getBindingHandler = function (b) {
            return a.d[b];
          };a.R = function (b, c, d, e, k) {
            function f() {
              var k = g ? b() : b,
                  m = a.a.c(k);c ? (c.Q && c.Q(), a.a.extend(l, c), l.Q = r) : (l.$parents = [], l.$root = m, l.ko = a);l.$rawData = k;l.$data = m;d && (l[d] = m);e && e(l, c, m);return l.$data;
            }function m() {
              return h && !a.a.Tb(h);
            }var l = this,
                g = "function" == typeof b && !a.I(b),
                h,
                r;k && k.exportDependencies ? f() : (r = a.B(f, null, { ya: m, i: !0 }), r.ca() && (l.Q = r, r.equalityComparer = null, h = [], r.Dc = function (b) {
              h.push(b);a.a.G.qa(b, function (b) {
                a.a.Na(h, b);h.length || (r.k(), l.Q = r = n);
              });
            }));
          };a.R.prototype.createChildContext = function (b, c, d, e) {
            return new a.R(b, this, c, function (a, b) {
              a.$parentContext = b;a.$parent = b.$data;a.$parents = (b.$parents || []).slice(0);a.$parents.unshift(a.$parent);
              d && d(a);
            }, e);
          };a.R.prototype.extend = function (b) {
            return new a.R(this.Q || this.$data, this, null, function (c, d) {
              c.$rawData = d.$rawData;a.a.extend(c, "function" == typeof b ? b() : b);
            });
          };a.R.prototype.ac = function (a, b) {
            return this.createChildContext(a, b, null, { exportDependencies: !0 });
          };var q = a.a.e.J(),
              p = a.a.e.J();a.xc = function (b, c) {
            if (2 == arguments.length) a.a.e.set(b, p, c), c.Q && c.Q.Dc(b);else return a.a.e.get(b, p);
          };a.La = function (b, c, d) {
            1 === b.nodeType && a.f.oc(b);return m(b, c, k(d), !0);
          };a.Ic = function (b, c, d) {
            d = k(d);return a.La(b, e(c, d, b), d);
          };a.hb = function (a, b) {
            1 !== b.nodeType && 8 !== b.nodeType || g(k(a), b, !0);
          };a.Ub = function (a, b) {
            !u && x.jQuery && (u = x.jQuery);if (b && 1 !== b.nodeType && 8 !== b.nodeType) throw Error("ko.applyBindings: first parameter should be your view model; second parameter should be a DOM node");b = b || x.document.body;h(k(a), b, !0);
          };a.nb = function (b) {
            switch (b.nodeType) {case 1:case 8:
                var c = a.xc(b);if (c) return c;if (b.parentNode) return a.nb(b.parentNode);}return n;
          };a.Oc = function (b) {
            return (b = a.nb(b)) ? b.$data : n;
          };a.b("bindingHandlers", a.d);a.b("applyBindings", a.Ub);a.b("applyBindingsToDescendants", a.hb);a.b("applyBindingAccessorsToNode", a.La);a.b("applyBindingsToNode", a.Ic);a.b("contextFor", a.nb);a.b("dataFor", a.Oc);
        })();(function (b) {
          function c(c, e) {
            var m = f.hasOwnProperty(c) ? f[c] : b,
                k;m ? m.Y(e) : (m = f[c] = new a.K(), m.Y(e), d(c, function (b, d) {
              var e = !(!d || !d.synchronous);g[c] = { definition: b, dd: e };delete f[c];k || e ? m.notifySubscribers(b) : a.Z.Za(function () {
                m.notifySubscribers(b);
              });
            }), k = !0);
          }function d(a, b) {
            e("getConfig", [a], function (c) {
              c ? e("loadComponent", [a, c], function (a) {
                b(a, c);
              }) : b(null, null);
            });
          }function e(c, d, f, k) {
            k || (k = a.g.loaders.slice(0));var g = k.shift();if (g) {
              var q = g[c];if (q) {
                var p = !1;if (q.apply(g, d.concat(function (a) {
                  p ? f(null) : null !== a ? f(a) : e(c, d, f, k);
                })) !== b && (p = !0, !g.suppressLoaderExceptions)) throw Error("Component loaders must supply values by invoking the callback, not by returning values synchronously.");
              } else e(c, d, f, k);
            } else f(null);
          }var f = {},
              g = {};a.g = { get: function get$$1(d, e) {
              var f = g.hasOwnProperty(d) ? g[d] : b;f ? f.dd ? a.l.w(function () {
                e(f.definition);
              }) : a.Z.Za(function () {
                e(f.definition);
              }) : c(d, e);
            }, $b: function $b(a) {
              delete g[a];
            }, Nb: e };a.g.loaders = [];a.b("components", a.g);a.b("components.get", a.g.get);a.b("components.clearCachedDefinition", a.g.$b);
        })();(function () {
          function b(b, c, d, e) {
            function g() {
              0 === --y && e(h);
            }var h = {},
                y = 2,
                v = d.template;d = d.viewModel;v ? f(c, v, function (c) {
              a.g.Nb("loadTemplate", [b, c], function (a) {
                h.template = a;g();
              });
            }) : g();d ? f(c, d, function (c) {
              a.g.Nb("loadViewModel", [b, c], function (a) {
                h[l] = a;g();
              });
            }) : g();
          }function c(a, b, d) {
            if ("function" === typeof b) d(function (a) {
              return new b(a);
            });else if ("function" === typeof b[l]) d(b[l]);else if ("instance" in b) {
              var e = b.instance;d(function () {
                return e;
              });
            } else "viewModel" in b ? c(a, b.viewModel, d) : a("Unknown viewModel value: " + b);
          }function d(b) {
            switch (a.a.A(b)) {case "script":
                return a.a.na(b.text);case "textarea":
                return a.a.na(b.value);case "template":
                if (e(b.content)) return a.a.wa(b.content.childNodes);}return a.a.wa(b.childNodes);
          }function e(a) {
            return x.DocumentFragment ? a instanceof DocumentFragment : a && 11 === a.nodeType;
          }function f(a, b, c) {
            "string" === typeof b.require ? O || x.require ? (O || x.require)([b.require], c) : a("Uses require, but no AMD loader is present") : c(b);
          }function g(a) {
            return function (b) {
              throw Error("Component '" + a + "': " + b);
            };
          }var h = {};a.g.register = function (b, c) {
            if (!c) throw Error("Invalid configuration for " + b);if (a.g.wb(b)) throw Error("Component " + b + " is already registered");h[b] = c;
          };a.g.wb = function (a) {
            return h.hasOwnProperty(a);
          };a.g.ud = function (b) {
            delete h[b];a.g.$b(b);
          };a.g.cc = { getConfig: function getConfig(a, b) {
              b(h.hasOwnProperty(a) ? h[a] : null);
            }, loadComponent: function loadComponent(a, c, d) {
              var e = g(a);f(e, c, function (c) {
                b(a, e, c, d);
              });
            }, loadTemplate: function loadTemplate(b, c, f) {
              b = g(b);if ("string" === typeof c) f(a.a.na(c));else if (c instanceof Array) f(c);else if (e(c)) f(a.a.W(c.childNodes));else if (c.element) {
                if (c = c.element, x.HTMLElement ? c instanceof HTMLElement : c && c.tagName && 1 === c.nodeType) f(d(c));else if ("string" === typeof c) {
                  var l = t.getElementById(c);l ? f(d(l)) : b("Cannot find element with ID " + c);
                } else b("Unknown element type: " + c);
              } else b("Unknown template value: " + c);
            }, loadViewModel: function loadViewModel(a, b, d) {
              c(g(a), b, d);
            } };var l = "createViewModel";a.b("components.register", a.g.register);a.b("components.isRegistered", a.g.wb);a.b("components.unregister", a.g.ud);a.b("components.defaultLoader", a.g.cc);a.g.loaders.push(a.g.cc);a.g.Ec = h;
        })();(function () {
          function b(b, e) {
            var f = b.getAttribute("params");if (f) {
              var f = c.parseBindingsString(f, e, b, { valueAccessors: !0, bindingParams: !0 }),
                  f = a.a.Ea(f, function (c) {
                return a.m(c, null, { i: b });
              }),
                  g = a.a.Ea(f, function (c) {
                var e = c.p();return c.ca() ? a.m({ read: function read() {
                    return a.a.c(c());
                  }, write: a.Da(e) && function (a) {
                    c()(a);
                  }, i: b }) : e;
              });g.hasOwnProperty("$raw") || (g.$raw = f);return g;
            }return { $raw: {} };
          }a.g.getComponentNameForNode = function (b) {
            var c = a.a.A(b);if (a.g.wb(c) && (-1 != c.indexOf("-") || "[object HTMLUnknownElement]" == "" + b || 8 >= a.a.C && b.tagName === c)) return c;
          };a.g.Rb = function (c, e, f, g) {
            if (1 === e.nodeType) {
              var h = a.g.getComponentNameForNode(e);if (h) {
                c = c || {};if (c.component) throw Error('Cannot use the "component" binding on a custom element matching a component');var l = { name: h, params: b(e, f) };c.component = g ? function () {
                  return l;
                } : l;
              }
            }return c;
          };var c = new a.S();9 > a.a.C && (a.g.register = function (a) {
            return function (b) {
              t.createElement(b);return a.apply(this, arguments);
            };
          }(a.g.register), t.createDocumentFragment = function (b) {
            return function () {
              var c = b(),
                  f = a.g.Ec,
                  g;for (g in f) {
                f.hasOwnProperty(g) && c.createElement(g);
              }return c;
            };
          }(t.createDocumentFragment));
        })();(function (b) {
          function c(b, c, d) {
            c = c.template;if (!c) throw Error("Component '" + b + "' has no template");b = a.a.wa(c);a.f.fa(d, b);
          }function d(a, b, c, d) {
            var e = a.createViewModel;return e ? e.call(a, d, { element: b, templateNodes: c }) : d;
          }var e = 0;a.d.component = { init: function init(f, g, h, l, m) {
              function k() {
                var a = r && r.dispose;"function" === typeof a && a.call(r);q = r = null;
              }var r,
                  q,
                  p = a.a.W(a.f.childNodes(f));a.a.G.qa(f, k);a.m(function () {
                var l = a.a.c(g()),
                    h,
                    v;"string" === typeof l ? h = l : (h = a.a.c(l.name), v = a.a.c(l.params));if (!h) throw Error("No component name specified");var n = q = ++e;a.g.get(h, function (e) {
                  if (q === n) {
                    k();if (!e) throw Error("Unknown component '" + h + "'");c(h, e, f);var l = d(e, f, p, v);e = m.createChildContext(l, b, function (a) {
                      a.$component = l;a.$componentTemplateNodes = p;
                    });r = l;a.hb(e, f);
                  }
                });
              }, null, { i: f });return { controlsDescendantBindings: !0 };
            } };a.f.aa.component = !0;
        })();var Q = { "class": "className", "for": "htmlFor" };a.d.attr = { update: function update(b, c) {
            var d = a.a.c(c()) || {};a.a.D(d, function (c, d) {
              d = a.a.c(d);var g = !1 === d || null === d || d === n;g && b.removeAttribute(c);8 >= a.a.C && c in Q ? (c = Q[c], g ? b.removeAttribute(c) : b[c] = d) : g || b.setAttribute(c, d.toString());"name" === c && a.a.vc(b, g ? "" : d.toString());
            });
          } };(function () {
          a.d.checked = { after: ["value", "attr"], init: function init(b, c, d) {
              function e() {
                var e = b.checked,
                    f = p ? g() : e;if (!a.xa.Va() && (!l || e)) {
                  var h = a.l.w(c);if (k) {
                    var m = r ? h.p() : h;q !== f ? (e && (a.a.ra(m, f, !0), a.a.ra(m, q, !1)), q = f) : a.a.ra(m, f, e);r && a.Da(h) && h(m);
                  } else a.h.Ga(h, d, "checked", f, !0);
                }
              }function f() {
                var d = a.a.c(c());b.checked = k ? 0 <= a.a.o(d, g()) : h ? d : g() === d;
              }var g = a.rc(function () {
                return d.has("checkedValue") ? a.a.c(d.get("checkedValue")) : d.has("value") ? a.a.c(d.get("value")) : b.value;
              }),
                  h = "checkbox" == b.type,
                  l = "radio" == b.type;if (h || l) {
                var m = c(),
                    k = h && a.a.c(m) instanceof Array,
                    r = !(k && m.push && m.splice),
                    q = k ? g() : n,
                    p = l || k;l && !b.name && a.d.uniqueName.init(b, function () {
                  return !0;
                });a.m(e, null, { i: b });a.a.q(b, "click", e);a.m(f, null, { i: b });m = n;
              }
            } };a.h.ga.checked = !0;a.d.checkedValue = { update: function update(b, c) {
              b.value = a.a.c(c());
            } };
        })();a.d.css = { update: function update(b, c) {
            var d = a.a.c(c());null !== d && "object" == (typeof d === "undefined" ? "undefined" : _typeof(d)) ? a.a.D(d, function (c, d) {
              d = a.a.c(d);a.a.fb(b, c, d);
            }) : (d = a.a.cb(String(d || "")), a.a.fb(b, b.__ko__cssValue, !1), b.__ko__cssValue = d, a.a.fb(b, d, !0));
          } };a.d.enable = { update: function update(b, c) {
            var d = a.a.c(c());
            d && b.disabled ? b.removeAttribute("disabled") : d || b.disabled || (b.disabled = !0);
          } };a.d.disable = { update: function update(b, c) {
            a.d.enable.update(b, function () {
              return !a.a.c(c());
            });
          } };a.d.event = { init: function init(b, c, d, e, f) {
            var g = c() || {};a.a.D(g, function (g) {
              "string" == typeof g && a.a.q(b, g, function (b) {
                var m,
                    k = c()[g];if (k) {
                  try {
                    var r = a.a.W(arguments);e = f.$data;r.unshift(e);m = k.apply(e, r);
                  } finally {
                    !0 !== m && (b.preventDefault ? b.preventDefault() : b.returnValue = !1);
                  }!1 === d.get(g + "Bubble") && (b.cancelBubble = !0, b.stopPropagation && b.stopPropagation());
                }
              });
            });
          } };
        a.d.foreach = { mc: function mc(b) {
            return function () {
              var c = b(),
                  d = a.a.Bb(c);if (!d || "number" == typeof d.length) return { foreach: c, templateEngine: a.X.vb };a.a.c(c);return { foreach: d.data, as: d.as, includeDestroyed: d.includeDestroyed, afterAdd: d.afterAdd, beforeRemove: d.beforeRemove, afterRender: d.afterRender, beforeMove: d.beforeMove, afterMove: d.afterMove, templateEngine: a.X.vb };
            };
          }, init: function init(b, c) {
            return a.d.template.init(b, a.d.foreach.mc(c));
          }, update: function update(b, c, d, e, f) {
            return a.d.template.update(b, a.d.foreach.mc(c), d, e, f);
          } };a.h.va.foreach = !1;a.f.aa.foreach = !0;a.d.hasfocus = { init: function init(b, c, d) {
            function e(e) {
              b.__ko_hasfocusUpdating = !0;var f = b.ownerDocument;if ("activeElement" in f) {
                var g;try {
                  g = f.activeElement;
                } catch (k) {
                  g = f.body;
                }e = g === b;
              }f = c();a.h.Ga(f, d, "hasfocus", e, !0);b.__ko_hasfocusLastValue = e;b.__ko_hasfocusUpdating = !1;
            }var f = e.bind(null, !0),
                g = e.bind(null, !1);a.a.q(b, "focus", f);a.a.q(b, "focusin", f);a.a.q(b, "blur", g);a.a.q(b, "focusout", g);
          }, update: function update(b, c) {
            var d = !!a.a.c(c());b.__ko_hasfocusUpdating || b.__ko_hasfocusLastValue === d || (d ? b.focus() : b.blur(), !d && b.__ko_hasfocusLastValue && b.ownerDocument.body.focus(), a.l.w(a.a.Fa, null, [b, d ? "focusin" : "focusout"]));
          } };a.h.ga.hasfocus = !0;a.d.hasFocus = a.d.hasfocus;a.h.ga.hasFocus = !0;a.d.html = { init: function init() {
            return { controlsDescendantBindings: !0 };
          }, update: function update(b, c) {
            a.a.Eb(b, c());
          } };K("if");K("ifnot", !1, !0);K("with", !0, !1, function (a, c) {
          return a.ac(c);
        });var L = {};a.d.options = { init: function init(b) {
            if ("select" !== a.a.A(b)) throw Error("options binding applies only to SELECT elements");for (; 0 < b.length;) {
              b.remove(0);
            }return { controlsDescendantBindings: !0 };
          }, update: function update(b, c, d) {
            function e() {
              return a.a.Ma(b.options, function (a) {
                return a.selected;
              });
            }function f(a, b, c) {
              var d = typeof b === "undefined" ? "undefined" : _typeof(b);return "function" == d ? b(a) : "string" == d ? a[b] : c;
            }function g(c, e) {
              if (A && k) a.j.ja(b, a.a.c(d.get("value")), !0);else if (p.length) {
                var f = 0 <= a.a.o(p, a.j.u(e[0]));a.a.wc(e[0], f);A && !f && a.l.w(a.a.Fa, null, [b, "change"]);
              }
            }var h = b.multiple,
                l = 0 != b.length && h ? b.scrollTop : null,
                m = a.a.c(c()),
                k = d.get("valueAllowUnset") && d.has("value"),
                r = d.get("optionsIncludeDestroyed");c = {};var q,
                p = [];k || (h ? p = a.a.ib(e(), a.j.u) : 0 <= b.selectedIndex && p.push(a.j.u(b.options[b.selectedIndex])));m && ("undefined" == typeof m.length && (m = [m]), q = a.a.Ma(m, function (b) {
              return r || b === n || null === b || !a.a.c(b._destroy);
            }), d.has("optionsCaption") && (m = a.a.c(d.get("optionsCaption")), null !== m && m !== n && q.unshift(L)));var A = !1;c.beforeRemove = function (a) {
              b.removeChild(a);
            };m = g;d.has("optionsAfterRender") && "function" == typeof d.get("optionsAfterRender") && (m = function m(b, c) {
              g(0, c);
              a.l.w(d.get("optionsAfterRender"), null, [c[0], b !== L ? b : n]);
            });a.a.Db(b, q, function (c, e, g) {
              g.length && (p = !k && g[0].selected ? [a.j.u(g[0])] : [], A = !0);e = b.ownerDocument.createElement("option");c === L ? (a.a.bb(e, d.get("optionsCaption")), a.j.ja(e, n)) : (g = f(c, d.get("optionsValue"), c), a.j.ja(e, a.a.c(g)), c = f(c, d.get("optionsText"), g), a.a.bb(e, c));return [e];
            }, c, m);a.l.w(function () {
              k ? a.j.ja(b, a.a.c(d.get("value")), !0) : (h ? p.length && e().length < p.length : p.length && 0 <= b.selectedIndex ? a.j.u(b.options[b.selectedIndex]) !== p[0] : p.length || 0 <= b.selectedIndex) && a.a.Fa(b, "change");
            });a.a.Sc(b);l && 20 < Math.abs(l - b.scrollTop) && (b.scrollTop = l);
          } };a.d.options.zb = a.a.e.J();a.d.selectedOptions = { after: ["options", "foreach"], init: function init(b, c, d) {
            a.a.q(b, "change", function () {
              var e = c(),
                  f = [];a.a.r(b.getElementsByTagName("option"), function (b) {
                b.selected && f.push(a.j.u(b));
              });a.h.Ga(e, d, "selectedOptions", f);
            });
          }, update: function update(b, c) {
            if ("select" != a.a.A(b)) throw Error("values binding applies only to SELECT elements");var d = a.a.c(c()),
                e = b.scrollTop;
            d && "number" == typeof d.length && a.a.r(b.getElementsByTagName("option"), function (b) {
              var c = 0 <= a.a.o(d, a.j.u(b));b.selected != c && a.a.wc(b, c);
            });b.scrollTop = e;
          } };a.h.ga.selectedOptions = !0;a.d.style = { update: function update(b, c) {
            var d = a.a.c(c() || {});a.a.D(d, function (c, d) {
              d = a.a.c(d);if (null === d || d === n || !1 === d) d = "";b.style[c] = d;
            });
          } };a.d.submit = { init: function init(b, c, d, e, f) {
            if ("function" != typeof c()) throw Error("The value for a submit binding must be a function");a.a.q(b, "submit", function (a) {
              var d,
                  e = c();try {
                d = e.call(f.$data, b);
              } finally {
                !0 !== d && (a.preventDefault ? a.preventDefault() : a.returnValue = !1);
              }
            });
          } };a.d.text = { init: function init() {
            return { controlsDescendantBindings: !0 };
          }, update: function update(b, c) {
            a.a.bb(b, c());
          } };a.f.aa.text = !0;(function () {
          if (x && x.navigator) var b = function b(a) {
            if (a) return parseFloat(a[1]);
          },
              c = x.opera && x.opera.version && parseInt(x.opera.version()),
              d = x.navigator.userAgent,
              e = b(d.match(/^(?:(?!chrome).)*version\/([^ ]*) safari/i)),
              f = b(d.match(/Firefox\/([^ ]*)/));if (10 > a.a.C) var g = a.a.e.J(),
              h = a.a.e.J(),
              l = function l(b) {
            var c = this.activeElement;(c = c && a.a.e.get(c, h)) && c(b);
          },
              m = function m(b, c) {
            var d = b.ownerDocument;a.a.e.get(d, g) || (a.a.e.set(d, g, !0), a.a.q(d, "selectionchange", l));a.a.e.set(b, h, c);
          };a.d.textInput = { init: function init(b, d, g) {
              function l(c, d) {
                a.a.q(b, c, d);
              }function h() {
                var c = a.a.c(d());if (null === c || c === n) c = "";u !== n && c === u ? a.a.setTimeout(h, 4) : b.value !== c && (s = c, b.value = c);
              }function y() {
                t || (u = b.value, t = a.a.setTimeout(v, 4));
              }function v() {
                clearTimeout(t);u = t = n;var c = b.value;s !== c && (s = c, a.h.Ga(d(), g, "textInput", c));
              }var s = b.value,
                  t,
                  u,
                  x = 9 == a.a.C ? y : v;10 > a.a.C ? (l("propertychange", function (a) {
                "value" === a.propertyName && x(a);
              }), 8 == a.a.C && (l("keyup", v), l("keydown", v)), 8 <= a.a.C && (m(b, x), l("dragend", y))) : (l("input", v), 5 > e && "textarea" === a.a.A(b) ? (l("keydown", y), l("paste", y), l("cut", y)) : 11 > c ? l("keydown", y) : 4 > f && (l("DOMAutoComplete", v), l("dragdrop", v), l("drop", v)));l("change", v);a.m(h, null, { i: b });
            } };a.h.ga.textInput = !0;a.d.textinput = { preprocess: function preprocess(a, b, c) {
              c("textInput", a);
            } };
        })();a.d.uniqueName = { init: function init(b, c) {
            if (c()) {
              var d = "ko_unique_" + ++a.d.uniqueName.Nc;a.a.vc(b, d);
            }
          } };a.d.uniqueName.Nc = 0;a.d.value = { after: ["options", "foreach"], init: function init(b, c, d) {
            if ("input" != b.tagName.toLowerCase() || "checkbox" != b.type && "radio" != b.type) {
              var e = ["change"],
                  f = d.get("valueUpdate"),
                  g = !1,
                  h = null;f && ("string" == typeof f && (f = [f]), a.a.ta(e, f), e = a.a.Wb(e));var l = function l() {
                h = null;g = !1;var e = c(),
                    f = a.j.u(b);a.h.Ga(e, d, "value", f);
              };!a.a.C || "input" != b.tagName.toLowerCase() || "text" != b.type || "off" == b.autocomplete || b.form && "off" == b.form.autocomplete || -1 != a.a.o(e, "propertychange") || (a.a.q(b, "propertychange", function () {
                g = !0;
              }), a.a.q(b, "focus", function () {
                g = !1;
              }), a.a.q(b, "blur", function () {
                g && l();
              }));a.a.r(e, function (c) {
                var d = l;a.a.sd(c, "after") && (d = function d() {
                  h = a.j.u(b);a.a.setTimeout(l, 0);
                }, c = c.substring(5));a.a.q(b, c, d);
              });var m = function m() {
                var e = a.a.c(c()),
                    f = a.j.u(b);if (null !== h && e === h) a.a.setTimeout(m, 0);else if (e !== f) if ("select" === a.a.A(b)) {
                  var g = d.get("valueAllowUnset"),
                      f = function f() {
                    a.j.ja(b, e, g);
                  };f();g || e === a.j.u(b) ? a.a.setTimeout(f, 0) : a.l.w(a.a.Fa, null, [b, "change"]);
                } else a.j.ja(b, e);
              };a.m(m, null, { i: b });
            } else a.La(b, { checkedValue: c });
          }, update: function update() {} };a.h.ga.value = !0;a.d.visible = { update: function update(b, c) {
            var d = a.a.c(c()),
                e = "none" != b.style.display;d && !e ? b.style.display = "" : !d && e && (b.style.display = "none");
          } };(function (b) {
          a.d[b] = { init: function init(c, d, e, f, g) {
              return a.d.event.init.call(this, c, function () {
                var a = {};a[b] = d();return a;
              }, e, f, g);
            } };
        })("click");a.P = function () {};a.P.prototype.renderTemplateSource = function () {
          throw Error("Override renderTemplateSource");
        };a.P.prototype.createJavaScriptEvaluatorBlock = function () {
          throw Error("Override createJavaScriptEvaluatorBlock");
        };a.P.prototype.makeTemplateSource = function (b, c) {
          if ("string" == typeof b) {
            c = c || t;var d = c.getElementById(b);if (!d) throw Error("Cannot find template with ID " + b);return new a.v.n(d);
          }if (1 == b.nodeType || 8 == b.nodeType) return new a.v.sa(b);throw Error("Unknown template type: " + b);
        };a.P.prototype.renderTemplate = function (a, c, d, e) {
          a = this.makeTemplateSource(a, e);return this.renderTemplateSource(a, c, d, e);
        };a.P.prototype.isTemplateRewritten = function (a, c) {
          return !1 === this.allowTemplateRewriting ? !0 : this.makeTemplateSource(a, c).data("isRewritten");
        };a.P.prototype.rewriteTemplate = function (a, c, d) {
          a = this.makeTemplateSource(a, d);c = c(a.text());a.text(c);a.data("isRewritten", !0);
        };a.b("templateEngine", a.P);a.Ib = function () {
          function b(b, c, d, h) {
            b = a.h.Ab(b);for (var l = a.h.va, m = 0; m < b.length; m++) {
              var k = b[m].key;if (l.hasOwnProperty(k)) {
                var r = l[k];if ("function" === typeof r) {
                  if (k = r(b[m].value)) throw Error(k);
                } else if (!r) throw Error("This template engine does not support the '" + k + "' binding within its templates");
              }
            }d = "ko.__tr_ambtns(function($context,$element){return(function(){return{ " + a.h.Xa(b, { valueAccessors: !0 }) + " } })()},'" + d.toLowerCase() + "')";return h.createJavaScriptEvaluatorBlock(d) + c;
          }var c = /(<([a-z]+\d*)(?:\s+(?!data-bind\s*=\s*)[a-z0-9\-]+(?:=(?:\"[^\"]*\"|\'[^\']*\'|[^>]*))?)*\s+)data-bind\s*=\s*(["'])([\s\S]*?)\3/gi,
              d = /\x3c!--\s*ko\b\s*([\s\S]*?)\s*--\x3e/g;return { Tc: function Tc(b, c, d) {
              c.isTemplateRewritten(b, d) || c.rewriteTemplate(b, function (b) {
                return a.Ib.jd(b, c);
              }, d);
            }, jd: function jd(a, f) {
              return a.replace(c, function (a, c, d, e, k) {
                return b(k, c, d, f);
              }).replace(d, function (a, c) {
                return b(c, "\x3c!-- ko --\x3e", "#comment", f);
              });
            }, Jc: function Jc(b, c) {
              return a.N.yb(function (d, h) {
                var l = d.nextSibling;l && l.nodeName.toLowerCase() === c && a.La(l, b, h);
              });
            } };
        }();a.b("__tr_ambtns", a.Ib.Jc);(function () {
          a.v = {};a.v.n = function (b) {
            if (this.n = b) {
              var c = a.a.A(b);this.eb = "script" === c ? 1 : "textarea" === c ? 2 : "template" == c && b.content && 11 === b.content.nodeType ? 3 : 4;
            }
          };a.v.n.prototype.text = function () {
            var b = 1 === this.eb ? "text" : 2 === this.eb ? "value" : "innerHTML";if (0 == arguments.length) return this.n[b];var c = arguments[0];"innerHTML" === b ? a.a.Eb(this.n, c) : this.n[b] = c;
          };var b = a.a.e.J() + "_";a.v.n.prototype.data = function (c) {
            if (1 === arguments.length) return a.a.e.get(this.n, b + c);a.a.e.set(this.n, b + c, arguments[1]);
          };var c = a.a.e.J();a.v.n.prototype.nodes = function () {
            var b = this.n;if (0 == arguments.length) return (a.a.e.get(b, c) || {}).mb || (3 === this.eb ? b.content : 4 === this.eb ? b : n);a.a.e.set(b, c, { mb: arguments[0] });
          };a.v.sa = function (a) {
            this.n = a;
          };a.v.sa.prototype = new a.v.n();a.v.sa.prototype.text = function () {
            if (0 == arguments.length) {
              var b = a.a.e.get(this.n, c) || {};b.Jb === n && b.mb && (b.Jb = b.mb.innerHTML);return b.Jb;
            }a.a.e.set(this.n, c, { Jb: arguments[0] });
          };a.b("templateSources", a.v);a.b("templateSources.domElement", a.v.n);a.b("templateSources.anonymousTemplate", a.v.sa);
        })();(function () {
          function b(b, c, d) {
            var e;for (c = a.f.nextSibling(c); b && (e = b) !== c;) {
              b = a.f.nextSibling(e), d(e, b);
            }
          }function c(c, d) {
            if (c.length) {
              var e = c[0],
                  f = c[c.length - 1],
                  g = e.parentNode,
                  h = a.S.instance,
                  n = h.preprocessNode;if (n) {
                b(e, f, function (a, b) {
                  var c = a.previousSibling,
                      d = n.call(h, a);d && (a === e && (e = d[0] || b), a === f && (f = d[d.length - 1] || c));
                });c.length = 0;if (!e) return;e === f ? c.push(e) : (c.push(e, f), a.a.Ba(c, g));
              }b(e, f, function (b) {
                1 !== b.nodeType && 8 !== b.nodeType || a.Ub(d, b);
              });b(e, f, function (b) {
                1 !== b.nodeType && 8 !== b.nodeType || a.N.Cc(b, [d]);
              });a.a.Ba(c, g);
            }
          }function d(a) {
            return a.nodeType ? a : 0 < a.length ? a[0] : null;
          }function e(b, e, f, h, q) {
            q = q || {};var p = (b && d(b) || f || {}).ownerDocument,
                n = q.templateEngine || g;
            a.Ib.Tc(f, n, p);f = n.renderTemplate(f, h, q, p);if ("number" != typeof f.length || 0 < f.length && "number" != typeof f[0].nodeType) throw Error("Template engine must return an array of DOM nodes");p = !1;switch (e) {case "replaceChildren":
                a.f.fa(b, f);p = !0;break;case "replaceNode":
                a.a.uc(b, f);p = !0;break;case "ignoreTargetNode":
                break;default:
                throw Error("Unknown renderMode: " + e);}p && (c(f, h), q.afterRender && a.l.w(q.afterRender, null, [f, h.$data]));return f;
          }function f(b, c, d) {
            return a.I(b) ? b() : "function" === typeof b ? b(c, d) : b;
          }
          var g;a.Fb = function (b) {
            if (b != n && !(b instanceof a.P)) throw Error("templateEngine must inherit from ko.templateEngine");g = b;
          };a.Cb = function (b, c, k, h, q) {
            k = k || {};if ((k.templateEngine || g) == n) throw Error("Set a template engine before calling renderTemplate");q = q || "replaceChildren";if (h) {
              var p = d(h);return a.B(function () {
                var g = c && c instanceof a.R ? c : new a.R(c, null, null, null, { exportDependencies: !0 }),
                    n = f(b, g.$data, g),
                    g = e(h, q, n, g, k);"replaceNode" == q && (h = g, p = d(h));
              }, null, { ya: function ya() {
                  return !p || !a.a.qb(p);
                }, i: p && "replaceNode" == q ? p.parentNode : p });
            }return a.N.yb(function (d) {
              a.Cb(b, c, k, d, "replaceNode");
            });
          };a.pd = function (b, d, g, h, q) {
            function p(a, b) {
              c(b, t);g.afterRender && g.afterRender(b, a);t = null;
            }function s(a, c) {
              t = q.createChildContext(a, g.as, function (a) {
                a.$index = c;
              });var d = f(b, a, t);return e(null, "ignoreTargetNode", d, t, g);
            }var t;return a.B(function () {
              var b = a.a.c(d) || [];"undefined" == typeof b.length && (b = [b]);b = a.a.Ma(b, function (b) {
                return g.includeDestroyed || b === n || null === b || !a.a.c(b._destroy);
              });a.l.w(a.a.Db, null, [h, b, s, g, p]);
            }, null, { i: h });
          };var h = a.a.e.J();a.d.template = { init: function init(b, c) {
              var d = a.a.c(c());if ("string" == typeof d || d.name) a.f.za(b);else {
                if ("nodes" in d) {
                  if (d = d.nodes || [], a.I(d)) throw Error('The "nodes" option must be a plain, non-observable array.');
                } else d = a.f.childNodes(b);d = a.a.nc(d);new a.v.sa(b).nodes(d);
              }return { controlsDescendantBindings: !0 };
            }, update: function update(b, c, d, e, f) {
              var g = c();c = a.a.c(g);d = !0;e = null;"string" == typeof c ? c = {} : (g = c.name, "if" in c && (d = a.a.c(c["if"])), d && "ifnot" in c && (d = !a.a.c(c.ifnot)));
              "foreach" in c ? e = a.pd(g || b, d && c.foreach || [], c, b, f) : d ? (f = "data" in c ? f.ac(c.data, c.as) : f, e = a.Cb(g || b, f, c, b)) : a.f.za(b);f = e;(c = a.a.e.get(b, h)) && "function" == typeof c.k && c.k();a.a.e.set(b, h, f && f.ca() ? f : n);
            } };a.h.va.template = function (b) {
            b = a.h.Ab(b);return 1 == b.length && b[0].unknown || a.h.fd(b, "name") ? null : "This template engine does not support anonymous templates nested within its templates";
          };a.f.aa.template = !0;
        })();a.b("setTemplateEngine", a.Fb);a.b("renderTemplate", a.Cb);a.a.hc = function (a, c, d) {
          if (a.length && c.length) {
            var e, f, g, h, l;for (e = f = 0; (!d || e < d) && (h = a[f]); ++f) {
              for (g = 0; l = c[g]; ++g) {
                if (h.value === l.value) {
                  h.moved = l.index;l.moved = h.index;c.splice(g, 1);e = g = 0;break;
                }
              }e += g;
            }
          }
        };a.a.lb = function () {
          function b(b, d, e, f, g) {
            var h = Math.min,
                l = Math.max,
                m = [],
                k,
                n = b.length,
                q,
                p = d.length,
                s = p - n || 1,
                t = n + p + 1,
                v,
                u,
                x;for (k = 0; k <= n; k++) {
              for (u = v, m.push(v = []), x = h(p, k + s), q = l(0, k - 1); q <= x; q++) {
                v[q] = q ? k ? b[k - 1] === d[q - 1] ? u[q - 1] : h(u[q] || t, v[q - 1] || t) + 1 : q + 1 : k + 1;
              }
            }h = [];l = [];s = [];k = n;for (q = p; k || q;) {
              p = m[k][q] - 1, q && p === m[k][q - 1] ? l.push(h[h.length] = { status: e,
                value: d[--q], index: q }) : k && p === m[k - 1][q] ? s.push(h[h.length] = { status: f, value: b[--k], index: k }) : (--q, --k, g.sparse || h.push({ status: "retained", value: d[q] }));
            }a.a.hc(s, l, !g.dontLimitMoves && 10 * n);return h.reverse();
          }return function (a, d, e) {
            e = "boolean" === typeof e ? { dontLimitMoves: e } : e || {};a = a || [];d = d || [];return a.length < d.length ? b(a, d, "added", "deleted", e) : b(d, a, "deleted", "added", e);
          };
        }();a.b("utils.compareArrays", a.a.lb);(function () {
          function b(b, c, d, h, l) {
            var m = [],
                k = a.B(function () {
              var k = c(d, l, a.a.Ba(m, b)) || [];0 < m.length && (a.a.uc(m, k), h && a.l.w(h, null, [d, k, l]));m.length = 0;a.a.ta(m, k);
            }, null, { i: b, ya: function ya() {
                return !a.a.Tb(m);
              } });return { ea: m, B: k.ca() ? k : n };
          }var c = a.a.e.J(),
              d = a.a.e.J();a.a.Db = function (e, f, g, h, l) {
            function m(b, c) {
              w = q[c];u !== c && (D[b] = w);w.tb(u++);a.a.Ba(w.ea, e);t.push(w);z.push(w);
            }function k(b, c) {
              if (b) for (var d = 0, e = c.length; d < e; d++) {
                c[d] && a.a.r(c[d].ea, function (a) {
                  b(a, d, c[d].ka);
                });
              }
            }f = f || [];h = h || {};var r = a.a.e.get(e, c) === n,
                q = a.a.e.get(e, c) || [],
                p = a.a.ib(q, function (a) {
              return a.ka;
            }),
                s = a.a.lb(p, f, h.dontLimitMoves),
                t = [],
                v = 0,
                u = 0,
                x = [],
                z = [];f = [];for (var D = [], p = [], w, C = 0, B, E; B = s[C]; C++) {
              switch (E = B.moved, B.status) {case "deleted":
                  E === n && (w = q[v], w.B && (w.B.k(), w.B = n), a.a.Ba(w.ea, e).length && (h.beforeRemove && (t.push(w), z.push(w), w.ka === d ? w = null : f[C] = w), w && x.push.apply(x, w.ea)));v++;break;case "retained":
                  m(C, v++);break;case "added":
                  E !== n ? m(C, E) : (w = { ka: B.value, tb: a.O(u++) }, t.push(w), z.push(w), r || (p[C] = w));}
            }a.a.e.set(e, c, t);k(h.beforeMove, D);a.a.r(x, h.beforeRemove ? a.ba : a.removeNode);for (var C = 0, r = a.f.firstChild(e), F; w = z[C]; C++) {
              w.ea || a.a.extend(w, b(e, g, w.ka, l, w.tb));for (v = 0; s = w.ea[v]; r = s.nextSibling, F = s, v++) {
                s !== r && a.f.kc(e, s, F);
              }!w.ad && l && (l(w.ka, w.ea, w.tb), w.ad = !0);
            }k(h.beforeRemove, f);for (C = 0; C < f.length; ++C) {
              f[C] && (f[C].ka = d);
            }k(h.afterMove, D);k(h.afterAdd, p);
          };
        })();a.b("utils.setDomNodeChildrenFromArrayMapping", a.a.Db);a.X = function () {
          this.allowTemplateRewriting = !1;
        };a.X.prototype = new a.P();a.X.prototype.renderTemplateSource = function (b, c, d, e) {
          if (c = (9 > a.a.C ? 0 : b.nodes) ? b.nodes() : null) return a.a.W(c.cloneNode(!0).childNodes);b = b.text();
          return a.a.na(b, e);
        };a.X.vb = new a.X();a.Fb(a.X.vb);a.b("nativeTemplateEngine", a.X);(function () {
          a.xb = function () {
            var a = this.ed = function () {
              if (!u || !u.tmpl) return 0;try {
                if (0 <= u.tmpl.tag.tmpl.open.toString().indexOf("__")) return 2;
              } catch (a) {}return 1;
            }();this.renderTemplateSource = function (b, e, f, g) {
              g = g || t;f = f || {};if (2 > a) throw Error("Your version of jQuery.tmpl is too old. Please upgrade to jQuery.tmpl 1.0.0pre or later.");var h = b.data("precompiled");h || (h = b.text() || "", h = u.template(null, "{{ko_with $item.koBindingContext}}" + h + "{{/ko_with}}"), b.data("precompiled", h));b = [e.$data];e = u.extend({ koBindingContext: e }, f.templateOptions);e = u.tmpl(h, b, e);e.appendTo(g.createElement("div"));u.fragments = {};return e;
            };this.createJavaScriptEvaluatorBlock = function (a) {
              return "{{ko_code ((function() { return " + a + " })()) }}";
            };this.addTemplate = function (a, b) {
              t.write("<script type='text/html' id='" + a + "'>" + b + "\x3c/script>");
            };0 < a && (u.tmpl.tag.ko_code = { open: "__.push($1 || '');" }, u.tmpl.tag.ko_with = { open: "with($1) {", close: "} " });
          };a.xb.prototype = new a.P();var b = new a.xb();0 < b.ed && a.Fb(b);a.b("jqueryTmplTemplateEngine", a.xb);
        })();
      });
    })();
  })();
});

var knockout_validation = createCommonjsModule(function (module, exports) {
/*=============================================================================
	Author:			Eric M. Barnard - @ericmbarnard								
	License:		MIT (http://opensource.org/licenses/mit-license.php)		
																				
	Description:	Validation Library for KnockoutJS							
	Version:		2.0.3											
===============================================================================
*/
/*globals require: false, exports: false, define: false, ko: false */

(function (factory) {
	// Module systems magic dance.

	if (typeof commonjsRequire === "function" && 'object' === "object" && 'object' === "object") {
		// CommonJS or Node: hard-coded dependency on "knockout"
		factory(knockout, exports);
	} else if (typeof undefined === "function" && undefined["amd"]) {
		// AMD anonymous module with hard-coded dependency on "knockout"
		undefined(["knockout", "exports"], factory);
	} else {
		// <script> tag: use the global `ko` object, attaching a `mapping` property
		factory(ko, ko.validation = {});
	}
}(function ( ko, exports ) {

	if (typeof (ko) === 'undefined') {
		throw new Error('Knockout is required, please ensure it is loaded before loading this validation plug-in');
	}

	// create our namespace object
	ko.validation = exports;

	var kv = ko.validation,
		koUtils = ko.utils,
		unwrap = koUtils.unwrapObservable,
		forEach = koUtils.arrayForEach,
		extend = koUtils.extend;
/*global ko: false*/

var defaults = {
	registerExtenders: true,
	messagesOnModified: true,
	errorsAsTitle: true,            // enables/disables showing of errors as title attribute of the target element.
	errorsAsTitleOnModified: false, // shows the error when hovering the input field (decorateElement must be true)
	messageTemplate: null,
	insertMessages: true,           // automatically inserts validation messages as <span></span>
	parseInputAttributes: false,    // parses the HTML5 validation attribute from a form element and adds that to the object
	writeInputAttributes: false,    // adds HTML5 input validation attributes to form elements that ko observable's are bound to
	decorateInputElement: false,         // false to keep backward compatibility
	decorateElementOnModified: true,// true to keep backward compatibility
	errorClass: null,               // single class for error message and element
	errorElementClass: 'validationElement',  // class to decorate error element
	errorMessageClass: 'validationMessage',  // class to decorate error message
	allowHtmlMessages: false,		// allows HTML in validation messages
	grouping: {
		deep: false,        //by default grouping is shallow
		observable: true,   //and using observables
		live: false		    //react to changes to observableArrays if observable === true
	},
	validate: {
		// throttle: 10
	}
};

// make a copy  so we can use 'reset' later
var configuration = extend({}, defaults);

configuration.html5Attributes = ['required', 'pattern', 'min', 'max', 'step'];
configuration.html5InputTypes = ['email', 'number', 'date'];

configuration.reset = function () {
	extend(configuration, defaults);
};

kv.configuration = configuration;
kv.utils = (function () {
	var seedId = new Date().getTime();

	var domData = {}; //hash of data objects that we reference from dom elements
	var domDataKey = '__ko_validation__';

	return {
		isArray: function (o) {
			return o.isArray || Object.prototype.toString.call(o) === '[object Array]';
		},
		isObject: function (o) {
			return o !== null && typeof o === 'object';
		},
		isNumber: function(o) {
			return !isNaN(o);	
		},
		isObservableArray: function(instance) {
			return !!instance &&
					typeof instance["remove"] === "function" &&
					typeof instance["removeAll"] === "function" &&
					typeof instance["destroy"] === "function" &&
					typeof instance["destroyAll"] === "function" &&
					typeof instance["indexOf"] === "function" &&
					typeof instance["replace"] === "function";
		},
		values: function (o) {
			var r = [];
			for (var i in o) {
				if (o.hasOwnProperty(i)) {
					r.push(o[i]);
				}
			}
			return r;
		},
		getValue: function (o) {
			return (typeof o === 'function' ? o() : o);
		},
		hasAttribute: function (node, attr) {
			return node.getAttribute(attr) !== null;
		},
		getAttribute: function (element, attr) {
			return element.getAttribute(attr);
		},
		setAttribute: function (element, attr, value) {
			return element.setAttribute(attr, value);
		},
		isValidatable: function (o) {
			return !!(o && o.rules && o.isValid && o.isModified);
		},
		insertAfter: function (node, newNode) {
			node.parentNode.insertBefore(newNode, node.nextSibling);
		},
		newId: function () {
			return seedId += 1;
		},
		getConfigOptions: function (element) {
			var options = kv.utils.contextFor(element);

			return options || kv.configuration;
		},
		setDomData: function (node, data) {
			var key = node[domDataKey];

			if (!key) {
				node[domDataKey] = key = kv.utils.newId();
			}

			domData[key] = data;
		},
		getDomData: function (node) {
			var key = node[domDataKey];

			if (!key) {
				return undefined;
			}

			return domData[key];
		},
		contextFor: function (node) {
			switch (node.nodeType) {
				case 1:
				case 8:
					var context = kv.utils.getDomData(node);
					if (context) { return context; }
					if (node.parentNode) { return kv.utils.contextFor(node.parentNode); }
					break;
			}
			return undefined;
		},
		isEmptyVal: function (val) {
			if (val === undefined) {
				return true;
			}
			if (val === null) {
				return true;
			}
			if (val === "") {
				return true;
			}
		},
		getOriginalElementTitle: function (element) {
			var savedOriginalTitle = kv.utils.getAttribute(element, 'data-orig-title'),
				currentTitle = element.title,
				hasSavedOriginalTitle = kv.utils.hasAttribute(element, 'data-orig-title');

			return hasSavedOriginalTitle ?
				savedOriginalTitle : currentTitle;
		},
		async: function (expr) {
			if (window.setImmediate) { window.setImmediate(expr); }
			else { window.setTimeout(expr, 0); }
		},
		forEach: function (object, callback) {
			if (kv.utils.isArray(object)) {
				return forEach(object, callback);
			}
			for (var prop in object) {
				if (object.hasOwnProperty(prop)) {
					callback(object[prop], prop);
				}
			}
		}
	};
}());var api = (function () {

	var isInitialized = 0,
		configuration = kv.configuration,
		utils = kv.utils;

	function cleanUpSubscriptions(context) {
		forEach(context.subscriptions, function (subscription) {
			subscription.dispose();
		});
		context.subscriptions = [];
	}

	function dispose(context) {
		if (context.options.deep) {
			forEach(context.flagged, function (obj) {
				delete obj.__kv_traversed;
			});
			context.flagged.length = 0;
		}

		if (!context.options.live) {
			cleanUpSubscriptions(context);
		}
	}

	function runTraversal(obj, context) {
		context.validatables = [];
		cleanUpSubscriptions(context);
		traverseGraph(obj, context);
		dispose(context);
	}

	function traverseGraph(obj, context, level) {
		var objValues = [],
			val = obj.peek ? obj.peek() : obj;

		if (obj.__kv_traversed === true) {
			return;
		}

		if (context.options.deep) {
			obj.__kv_traversed = true;
			context.flagged.push(obj);
		}

		//default level value depends on deep option.
		level = (level !== undefined ? level : context.options.deep ? 1 : -1);

		// if object is observable then add it to the list
		if (ko.isObservable(obj)) {
			// ensure it's validatable but don't extend validatedObservable because it
			// would overwrite isValid property.
			if (!obj.errors && !utils.isValidatable(obj)) {
				obj.extend({ validatable: true });
			}
			context.validatables.push(obj);

			if (context.options.live && utils.isObservableArray(obj)) {
				context.subscriptions.push(obj.subscribe(function () {
					context.graphMonitor.valueHasMutated();
				}));
			}
		}

		//get list of values either from array or object but ignore non-objects
		// and destroyed objects
		if (val && !val._destroy) {
			if (utils.isArray(val)) {
				objValues = val;
			}
			else if (utils.isObject(val)) {
				objValues = utils.values(val);
			}
		}

		//process recursively if it is deep grouping
		if (level !== 0) {
			utils.forEach(objValues, function (observable) {
				//but not falsy things and not HTML Elements
				if (observable && !observable.nodeType && (!ko.isComputed(observable) || observable.rules)) {
					traverseGraph(observable, context, level + 1);
				}
			});
		}
	}

	function collectErrors(array) {
		var errors = [];
		forEach(array, function (observable) {
			// Do not collect validatedObservable errors
			if (utils.isValidatable(observable) && !observable.isValid()) {
				// Use peek because we don't want a dependency for 'error' property because it
				// changes before 'isValid' does. (Issue #99)
				errors.push(observable.error.peek());
			}
		});
		return errors;
	}

	return {
		//Call this on startup
		//any config can be overridden with the passed in options
		init: function (options, force) {
			//done run this multiple times if we don't really want to
			if (isInitialized > 0 && !force) {
				return;
			}

			//because we will be accessing options properties it has to be an object at least
			options = options || {};
			//if specific error classes are not provided then apply generic errorClass
			//it has to be done on option so that options.errorClass can override default
			//errorElementClass and errorMessage class but not those provided in options
			options.errorElementClass = options.errorElementClass || options.errorClass || configuration.errorElementClass;
			options.errorMessageClass = options.errorMessageClass || options.errorClass || configuration.errorMessageClass;

			extend(configuration, options);

			if (configuration.registerExtenders) {
				kv.registerExtenders();
			}

			isInitialized = 1;
		},

		// resets the config back to its original state
		reset: kv.configuration.reset,

		// recursively walks a viewModel and creates an object that
		// provides validation information for the entire viewModel
		// obj -> the viewModel to walk
		// options -> {
		//	  deep: false, // if true, will walk past the first level of viewModel properties
		//	  observable: false // if true, returns a computed observable indicating if the viewModel is valid
		// }
		group: function group(obj, options) { // array of observables or viewModel
			options = extend(extend({}, configuration.grouping), options);

			var context = {
				options: options,
				graphMonitor: ko.observable(),
				flagged: [],
				subscriptions: [],
				validatables: []
			};

			var result = null;

			//if using observables then traverse structure once and add observables
			if (options.observable) {
				result = ko.computed(function () {
					context.graphMonitor(); //register dependency
					runTraversal(obj, context);
					return collectErrors(context.validatables);
				});
			}
			else { //if not using observables then every call to error() should traverse the structure
				result = function () {
					runTraversal(obj, context);
					return collectErrors(context.validatables);
				};
			}

			result.showAllMessages = function (show) { // thanks @heliosPortal
				if (show === undefined) {//default to true
					show = true;
				}

				result.forEach(function (observable) {
					if (utils.isValidatable(observable)) {
						observable.isModified(show);
					}
				});
			};

			result.isAnyMessageShown = function () {
				var invalidAndModifiedPresent;

				invalidAndModifiedPresent = !!result.find(function (observable) {
					return utils.isValidatable(observable) && !observable.isValid() && observable.isModified();
				});
				return invalidAndModifiedPresent;
			};

			result.filter = function(predicate) {
				predicate = predicate || function () { return true; };
				// ensure we have latest changes
				result();

				return koUtils.arrayFilter(context.validatables, predicate);
			};

			result.find = function(predicate) {
				predicate = predicate || function () { return true; };
				// ensure we have latest changes
				result();

				return koUtils.arrayFirst(context.validatables, predicate);
			};

			result.forEach = function(callback) {
				callback = callback || function () { };
				// ensure we have latest changes
				result();

				forEach(context.validatables, callback);
			};

			result.map = function(mapping) {
				mapping = mapping || function (item) { return item; };
				// ensure we have latest changes
				result();

				return koUtils.arrayMap(context.validatables, mapping);
			};

			/**
			 * @private You should not rely on this method being here.
			 * It's a private method and it may change in the future.
			 *
			 * @description Updates the validated object and collects errors from it.
			 */
			result._updateState = function(newValue) {
				if (!utils.isObject(newValue)) {
					throw new Error('An object is required.');
				}
				obj = newValue;
				if (options.observable) {
					context.graphMonitor.valueHasMutated();
				}
				else {
					runTraversal(newValue, context);
					return collectErrors(context.validatables);
				}
			};
			return result;
		},

		formatMessage: function (message, params, observable) {
			if (utils.isObject(params) && params.typeAttr) {
				params = params.value;
			}
			if (typeof message === 'function') {
				return message(params, observable);
			}
			var replacements = unwrap(params);
            if (replacements == null) {
                replacements = [];
            }
			if (!utils.isArray(replacements)) {
				replacements = [replacements];
			}
			return message.replace(/{(\d+)}/gi, function(match, index) {
				if (typeof replacements[index] !== 'undefined') {
					return replacements[index];
				}
				return match;
			});
		},

		// addRule:
		// This takes in a ko.observable and a Rule Context - which is just a rule name and params to supply to the validator
		// ie: kv.addRule(myObservable, {
		//		  rule: 'required',
		//		  params: true
		//	  });
		//
		addRule: function (observable, rule) {
			observable.extend({ validatable: true });

			var hasRule = !!koUtils.arrayFirst(observable.rules(), function(item) {
				return item.rule && item.rule === rule.rule;
			});

			if (!hasRule) {
				//push a Rule Context to the observables local array of Rule Contexts
				observable.rules.push(rule);
			}
			return observable;
		},

		// addAnonymousRule:
		// Anonymous Rules essentially have all the properties of a Rule, but are only specific for a certain property
		// and developers typically are wanting to add them on the fly or not register a rule with the 'kv.rules' object
		//
		// Example:
		// var test = ko.observable('something').extend{(
		//	  validation: {
		//		  validator: function(val, someOtherVal){
		//			  return true;
		//		  },
		//		  message: "Something must be really wrong!',
		//		  params: true
		//	  }
		//  )};
		addAnonymousRule: function (observable, ruleObj) {
			if (ruleObj['message'] === undefined) {
				ruleObj['message'] = 'Error';
			}

			//make sure onlyIf is honoured
			if (ruleObj.onlyIf) {
				ruleObj.condition = ruleObj.onlyIf;
			}

			//add the anonymous rule to the observable
			kv.addRule(observable, ruleObj);
		},

		addExtender: function (ruleName) {
			ko.extenders[ruleName] = function (observable, params) {
				//params can come in a few flavors
				// 1. Just the params to be passed to the validator
				// 2. An object containing the Message to be used and the Params to pass to the validator
				// 3. A condition when the validation rule to be applied
				//
				// Example:
				// var test = ko.observable(3).extend({
				//	  max: {
				//		  message: 'This special field has a Max of {0}',
				//		  params: 2,
				//		  onlyIf: function() {
				//					  return specialField.IsVisible();
				//				  }
				//	  }
				//  )};
				//
				if (params && (params.message || params.onlyIf)) { //if it has a message or condition object, then its an object literal to use
					return kv.addRule(observable, {
						rule: ruleName,
						message: params.message,
						params: utils.isEmptyVal(params.params) ? true : params.params,
						condition: params.onlyIf
					});
				} else {
					return kv.addRule(observable, {
						rule: ruleName,
						params: params
					});
				}
			};
		},

		// loops through all kv.rules and adds them as extenders to
		// ko.extenders
		registerExtenders: function () { // root extenders optional, use 'validation' extender if would cause conflicts
			if (configuration.registerExtenders) {
				for (var ruleName in kv.rules) {
					if (kv.rules.hasOwnProperty(ruleName)) {
						if (!ko.extenders[ruleName]) {
							kv.addExtender(ruleName);
						}
					}
				}
			}
		},

		//creates a span next to the @element with the specified error class
		insertValidationMessage: function (element) {
			var span = document.createElement('SPAN');
			span.className = utils.getConfigOptions(element).errorMessageClass;
			utils.insertAfter(element, span);
			return span;
		},

		// if html-5 validation attributes have been specified, this parses
		// the attributes on @element
		parseInputValidationAttributes: function (element, valueAccessor) {
			forEach(kv.configuration.html5Attributes, function (attr) {
				if (utils.hasAttribute(element, attr)) {

					var params = element.getAttribute(attr) || true;

					if (attr === 'min' || attr === 'max')
					{
						// If we're validating based on the min and max attributes, we'll
						// need to know what the 'type' attribute is set to
						var typeAttr = element.getAttribute('type');
						if (typeof typeAttr === "undefined" || !typeAttr)
						{
							// From http://www.w3.org/TR/html-markup/input:
							//   An input element with no type attribute specified represents the
							//   same thing as an input element with its type attribute set to "text".
							typeAttr = "text";
						}
						params = {typeAttr: typeAttr, value: params};
					}

					kv.addRule(valueAccessor(), {
						rule: attr,
						params: params
					});
				}
			});

			var currentType = element.getAttribute('type');
			forEach(kv.configuration.html5InputTypes, function (type) {
				if (type === currentType) {
					kv.addRule(valueAccessor(), {
						rule: (type === 'date') ? 'dateISO' : type,
						params: true
					});
				}
			});
		},

		// writes html5 validation attributes on the element passed in
		writeInputValidationAttributes: function (element, valueAccessor) {
			var observable = valueAccessor();

			if (!observable || !observable.rules) {
				return;
			}

			var contexts = observable.rules(); // observable array

			// loop through the attributes and add the information needed
			forEach(kv.configuration.html5Attributes, function (attr) {
				var ctx = koUtils.arrayFirst(contexts, function (ctx) {
					return ctx.rule && ctx.rule.toLowerCase() === attr.toLowerCase();
				});

				if (!ctx) {
					return;
				}

				// we have a rule matching a validation attribute at this point
				// so lets add it to the element along with the params
				ko.computed({
					read: function() {
						var params = ko.unwrap(ctx.params);

						// we have to do some special things for the pattern validation
						if (ctx.rule === "pattern" && params instanceof RegExp) {
							// we need the pure string representation of the RegExpr without the //gi stuff
							params = params.source;
						}

						element.setAttribute(attr, params);
					},
					disposeWhenNodeIsRemoved: element
				});
			});

			contexts = null;
		},

		//take an existing binding handler and make it cause automatic validations
		makeBindingHandlerValidatable: function (handlerName) {
			var init = ko.bindingHandlers[handlerName].init;

			ko.bindingHandlers[handlerName].init = function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {

				init(element, valueAccessor, allBindingsAccessor, viewModel, bindingContext);

				return ko.bindingHandlers['validationCore'].init(element, valueAccessor, allBindingsAccessor, viewModel, bindingContext);
			};
		},

		// visit an objects properties and apply validation rules from a definition
		setRules: function (target, definition) {
			var setRules = function (target, definition) {
				if (!target || !definition) { return; }

				for (var prop in definition) {
					if (!definition.hasOwnProperty(prop)) { continue; }
					var ruleDefinitions = definition[prop];

					//check the target property exists and has a value
					if (!target[prop]) { continue; }
					var targetValue = target[prop],
						unwrappedTargetValue = unwrap(targetValue),
						rules = {},
						nonRules = {};

					for (var rule in ruleDefinitions) {
						if (!ruleDefinitions.hasOwnProperty(rule)) { continue; }
						if (kv.rules[rule]) {
							rules[rule] = ruleDefinitions[rule];
						} else {
							nonRules[rule] = ruleDefinitions[rule];
						}
					}

					//apply rules
					if (ko.isObservable(targetValue)) {
						targetValue.extend(rules);
					}

					//then apply child rules
					//if it's an array, apply rules to all children
					if (unwrappedTargetValue && utils.isArray(unwrappedTargetValue)) {
						for (var i = 0; i < unwrappedTargetValue.length; i++) {
							setRules(unwrappedTargetValue[i], nonRules);
						}
						//otherwise, just apply to this property
					} else {
						setRules(unwrappedTargetValue, nonRules);
					}
				}
			};
			setRules(target, definition);
		}
	};

}());

// expose api publicly
extend(ko.validation, api);
//Validation Rules:
// You can view and override messages or rules via:
// kv.rules[ruleName]
//
// To implement a custom Rule, simply use this template:
// kv.rules['<custom rule name>'] = {
//      validator: function (val, param) {
//          <custom logic>
//          return <true or false>;
//      },
//      message: '<custom validation message>' //optionally you can also use a '{0}' to denote a placeholder that will be replaced with your 'param'
// };
//
// Example:
// kv.rules['mustEqual'] = {
//      validator: function( val, mustEqualVal ){
//          return val === mustEqualVal;
//      },
//      message: 'This field must equal {0}'
// };
//
kv.rules = {};
kv.rules['required'] = {
	validator: function (val, required) {
		var testVal;

		if (val === undefined || val === null) {
			return !required;
		}

		testVal = val;
		if (typeof (val) === 'string') {
			if (String.prototype.trim) {
				testVal = val.trim();
			}
			else {
				testVal = val.replace(/^\s+|\s+$/g, '');
			}
		}

		if (!required) {// if they passed: { required: false }, then don't require this
			return true;
		}

		return ((testVal + '').length > 0);
	},
	message: 'This field is required.'
};

function minMaxValidatorFactory(validatorName) {
    var isMaxValidation = validatorName === "max";

    return function (val, options) {
        if (kv.utils.isEmptyVal(val)) {
            return true;
        }

        var comparisonValue, type;
        if (options.typeAttr === undefined) {
            // This validator is being called from javascript rather than
            // being bound from markup
            type = "text";
            comparisonValue = options;
        } else {
            type = options.typeAttr;
            comparisonValue = options.value;
        }

        // From http://www.w3.org/TR/2012/WD-html5-20121025/common-input-element-attributes.html#attr-input-min,
        // if the value is parseable to a number, then the minimum should be numeric
        if (!isNaN(comparisonValue) && !(comparisonValue instanceof Date)) {
            type = "number";
        }

        var regex, valMatches, comparisonValueMatches;
        switch (type.toLowerCase()) {
            case "week":
                regex = /^(\d{4})-W(\d{2})$/;
                valMatches = val.match(regex);
                if (valMatches === null) {
                    throw new Error("Invalid value for " + validatorName + " attribute for week input.  Should look like " +
                        "'2000-W33' http://www.w3.org/TR/html-markup/input.week.html#input.week.attrs.min");
                }
                comparisonValueMatches = comparisonValue.match(regex);
                // If no regex matches were found, validation fails
                if (!comparisonValueMatches) {
                    return false;
                }

                if (isMaxValidation) {
                    return (valMatches[1] < comparisonValueMatches[1]) || // older year
                        // same year, older week
                        ((valMatches[1] === comparisonValueMatches[1]) && (valMatches[2] <= comparisonValueMatches[2]));
                } else {
                    return (valMatches[1] > comparisonValueMatches[1]) || // newer year
                        // same year, newer week
                        ((valMatches[1] === comparisonValueMatches[1]) && (valMatches[2] >= comparisonValueMatches[2]));
                }
                break;

            case "month":
                regex = /^(\d{4})-(\d{2})$/;
                valMatches = val.match(regex);
                if (valMatches === null) {
                    throw new Error("Invalid value for " + validatorName + " attribute for month input.  Should look like " +
                        "'2000-03' http://www.w3.org/TR/html-markup/input.month.html#input.month.attrs.min");
                }
                comparisonValueMatches = comparisonValue.match(regex);
                // If no regex matches were found, validation fails
                if (!comparisonValueMatches) {
                    return false;
                }

                if (isMaxValidation) {
                    return ((valMatches[1] < comparisonValueMatches[1]) || // older year
                        // same year, older month
                        ((valMatches[1] === comparisonValueMatches[1]) && (valMatches[2] <= comparisonValueMatches[2])));
                } else {
                    return (valMatches[1] > comparisonValueMatches[1]) || // newer year
                        // same year, newer month
                        ((valMatches[1] === comparisonValueMatches[1]) && (valMatches[2] >= comparisonValueMatches[2]));
                }
                break;

            case "number":
            case "range":
                if (isMaxValidation) {
                    return (!isNaN(val) && parseFloat(val) <= parseFloat(comparisonValue));
                } else {
                    return (!isNaN(val) && parseFloat(val) >= parseFloat(comparisonValue));
                }
                break;

            default:
                if (isMaxValidation) {
                    return val <= comparisonValue;
                } else {
                    return val >= comparisonValue;
                }
        }
    };
}

kv.rules['min'] = {
	validator: minMaxValidatorFactory("min"),
	message: 'Please enter a value greater than or equal to {0}.'
};

kv.rules['max'] = {
	validator: minMaxValidatorFactory("max"),
	message: 'Please enter a value less than or equal to {0}.'
};

kv.rules['minLength'] = {
	validator: function (val, minLength) {
		if(kv.utils.isEmptyVal(val)) { return true; }
		var normalizedVal = kv.utils.isNumber(val) ? ('' + val) : val;
		return normalizedVal.length >= minLength;
	},
	message: 'Please enter at least {0} characters.'
};

kv.rules['maxLength'] = {
	validator: function (val, maxLength) {
		if(kv.utils.isEmptyVal(val)) { return true; }
		var normalizedVal = kv.utils.isNumber(val) ? ('' + val) : val;
		return normalizedVal.length <= maxLength;
	},
	message: 'Please enter no more than {0} characters.'
};

kv.rules['pattern'] = {
	validator: function (val, regex) {
		return kv.utils.isEmptyVal(val) || val.toString().match(regex) !== null;
	},
	message: 'Please check this value.'
};

kv.rules['step'] = {
	validator: function (val, step) {

		// in order to handle steps of .1 & .01 etc.. Modulus won't work
		// if the value is a decimal, so we have to correct for that
		if (kv.utils.isEmptyVal(val) || step === 'any') { return true; }
		var dif = (val * 100) % (step * 100);
		return Math.abs(dif) < 0.00001 || Math.abs(1 - dif) < 0.00001;
	},
	message: 'The value must increment by {0}.'
};

kv.rules['email'] = {
	validator: function (val, validate) {
		if (!validate) { return true; }

		//I think an empty email address is also a valid entry
		//if one want's to enforce entry it should be done with 'required: true'
		return kv.utils.isEmptyVal(val) || (
			// jquery validate regex - thanks Scott Gonzalez
			validate && /^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$/i.test(val)
		);
	},
	message: 'Please enter a proper email address.'
};

kv.rules['date'] = {
	validator: function (value, validate) {
		if (!validate) { return true; }
		return kv.utils.isEmptyVal(value) || (validate && !/Invalid|NaN/.test(new Date(value)));
	},
	message: 'Please enter a proper date.'
};

kv.rules['dateISO'] = {
	validator: function (value, validate) {
		if (!validate) { return true; }
		return kv.utils.isEmptyVal(value) || (validate && /^\d{4}[-/](?:0?[1-9]|1[012])[-/](?:0?[1-9]|[12][0-9]|3[01])$/.test(value));
	},
	message: 'Please enter a proper date.'
};

kv.rules['number'] = {
	validator: function (value, validate) {
		if (!validate) { return true; }
		return kv.utils.isEmptyVal(value) || (validate && /^-?(?:\d+|\d{1,3}(?:,\d{3})+)?(?:\.\d+)?$/.test(value));
	},
	message: 'Please enter a number.'
};

kv.rules['digit'] = {
	validator: function (value, validate) {
		if (!validate) { return true; }
		return kv.utils.isEmptyVal(value) || (validate && /^\d+$/.test(value));
	},
	message: 'Please enter a digit.'
};

kv.rules['phoneUS'] = {
	validator: function (phoneNumber, validate) {
		if (!validate) { return true; }
		if (kv.utils.isEmptyVal(phoneNumber)) { return true; } // makes it optional, use 'required' rule if it should be required
		if (typeof (phoneNumber) !== 'string') { return false; }
		phoneNumber = phoneNumber.replace(/\s+/g, "");
		return validate && phoneNumber.length > 9 && phoneNumber.match(/^(1-?)?(\([2-9]\d{2}\)|[2-9]\d{2})-?[2-9]\d{2}-?\d{4}$/);
	},
	message: 'Please specify a valid phone number.'
};

kv.rules['equal'] = {
	validator: function (val, params) {
		var otherValue = params;
		return val === kv.utils.getValue(otherValue);
	},
	message: 'Values must equal.'
};

kv.rules['notEqual'] = {
	validator: function (val, params) {
		var otherValue = params;
		return val !== kv.utils.getValue(otherValue);
	},
	message: 'Please choose another value.'
};

//unique in collection
// options are:
//    collection: array or function returning (observable) array
//              in which the value has to be unique
//    valueAccessor: function that returns value from an object stored in collection
//              if it is null the value is compared directly
//    external: set to true when object you are validating is automatically updating collection
kv.rules['unique'] = {
	validator: function (val, options) {
		var c = kv.utils.getValue(options.collection),
			external = kv.utils.getValue(options.externalValue),
			counter = 0;

		if (!val || !c) { return true; }

		koUtils.arrayFilter(c, function (item) {
			if (val === (options.valueAccessor ? options.valueAccessor(item) : item)) { counter++; }
		});
		// if value is external even 1 same value in collection means the value is not unique
		return counter < (!!external ? 1 : 2);
	},
	message: 'Please make sure the value is unique.'
};


//now register all of these!
(function () {
	kv.registerExtenders();
}());
// The core binding handler
// this allows us to setup any value binding that internally always
// performs the same functionality
ko.bindingHandlers['validationCore'] = (function () {

	return {
		init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
			var config = kv.utils.getConfigOptions(element);
			var observable = valueAccessor();

			// parse html5 input validation attributes, optional feature
			if (config.parseInputAttributes) {
				kv.utils.async(function () { kv.parseInputValidationAttributes(element, valueAccessor); });
			}

			// if requested insert message element and apply bindings
			if (config.insertMessages && kv.utils.isValidatable(observable)) {

				// insert the <span></span>
				var validationMessageElement = kv.insertValidationMessage(element);

				// if we're told to use a template, make sure that gets rendered
				if (config.messageTemplate) {
					ko.renderTemplate(config.messageTemplate, { field: observable }, null, validationMessageElement, 'replaceNode');
				} else {
					ko.applyBindingsToNode(validationMessageElement, { validationMessage: observable });
				}
			}

			// write the html5 attributes if indicated by the config
			if (config.writeInputAttributes && kv.utils.isValidatable(observable)) {

				kv.writeInputValidationAttributes(element, valueAccessor);
			}

			// if requested, add binding to decorate element
			if (config.decorateInputElement && kv.utils.isValidatable(observable)) {
				ko.applyBindingsToNode(element, { validationElement: observable });
			}
		}
	};

}());

// override for KO's default 'value', 'checked', 'textInput' and selectedOptions bindings
kv.makeBindingHandlerValidatable("value");
kv.makeBindingHandlerValidatable("checked");
if (ko.bindingHandlers.textInput) {
	kv.makeBindingHandlerValidatable("textInput");
}
kv.makeBindingHandlerValidatable("selectedOptions");


ko.bindingHandlers['validationMessage'] = { // individual error message, if modified or post binding
	update: function (element, valueAccessor) {
		var obsv = valueAccessor(),
			config = kv.utils.getConfigOptions(element),
			val = unwrap(obsv),
			msg = null,
			isModified = false,
			isValid = false;

		if (obsv === null || typeof obsv === 'undefined') {
			throw new Error('Cannot bind validationMessage to undefined value. data-bind expression: ' +
				element.getAttribute('data-bind'));
		}

		isModified = obsv.isModified && obsv.isModified();
		isValid = obsv.isValid && obsv.isValid();

		var error = null;
		if (!config.messagesOnModified || isModified) {
			error = isValid ? null : obsv.error;
		}

		var isVisible = !config.messagesOnModified || isModified ? !isValid : false;
		var isCurrentlyVisible = element.style.display !== "none";

		if (config.allowHtmlMessages) {
			koUtils.setHtml(element, error);
		} else {
			ko.bindingHandlers.text.update(element, function () { return error; });
		}

		if (isCurrentlyVisible && !isVisible) {
			element.style.display = 'none';
		} else if (!isCurrentlyVisible && isVisible) {
			element.style.display = '';
		}
	}
};

ko.bindingHandlers['validationElement'] = {
	update: function (element, valueAccessor, allBindingsAccessor) {
		var obsv = valueAccessor(),
			config = kv.utils.getConfigOptions(element),
			val = unwrap(obsv),
			msg = null,
			isModified = false,
			isValid = false;

		if (obsv === null || typeof obsv === 'undefined') {
			throw new Error('Cannot bind validationElement to undefined value. data-bind expression: ' +
				element.getAttribute('data-bind'));
		}

		isModified = obsv.isModified && obsv.isModified();
		isValid = obsv.isValid && obsv.isValid();

		// create an evaluator function that will return something like:
		// css: { validationElement: true }
		var cssSettingsAccessor = function () {
			var css = {};

			var shouldShow = ((!config.decorateElementOnModified || isModified) ? !isValid : false);

			// css: { validationElement: false }
			css[config.errorElementClass] = shouldShow;

			return css;
		};

		//add or remove class on the element;
		ko.bindingHandlers.css.update(element, cssSettingsAccessor, allBindingsAccessor);
		if (!config.errorsAsTitle) { return; }

		ko.bindingHandlers.attr.update(element, function () {
			var
				hasModification = !config.errorsAsTitleOnModified || isModified,
				title = kv.utils.getOriginalElementTitle(element);

			if (hasModification && !isValid) {
				return { title: obsv.error, 'data-orig-title': title };
			} else if (!hasModification || isValid) {
				return { title: title, 'data-orig-title': null };
			}
		});
	}
};

// ValidationOptions:
// This binding handler allows you to override the initial config by setting any of the options for a specific element or context of elements
//
// Example:
// <div data-bind="validationOptions: { insertMessages: true, messageTemplate: 'customTemplate', errorMessageClass: 'mySpecialClass'}">
//      <input type="text" data-bind="value: someValue"/>
//      <input type="text" data-bind="value: someValue2"/>
// </div>
ko.bindingHandlers['validationOptions'] = (function () {
	return {
		init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
			var options = unwrap(valueAccessor());
			if (options) {
				var newConfig = extend({}, kv.configuration);
				extend(newConfig, options);

				//store the validation options on the node so we can retrieve it later
				kv.utils.setDomData(element, newConfig);
			}
		}
	};
}());
// Validation Extender:
// This is for creating custom validation logic on the fly
// Example:
// var test = ko.observable('something').extend{(
//      validation: {
//          validator: function(val, someOtherVal){
//              return true;
//          },
//          message: "Something must be really wrong!',
//          params: true
//      }
//  )};
ko.extenders['validation'] = function (observable, rules) { // allow single rule or array
	forEach(kv.utils.isArray(rules) ? rules : [rules], function (rule) {
		// the 'rule' being passed in here has no name to identify a core Rule,
		// so we add it as an anonymous rule
		// If the developer is wanting to use a core Rule, but use a different message see the 'addExtender' logic for examples
		kv.addAnonymousRule(observable, rule);
	});
	return observable;
};

//This is the extender that makes a Knockout Observable also 'Validatable'
//examples include:
// 1. var test = ko.observable('something').extend({validatable: true});
// this will ensure that the Observable object is setup properly to respond to rules
//
// 2. test.extend({validatable: false});
// this will remove the validation properties from the Observable object should you need to do that.
ko.extenders['validatable'] = function (observable, options) {
	if (!kv.utils.isObject(options)) {
		options = { enable: options };
	}

	if (!('enable' in options)) {
		options.enable = true;
	}

	if (options.enable && !kv.utils.isValidatable(observable)) {
		var config = kv.configuration.validate || {};
		var validationOptions = {
			throttleEvaluation : options.throttle || config.throttle
		};

		observable.error = ko.observable(null); // holds the error message, we only need one since we stop processing validators when one is invalid

		// observable.rules:
		// ObservableArray of Rule Contexts, where a Rule Context is simply the name of a rule and the params to supply to it
		//
		// Rule Context = { rule: '<rule name>', params: '<passed in params>', message: '<Override of default Message>' }
		observable.rules = ko.observableArray(); //holds the rule Contexts to use as part of validation

		//in case async validation is occurring
		observable.isValidating = ko.observable(false);

		//the true holder of whether the observable is valid or not
		observable.__valid__ = ko.observable(true);

		observable.isModified = ko.observable(false);

		// a semi-protected observable
		observable.isValid = ko.computed(observable.__valid__);

		//manually set error state
		observable.setError = function (error) {
			var previousError = observable.error.peek();
			var previousIsValid = observable.__valid__.peek();

			observable.error(error);
			observable.__valid__(false);

			if (previousError !== error && !previousIsValid) {
				// if the observable was not valid before then isValid will not mutate,
				// hence causing any grouping to not display the latest error.
				observable.isValid.notifySubscribers();
			}
		};

		//manually clear error state
		observable.clearError = function () {
			observable.error(null);
			observable.__valid__(true);
			return observable;
		};

		//subscribe to changes in the observable
		var h_change = observable.subscribe(function () {
			observable.isModified(true);
		});

		// we use a computed here to ensure that anytime a dependency changes, the
		// validation logic evaluates
		var h_obsValidationTrigger = ko.computed(extend({
			read: function () {
				var obs = observable(),
					ruleContexts = observable.rules();

				kv.validateObservable(observable);

				return true;
			}
		}, validationOptions));

		extend(h_obsValidationTrigger, validationOptions);

		observable._disposeValidation = function () {
			//first dispose of the subscriptions
			observable.isValid.dispose();
			observable.rules.removeAll();
			h_change.dispose();
			h_obsValidationTrigger.dispose();

			delete observable['rules'];
			delete observable['error'];
			delete observable['isValid'];
			delete observable['isValidating'];
			delete observable['__valid__'];
			delete observable['isModified'];
            delete observable['setError'];
            delete observable['clearError'];
            delete observable['_disposeValidation'];
		};
	} else if (options.enable === false && observable._disposeValidation) {
		observable._disposeValidation();
	}
	return observable;
};

function validateSync(observable, rule, ctx) {
	//Execute the validator and see if its valid
	if (!rule.validator(observable(), (ctx.params === undefined ? true : unwrap(ctx.params)))) { // default param is true, eg. required = true

		//not valid, so format the error message and stick it in the 'error' variable
		observable.setError(kv.formatMessage(
					ctx.message || rule.message,
					unwrap(ctx.params),
					observable));
		return false;
	} else {
		return true;
	}
}

function validateAsync(observable, rule, ctx) {
	observable.isValidating(true);

	var callBack = function (valObj) {
		var isValid = false,
			msg = '';

		if (!observable.__valid__()) {

			// since we're returning early, make sure we turn this off
			observable.isValidating(false);

			return; //if its already NOT valid, don't add to that
		}

		//we were handed back a complex object
		if (valObj['message']) {
			isValid = valObj.isValid;
			msg = valObj.message;
		} else {
			isValid = valObj;
		}

		if (!isValid) {
			//not valid, so format the error message and stick it in the 'error' variable
			observable.error(kv.formatMessage(
				msg || ctx.message || rule.message,
				unwrap(ctx.params),
				observable));
			observable.__valid__(isValid);
		}

		// tell it that we're done
		observable.isValidating(false);
	};

	kv.utils.async(function() {
	    //fire the validator and hand it the callback
        rule.validator(observable(), ctx.params === undefined ? true : unwrap(ctx.params), callBack);
	});
}

kv.validateObservable = function (observable) {
	var i = 0,
		rule, // the rule validator to execute
		ctx, // the current Rule Context for the loop
		ruleContexts = observable.rules(), //cache for iterator
		len = ruleContexts.length; //cache for iterator

	for (; i < len; i++) {

		//get the Rule Context info to give to the core Rule
		ctx = ruleContexts[i];

		// checks an 'onlyIf' condition
		if (ctx.condition && !ctx.condition()) {
			continue;
		}

		//get the core Rule to use for validation
		rule = ctx.rule ? kv.rules[ctx.rule] : ctx;

		if (rule['async'] || ctx['async']) {
			//run async validation
			validateAsync(observable, rule, ctx);

		} else {
			//run normal sync validation
			if (!validateSync(observable, rule, ctx)) {
				return false; //break out of the loop
			}
		}
	}
	//finally if we got this far, make the observable valid again!
	observable.clearError();
	return true;
};

var _locales = {};
var _currentLocale;

kv.defineLocale = function(name, values) {
	if (name && values) {
		_locales[name.toLowerCase()] = values;
		return values;
	}
	return null;
};

kv.locale = function(name) {
	if (name) {
		name = name.toLowerCase();

		if (_locales.hasOwnProperty(name)) {
			kv.localize(_locales[name]);
			_currentLocale = name;
		}
		else {
			throw new Error('Localization ' + name + ' has not been loaded.');
		}
	}
	return _currentLocale;
};

//quick function to override rule messages
kv.localize = function (msgTranslations) {
	var rules = kv.rules;

	//loop the properties in the object and assign the msg to the rule
	for (var ruleName in msgTranslations) {
		if (rules.hasOwnProperty(ruleName)) {
			rules[ruleName].message = msgTranslations[ruleName];
		}
	}
};

// Populate default locale (this will make en-US.js somewhat redundant)
(function() {
	var localeData = {};
	var rules = kv.rules;

	for (var ruleName in rules) {
		if (rules.hasOwnProperty(ruleName)) {
			localeData[ruleName] = rules[ruleName].message;
		}
	}
	kv.defineLocale('en-us', localeData);
})();

// No need to invoke locale because the messages are already defined along with the rules for en-US
_currentLocale = 'en-us';
/**
 * Possible invocations:
 * 		applyBindingsWithValidation(viewModel)
 * 		applyBindingsWithValidation(viewModel, options)
 * 		applyBindingsWithValidation(viewModel, rootNode)
 *		applyBindingsWithValidation(viewModel, rootNode, options)
 */
ko.applyBindingsWithValidation = function (viewModel, rootNode, options) {
	var node = document.body,
		config;

	if (rootNode && rootNode.nodeType) {
		node = rootNode;
		config = options;
	}
	else {
		config = rootNode;
	}

	kv.init();

	if (config) {
		config = extend(extend({}, kv.configuration), config);
		kv.utils.setDomData(node, config);
	}

	ko.applyBindings(viewModel, node);
};

//override the original applyBindings so that we can ensure all new rules and what not are correctly registered
var origApplyBindings = ko.applyBindings;
ko.applyBindings = function (viewModel, rootNode) {

	kv.init();

	origApplyBindings(viewModel, rootNode);
};

ko.validatedObservable = function (initialValue, options) {
	if (!options && !kv.utils.isObject(initialValue)) {
		return ko.observable(initialValue).extend({ validatable: true });
	}

	var obsv = ko.observable(initialValue);
	obsv.errors = kv.group(kv.utils.isObject(initialValue) ? initialValue : {}, options);
	obsv.isValid = ko.observable(obsv.errors().length === 0);

	if (ko.isObservable(obsv.errors)) {
		obsv.errors.subscribe(function(errors) {
			obsv.isValid(errors.length === 0);
		});
	}
	else {
		ko.computed(obsv.errors).subscribe(function (errors) {
			obsv.isValid(errors.length === 0);
		});
	}

	obsv.subscribe(function(newValue) {
		if (!kv.utils.isObject(newValue)) {
			/*
			 * The validation group works on objects.
			 * Since the new value is a primitive (scalar, null or undefined) we need
			 * to create an empty object to pass along.
			 */
			newValue = {};
		}
		// Force the group to refresh
		obsv.errors._updateState(newValue);
		obsv.isValid(obsv.errors().length === 0);
	});

	return obsv;
};
}));
});

var IdeaBasicInfoViewModel = function () {
    function IdeaBasicInfoViewModel(_ref) {
        var _ref$idea = _ref.idea,
            idea = _ref$idea === undefined ? {} : _ref$idea,
            save = _ref.actions.save;
        classCallCheck(this, IdeaBasicInfoViewModel);


        knockout.validation.init({
            errorElementClass: 'has-error',
            errorMessageClass: 'help-block',
            decorateInputElement: true
        });

        this.actions = { save: save };
        this.isSaving = knockout.observable(false);
        this._initForm(idea);
    }

    createClass(IdeaBasicInfoViewModel, [{
        key: '_initForm',
        value: function _initForm(_ref2) {
            var id = _ref2.id,
                _ref2$title = _ref2.title,
                title = _ref2$title === undefined ? '' : _ref2$title,
                _ref2$description = _ref2.description,
                description = _ref2$description === undefined ? '' : _ref2$description,
                _ref2$isFundingRequir = _ref2.isFundingRequired,
                isFundingRequired = _ref2$isFundingRequir === undefined ? false : _ref2$isFundingRequir,
                _ref2$fundingRequirem = _ref2.fundingRequirement,
                fundingRequirement = _ref2$fundingRequirem === undefined ? '' : _ref2$fundingRequirem;

            this.form = knockout.validatedObservable({
                id: id,
                title: knockout.observable(title).extend({ required: true }),
                description: knockout.observable(description).extend({ required: true }),
                isFundingRequired: knockout.observable(isFundingRequired).extend({ required: true }),
                fundingRequirement: knockout.observable(fundingRequirement).extend({ required: false })
            });
        }
    }, {
        key: '_validate',
        value: function _validate() {

            if (!this.form.isValid()) {
                this.form.errors.showAllMessages();
                return false;
            }
            return true;
        }
    }, {
        key: 'save',
        value: function save() {
            var _this = this;

            if (!this._validate()) {
                return Promise.reject('Validation failed');
            }

            var form = knockout.toJS(this.form);

            form.isFundingRequired = !!(form.fundingRequirement && form.fundingRequirement.length > 0);

            this.isSaving(true);
            var save = this.actions.save;

            return save(form).then(function (res) {
                _this.isSaving(false);
                return Promise.resolve(res);
            }).catch(function (e) {
                _this.isSaving(false);
            });
        }
    }]);
    return IdeaBasicInfoViewModel;
}();

var turbolinks = createCommonjsModule(function (module) {
/*
Turbolinks 5.0.0
Copyright © 2016 Basecamp, LLC
 */
(function(){(function(){(function(){this.Turbolinks={supported:function(){return null!=window.history.pushState&&null!=window.requestAnimationFrame}(),visit:function(e,r){return t.controller.visit(e,r)},clearCache:function(){return t.controller.clearCache()}};}).call(this);}).call(this);var t=this.Turbolinks;(function(){(function(){var e,r;t.copyObject=function(t){var e,r,n;r={};for(e in t)n=t[e],r[e]=n;return r},t.closest=function(t,r){return e.call(t,r)},e=function(){var t,e;return t=document.documentElement,null!=(e=t.closest)?e:function(t){var e;for(e=this;e;){if(e.nodeType===Node.ELEMENT_NODE&&r.call(e,t))return e;e=e.parentNode;}}}(),t.defer=function(t){return setTimeout(t,1)},t.dispatch=function(t,e){var r,n,o,i,s;return i=null!=e?e:{},s=i.target,r=i.cancelable,n=i.data,o=document.createEvent("Events"),o.initEvent(t,!0,r===!0),o.data=null!=n?n:{},(null!=s?s:document).dispatchEvent(o),o},t.match=function(t,e){return r.call(t,e)},r=function(){var t,e,r,n;return t=document.documentElement,null!=(e=null!=(r=null!=(n=t.matchesSelector)?n:t.webkitMatchesSelector)?r:t.msMatchesSelector)?e:t.mozMatchesSelector}(),t.uuid=function(){var t,e,r;for(r="",t=e=1;36>=e;t=++e)r+=9===t||14===t||19===t||24===t?"-":15===t?"4":20===t?(Math.floor(4*Math.random())+8).toString(16):Math.floor(15*Math.random()).toString(16);return r};}).call(this),function(){t.Location=function(){function t(t){var e,r;null==t&&(t=""),r=document.createElement("a"),r.href=t.toString(),this.absoluteURL=r.href,e=r.hash.length,2>e?this.requestURL=this.absoluteURL:(this.requestURL=this.absoluteURL.slice(0,-e),this.anchor=r.hash.slice(1));}var e,r,n,o;return t.wrap=function(t){return t instanceof this?t:new this(t)},t.prototype.getOrigin=function(){return this.absoluteURL.split("/",3).join("/")},t.prototype.getPath=function(){var t,e;return null!=(t=null!=(e=this.absoluteURL.match(/\/\/[^\/]*(\/[^?;]*)/))?e[1]:void 0)?t:"/"},t.prototype.getPathComponents=function(){return this.getPath().split("/").slice(1)},t.prototype.getLastPathComponent=function(){return this.getPathComponents().slice(-1)[0]},t.prototype.getExtension=function(){var t,e;return null!=(t=null!=(e=this.getLastPathComponent().match(/\.[^.]*$/))?e[0]:void 0)?t:""},t.prototype.isHTML=function(){return this.getExtension().match(/^(?:|\.(?:htm|html|xhtml))$/)},t.prototype.isPrefixedBy=function(t){var e;return e=r(t),this.isEqualTo(t)||o(this.absoluteURL,e)},t.prototype.isEqualTo=function(t){return this.absoluteURL===(null!=t?t.absoluteURL:void 0)},t.prototype.toCacheKey=function(){return this.requestURL},t.prototype.toJSON=function(){return this.absoluteURL},t.prototype.toString=function(){return this.absoluteURL},t.prototype.valueOf=function(){return this.absoluteURL},r=function(t){return e(t.getOrigin()+t.getPath())},e=function(t){return n(t,"/")?t:t+"/"},o=function(t,e){return t.slice(0,e.length)===e},n=function(t,e){return t.slice(-e.length)===e},t}();}.call(this),function(){var e=function(t,e){return function(){return t.apply(e,arguments)}};t.HttpRequest=function(){function r(r,n,o){this.delegate=r,this.requestCanceled=e(this.requestCanceled,this),this.requestTimedOut=e(this.requestTimedOut,this),this.requestFailed=e(this.requestFailed,this),this.requestLoaded=e(this.requestLoaded,this),this.requestProgressed=e(this.requestProgressed,this),this.url=t.Location.wrap(n).requestURL,this.referrer=t.Location.wrap(o).absoluteURL,this.createXHR();}return r.NETWORK_FAILURE=0,r.TIMEOUT_FAILURE=-1,r.timeout=60,r.prototype.send=function(){var t;return this.xhr&&!this.sent?(this.notifyApplicationBeforeRequestStart(),this.setProgress(0),this.xhr.send(),this.sent=!0,"function"==typeof(t=this.delegate).requestStarted?t.requestStarted():void 0):void 0},r.prototype.cancel=function(){return this.xhr&&this.sent?this.xhr.abort():void 0},r.prototype.requestProgressed=function(t){return t.lengthComputable?this.setProgress(t.loaded/t.total):void 0},r.prototype.requestLoaded=function(){return this.endRequest(function(t){return function(){var e;return 200<=(e=t.xhr.status)&&300>e?t.delegate.requestCompletedWithResponse(t.xhr.responseText,t.xhr.getResponseHeader("Turbolinks-Location")):(t.failed=!0,t.delegate.requestFailedWithStatusCode(t.xhr.status,t.xhr.responseText))}}(this))},r.prototype.requestFailed=function(){return this.endRequest(function(t){return function(){return t.failed=!0,t.delegate.requestFailedWithStatusCode(t.constructor.NETWORK_FAILURE)}}(this))},r.prototype.requestTimedOut=function(){return this.endRequest(function(t){return function(){return t.failed=!0,t.delegate.requestFailedWithStatusCode(t.constructor.TIMEOUT_FAILURE)}}(this))},r.prototype.requestCanceled=function(){return this.endRequest()},r.prototype.notifyApplicationBeforeRequestStart=function(){return t.dispatch("turbolinks:request-start",{data:{url:this.url,xhr:this.xhr}})},r.prototype.notifyApplicationAfterRequestEnd=function(){return t.dispatch("turbolinks:request-end",{data:{url:this.url,xhr:this.xhr}})},r.prototype.createXHR=function(){return this.xhr=new XMLHttpRequest,this.xhr.open("GET",this.url,!0),this.xhr.timeout=1e3*this.constructor.timeout,this.xhr.setRequestHeader("Accept","text/html, application/xhtml+xml"),this.xhr.setRequestHeader("Turbolinks-Referrer",this.referrer),this.xhr.onprogress=this.requestProgressed,this.xhr.onload=this.requestLoaded,this.xhr.onerror=this.requestFailed,this.xhr.ontimeout=this.requestTimedOut,this.xhr.onabort=this.requestCanceled},r.prototype.endRequest=function(t){return this.xhr?(this.notifyApplicationAfterRequestEnd(),null!=t&&t.call(this),this.destroy()):void 0},r.prototype.setProgress=function(t){var e;return this.progress=t,"function"==typeof(e=this.delegate).requestProgressed?e.requestProgressed(this.progress):void 0},r.prototype.destroy=function(){var t;return this.setProgress(1),"function"==typeof(t=this.delegate).requestFinished&&t.requestFinished(),this.delegate=null,this.xhr=null},r}();}.call(this),function(){var e=function(t,e){return function(){return t.apply(e,arguments)}};t.ProgressBar=function(){function t(){this.trickle=e(this.trickle,this),this.stylesheetElement=this.createStylesheetElement(),this.progressElement=this.createProgressElement();}var r;return r=300,t.defaultCSS=".turbolinks-progress-bar {\n  position: fixed;\n  display: block;\n  top: 0;\n  left: 0;\n  height: 3px;\n  background: #0076ff;\n  z-index: 9999;\n  transition: width "+r+"ms ease-out, opacity "+r/2+"ms "+r/2+"ms ease-in;\n  transform: translate3d(0, 0, 0);\n}",t.prototype.show=function(){return this.visible?void 0:(this.visible=!0,this.installStylesheetElement(),this.installProgressElement(),this.startTrickling())},t.prototype.hide=function(){return this.visible&&!this.hiding?(this.hiding=!0,this.fadeProgressElement(function(t){return function(){return t.uninstallProgressElement(),t.stopTrickling(),t.visible=!1,t.hiding=!1}}(this))):void 0},t.prototype.setValue=function(t){return this.value=t,this.refresh()},t.prototype.installStylesheetElement=function(){return document.head.insertBefore(this.stylesheetElement,document.head.firstChild)},t.prototype.installProgressElement=function(){return this.progressElement.style.width=0,this.progressElement.style.opacity=1,document.documentElement.insertBefore(this.progressElement,document.body),this.refresh()},t.prototype.fadeProgressElement=function(t){return this.progressElement.style.opacity=0,setTimeout(t,1.5*r)},t.prototype.uninstallProgressElement=function(){return this.progressElement.parentNode?document.documentElement.removeChild(this.progressElement):void 0},t.prototype.startTrickling=function(){return null!=this.trickleInterval?this.trickleInterval:this.trickleInterval=setInterval(this.trickle,r)},t.prototype.stopTrickling=function(){return clearInterval(this.trickleInterval),this.trickleInterval=null},t.prototype.trickle=function(){return this.setValue(this.value+Math.random()/100)},t.prototype.refresh=function(){return requestAnimationFrame(function(t){return function(){return t.progressElement.style.width=10+90*t.value+"%"}}(this))},t.prototype.createStylesheetElement=function(){var t;return t=document.createElement("style"),t.type="text/css",t.textContent=this.constructor.defaultCSS,t},t.prototype.createProgressElement=function(){var t;return t=document.createElement("div"),t.className="turbolinks-progress-bar",t},t}();}.call(this),function(){var e=function(t,e){return function(){return t.apply(e,arguments)}};t.BrowserAdapter=function(){function r(r){this.controller=r,this.showProgressBar=e(this.showProgressBar,this),this.progressBar=new t.ProgressBar;}var n,o,i,s;return s=t.HttpRequest,n=s.NETWORK_FAILURE,i=s.TIMEOUT_FAILURE,o=500,r.prototype.visitProposedToLocationWithAction=function(t,e){return this.controller.startVisitToLocationWithAction(t,e)},r.prototype.visitStarted=function(t){return t.issueRequest(),t.changeHistory(),t.loadCachedSnapshot()},r.prototype.visitRequestStarted=function(t){return this.progressBar.setValue(0),t.hasCachedSnapshot()||"restore"!==t.action?this.showProgressBarAfterDelay():this.showProgressBar()},r.prototype.visitRequestProgressed=function(t){return this.progressBar.setValue(t.progress)},r.prototype.visitRequestCompleted=function(t){return t.loadResponse()},r.prototype.visitRequestFailedWithStatusCode=function(t,e){switch(e){case n:case i:return this.reload();default:return t.loadResponse()}},r.prototype.visitRequestFinished=function(t){return this.hideProgressBar()},r.prototype.visitCompleted=function(t){return t.followRedirect()},r.prototype.pageInvalidated=function(){return this.reload()},r.prototype.showProgressBarAfterDelay=function(){return this.progressBarTimeout=setTimeout(this.showProgressBar,o)},r.prototype.showProgressBar=function(){return this.progressBar.show()},r.prototype.hideProgressBar=function(){return this.progressBar.hide(),clearTimeout(this.progressBarTimeout)},r.prototype.reload=function(){return window.location.reload()},r}();}.call(this),function(){var e,r=function(t,e){return function(){return t.apply(e,arguments)}};e=!1,addEventListener("load",function(){return t.defer(function(){return e=!0})},!1),t.History=function(){function n(t){this.delegate=t,this.onPopState=r(this.onPopState,this);}return n.prototype.start=function(){return this.started?void 0:(addEventListener("popstate",this.onPopState,!1),this.started=!0)},n.prototype.stop=function(){return this.started?(removeEventListener("popstate",this.onPopState,!1),this.started=!1):void 0},n.prototype.push=function(e,r){return e=t.Location.wrap(e),this.update("push",e,r)},n.prototype.replace=function(e,r){return e=t.Location.wrap(e),this.update("replace",e,r)},n.prototype.onPopState=function(e){var r,n,o,i;return this.shouldHandlePopState()&&(i=null!=(n=e.state)?n.turbolinks:void 0)?(r=t.Location.wrap(window.location),o=i.restorationIdentifier,this.delegate.historyPoppedToLocationWithRestorationIdentifier(r,o)):void 0},n.prototype.shouldHandlePopState=function(){return e===!0},n.prototype.update=function(t,e,r){var n;return n={turbolinks:{restorationIdentifier:r}},history[t+"State"](n,null,e)},n}();}.call(this),function(){t.Snapshot=function(){function e(t){var e,r;r=t.head,e=t.body,this.head=null!=r?r:document.createElement("head"),this.body=null!=e?e:document.createElement("body");}return e.wrap=function(t){return t instanceof this?t:this.fromHTML(t)},e.fromHTML=function(t){var e;return e=document.createElement("html"),e.innerHTML=t,this.fromElement(e)},e.fromElement=function(t){return new this({head:t.querySelector("head"),body:t.querySelector("body")})},e.prototype.clone=function(){return new e({head:this.head.cloneNode(!0),body:this.body.cloneNode(!0)})},e.prototype.getRootLocation=function(){var e,r;return r=null!=(e=this.getSetting("root"))?e:"/",new t.Location(r)},e.prototype.getCacheControlValue=function(){return this.getSetting("cache-control")},e.prototype.hasAnchor=function(t){try{return null!=this.body.querySelector("[id='"+t+"']")}catch(e){}},e.prototype.isPreviewable=function(){return"no-preview"!==this.getCacheControlValue()},e.prototype.isCacheable=function(){return"no-cache"!==this.getCacheControlValue()},e.prototype.getSetting=function(t){var e,r;return r=this.head.querySelectorAll("meta[name='turbolinks-"+t+"']"),e=r[r.length-1],null!=e?e.getAttribute("content"):void 0},e}();}.call(this),function(){var e=[].slice;t.Renderer=function(){function t(){}var r;return t.render=function(){var t,r,n,o;return n=arguments[0],r=arguments[1],t=3<=arguments.length?e.call(arguments,2):[],o=function(t,e,r){r.prototype=t.prototype;var n=new r,o=t.apply(n,e);return Object(o)===o?o:n}(this,t,function(){}),o.delegate=n,o.render(r),o},t.prototype.renderView=function(t){return this.delegate.viewWillRender(this.newBody),t(),this.delegate.viewRendered(this.newBody)},t.prototype.invalidateView=function(){return this.delegate.viewInvalidated()},t.prototype.createScriptElement=function(t){var e;return"false"===t.getAttribute("data-turbolinks-eval")?t:(e=document.createElement("script"),e.textContent=t.textContent,r(e,t),e)},r=function(t,e){var r,n,o,i,s,a,u;for(i=e.attributes,a=[],r=0,n=i.length;n>r;r++)s=i[r],o=s.name,u=s.value,a.push(t.setAttribute(o,u));return a},t}();}.call(this),function(){t.HeadDetails=function(){function t(t){var e,r,i,s,a,u,c;for(this.element=t,this.elements={},c=this.element.childNodes,s=0,u=c.length;u>s;s++)i=c[s],i.nodeType===Node.ELEMENT_NODE&&(a=i.outerHTML,r=null!=(e=this.elements)[a]?e[a]:e[a]={type:o(i),tracked:n(i),elements:[]},r.elements.push(i));}var e,r,n,o;return t.prototype.hasElementWithKey=function(t){return t in this.elements},t.prototype.getTrackedElementSignature=function(){var t,e;return function(){var r,n;r=this.elements,n=[];for(t in r)e=r[t].tracked,e&&n.push(t);return n}.call(this).join("")},t.prototype.getScriptElementsNotInDetails=function(t){return this.getElementsMatchingTypeNotInDetails("script",t)},t.prototype.getStylesheetElementsNotInDetails=function(t){return this.getElementsMatchingTypeNotInDetails("stylesheet",t)},t.prototype.getElementsMatchingTypeNotInDetails=function(t,e){var r,n,o,i,s,a;o=this.elements,s=[];for(n in o)i=o[n],a=i.type,r=i.elements,a!==t||e.hasElementWithKey(n)||s.push(r[0]);return s},t.prototype.getProvisionalElements=function(){var t,e,r,n,o,i,s;r=[],n=this.elements;for(e in n)o=n[e],s=o.type,i=o.tracked,t=o.elements,null!=s||i?t.length>1&&r.push.apply(r,t.slice(1)):r.push.apply(r,t);return r},o=function(t){return e(t)?"script":r(t)?"stylesheet":void 0},n=function(t){return"reload"===t.getAttribute("data-turbolinks-track")},e=function(t){var e;return e=t.tagName.toLowerCase(),"script"===e},r=function(t){var e;return e=t.tagName.toLowerCase(),"style"===e||"link"===e&&"stylesheet"===t.getAttribute("rel")},t}();}.call(this),function(){var e=function(t,e){function n(){this.constructor=t;}for(var o in e)r.call(e,o)&&(t[o]=e[o]);return n.prototype=e.prototype,t.prototype=new n,t.__super__=e.prototype,t},r={}.hasOwnProperty;t.SnapshotRenderer=function(r){function n(e,r){this.currentSnapshot=e,this.newSnapshot=r,this.currentHeadDetails=new t.HeadDetails(this.currentSnapshot.head),this.newHeadDetails=new t.HeadDetails(this.newSnapshot.head),this.newBody=this.newSnapshot.body;}return e(n,r),n.prototype.render=function(t){return this.trackedElementsAreIdentical()?(this.mergeHead(),this.renderView(function(e){return function(){return e.replaceBody(),e.focusFirstAutofocusableElement(),t()}}(this))):this.invalidateView()},n.prototype.mergeHead=function(){return this.copyNewHeadStylesheetElements(),this.copyNewHeadScriptElements(),this.removeCurrentHeadProvisionalElements(),this.copyNewHeadProvisionalElements()},n.prototype.replaceBody=function(){return this.activateBodyScriptElements(),this.importBodyPermanentElements(),this.assignNewBody()},n.prototype.trackedElementsAreIdentical=function(){return this.currentHeadDetails.getTrackedElementSignature()===this.newHeadDetails.getTrackedElementSignature()},n.prototype.copyNewHeadStylesheetElements=function(){var t,e,r,n,o;for(n=this.getNewHeadStylesheetElements(),o=[],e=0,r=n.length;r>e;e++)t=n[e],o.push(document.head.appendChild(t));return o},n.prototype.copyNewHeadScriptElements=function(){var t,e,r,n,o;for(n=this.getNewHeadScriptElements(),o=[],e=0,r=n.length;r>e;e++)t=n[e],o.push(document.head.appendChild(this.createScriptElement(t)));return o},n.prototype.removeCurrentHeadProvisionalElements=function(){var t,e,r,n,o;for(n=this.getCurrentHeadProvisionalElements(),o=[],e=0,r=n.length;r>e;e++)t=n[e],o.push(document.head.removeChild(t));return o},n.prototype.copyNewHeadProvisionalElements=function(){var t,e,r,n,o;for(n=this.getNewHeadProvisionalElements(),o=[],e=0,r=n.length;r>e;e++)t=n[e],o.push(document.head.appendChild(t));return o},n.prototype.importBodyPermanentElements=function(){var t,e,r,n,o,i;for(n=this.getNewBodyPermanentElements(),i=[],e=0,r=n.length;r>e;e++)o=n[e],(t=this.findCurrentBodyPermanentElement(o))?i.push(o.parentNode.replaceChild(t,o)):i.push(void 0);return i},n.prototype.activateBodyScriptElements=function(){var t,e,r,n,o,i;for(n=this.getNewBodyScriptElements(),i=[],e=0,r=n.length;r>e;e++)o=n[e],t=this.createScriptElement(o),i.push(o.parentNode.replaceChild(t,o));return i},n.prototype.assignNewBody=function(){return document.body=this.newBody},n.prototype.focusFirstAutofocusableElement=function(){var t;return null!=(t=this.findFirstAutofocusableElement())?t.focus():void 0},n.prototype.getNewHeadStylesheetElements=function(){return this.newHeadDetails.getStylesheetElementsNotInDetails(this.currentHeadDetails)},n.prototype.getNewHeadScriptElements=function(){return this.newHeadDetails.getScriptElementsNotInDetails(this.currentHeadDetails)},n.prototype.getCurrentHeadProvisionalElements=function(){return this.currentHeadDetails.getProvisionalElements()},n.prototype.getNewHeadProvisionalElements=function(){return this.newHeadDetails.getProvisionalElements()},n.prototype.getNewBodyPermanentElements=function(){return this.newBody.querySelectorAll("[id][data-turbolinks-permanent]")},n.prototype.findCurrentBodyPermanentElement=function(t){return document.body.querySelector("#"+t.id+"[data-turbolinks-permanent]")},n.prototype.getNewBodyScriptElements=function(){return this.newBody.querySelectorAll("script")},n.prototype.findFirstAutofocusableElement=function(){return document.body.querySelector("[autofocus]")},n}(t.Renderer);}.call(this),function(){var e=function(t,e){function n(){this.constructor=t;}for(var o in e)r.call(e,o)&&(t[o]=e[o]);return n.prototype=e.prototype,t.prototype=new n,t.__super__=e.prototype,t},r={}.hasOwnProperty;t.ErrorRenderer=function(t){function r(t){this.html=t;}return e(r,t),r.prototype.render=function(t){return this.renderView(function(e){return function(){return e.replaceDocumentHTML(),e.activateBodyScriptElements(),t()}}(this))},r.prototype.replaceDocumentHTML=function(){return document.documentElement.innerHTML=this.html},r.prototype.activateBodyScriptElements=function(){var t,e,r,n,o,i;for(n=this.getScriptElements(),i=[],e=0,r=n.length;r>e;e++)o=n[e],t=this.createScriptElement(o),i.push(o.parentNode.replaceChild(t,o));return i},r.prototype.getScriptElements=function(){return document.documentElement.querySelectorAll("script")},r}(t.Renderer);}.call(this),function(){t.View=function(){function e(t){this.delegate=t,this.element=document.documentElement;}return e.prototype.getRootLocation=function(){return this.getSnapshot().getRootLocation()},e.prototype.getSnapshot=function(){return t.Snapshot.fromElement(this.element)},e.prototype.render=function(t,e){var r,n,o;return o=t.snapshot,r=t.error,n=t.isPreview,this.markAsPreview(n),null!=o?this.renderSnapshot(o,e):this.renderError(r,e)},e.prototype.markAsPreview=function(t){return t?this.element.setAttribute("data-turbolinks-preview",""):this.element.removeAttribute("data-turbolinks-preview")},e.prototype.renderSnapshot=function(e,r){return t.SnapshotRenderer.render(this.delegate,r,this.getSnapshot(),t.Snapshot.wrap(e))},e.prototype.renderError=function(e,r){return t.ErrorRenderer.render(this.delegate,r,e)},e}();}.call(this),function(){var e=function(t,e){return function(){return t.apply(e,arguments)}};t.ScrollManager=function(){function t(t){this.delegate=t,this.onScroll=e(this.onScroll,this);}return t.prototype.start=function(){return this.started?void 0:(addEventListener("scroll",this.onScroll,!1),this.onScroll(),this.started=!0)},t.prototype.stop=function(){return this.started?(removeEventListener("scroll",this.onScroll,!1),this.started=!1):void 0},t.prototype.scrollToElement=function(t){return t.scrollIntoView()},t.prototype.scrollToPosition=function(t){var e,r;return e=t.x,r=t.y,window.scrollTo(e,r)},t.prototype.onScroll=function(t){return this.updatePosition({x:window.pageXOffset,y:window.pageYOffset})},t.prototype.updatePosition=function(t){var e;return this.position=t,null!=(e=this.delegate)?e.scrollPositionChanged(this.position):void 0},t}();}.call(this),function(){t.SnapshotCache=function(){function e(t){this.size=t,this.keys=[],this.snapshots={};}var r;return e.prototype.has=function(t){var e;return e=r(t),e in this.snapshots},e.prototype.get=function(t){var e;if(this.has(t))return e=this.read(t),this.touch(t),e},e.prototype.put=function(t,e){return this.write(t,e),this.touch(t),e},e.prototype.read=function(t){var e;return e=r(t),this.snapshots[e]},e.prototype.write=function(t,e){var n;return n=r(t),this.snapshots[n]=e},e.prototype.touch=function(t){var e,n;return n=r(t),e=this.keys.indexOf(n),e>-1&&this.keys.splice(e,1),this.keys.unshift(n),this.trim()},e.prototype.trim=function(){var t,e,r,n,o;for(n=this.keys.splice(this.size),o=[],t=0,r=n.length;r>t;t++)e=n[t],o.push(delete this.snapshots[e]);return o},r=function(e){return t.Location.wrap(e).toCacheKey()},e}();}.call(this),function(){var e=function(t,e){return function(){return t.apply(e,arguments)}};t.Visit=function(){function r(r,n,o){this.controller=r,this.action=o,this.performScroll=e(this.performScroll,this),this.identifier=t.uuid(),this.location=t.Location.wrap(n),this.adapter=this.controller.adapter,this.state="initialized",this.timingMetrics={};}var n;return r.prototype.start=function(){return"initialized"===this.state?(this.recordTimingMetric("visitStart"),this.state="started",this.adapter.visitStarted(this)):void 0},r.prototype.cancel=function(){var t;return"started"===this.state?(null!=(t=this.request)&&t.cancel(),this.cancelRender(),this.state="canceled"):void 0},r.prototype.complete=function(){var t;return"started"===this.state?(this.recordTimingMetric("visitEnd"),this.state="completed","function"==typeof(t=this.adapter).visitCompleted&&t.visitCompleted(this),this.controller.visitCompleted(this)):void 0},r.prototype.fail=function(){var t;return"started"===this.state?(this.state="failed","function"==typeof(t=this.adapter).visitFailed?t.visitFailed(this):void 0):void 0},r.prototype.changeHistory=function(){var t,e;return this.historyChanged?void 0:(t=this.location.isEqualTo(this.referrer)?"replace":this.action,e=n(t),this.controller[e](this.location,this.restorationIdentifier),this.historyChanged=!0)},r.prototype.issueRequest=function(){return this.shouldIssueRequest()&&null==this.request?(this.progress=0,this.request=new t.HttpRequest(this,this.location,this.referrer),this.request.send()):void 0},r.prototype.getCachedSnapshot=function(){var t;return!(t=this.controller.getCachedSnapshotForLocation(this.location))||null!=this.location.anchor&&!t.hasAnchor(this.location.anchor)||"restore"!==this.action&&!t.isPreviewable()?void 0:t},r.prototype.hasCachedSnapshot=function(){return null!=this.getCachedSnapshot()},r.prototype.loadCachedSnapshot=function(){var t,e;return(e=this.getCachedSnapshot())?(t=this.shouldIssueRequest(),this.render(function(){var r;return this.cacheSnapshot(),this.controller.render({snapshot:e,isPreview:t},this.performScroll),"function"==typeof(r=this.adapter).visitRendered&&r.visitRendered(this),t?void 0:this.complete()})):void 0},r.prototype.loadResponse=function(){return null!=this.response?this.render(function(){var t,e;return this.cacheSnapshot(),this.request.failed?(this.controller.render({error:this.response},this.performScroll),"function"==typeof(t=this.adapter).visitRendered&&t.visitRendered(this),this.fail()):(this.controller.render({snapshot:this.response},this.performScroll),"function"==typeof(e=this.adapter).visitRendered&&e.visitRendered(this),this.complete())}):void 0},r.prototype.followRedirect=function(){return this.redirectedToLocation&&!this.followedRedirect?(this.location=this.redirectedToLocation,this.controller.replaceHistoryWithLocationAndRestorationIdentifier(this.redirectedToLocation,this.restorationIdentifier),this.followedRedirect=!0):void 0},r.prototype.requestStarted=function(){var t;return this.recordTimingMetric("requestStart"),"function"==typeof(t=this.adapter).visitRequestStarted?t.visitRequestStarted(this):void 0},r.prototype.requestProgressed=function(t){var e;return this.progress=t,"function"==typeof(e=this.adapter).visitRequestProgressed?e.visitRequestProgressed(this):void 0},r.prototype.requestCompletedWithResponse=function(e,r){return this.response=e,null!=r&&(this.redirectedToLocation=t.Location.wrap(r)),this.adapter.visitRequestCompleted(this)},r.prototype.requestFailedWithStatusCode=function(t,e){return this.response=e,this.adapter.visitRequestFailedWithStatusCode(this,t)},r.prototype.requestFinished=function(){var t;return this.recordTimingMetric("requestEnd"),"function"==typeof(t=this.adapter).visitRequestFinished?t.visitRequestFinished(this):void 0},r.prototype.performScroll=function(){return this.scrolled?void 0:("restore"===this.action?this.scrollToRestoredPosition()||this.scrollToTop():this.scrollToAnchor()||this.scrollToTop(),this.scrolled=!0)},r.prototype.scrollToRestoredPosition=function(){var t,e;return t=null!=(e=this.restorationData)?e.scrollPosition:void 0,null!=t?(this.controller.scrollToPosition(t),!0):void 0},r.prototype.scrollToAnchor=function(){return null!=this.location.anchor?(this.controller.scrollToAnchor(this.location.anchor),!0):void 0},r.prototype.scrollToTop=function(){return this.controller.scrollToPosition({x:0,y:0})},r.prototype.recordTimingMetric=function(t){var e;return null!=(e=this.timingMetrics)[t]?e[t]:e[t]=(new Date).getTime()},r.prototype.getTimingMetrics=function(){return t.copyObject(this.timingMetrics)},n=function(t){switch(t){case"replace":return"replaceHistoryWithLocationAndRestorationIdentifier";case"advance":case"restore":return"pushHistoryWithLocationAndRestorationIdentifier"}},r.prototype.shouldIssueRequest=function(){return"restore"===this.action?!this.hasCachedSnapshot():!0},r.prototype.cacheSnapshot=function(){return this.snapshotCached?void 0:(this.controller.cacheSnapshot(),this.snapshotCached=!0)},r.prototype.render=function(t){return this.cancelRender(),this.frame=requestAnimationFrame(function(e){return function(){return e.frame=null,t.call(e)}}(this))},r.prototype.cancelRender=function(){return this.frame?cancelAnimationFrame(this.frame):void 0},r}();}.call(this),function(){var e=function(t,e){return function(){return t.apply(e,arguments)}};t.Controller=function(){function r(){this.clickBubbled=e(this.clickBubbled,this),this.clickCaptured=e(this.clickCaptured,this),this.pageLoaded=e(this.pageLoaded,this),this.history=new t.History(this),this.view=new t.View(this),this.scrollManager=new t.ScrollManager(this),this.restorationData={},this.clearCache();}return r.prototype.start=function(){return t.supported&&!this.started?(addEventListener("click",this.clickCaptured,!0),addEventListener("DOMContentLoaded",this.pageLoaded,!1),this.scrollManager.start(),this.startHistory(),this.started=!0,this.enabled=!0):void 0},r.prototype.disable=function(){return this.enabled=!1},r.prototype.stop=function(){return this.started?(removeEventListener("click",this.clickCaptured,!0),removeEventListener("DOMContentLoaded",this.pageLoaded,!1),this.scrollManager.stop(),this.stopHistory(),this.started=!1):void 0},r.prototype.clearCache=function(){return this.cache=new t.SnapshotCache(10)},r.prototype.visit=function(e,r){var n,o;return null==r&&(r={}),e=t.Location.wrap(e),this.applicationAllowsVisitingLocation(e)?this.locationIsVisitable(e)?(n=null!=(o=r.action)?o:"advance",this.adapter.visitProposedToLocationWithAction(e,n)):window.location=e:void 0},r.prototype.startVisitToLocationWithAction=function(e,r,n){var o;return t.supported?(o=this.getRestorationDataForIdentifier(n),this.startVisit(e,r,{restorationData:o})):window.location=e},r.prototype.startHistory=function(){return this.location=t.Location.wrap(window.location),this.restorationIdentifier=t.uuid(),this.history.start(),this.history.replace(this.location,this.restorationIdentifier)},r.prototype.stopHistory=function(){return this.history.stop()},r.prototype.pushHistoryWithLocationAndRestorationIdentifier=function(e,r){return this.restorationIdentifier=r,this.location=t.Location.wrap(e),this.history.push(this.location,this.restorationIdentifier)},r.prototype.replaceHistoryWithLocationAndRestorationIdentifier=function(e,r){return this.restorationIdentifier=r,this.location=t.Location.wrap(e),this.history.replace(this.location,this.restorationIdentifier)},r.prototype.historyPoppedToLocationWithRestorationIdentifier=function(e,r){var n;return this.restorationIdentifier=r,this.enabled?(n=this.getRestorationDataForIdentifier(this.restorationIdentifier),this.startVisit(e,"restore",{restorationIdentifier:this.restorationIdentifier,restorationData:n,historyChanged:!0}),this.location=t.Location.wrap(e)):this.adapter.pageInvalidated()},r.prototype.getCachedSnapshotForLocation=function(t){var e;return e=this.cache.get(t),e?e.clone():void 0},r.prototype.shouldCacheSnapshot=function(){return this.view.getSnapshot().isCacheable()},r.prototype.cacheSnapshot=function(){var t;return this.shouldCacheSnapshot()?(this.notifyApplicationBeforeCachingSnapshot(),t=this.view.getSnapshot(),this.cache.put(this.lastRenderedLocation,t.clone())):void 0},r.prototype.scrollToAnchor=function(t){var e;return(e=document.getElementById(t))?this.scrollToElement(e):this.scrollToPosition({x:0,y:0})},r.prototype.scrollToElement=function(t){return this.scrollManager.scrollToElement(t)},r.prototype.scrollToPosition=function(t){return this.scrollManager.scrollToPosition(t)},r.prototype.scrollPositionChanged=function(t){var e;return e=this.getCurrentRestorationData(),e.scrollPosition=t},r.prototype.render=function(t,e){return this.view.render(t,e)},r.prototype.viewInvalidated=function(){return this.adapter.pageInvalidated()},r.prototype.viewWillRender=function(t){return this.notifyApplicationBeforeRender(t)},r.prototype.viewRendered=function(){return this.lastRenderedLocation=this.currentVisit.location,this.notifyApplicationAfterRender()},r.prototype.pageLoaded=function(){return this.lastRenderedLocation=this.location,this.notifyApplicationAfterPageLoad()},r.prototype.clickCaptured=function(){return removeEventListener("click",this.clickBubbled,!1),addEventListener("click",this.clickBubbled,!1)},r.prototype.clickBubbled=function(t){var e,r,n;return this.enabled&&this.clickEventIsSignificant(t)&&(r=this.getVisitableLinkForNode(t.target))&&(n=this.getVisitableLocationForLink(r))&&this.applicationAllowsFollowingLinkToLocation(r,n)?(t.preventDefault(),e=this.getActionForLink(r),this.visit(n,{action:e})):void 0},r.prototype.applicationAllowsFollowingLinkToLocation=function(t,e){var r;return r=this.notifyApplicationAfterClickingLinkToLocation(t,e),!r.defaultPrevented},r.prototype.applicationAllowsVisitingLocation=function(t){var e;return e=this.notifyApplicationBeforeVisitingLocation(t),!e.defaultPrevented},r.prototype.notifyApplicationAfterClickingLinkToLocation=function(e,r){return t.dispatch("turbolinks:click",{target:e,data:{url:r.absoluteURL},cancelable:!0})},r.prototype.notifyApplicationBeforeVisitingLocation=function(e){return t.dispatch("turbolinks:before-visit",{data:{url:e.absoluteURL},cancelable:!0})},r.prototype.notifyApplicationAfterVisitingLocation=function(e){return t.dispatch("turbolinks:visit",{data:{url:e.absoluteURL}})},r.prototype.notifyApplicationBeforeCachingSnapshot=function(){return t.dispatch("turbolinks:before-cache")},r.prototype.notifyApplicationBeforeRender=function(e){return t.dispatch("turbolinks:before-render",{data:{newBody:e}})},r.prototype.notifyApplicationAfterRender=function(){return t.dispatch("turbolinks:render")},r.prototype.notifyApplicationAfterPageLoad=function(e){return null==e&&(e={}),t.dispatch("turbolinks:load",{data:{url:this.location.absoluteURL,timing:e}})},r.prototype.startVisit=function(t,e,r){var n;return null!=(n=this.currentVisit)&&n.cancel(),this.currentVisit=this.createVisit(t,e,r),this.currentVisit.start(),this.notifyApplicationAfterVisitingLocation(t)},r.prototype.createVisit=function(e,r,n){
var o,i,s,a,u;return i=null!=n?n:{},a=i.restorationIdentifier,s=i.restorationData,o=i.historyChanged,u=new t.Visit(this,e,r),u.restorationIdentifier=null!=a?a:t.uuid(),u.restorationData=t.copyObject(s),u.historyChanged=o,u.referrer=this.location,u},r.prototype.visitCompleted=function(t){return this.notifyApplicationAfterPageLoad(t.getTimingMetrics())},r.prototype.clickEventIsSignificant=function(t){return!(t.defaultPrevented||t.target.isContentEditable||t.which>1||t.altKey||t.ctrlKey||t.metaKey||t.shiftKey)},r.prototype.getVisitableLinkForNode=function(e){return this.nodeIsVisitable(e)?t.closest(e,"a[href]:not([target])"):void 0},r.prototype.getVisitableLocationForLink=function(e){var r;return r=new t.Location(e.getAttribute("href")),this.locationIsVisitable(r)?r:void 0},r.prototype.getActionForLink=function(t){var e;return null!=(e=t.getAttribute("data-turbolinks-action"))?e:"advance"},r.prototype.nodeIsVisitable=function(e){var r;return(r=t.closest(e,"[data-turbolinks]"))?"false"!==r.getAttribute("data-turbolinks"):!0},r.prototype.locationIsVisitable=function(t){return t.isPrefixedBy(this.view.getRootLocation())&&t.isHTML()},r.prototype.getCurrentRestorationData=function(){return this.getRestorationDataForIdentifier(this.restorationIdentifier)},r.prototype.getRestorationDataForIdentifier=function(t){var e;return null!=(e=this.restorationData)[t]?e[t]:e[t]={}},r}();}.call(this),function(){var e,r,n;t.start=function(){return r()?(null==t.controller&&(t.controller=e()),t.controller.start()):void 0},r=function(){return null==window.Turbolinks&&(window.Turbolinks=t),n()},e=function(){var e;return e=new t.Controller,e.adapter=new t.BrowserAdapter(e),e},n=function(){return window.Turbolinks===t},n()&&t.start();}.call(this);}).call(this),"object"=='object'&&module.exports?module.exports=t:"function"==typeof undefined&&undefined.amd&&undefined(t);}).call(commonjsGlobal);
});

//import Barba from 'barba.js';
//import FadeTransition from './transitions/fade';
$(function () {
    turbolinks.start();
    $(document).off('turbolinks:before-cache').off('turbolinks:before-render').on('turbolinks:before-cache', function (e) {
        $('.custom-scrollable').mCustomScrollbar('destroy');
        $('body').addClass('animated fadeOut');
    }).on('turbolinks:before-render', function () {
        $(event.data.newBody).removeClass('fadeOut').addClass('animated fadeIn');
    });
});

//Turbolinks would show progress bar automatically when page takes longer than 500ms to load
//https://github.com/turbolinks/turbolinks/issues/17#issuecomment-186635946
//Turbolinks.controller.adapter.hideProgressBar()
//Turbolinks.controller.adapter.showProgressBar()
$.material.init();

var BasePage = function () {
    function BasePage() {

        //this._url = Barba.Pjax.getCurrentUrl();

        classCallCheck(this, BasePage);
    }

    createClass(BasePage, [{
        key: 'getTransition',
        value: function getTransition() {
            return FadeTransition;
        }
    }, {
        key: 'configure',
        value: function configure() {
            //Barba.Pjax.getTransition = () => this.getTransition();
        }
    }, {
        key: 'init',
        value: function init() {
            this.configure();

            $('.custom-scrollable').mCustomScrollbar();

            this.onReady().then(function () {
                $.material.init();
            });

            //Barba.Dispatcher.on('newPageReady', (currentStatus, oldStatus, container) => {
            //    debugger;
            //    if (currentStatus.url == this._url)
            //    { this.onReady(); }
            //});
        }
    }, {
        key: 'onReady',
        value: function onReady() {}
    }]);
    return BasePage;
}();

var navigationHelper = {
    toIdeaImages: function toIdeaImages(id) {
        turbolinks.visit('/ideas/' + id + '/images');
    },
    toEditIdea: function toEditIdea(id) {
        turbolinks.visit('/ideas/' + id + '/edit');
    },
    toIdeaPages: function toIdeaPages(id) {
        turbolinks.visit('/ideas/' + id + '/pages');
    },
    toIdeaDetails: function toIdeaDetails(id) {
        turbolinks.visit('/ideas/' + id + '/details');
    }
};

var CreatePage = function (_BasePage) {
    inherits(CreatePage, _BasePage);

    function CreatePage(client) {
        classCallCheck(this, CreatePage);

        var _this = possibleConstructorReturn(this, (CreatePage.__proto__ || Object.getPrototypeOf(CreatePage)).apply(this, arguments));

        _this._client = client;
        _this._model = new IdeaBasicInfoViewModel({
            actions: {
                save: function save(idea) {
                    return _this._client.createIdea(idea).then(function (_ref) {
                        var id = _ref.id;

                        navigationHelper.toIdeaImages(id);
                    });
                }
            }
        });
        return _this;
    }

    createClass(CreatePage, [{
        key: 'onReady',
        value: function onReady() {
            knockout.applyBindings(this._model, document.getElementById('create-idea-form'));
            return Promise.resolve(true);
        }
    }]);
    return CreatePage;
}(BasePage);

var client = new ApiClient();

var page = new CreatePage(client);
page.init();

}());
//# sourceMappingURL=create.bundle.js.map
