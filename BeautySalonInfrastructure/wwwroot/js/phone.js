//$(document).ready(function () {
//    const phoneInput = document.getElementById('phoneInput');
//    const defaultPrefix = '+380';

//    if (!phoneInput.value.startsWith(defaultPrefix)) {
//        phoneInput.value = defaultPrefix;
//    }

//    $("#phoneInput").inputmask({
//        mask: "+380 (99) 999-99-99",
//        oncomplete: function () {
//            // do something when the user completes input
//        },
//        onincomplete: function () {
//            // do something when the user's input is incomplete
//        },
//        oncleared: function () {
//            // do something when the user clears the input field
//        }
//    });
//});

$(document).ready(function () {
    $("#phoneInput").inputmask("+380 (99) 999-99-99");
});

