window.onload = function () {
    const selectElements = document.getElementsByTagName('select');

    for (let i = 0; i < selectElements.length; i++) {
        selectElements[i].selectedIndex = -1;
    }
};
