﻿$(function () { $(".collapse-link").click(function () { var a = $(this).closest("div.ibox"), b = $(this).find("i"); a.find("div.ibox-content").slideToggle(200); b.toggleClass("fa-chevron-up").toggleClass("fa-chevron-down"); a.toggleClass("").toggleClass("border-bottom"); setTimeout(function () { a.resize(); a.find("[id^\x3dmap-]").resize() }, 50) }); $(".close-link").click(function () { $(this).closest("div.ibox").parent().remove() }); window.onload = function () { Loading(!0) }; $(".ui-filter-text").click(function () { $(this).next(".ui-filter-list").is(":hidden") ? ($(".ui-filter-list").css({ display: "block" }), $(this).addClass("active")) : ($(this).css("border-bottom-color", "#ccc"), $(".ui-filter-list").css({ display: "none" }), $(this).removeClass("active")) }); $(".profile-nav li").click(function () { $(".profile-nav li").removeClass("active"); $(".profile-nav li").removeClass("hover"); $(this).addClass("active") }).hover(function () { $(this).hasClass("active") || $(this).addClass("hover") }, function () { $(this).removeClass("hover") }) }); Loading = function (a, b) { var c = top.$("#loading_background"); a ? c.show() : (c.hide(), top.$(".ajax-loader").remove()); b ? c.find("p").text(b) : c.find("p").text("\u6b63\u5728\u52a0\u8f7d\u6570\u636e\u2026") }; tabiframeId = function () { top.$(".mainContent").find(".on") || top.$(".mainContent").find("iframe[style\x3d'display: inline;']"); return void 0 == top.$(".mainContent").find(".on").attr("id") ? top.$(".mainContent").find("iframe[style\x3d'display: inline;']").attr("id") : top.$(".mainContent").find(".on").attr("id") }; $.fn.ComboBox = function (a) { function b(b, e, h) { if (0 < b.length) { var f = $("\x3cul\x3e\x3c/ul\x3e"); a.description && f.append('\x3cli data-value\x3d""\x3e' + a.description + "\x3c/li\x3e"); $.each(b, function (c) { c = b[c]; var d = c[a.title]; void 0 == d && (d = ""); void 0 != e ? -1 != c[h.text].indexOf(e) && f.append('\x3cli data-value\x3d"' + c[a.id] + '" title\x3d"' + d + '"\x3e' + c[a.text] + "\x3c/li\x3e") : f.append('\x3cli data-value\x3d"' + c[a.id] + '" title\x3d"' + d + '"\x3e' + c[a.text] + "\x3c/li\x3e") }); d.find(".ui-select-option-content").html(f); d.find("li").css("padding", "0 5px"); d.find("li").click(function (a) { var b = $(this).text(), e = $(this).attr("data-value"); c.attr("data-value", e).attr("data-text", b); c.find(".ui-select-text").html(b).css("color", "#000"); d.slideUp(150); c.trigger("change"); a.stopPropagation() }).hover(function (a) { $(this).hasClass("liactive") || $(this).toggleClass("on"); a.stopPropagation() }) } } var c = $(this); if (!c.attr("id")) return !1; if (a && 0 == c.find(".ui-select-text").length) { var e; e = "" + ("\x3cdiv class\x3d\"ui-select-text\" style\x3d'color:#999;'\x3e" + a.description + "\x3c/div\x3e"); e = e + '\x3cdiv class\x3d"ui-select-option"\x3e' + ('\x3cdiv class\x3d"ui-select-option-content" style\x3d"max-height: ' + a.height + '"\x3e' + c.html() + "\x3c/div\x3e"); a.allowSearch && (e += '\x3cdiv class\x3d"ui-select-option-search"\x3e\x3cinput type\x3d"text" class\x3d"form-control" placeholder\x3d"\u641c\u7d22\u5173\u952e\u5b57" /\x3e\x3cspan class\x3d"input-query" title\x3d"Search"\x3e\x3ci class\x3d"fa fa-search"\x3e\x3c/i\x3e\x3c/span\x3e\x3c/div\x3e'); e += "\x3c/div\x3e"; c.html(""); c.append(e) } e = $($("\x3cp\x3e").append(c.find(".ui-select-option").clone()).html()); e.attr("id", c.attr("id") + "-option"); c.find(".ui-select-option").remove(); 0 < e.length && $("body").find("#" + c.attr("id") + "-option").remove(); $("body").prepend(e); var d = $("#" + c.attr("id") + "-option"); void 0 != a.url ? (d.find(".ui-select-option-content").html(""), $.ajax({ url: a.url, data: a.param, type: "GET", dataType: "json", async: !1, success: function (c) { a.data = c; b(c) }, error: function (a, b, c) { dialogMsg(c, -1) } })) : void 0 != a.data ? b(a.data) : (d.find("li").css("padding", "0 5px"), d.find("li").click(function (a) { var b = $(this).text(), e = $(this).attr("data-value"); c.attr("data-value", e).attr("data-text", b); c.find(".ui-select-text").html(b).css("color", "#000"); d.slideUp(150); c.trigger("change"); a.stopPropagation() }).hover(function (a) { $(this).hasClass("liactive") || $(this).toggleClass("on"); a.stopPropagation() })); a.allowSearch && (d.find(".ui-select-option-search").find("input").bind("keypress", function (a) { "13" == event.keyCode && (a = $(this).val(), b($(this)[0].options.data, a, $(this)[0].options)) }).focus(function () { $(this).select() })[0].options = a); c.unbind("click"); c.bind("click", function (b) { if ("readonly" == c.attr("readonly") || "disabled" == c.attr("disabled")) return !1; $(this).addClass("ui-select-focus"); if (d.is(":hidden")) { c.find(".ui-select-option").hide(); $(".ui-select-option").hide(); var e = c.offset().left, h = c.offset().top + 29, f = c.width(); a.width && (f = a.width); d.height() + h < $(window).height() ? d.slideDown(150).css({ top: h, left: e, width: f }) : (h = h - d.height() - 32, d.show().css({ top: h, left: e, width: f }), d.attr("data-show", !0)); d.css("border-top", "1px solid #ccc"); d.find("li").removeClass("liactive"); d.find("[data-value\x3d" + c.attr("data-value") + "]").addClass("liactive"); d.find(".ui-select-option-search").find("input").select() } else d.attr("data-show") ? d.hide() : d.slideUp(150); b.stopPropagation() }); $(document).click(function (a) { a = a ? a : window.event; $(a.srcElement || a.target).hasClass("form-control") || (d.attr("data-show") ? d.hide() : d.slideUp(150), c.removeClass("ui-select-focus"), a.stopPropagation()) }); return c }; $.fn.ComboBoxSetValue = function (a) { if (!$.isNullOrEmpty(a)) { var b = $(this), c = $("#" + b.attr("id") + "-option"); b.attr("data-value", a); var e = c.find("ul").find("[data-value\x3d" + a + "]").html(); e && (b.attr("data-text", e), b.find(".ui-select-text").html(e).css("color", "#000"), c.find("ul").find("[data-value\x3d" + a + "]").addClass("liactive")); return b } }; $.fn.ComboBoxTree = function (a) { function b(b) { f.treeview({ onnodeclick: function (b) { c.attr("data-value", b.id).attr("data-text", b.text); c.find(".ui-select-text").html(b.text).css("color", "#000"); c.trigger("change"); a.click && a.click(b) }, height: a.height, url: b, param: a.param, method: a.method, description: a.description }) } var c = $(this); if (!c.attr("id")) return !1; if (0 == c.find(".ui-select-text").length) { var e; e = "" + ("\x3cdiv class\x3d\"ui-select-text\"  style\x3d'color:#999;'\x3e" + a.description + "\x3c/div\x3e"); e = e + '\x3cdiv class\x3d"ui-select-option"\x3e' + ('\x3cdiv class\x3d"ui-select-option-content" style\x3d"max-height: ' + a.height + '"\x3e\x3c/div\x3e'); a.allowSearch && (e += '\x3cdiv class\x3d"ui-select-option-search"\x3e\x3cinput type\x3d"text" class\x3d"form-control" placeholder\x3d"\u641c\u7d22\u5173\u952e\u5b57" /\x3e\x3cspan class\x3d"input-query" title\x3d"Search"\x3e\x3ci class\x3d"fa fa-search" title\x3d"\u6309\u56de\u8f66\u67e5\u8be2"\x3e\x3c/i\x3e\x3c/span\x3e\x3c/div\x3e'); c.append(e + "\x3c/div\x3e") } e = $($("\x3cp\x3e").append(c.find(".ui-select-option").clone()).html()); e.attr("id", c.attr("id") + "-option"); c.find(".ui-select-option").remove(); a.appendTo ? $(a.appendTo).prepend(e) : $("body").prepend(e); var d = $("#" + c.attr("id") + "-option"), f = $("#" + c.attr("id") + "-option").find(".ui-select-option-content"); b(a.url); a.allowSearch && (d.find(".ui-select-option-search").find("input").attr("data-url", a.url), d.find(".ui-select-option-search").find("input").bind("keypress", function (a) { "13" == event.keyCode && (a = $(this).val(), a = changeUrlParam(d.find(".ui-select-option-search").find("input").attr("data-url"), "keyword", escape(a)), b(a)) }).focus(function () { $(this).select() })); a.icon && (d.find("i").remove(), d.find("img").remove()); c.find(".ui-select-text").unbind("click"); c.find(".ui-select-text").bind("click", function (b) { if ("readonly" == c.attr("readonly") || "disabled" == c.attr("disabled")) return !1; $(this).parent().addClass("ui-select-focus"); if (d.is(":hidden")) { c.find(".ui-select-option").hide(); $(".ui-select-option").hide(); var e = c.offset().left, f = c.offset().top + 29, g = c.width(); a.width && (g = a.width); d.height() + f < $(window).height() ? d.slideDown(150).css({ top: f, left: e, width: g }) : (f = f - d.height() - 32, d.show().css({ top: f, left: e, width: g }), d.attr("data-show", !0)); d.css("border-top", "1px solid #ccc"); a.appendTo && d.css("position", "inherit"); d.find(".ui-select-option-search").find("input").select() } else d.attr("data-show") ? d.hide() : d.slideUp(150); b.stopPropagation() }); c.find("li div").click(function (a) { a = a ? a : window.event; $(a.srcElement || a.target).hasClass("bbit-tree-ec-icon") || (d.slideUp(150), a.stopPropagation()) }); $(document).click(function (a) { a = a ? a : window.event; var b = a.srcElement || a.target; $(b).hasClass("bbit-tree-ec-icon") || $(b).hasClass("form-control") || (d.attr("data-show") ? d.hide() : d.slideUp(150), c.removeClass("ui-select-focus"), a.stopPropagation()) }); return c }; $.fn.ComboBoxTreeSetValue = function (a) { if ("" != a) { var b = $(this), c = $("#" + b.attr("id") + "-option"); b.attr("data-value", a); var e = c.find("ul").find("[data-value\x3d" + a + "]").html(); e && (b.attr("data-text", e), b.find(".ui-select-text").html(e).css("color", "#000"), c.find("ul").find("[data-value\x3d" + a + "]").parent().parent().addClass("bbit-tree-selected")); return b } }; $.fn.GetWebControls = function (a) { var b = ""; $(this).find("input[type\x3d'text'],input[type\x3d'hidden'],input[type\x3d'checkbox'],select,textarea,.ui-select").each(function (a) { if (a = $(this).attr("id")) switch ($(this).attr("type")) { case "checkbox": b = $("#" + a).is(":checked") ? b + ('"' + a + '":"1",') : b + ('"' + a + '":"0",'); break; case "select": var c = $("#" + a).attr("data-value"); "" == c && (c = ""); b += '"' + a + '":"' + $.trim(c) + '",'; break; case "selectTree": c = $("#" + a).attr("data-value"); "" == c && (c = ""); b += '"' + a + '":"' + $.trim(c) + '",'; break; default: var c = $("#" + a).attr("class"), d = void 0 == c ? !1 : 0 <= c.indexOf("input-wdatepicker"), c = $("#" + a).val(); "" != c || d || (c = ""); b += '"' + a + '":"' + $.trim(c) + '",' } }); b = b.substr(0, b.length - 1); a || (b = b.replace(/&nbsp;/g, "")); b = b.replace(/\\/g, "\\\\"); b = b.replace(/\n/g, "\\n"); return jQuery.parseJSON("{" + b + "}") }; $.fn.SetWebControls = function (a) { var b = $(this), c; for (c in a) { var e = b.find("#" + c); if (e.attr("id")) { var d = e.attr("type"); e.hasClass("input-datepicker") && (d = "datepicker"); var f = $.trim(a[c]).replace(/&nbsp;/g, ""); switch (d) { case "checkbox": 1 == f ? e.attr("checked", "checked") : e.removeAttr("checked"); break; case "select": e.ComboBoxSetValue(f); break; case "selectTree": e.ComboBoxTreeSetValue(f); break; case "datepicker": e.val(formatDate(f, "yyyy-MM-dd")); break; default: e.val(f) } } } }; $.fn.Contextmenu = function () { var a = $(this), b = $(".contextmenu"); $(document).click(function () { b.hide() }); $(document).mousedown(function (a) { 3 == a.which && b.hide() }); b.find("ul"); var c = b.find("li"), e = hideTimer = null, d = 0, f = maxHeight = 0, g = [document.documentElement.offsetWidth, document.documentElement.offsetHeight]; b.hide(); for (d = 0; d < c.length; d++) c[d].getElementsByTagName("ul")[0] && (c[d].className = "sub"), c[d].onmouseover = function () { var a = this, b = a.getElementsByTagName("ul"); a.className += " active"; b[0] && (clearTimeout(hideTimer), e = setTimeout(function () { for (d = 0; d < a.parentNode.children.length; d++) a.parentNode.children[d].getElementsByTagName("ul")[0] && (a.parentNode.children[d].getElementsByTagName("ul")[0].style.display = "none"); b[0].style.display = "block"; b[0].style.top = a.offsetTop + "px"; b[0].style.left = a.offsetWidth + "px"; f = g[0] - b[0].offsetWidth; maxHeight = g[1] - b[0].offsetHeight; f < getOffset.left(b[0]) && (b[0].style.left = -b[0].clientWidth + "px"); maxHeight < getOffset.top(b[0]) && (b[0].style.top = -b[0].clientHeight + a.offsetTop + a.clientHeight + "px") }, 300)) }, c[d].onmouseout = function () { var a = this; a.getElementsByTagName("ul"); a.className = a.className.replace(/\s?active/, ""); clearTimeout(e); hideTimer = setTimeout(function () { for (d = 0; d < a.parentNode.children.length; d++) a.parentNode.children[d].getElementsByTagName("ul")[0] && (a.parentNode.children[d].getElementsByTagName("ul")[0].style.display = "none") }, 300) }; $(a).bind("contextmenu", function () { var a = a || window.event; b.show(); b.css("top", a.clientY + "px"); b.css("left", a.clientX + "px"); f = g[0] - b.width(); maxHeight = g[1] - b.height(); b.offset().top > maxHeight && b.css("top", maxHeight + "px"); b.offset().left > f && b.css("left", f + "px"); return !1 }).bind("click", function () { b.hide() }) }; $.fn.panginationEx = function (a) { var b = $(this); if (!b.attr("id")) return !1; a = $.extend({ firstBtnText: "\u9996\u9875", lastBtnText: "\u5c3e\u9875", prevBtnText: "\u4e0a\u4e00\u9875", nextBtnText: "\u4e0b\u4e00\u9875", showInfo: !0, showJump: !0, jumpBtnText: "\u8df3\u8f6c", showPageSizes: !0, infoFormat: "{start} ~ {end}\u6761\uff0c\u5171{total}\u6761", sortname: "", url: "", success: null, beforeSend: null, complete: null }, a); var c = $.extend({ sidx: a.sortname, sord: "asc" }, a.params); a.remote = { url: a.url, params: c, beforeSend: function (b) { null != a.beforeSend && a.beforeSend(b) }, success: function (b, c) { null != a.success && a.success(b.rows, c) }, complete: function (b, c) { null != a.complete && a.complete(b, c) }, pageIndexName: "page", pageSizeName: "rows", totalName: "records" }; b.page(a) }; $.fn.LeftListShowOfemail = function (a) { var b = $(this); if (!b.attr("id")) return !1; b.append('\x3cul  style\x3d"padding-top: 10px;"\x3e\x3c/ul\x3e'); a = $.extend({ id: "id", name: "text", img: "fa fa-file-o" }, a); b.height(a.height); $.ajax({ url: a.url, data: a.param, type: "GET", dataType: "json", async: !1, success: function (c) { $.each(c, function (c, d) { var e = $('\x3cli class\x3d"" data-value\x3d"' + d[a.id] + '"  data-text\x3d"' + d[a.name] + '" \x3e\x3ci class\x3d"' + a.img + '" style\x3d"vertical-align: middle; margin-top: -2px; margin-right: 8px; font-size: 14px; color: #666666; opacity: 0.9;"\x3e\x3c/i\x3e' + d[a.name] + "\x3c/li\x3e"); 0 == c && e.addClass("active"); b.find("ul").append(e) }); b.find("li").click(function () { var c = $(this).attr("data-value"), d = $(this).attr("data-text"); b.find("li").removeClass("active"); $(this).addClass("active"); a.onnodeclick({ id: c, name: d }) }) }, error: function (a, b, d) { dialogMsg(d, -1) } }) }; $.fn.authorizeButton = function () { var a = $(this); a.find("a.btn").attr("authorize", "no"); a.find("ul.dropdown-menu").find("li").attr("authorize", "no"); var b = tabiframeId().substr(12), c = top.authorizeButtonData[b]; void 0 != c && $.each(c, function (b) { a.find("#" + c[b].EnCode).attr("authorize", "yes") }); a.find("[authorize\x3dno]").remove() }; $.fn.authorizePanel = function () { var a = $(this); a.find("div.panel").attr("authorize", "no"); var b = tabiframeId().substr(12), c = top.authorizeButtonData[b]; void 0 != c && $.each(c, function (b) { a.find("#" + c[b].EnCode).attr("authorize", "yes") }); a.find("[authorize\x3dno]").remove() }; $.fn.authorizeColModel = function () { var a = $(this), b = a.jqGrid("getGridParam", "colModel"); $.each(b, function (c) { "rn" != b[c].name && a.hideCol(b[c].name) }); var c = tabiframeId().substr(12), e = top.authorizeColumnData[c]; void 0 != e && $.each(e, function (b) { a.showCol(e[b].EnCode) }) }; $.fn.jqGridEx = function (a) { var b = $(this), c; if (!b.attr("id")) return !1; var e = { url: "", datatype: "json", height: $(window).height() - 139.5, autowidth: !0, colModel: [], viewrecords: !0, rowNum: 30, rowList: [30, 50, 100], pager: "#gridPager", sortname: "CreateDate desc", rownumbers: !0, shrinkToFit: !1, gridview: !0, onSelectRow: function () { c = $("#" + this.id).getGridParam("selrow") }, gridComplete: function () { $("#" + this.id).setSelection(c, !1) } }; a = $.extend(e, a); b.jqGrid(a) }; $.fn.jqGridRowValue = function (a) { var b = $(this), c = [], e = b.jqGrid("getGridParam", "selarrrow"); if (void 0 != e && "" != e) for (var d = e.length, f = 0; f < d; f++) { var g = b.jqGrid("getRowData", e[f]); c.push(g[a]) } else g = b.jqGrid("getRowData", b.jqGrid("getGridParam", "selrow")), c.push(g[a]); return String(c) }; $.fn.jqGridRow = function () { var a = $(this), b = [], c = a.jqGrid("getGridParam", "selarrrow"); if ("" != c) for (var e = c.length, d = 0; d < e; d++) { var f = a.jqGrid("getRowData", c[d]); b.push(f) } else f = a.jqGrid("getRowData", a.jqGrid("getGridParam", "selrow")), b.push(f); return b }; dialogTop = function (a, b) { $(".tip_container").remove(); var c = parseInt(1E5 * Math.random()); $("body").prepend('\x3cdiv id\x3d"tip_container' + c + '" class\x3d"container tip_container"\x3e\x3cdiv id\x3d"tip' + c + '" class\x3d"mtip"\x3e\x3ci class\x3d"micon"\x3e\x3c/i\x3e\x3cspan id\x3d"tsc' + c + '"\x3e\x3c/span\x3e\x3ci id\x3d"mclose' + c + '" class\x3d"mclose"\x3e\x3c/i\x3e\x3c/div\x3e\x3c/div\x3e'); $(this); var e = $("#tip_container" + c), d = $("#tip" + c), f = $("#tsc" + c); clearTimeout(window.timer); d.attr("class", b).addClass("mtip"); f.html(a); e.slideDown(300); window.timer = setTimeout(function () { e.slideUp(300); $(".tip_container").remove() }, 4E3); $("#tip_container" + c).css("left", ($(window).width() - $("#tip_container" + c).width()) / 2) }; dialogOpen = function (a) { Loading(!0); a = $.extend({ id: null, title: "\u7cfb\u7edf\u7a97\u53e3", width: "100px", height: "100px", url: "", shade: .3, btn: ["\u786e\u8ba4", "\u5173\u95ed"], callBack: null }, a); var b = a.url, c = top.$.windowWidth() > parseInt(a.width.replace("px", "")) ? a.width : top.$.windowWidth() + "px", e = top.$.windowHeight() > parseInt(a.height.replace("px", "")) ? a.height : top.$.windowHeight() + "px"; top.layer.open({ id: a.id, type: 2, shade: a.shade, title: a.title, fix: !1, area: [c, e], content: top.contentPath + b, btn: a.btn, yes: function () { a.callBack(a.id) }, cancel: function () { void 0 != a.cancel && a.cancel(); return !0 } }) }; dialogContent = function (a) { a = $.extend({ id: null, title: "\u7cfb\u7edf\u7a97\u53e3", width: "100px", height: "100px", content: "", btn: ["\u786e\u8ba4", "\u5173\u95ed"], callBack: null }, a); top.layer.open({ id: a.id, type: 1, title: a.title, fix: !1, area: [a.width, a.height], content: a.content, btn: a.btn, yes: function () { a.callBack(a.id) } }) }; dialogAlert = function (a, b) { -1 == b && (b = 2); top.layer.alert(a, { icon: b, title: "\u63d0\u793a" }) }; dialogConfirm = function (a, b) { top.layer.confirm(a, { shift: 0, icon: 7, title: "\u63d0\u793a", btn: ["\u786e\u8ba4", "\u53d6\u6d88"] }, function () { b(!0) }, function () { b(!1) }) }; dialogMsg = function (a, b) { -1 == b && (b = 2); top.layer.msg(a, { icon: b, time: 4E3, shift: 5 }) }; dialogClose = function () { try { var a = top.layer.getFrameIndex(window.name), b = top.$("#layui-layer" + a).find(".layui-layer-btn").find("#IsdialogClose"), c = b.is(":checked"); 0 == b.length && (c = !0); c ? top.layer.close(a) : location.reload() } catch (e) { alert(e) } }; reload = function () { location.reload(); return !1 }; newGuid = function () { for (var a = "", b = 1; 32 >= b; b++) { var c = Math.floor(16 * Math.random()).toString(16), a = a + c; if (8 == b || 12 == b || 16 == b || 20 == b) a += "-" } return a }; formatDate = function (a, b) { if (!a) return ""; var c = a; "string" === typeof a && (c = -1 < a.indexOf("/Date(") ? new Date(parseInt(a.replace("/Date(", "").replace(")/", ""), 10)) : new Date(Date.parse(a.replace(/-/g, "/").replace("T", " ").split(".")[0]))); var e = { "M+": c.getMonth() + 1, "d+": c.getDate(), "h+": c.getHours(), "m+": c.getMinutes(), "s+": c.getSeconds(), "q+": Math.floor((c.getMonth() + 3) / 3), S: c.getMilliseconds() }; /(y+)/.test(b) && (b = b.replace(RegExp.$1, (c.getFullYear() + "").substr(4 - RegExp.$1.length))); for (var d in e) (new RegExp("(" + d + ")")).test(b) && (b = b.replace(RegExp.$1, 1 == RegExp.$1.length ? e[d] : ("00" + e[d]).substr(("" + e[d]).length))); return b }; toDecimal = function (a) { null == a && (a = "0"); a = a.toString().replace(/\$|\,/g, ""); isNaN(a) && (a = "0"); sign = a == (a = Math.abs(a)); a = Math.floor(100 * a + .50000000001); cents = a % 100; a = Math.floor(a / 100).toString(); 10 > cents && (cents = "0" + cents); for (var b = 0; b < Math.floor((a.length - (1 + b)) / 3) ; b++) a = a.substring(0, a.length - (4 * b + 3)) + "" + a.substring(a.length - (4 * b + 3)); return (sign ? "" : "-") + a + "." + cents }; Date.prototype.DateAdd = function (a, b) { switch (a) { case "s": return new Date(Date.parse(this) + 1E3 * b); case "n": return new Date(Date.parse(this) + 6E4 * b); case "h": return new Date(Date.parse(this) + 36E5 * b); case "d": return new Date(Date.parse(this) + 864E5 * b); case "w": return new Date(Date.parse(this) + 6048E5 * b); case "q": return new Date(this.getFullYear(), this.getMonth() + 3 * b, this.getDate(), this.getHours(), this.getMinutes(), this.getSeconds()); case "m": return new Date(this.getFullYear(), this.getMonth() + b, this.getDate(), this.getHours(), this.getMinutes(), this.getSeconds()); case "y": return new Date(this.getFullYear() + b, this.getMonth(), this.getDate(), this.getHours(), this.getMinutes(), this.getSeconds()) } }; request = function (a) { for (var b = location.search.slice(1).split("\x26"), c = 0; c < b.length; c++) { var e = b[c].split("\x3d"); if (e[0] == a) if ("undefined" == unescape(e[1])) break; else return unescape(e[1]) } return "" }; changeUrlParam = function (a, b, c) { var e = new RegExp("(^|)" + b + "\x3d([^\x26]*)(|$)"); b = b + "\x3d" + c; return null != a.match(e) ? a.replace(eval(e), b) : a.match("[?]") ? a + "\x26" + b : a + "?" + b }; $.currentIframe = function () { return "Chrome" == $.isbrowsername() || "FF" == $.isbrowsername() ? null == top.frames[tabiframeId()].contentWindow ? top.frames[tabiframeId()] : top.frames[tabiframeId()].contentWindow : top.frames[tabiframeId()] }; $.isbrowsername = function () { var a = navigator.userAgent, b = -1 < a.indexOf("Opera"); if (b) return "Opera"; if (-1 < a.indexOf("Firefox")) return "FF"; if (-1 < a.indexOf("Chrome")) return -1 < window.navigator.webkitPersistentStorage.toString().indexOf("DeprecatedStorageQuota") ? "Chrome" : "360"; if (-1 < a.indexOf("Safari")) return "Safari"; if (-1 < a.indexOf("compatible") && -1 < a.indexOf("MSIE") && !b) return "IE" }; $.download = function (a, b, c) { if (a && b) { b = "string" == typeof b ? b : jQuery.param(b); var e = ""; $.each(b.split("\x26"), function () { var a = this.split("\x3d"); e += '\x3cinput type\x3d"hidden" name\x3d"' + a[0] + '" value\x3d"' + a[1] + '" /\x3e' }); $('\x3cform action\x3d"' + a + '" method\x3d"' + (c || "post") + '"\x3e' + e + "\x3c/form\x3e").appendTo("body").submit().remove() } }; $.standTabchange = function (a, b) { $(".standtabactived").removeClass("standtabactived"); $(a).addClass("standtabactived"); $(".standtab-pane").css("display", "none"); $("#" + b).css("display", "block") }; $.isNullOrEmpty = function (a) { return "string" == typeof a && "" == a || null == a || void 0 == a ? !0 : !1 }; $.arrayClone = function (a) { return $.map(a, function (a) { return $.extend(!0, {}, a) }) }; $.windowWidth = function () { return $(window).width() }; $.windowHeight = function () { return $(window).height() }; IsNumber = function (a) { $("#" + a).bind("contextmenu", function () { return !1 }); $("#" + a).css("ime-mode", "disabled"); $("#" + a).keypress(function (a) { if (8 != a.which && 0 != a.which && (48 > a.which || 57 < a.which)) return !1 }) }; IsMoney = function (a) { $("#" + a).bind("contextmenu", function () { return !1 }); $("#" + a).css("ime-mode", "disabled"); $("#" + a).bind("keydown", function (a) { var b = window.event ? a.keyCode : a.which; return 190 == b || 110 == b ? 0 > $(this).val().indexOf(".") : 8 == b || 46 == b || 37 <= b && 40 >= b || 35 == b || 36 == b || 9 == b || 13 == b || 48 <= b && 57 >= b && !a.shiftKey }) }; checkedArray = function (a) { var b = !0; if (void 0 == a || "" == a || "null" == a || "undefined" == a) b = !1, dialogMsg("\u60a8\u6ca1\u6709\u9009\u4e2d\u4efb\u4f55\u9879,\u8bf7\u60a8\u9009\u4e2d\u540e\u518d\u64cd\u4f5c\u3002", 0); return b }; checkedRow = function (a) { var b = !0; void 0 == a || "" == a || "null" == a || "undefined" == a ? (b = !1, dialogMsg("\u60a8\u6ca1\u6709\u9009\u4e2d\u4efb\u4f55\u6570\u636e\u9879,\u8bf7\u9009\u4e2d\u540e\u518d\u64cd\u4f5c\uff01", 0)) : 1 < a.split(",").length && (b = !1, dialogMsg("\u5f88\u62b1\u6b49,\u4e00\u6b21\u53ea\u80fd\u9009\u62e9\u4e00\u6761\u8bb0\u5f55\uff01", 0)); return b }; cutStrLength = function (a, b, c) { return a.length > b ? a.substring(0, b) + c : a }; encodeHtml = function (a) { return a && 0 != a.length ? a.replace(/&/g, "\x26amp;").replace(/>/g, "\x26gt;").replace(/</g, "\x26lt;").replace(/'/, "\x26quot;") : "" }; decodeHtml = function (a) { return a && 0 != a.length ? a.replace(/&lt;/g, "\x3c").replace(/&gt;/g, "\x3e").replace(/&amp;/g, "\x26").replace(/&quot;/g, "'") : "" }; String.prototype.Trim = function (a) { try { return a.replace(/^\s+|\s+$/g, "") } catch (b) { return s } };