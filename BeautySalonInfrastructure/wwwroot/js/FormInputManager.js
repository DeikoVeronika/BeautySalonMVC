window.onload = function () {
    var selectElements = document.getElementsByTagName('select');
    for (var i = 0; i < selectElements.length; i++) {
        selectElements[i].selectedIndex = -1;
    }
};
