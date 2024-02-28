function validateIntegerInput(input) {
    let value = input.value.trim();

    value = value.replace(/[^0-9]/g, '');

    value = value.substring(0, 5);

    input.value = value;
}