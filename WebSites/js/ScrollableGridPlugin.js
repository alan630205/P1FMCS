/*!
* This plug-in is developed for ASP.Net GridView control to make the GridView scrollable with Fixed headers.
*/
(function ($) {
    $.fn.Scrollable = function (options) {//1、定義一個jQuery實例方法，也是我們調用這個插件的入口
        var defaults = {
            ScrollHeight: 300,
            Width: 0
        };
        var options = $.extend(defaults, options); //2、擴展集合，如果外部沒有傳入ScrollHeight的值，就默認使用300這個值，如果傳入，就用傳入的ScrollHeight值
        return this.each(function () {
            var grid = $(this).get(0); //3、獲取當前gridview控件的對象
            var gridWidth = grid.offsetWidth; //4、獲取gridview的寬度
            var headerCellWidths = new Array();
            for (var i = 0; i < grid.getElementsByTagName("TH").length; i++) {
                headerCellWidths[i] = grid.getElementsByTagName("TH")[i].offsetWidth;
            } //5、創建了一個存儲表頭各個標題列的寬度的數組
            grid.parentNode.appendChild(document.createElement("div")); //6、在文檔中gridview的同級位置最後加一個div元素
            var parentDiv = grid.parentNode; //7、gridview的父節點，是個div

            var table = document.createElement("table"); //8、在dom中創建一個table元素
            for (i = 0; i < grid.attributes.length; i++) {
                if (grid.attributes[i].specified && grid.attributes[i].name != "id") {
                    table.setAttribute(grid.attributes[i].name, grid.attributes[i].value);
                }
            } //9、把gridview的所有屬性加到新創建的table元素上
            table.style.cssText = grid.style.cssText; //10、將樣式也加到table元素上
            table.style.width = gridWidth + "px"; //11、為table元素設置與gridview同樣的寬
            table.appendChild(document.createElement("tbody")); //12、為table元素加一個tbody元素
            table.getElementsByTagName("tbody")[0].appendChild(grid.getElementsByTagName("TR")[0]); //13、把gridview中的第一行數據加到tbody元素中，即table裏現在存著一行gridview的標題元素，同時在gridview裏刪除了標題這一行的元素
            var cells = table.getElementsByTagName("TH"); //14、取得表格標題列的集合

            var gridRow = grid.getElementsByTagName("TR")[0]; //15、gridview中第一行數據列的集合
            for (var i = 0; i < cells.length; i++) {
                var width;
                if (headerCellWidths[i] > gridRow.getElementsByTagName("TD")[i].offsetWidth) {//16、如果標題格的寬度大於數據列的寬度
                    width = headerCellWidths[i];
                }
                else {//17、如果標題格的寬度小於數據列的寬度
                    width = gridRow.getElementsByTagName("TD")[i].offsetWidth;
                }
                cells[i].style.width = parseInt(width - 3) + "px";
                gridRow.getElementsByTagName("TD")[i].style.width = parseInt(width - 3) + "px"; //18、將每個標題列和數據列的寬度均調整為取其中更寬的一個，-3是出於對表格的樣式進行微調考慮，不是必須
            }
            parentDiv.removeChild(grid); //19、刪除gridview控件（註意只是從文檔流中刪除，實際還在內存當中，註意27條），現在的情況是table裏只有一個表頭

            var dummyHeader = document.createElement("div"); //20、在文檔中再創建一個div元素
            dummyHeader.appendChild(table); //21、把表頭table加入其中
            parentDiv.appendChild(dummyHeader); //22、把這個div插入到原來gridview的位置裏
            if (options.Width > 0) {//23、如果我們最初傳入了一個想要的表格寬度值，就將gridWidth賦上這個值
                gridWidth = options.Width;
            }
            var scrollableDiv = document.createElement("div"); //24、再創建一個div
            gridWidth = parseInt(gridWidth) + 17; //25、在原基礎上+17是因為這是一個具有滑動條的table，當出現滑動條的時候，會在寬度上多出一個條的寬度，為了使數據列與標題列保持一致，要把這個寬度算進行，17這個值也不是必須，這個可以試驗著來。
            scrollableDiv.style.cssText = "overflow:auto;height:" + options.ScrollHeight + "px;width:" + gridWidth + "px"; //26、給具有滾動條的div加上樣式，height就是我們想讓它在多大的長度時出現滾動條
            scrollableDiv.appendChild(grid); //27、將gridview（目前只存在數據存在數據列）加到這個帶有滾動條的div中，這裏是從內存中將grid取出
            parentDiv.appendChild(scrollableDiv); //28、將帶有滾動條的div加到table的下面
        });
    };
})(jQuery);