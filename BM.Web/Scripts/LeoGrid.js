/// <reference path="jquery-1.4.4-vsdoc.js" />
/// <reference path="jquery.styling.js" /> this script is used in the grid.Busy() interface
/// last modified: 08-08-2011

var _isInTestModel = false;

var SortDirection = {
    Ascending: "asc",
    Descending: "desc"
};

var DataType = {
    String: "string",
    Number: "number",
    Date: "date"
}

var GridInfoFormats = {
    ItemFrom: "{itemFrom}",
    ItemTo: "{itemTo}",
    Page: "{page}",
    TotalItems: "{totalItems}",
    TotalPages: "{totalPages}"
}

var GridRowSelectMode = {
    Single: "Single",
    Mutiple: "Mutiple"
}

var AjaxOptions = function (url, data) {
    this.Method = "get";
    this.Url = url;
    this.DataType = "json";
    this.ContentType = "application/json";
    this.Data = data;
    alert(url);
}

var Column = function () {
    this.Width = 80;
    this.Sortable = true;
    this.Sorted = false;
    this.SortedDirection = SortDirection.Ascending
    this.MemberName = "";
    this.MemberDataType = DataType.String;
    this.HeaderHtml = "";
    this.HeaderClass = "";
    this.CellClass = "";
    this.CellTemplate = "";

    this.OnSorting = null;
    this.OnSorted = null;
    this.CellFormater = null;
}

var GridRow = function () {
    this.RowElement = null;
    this.RowData = null;
}

var EventArgs = function (data) {
    this.Data = data;
}

var SortingEventArgs = function (column, canceld) {
    this.Column = column;
    this.Canceld = canceld;
}

var PageIndexChangedEventArgs = function (oldIndex, newIndex) {
    this.OldIndex = oldIndex;
    this.NewIndex = newIndex;
}

var PageIndexChangingEventArgs = function (oldIndex, newIndex, canceled) {
    this.OldIndex = oldIndex;
    this.NewIndex = newIndex;
    this.Canceled = canceled;
}

var LeoGrid = function (gridId) {
    if (gridId == null || gridId == "") {
        alert("Please specify the div ID for the grid!");
    }

    this.Grid = $("#" + gridId);
    this.AjaxOptions = null;
    this.BodyHeight = 300;
    this.AutoLoad = true;
    this.GridRowSelectMode = GridRowSelectMode.Mutiple;

    this.Pageable = true;
    this.PageIndex = 0;
    this.PageSize = 15;
    this.PageButtonsCount = 10;
    this.InfoFormat = "Displaying items " + GridInfoFormats.ItemFrom + " - " + GridInfoFormats.ItemTo + " of " + GridInfoFormats.TotalItems;

    this.Columns = new Array();
    this.Rows = new Array();
    this.FilteredRows = new Array();
    this.ShowRefresher = true;
    this.EnableRowSelect = true;

    this.OnInitialized = null;
    this.OnRowDataBinding = null;
    this.OnRowDataBound = null;
    this.OnDataBinding = null;
    this.OnDataBound = null;
    this.OnRowSelected = null;
    this.OnRowUnselected = null;
    this.OnRowDoubleClick = null;
    this.OnPageIndexChanging = null;
    this.OnPageIndexChanged = null;

    this.OnLoadingData = null;
    this.OnLoadingDataComplete = null;
    this.OnLoadingDataError = null;
}

LeoGrid.prototype.GetInstance = function () {
    var grid = this;
    //grid = new LeoGrid(null);
    return grid;
}

LeoGrid.prototype.Initialize = function () {
    var grid = this.GetInstance();

    grid.Grid.empty();
    grid.Grid.addClass("leogrid");

    var header = GridFactory.CreateGridHeader(grid, grid.Columns);
    header.appendTo(grid.Grid);

    var body = GridFactory.CreateGridBody(grid.Columns, grid.BodyHeight);
    body.appendTo(grid.Grid);

    var footer = GridFactory.CreateGridFooter();
    footer.appendTo(grid.Grid);

    if (grid.ShowRefresher) {
        var refresher = grid.GetRefreshWrapper();
        GridFactory.CreateRefresher(refresher, function () {
            grid.Refresh();
        });
    }
    if (grid.OnInitialized != null) {
        grid.OnInitialized();
    }
    if (grid.AutoLoad) {
        grid.Refresh();
    }
}

LeoGrid.prototype.Refresh = function () {
    var grid = this.GetInstance();
    if (grid.AjaxOptions == null) {
        return;
    }
    ajaxOptions = grid.AjaxOptions;
    $.ajax({
        type: ajaxOptions.Method,
        url: ajaxOptions.Url,
        contentType: ajaxOptions.ContentType,
        data: ajaxOptions.Data,
        dataType: ajaxOptions.DataType,
        success: function (data) {
            if (grid.OnLoadingDataComplete != null) {
                data = grid.OnLoadingDataComplete(data);
            }
            grid.Busy(false);
            grid.BindData(data);
        },
        beforeSend: function (request) {
            if (grid.OnLoadingData != null) {
                var cancled = false;
                grid.OnLoadingData(cancled);
                if (cancled) {
                    request.abort();
                }
            }
            grid.Busy(true, request);
        },
        error: function (err) {
            if (grid.OnLoadingDataError != null) {
                grid.OnLoadingDataError(err);
            }
        }
    });
}

LeoGrid.prototype.RefreshGridInfo = function () {
    var grid = this.GetInstance();
    var infoWrapper = grid.GetInfoWrapper();
    GridHelper.BuildGridInfo(infoWrapper, grid.InfoFormat, grid.FilteredRows.length, grid.PageSize, grid.PageIndex);
}

LeoGrid.prototype.Rebind = function () {
    var grid = this.GetInstance();
    for (var i = 0; i < grid.Columns.length; i++) {
        var column = grid.Columns[i];
        if (column.Sortable && column.Sorted) {
            GridHelper.SortData(grid.FilteredRows, column.MemberName, column.MemberDataType, column.SortedDirection);
            break;
        }
    }
    var totalPage = GridHelper.GetTotalPages(grid.FilteredRows.length, grid.PageSize);
    if (grid.PageIndex + 1 > totalPage) {
        grid.PageIndex = totalPage - 1;
    }
    if (grid.PageIndex < 0) {
        grid.PageIndex = 0;
    }
    grid.PageTo(grid.PageIndex);
}

LeoGrid.prototype.BindData = function (rows) {
    var grid = this.GetInstance();
    grid.Rows = rows;
    grid.FilteredRows = new Array();
    for (var i = 0; i < rows.length; i++) {
        grid.FilteredRows.push(rows[i]);
    }

    grid.PageTo(grid.PageIndex);
}

LeoGrid.prototype.PageTo = function (index) {
    var grid = this.GetInstance();
    var canceld = false;
    if (grid.OnPageIndexChanging != null) {
        var args = new PageIndexChangingEventArgs(grid.PageIndex, index, canceld);
        grid.OnPageIndexChanging(grid, args);
        if (args.Canceled) {
            return false;
        }
    }
    if (grid.Pageable) {
        var pagerWrapper = grid.GetPagerWrapper();
        GridHelper.BuildPager(pagerWrapper, index, grid.PageButtonsCount, grid.FilteredRows.length, grid.PageSize, function (newIndex) {
            grid.PageTo(newIndex);
        });
    }
    else {
        grid.PageIndex = 0;
        grid.PageSize = grid.FilteredRows.length;
    }

    var oldIndex = grid.PageIndex;
    grid.PageIndex = index;
    var skip = index * grid.PageSize;
    var end = skip + grid.PageSize;
    end = end > grid.FilteredRows.length ? grid.FilteredRows.length : end;
    var tbody = grid.GetTableBody();
    tbody.empty();

    if (grid.OnDataBinding != null) {
        grid.OnDataBinding(grid, null);
    }
    if (end < skip + 1) {
        var tr = $("<tr><td colspan='" + grid.Columns.length + "'><div class='row-empty'>No items found.</div></td></tr>");
        tr.appendTo(tbody);
    }
    else {
        for (var i = skip; i < end; i++) {
            grid.AppendRow(grid.FilteredRows[i], i, tbody);
        }
    }
    if (grid.OnDataBound != null) {
        grid.OnDataBound(grid, null);
    }
    grid.RefreshGridInfo();
    if (grid.OnPageIndexChanged != null) {
        var args = new PageIndexChangedEventArgs(oldIndex, index);
        grid.OnPageIndexChanged(grid, args);
    }
    return true;
}

LeoGrid.prototype.AppendRow = function (row, i, tbody) {
    var grid = this.GetInstance();
    if (tbody == undefined || tbody == null) {
        tbody = grid.GetTableBody();
    }
    var evenOdd = i % 2 == 0 ? "even" : "odd";
    var tr = $("<tr id='row_" + i + "' class='row_" + evenOdd + "'></tr>");
    tr.appendTo(tbody);
    tr.data("dataItem", row);

    if (grid.OnRowDataBinding != null) {
        grid.OnRowDataBinding(tr, new EventArgs(row));
    }

    for (var j = 0; j < grid.Columns.length; j++) {
        var column = grid.Columns[j];
        var cell = $("<td></td>");
        if (j == grid.Columns.length - 1) {
            cell.addClass("cell-last");
        }
        cell.appendTo(tr);
        if (column.CellClass != "") {
            var wrapper = $("<div class='" + column.CellClass + "'></div>");
            wrapper.appendTo(cell);
            cell = wrapper;
        }
        var value = row[column.MemberName];
        if (column.CellFormater != null) {
            value = column.CellFormater(value);
        }
        value = value + "";
        if (column.CellTemplate != "") {
            var reg = new RegExp("##" + column.MemberName + "##", "g");
            value = column.CellTemplate.replace(reg, value);
        }
        cell.html(value);
    }
    if (grid.OnRowDataBound != null) {
        grid.OnRowDataBound(tr, row);
    }
    tr.mouseover(function () {
        $(this).addClass("row-mouseover");
    });
    tr.mouseout(function () {
        $(this).removeClass("row-mouseover");
    });
    if (grid.EnableRowSelect) {
        tr.click(function () {
            var currentRow = $(this);
            if (grid.GridRowSelectMode == GridRowSelectMode.Single) {
                if (!currentRow.hasClass("row-selected")) {
                    var tbody = grid.GetTableBody();
                    tbody.find(".row-selected").removeClass("row-selected");
                    currentRow.addClass("row-selected");
                    if (grid.OnRowSelected != null) {
                        var rowIndex = this.id.replace(/row_/, "") * 1;
                        grid.OnRowSelected(currentRow, grid.FilteredRows[rowIndex]);
                    }
                }
            }
            else {
                if (currentRow.hasClass("row-selected")) {
                    currentRow.removeClass("row-selected");
                    if (grid.OnRowUnselected != null) {
                        var rowIndex = this.id.replace(/row_/, "") * 1;
                        grid.OnRowUnselected(currentRow, grid.FilteredRows[rowIndex]);
                    }
                }
                else {
                    currentRow.addClass("row-selected");
                    if (grid.OnRowSelected != null) {
                        var rowIndex = this.id.replace(/row_/, "") * 1;
                        grid.OnRowSelected(currentRow, grid.FilteredRows[rowIndex]);
                    }
                } 
            }

        });
    }
    if (grid.OnRowDoubleClick != null) {
        tr.dblclick(function () {
            var currentRow = $(this);
            var rowIndex = this.id.replace(/row_/, "") * 1;
            var rowData = grid.FilteredRows[rowIndex];
            grid.OnRowDoubleClick(currentRow, rowData);
        });
    }
}

LeoGrid.prototype.Sort = function (column) {
    var grid = this.GetInstance();
    if (column.OnSorting != null) {
        var args = new SortingEventArgs(column, false);
        column.OnSorting(grid, args);
        if (args.Canceld) {
            return;
        }
    }
    var td;
    grid.Grid.find(".grid-header th").each(function () {
        if ($(this).data("columnData") == column) {
            td = $(this);
        }
    });
    var gridHeader = grid.GetHeader();
    gridHeader.find(".sortable").removeClass("sorted-asc");
    gridHeader.find(".sortable").removeClass("sorted-desc");

    var sortWrapper = td.find(".sortable");
    if (column.Sorted) {
        if (column.SortedDirection == SortDirection.Descending) {
            column.SortedDirection = SortDirection.Ascending;
            sortWrapper.removeClass("sorted-desc");
            sortWrapper.addClass("sorted-asc");
        }
        else {
            column.SortedDirection = SortDirection.Descending;
            sortWrapper.removeClass("sorted-asc");
            sortWrapper.addClass("sorted-desc");
        }
        grid.FilteredRows.reverse();
    }
    else {
        column.SortedDirection = SortDirection.Ascending;
        sortWrapper.removeClass("sorted-desc");
        sortWrapper.addClass("sorted-asc");
        GridHelper.SortData(grid.FilteredRows, column.MemberName, column.MemberDataType, column.SortedDirection);
    }
    for (var i = 0; i < grid.Columns.length; i++) {
        grid.Columns[i].Sorted = false;
    }
    column.Sorted = true;
    grid.PageTo(grid.PageIndex);

    if (column.OnSorted != null) {
        column.OnSorted(grid, column);
    }
}

LeoGrid.prototype.GetSelectedRows = function () {
    var grid = this.GetInstance();
    var rows = new Array();
    grid.GetTableBody().find(".row-selected").each(function () {
        var rowIndex = this.id.replace(/row_/, "") * 1;
        var row = new GridRow();
        row.RowElement = $(this);
        row.RowData = grid.FilteredRows[rowIndex];
        rows.push(row);
    });
    return rows;
}

LeoGrid.prototype.GetHeader = function () {
    return this.Grid.find(".grid-header");
}

LeoGrid.prototype.GetTableBody = function () {
    return this.Grid.find(".grid-body tbody");
}

LeoGrid.prototype.GetPagerWrapper = function () {
    return this.Grid.find(".grid-pager");
}

LeoGrid.prototype.GetInfoWrapper = function () {
    return this.Grid.find(".grid-info");
}

LeoGrid.prototype.GetRefreshWrapper = function () {
    return this.Grid.find(".grid-refresher");
}

//interfaces

LeoGrid.prototype.Busy = function (isBusy, request) {
    //$.busy(isBusy, "Loading...", request);
}


LeoGrid.prototype.FormatDate = function (date) {
    if (date.constructor != Date) {
        return date;
    }
    var year = date.getYear() + 1900;
    var month = date.getMonth() + 1;
    var day = date.getDate();
    var hour = date.getHours();
    var minute = date.getMinutes();

    return month + "/" + day + "/" + year + " " + hour + ":" + minute;
}

/**************************************************************************************************/
/*++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/
/**************************************************************************************************/


GridFactory = function () {

}

GridFactory.CreateSimpleTable = function (columns) {
    var table = $("<table></table>");
    var group = $("<colgroup></colgroup>");
    group.appendTo(table);
    for (var i = 0; i < columns.length; i++) {
        var col = $("<col>");
        if (columns[i].Width != "auto") {
            col.css("width", columns[i].Width + "px");
        }
        col.appendTo(group);
    }
    var tbody = $("<tbody></tbody>");
    tbody.appendTo(table);
    return table;
}

GridFactory.CreateGridHeader = function (grid, columns) {
    var header = $("<div class='grid-header'></div>");
    var headWrapper = $("<div class='grid-header-wrapper'></div>");
    headWrapper.appendTo(header);

    var headerTable = GridFactory.CreateSimpleTable(columns);
    headerTable.appendTo(headWrapper);
    var headerTableBody = headerTable.find("tbody");
    var tr = $("<tr></tr>");
    tr.appendTo(headerTableBody);

    for (var i = 0; i < columns.length; i++) {
        var column = columns[i];
        var th = $("<th id='column_" + i + "'></th>");
        th.data("columnData", column);
        if (column.Sortable) {
            th.click(function () {
                var columnIndex = this.id.replace(/column_/, "") * 1;
                grid.Sort(columns[columnIndex]);
            });
        }
        var wrapper = th;
        if (column.HeaderClass != "") {
            var userClassWrapper = $("<div class='" + column.HeaderClass + "'></div>");
            userClassWrapper.appendTo(wrapper);
            wrapper = userClassWrapper;
        }
        var headerHtml = column.HeaderHtml;
        headerHtml = headerHtml == "" ? column.MemberName : headerHtml;
        wrapper.html(headerHtml);

        if (column.Sortable) {
            var sortWrapper = $("<span class='sortable'></span>");
            sortWrapper.appendTo(wrapper);
        }
        th.appendTo(tr);
    }
    return header;
}

GridFactory.CreateGridBody = function (columns, height) {
    var body = $("<div class='grid-body'></div>");
    body.css("height", height + "px");
    var bodyTable = GridFactory.CreateSimpleTable(columns);
    bodyTable.appendTo(body);
    return body;
}

GridFactory.CreateGridFooter = function () {
    var footer = $("<div class='grid-footer'></div>");
    var refresher = $("<div class='grid-refresher'></div>");
    refresher.appendTo(footer);

    var pagerWrapper = $("<div class='grid-pager'></div>");
    pagerWrapper.appendTo(footer);

    var info = $("<div class='grid-info'></div>");
    info.appendTo(footer);

    var clear = $("<div class='clear'></div>");
    clear.appendTo(footer);

    return footer;
}

GridFactory.CreateRefresher = function (wrapper, refreshCallback) {
    var button = $("<a class='link-refresh'>Refresh</a>");
    button.appendTo(wrapper);
    button.click(function () {
        refreshCallback();
    });
}

/**************************************************************************************************/
/*++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/
/**************************************************************************************************/

GridHelper = function () {

}

GridHelper.SortData = function (rows, memberName, dataType, direction) {
    for (var i = 0; i < rows.length - 1; i++) {
        for (var j = 0; j < rows.length - 1 - i; j++) {
            var current = rows[j][memberName];
            var next = rows[j + 1][memberName]
            var isCurrentLarger = GridHelper.IsValue1Larger(current, next, dataType);
            if (direction == SortDirection.Ascending) {
                if (isCurrentLarger) {
                    var temp = rows[j];
                    rows[j] = rows[j + 1];
                    rows[j + 1] = temp;
                }
            }
            else {
                if (!isCurrentLarger) {
                    var temp = rows[j + 1];
                    rows[j + 1] = rows[j];
                    rows[j] = temp;
                }
            }
        }
    }
}

GridHelper.ParseDateFromString = function (dateString) {
    if (dateString == null || dateString.toLowerCase().indexOf("/date(") == -1) {
        return null;
    }
    dateString = dateString.substring(6, dateString.length - 2);
    var date = new Date(dateString * 1);
    return date;
}

GridHelper.IsValue1Larger = function (value1, value2, dataType) {
    var result = false;
    switch (dataType) {
        case DataType.Number:
            return value1 * 1 > value2 * 1;
        case DataType.Date:
            if (value1.constructor == Date) {
                if (value2.constructor == Date) {
                    return (value1 - value2) > 0;
                }
                else {
                    return true;
                }
            }
            else {
                if (value2.constructor == Date) {
                    return false;
                }
                else {
                    return value1 > value2;
                }
            }
        default:
            return value1 > value2;
    }
}

GridHelper.BuildPager = function (wrapper, pageIndex, buttonsCount, rowsCount, pageSize, pageClicked) {
    wrapper.empty();

    var totalPage = GridHelper.GetTotalPages(rowsCount, pageSize);
    totalPage = totalPage <= 0 ? 1 : totalPage;

    var firstLink = null;
    var previousLink = null;
    var nextLink = null;
    var lastLink = null;


    if (pageIndex <= 0) {
        firstLink = $("<span class='pager-first'>&nbsp;</span>");
        previousLink = $("<span class='pager-previous'>&nbsp;</span>");
    }
    else {
        firstLink = $("<a class='pager-first'>&nbsp;</a>");
        previousLink = $("<a class='pager-previous'>&nbsp;</a>");
        firstLink.click(function () {
            var paged = pageClicked(0);
            if (paged) {
                GridHelper.BuildPager(wrapper, 0, buttonsCount, rowsCount, pageSize, pageClicked);
            }
        });
        previousLink.click(function () {
            var current = wrapper.find(".pager-current");
            if (current.length == 0) {
                return;
            }
            current = current.html() * 1;
            var paged = pageClicked(current - 2);
            if (paged) {
                GridHelper.BuildPager(wrapper, current - 2, buttonsCount, rowsCount, pageSize, pageClicked);
            }
        });
    }

    if (pageIndex >= totalPage - 1) {
        nextLink = $("<span class='pager-next'>&nbsp;</span>");
        lastLink = $("<span class='pager-last'>&nbsp;</span>");
    }
    else {
        nextLink = $("<a class='pager-next'>&nbsp;</a>");
        lastLink = $("<a class='pager-last'>&nbsp;</a>");
        lastLink.click(function () {
            var paged = pageClicked(totalPage - 1);
            if (paged) {
                GridHelper.BuildPager(wrapper, totalPage - 1, buttonsCount, rowsCount, pageSize, pageClicked);
            }
        });
        nextLink.click(function () {
            var current = wrapper.find(".pager-current");
            if (current.length == 0) {
                return;
            }
            current = current.html() * 1;
            var paged = pageClicked(current);
            if (paged) {
                GridHelper.BuildPager(wrapper, current, buttonsCount, rowsCount, pageSize, pageClicked);
            }
        });
    }

    firstLink.appendTo(wrapper);
    previousLink.appendTo(wrapper);

    var skipedPagers = 0;
    if ((pageIndex + 1) % buttonsCount == 0) {
        skipedPagers = Math.floor((pageIndex + 1) / buttonsCount) - 1;
    }
    else {
        skipedPagers = Math.floor((pageIndex + 1) / buttonsCount);
    }
    var hasPreviousPages = skipedPagers > 0;
    var hasNextPages = (skipedPagers + 1) * buttonsCount < totalPage;
    if (hasPreviousPages) {
        var link = $("<a class='pager-number'>...</a>");
        link.appendTo(wrapper);
        link.click(function () {
            var targetIndex = skipedPagers * buttonsCount - 1;
            var paged = pageClicked(targetIndex);
            if (paged) {
                GridHelper.BuildPager(wrapper, targetIndex, buttonsCount, rowsCount, pageSize, pageClicked);
            }
        });
    }
    var startPage = skipedPagers * buttonsCount + 1;
    var endPage = (skipedPagers + 1) * buttonsCount;
    if (endPage > totalPage) {
        endPage = totalPage;
    }
    for (var i = startPage; i <= endPage; i++) {
        if (pageIndex == i - 1) {
            var link = $("<span class='pager-current'>" + i + "</span>");
            link.appendTo(wrapper);
        }
        else {
            var link = $("<a class='pager-number'>" + i + "</a>");
            link.appendTo(wrapper);
            link.click(function () {
                var targetIndex = $(this).html() * 1 - 1;
                var paged = pageClicked(targetIndex);
                if (paged) {
                    GridHelper.BuildPager(wrapper, targetIndex, buttonsCount, rowsCount, pageSize, pageClicked);
                }
            });
        }
    }

    if (hasNextPages) {
        var link = $("<a class='pager-number'>...</a>");
        link.appendTo(wrapper);
        link.click(function () {
            var targetIndex = (skipedPagers + 1) * buttonsCount;
            var paged = pageClicked(targetIndex);
            if (paged) {
                GridHelper.BuildPager(wrapper, targetIndex, buttonsCount, rowsCount, pageSize, pageClicked);
            }
        });
    }

    nextLink.appendTo(wrapper);
    lastLink.appendTo(wrapper);
}

GridHelper.GetTotalPages = function (rowsCount, pageSize) {
    var totalPage = 0;
    if (rowsCount % pageSize == 0) {
        totalPage = rowsCount / pageSize;
    }
    else {
        totalPage = Math.floor(rowsCount / pageSize) + 1;
    }
    return totalPage;
}

GridHelper.BuildGridInfo = function (wrapper, format, rowsCount, pageSize, pageIndex) {
    wrapper.empty();

    var totalPage = GridHelper.GetTotalPages(rowsCount, pageSize);
    totalPage = totalPage <= 0 ? 1 : totalPage;

    var itemFrom = pageIndex * pageSize + 1;
    itemTo = itemFrom + pageSize - 1;
    if (itemTo > rowsCount) {
        itemTo = rowsCount;
    }
    var page = pageIndex + 1;

    var regFrom = new RegExp("{itemFrom}", "g");
    var regTo = new RegExp("{itemTo}", "g");
    var regPage = new RegExp("{page}", "g");
    var regTotalItems = new RegExp("{totalItems}", "g");
    var regTotalPages = new RegExp("{totalPages}", "g");

    var result = format.replace(regFrom, itemFrom).replace(regTo, itemTo).replace(regPage, page);
    result = result.replace(regTotalItems, rowsCount).replace(regTotalPages, totalPage);

    wrapper.html(result);
}


