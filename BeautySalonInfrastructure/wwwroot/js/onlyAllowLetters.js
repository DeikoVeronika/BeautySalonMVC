function onlyAllowLetters(e) {
    var charCode = e.which ? e.which : e.keyCode;
    if ((charCode >= 65 && charCode <= 90) ||  
        (charCode >= 97 && charCode <= 122) || 
        (charCode >= 1040 && charCode <= 1103) || 
        charCode == 8 || 
        charCode == 32 || 
        charCode == 1110 ||
        charCode == 1030 ||
        charCode == 1111 ||
        charCode == 1031) { 
        return true;
    } else {
        return false;
    }
}
