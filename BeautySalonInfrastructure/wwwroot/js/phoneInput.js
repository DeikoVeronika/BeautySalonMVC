document.addEventListener('DOMContentLoaded', (event) => {
    const phoneInput = document.getElementById('phoneInput');
    const defaultPrefix = '+380';   

    if (!phoneInput.value.startsWith(defaultPrefix)) {
        phoneInput.value = defaultPrefix;
    }

    phoneInput.addEventListener('input', handleInput);

    phoneInput.addEventListener('keypress', (e) => {
        // Cancel input if it's not a digit
        if (!/\d/.test(e.key)) {
            e.preventDefault();
        }
    });

    function handleInput(e) {
        const { value } = e.target;
        if (!value.startsWith(defaultPrefix) || value.length < defaultPrefix.length) {
            e.target.value = defaultPrefix;
        } else if (value.length > 13) { // +38 and 9 digits for Ukrainian numbers
            e.target.value = value.slice(0, 13);
        }
    }
});
