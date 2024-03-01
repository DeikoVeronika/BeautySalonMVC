window.onload = function () {
    var selectelements = document.getelementsbytagname('select');
    for (var i = 0; i < selectelements.length; i++) {
        if (selectelements[i].value == "") {
            selectelements[i].selectedindex = -1;
        }
    }
};