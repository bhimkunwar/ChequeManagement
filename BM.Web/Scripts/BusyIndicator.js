/// <reference path="../jquery-1.4.4-vsdoc.js" />



(function ($) {
    $.fn.busy = function (options) {
        return $(this).each(function (i, elem) {
            var item = $(elem);
            options = BusyOptions.Fix(options);
            var timer = null;
            if (options.isBusy) {
                var indicator = item.find(".busy-indicator");
                if (indicator.length == 0) {
                    indicator = CreateIndicator(options);
                    item.prepend(indicator);
                    indicator.width(item.width() + "px");
                    indicator.height(item.height() + "px");
                    var wrapper = indicator.find(".busy-wrapper");
                    if (options.centerScreen) {
                        var contentHeight = wrapper.height();
                        var contentWidth = wrapper.width();
                        var screenHeight = $(window).height();
                        var screenWidth = $(window).width();
                        var top = (screenHeight - contentHeight) / 2;
                        var left = (screenWidth - contentWidth) / 2;
                        wrapper.css("left", left + "px");
                        wrapper.css("top", top + "px");
                    }
                    else {
                        var padding = (item.height() - indicator.find(".busy-wrapper-inner").height()) / 2;
                        wrapper.css("padding-top", padding + "px");
                    }
                }
                indicator.find(".busy-content").html(options.busyContent);
                indicator.hide();

                timer = setTimeout(function () {
                    indicator.show();
                }, options.delay);
            }
            else {
                item.find(".busy-indicator").remove();
                clearTimeout(timer);
            }
        });
    }
})(jQuery);

$.busy = function (options) {
    $("body").busy(options);
}

function CreateIndicator(options) {
    var container = $("<div class='busy-indicator'></div>");
    if (options.modal) {
        container.addClass("busy-modal");
    }
    var wrapper = $("<div class='busy-wrapper'></div>");
    container.append(wrapper);
    if (options.centerScreen) {
        wrapper.addClass("busy-screen");
    }
    else {
        var innerWrapper = $("<div class='busy-wrapper-inner'></div>");
        innerWrapper.appendTo(wrapper);
        wrapper = innerWrapper;
    }
    var contentWrapper = $("<div class='busy-content-wrapper'></div>");
    contentWrapper.appendTo(wrapper);
    if (options.showCancel) {
        var cancelWrapper = $("<div class='busy-cancel-wrapper'></div>");
        cancelWrapper.appendTo(contentWrapper);
        var cancelLink = $("<a href='javascript:void(0)' title='cancel'>X</a>");
        cancelLink.appendTo(cancelWrapper);
        cancelLink.click(function () {
            if (options.request != undefined && options.request != null) {
                options.request.abort();
            }
            container.remove();
        });
    }
    var innerContentWrapper = $("<div class='busy-innercontent-wrapper'></div>");
    innerContentWrapper.appendTo(contentWrapper);
    $("<span class='busy-img'></span><span class='busy-content'></span>").appendTo(innerContentWrapper);
    return container;
}


var BusyOptions = function (isbusy) {
    this.isBusy = isbusy;
    this.busyContent = "Loading...";
    this.request = null;
    this.showCancel = true;
    this.centerScreen = true;
    this.modal = true;
    this.delay = 0;
}

BusyOptions.Fix = function (obj) {
    if (obj.constructor == Boolean) {
        return new BusyOptions(obj);
    }
    var options = new BusyOptions(true);
    for (property in obj) {
        options[property] = obj[property];
    }
    return options;
}