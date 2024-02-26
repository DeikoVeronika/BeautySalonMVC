document.addEventListener('DOMContentLoaded', (event) => {
    const phoneInput = document.getElementById('phoneInput');
    const defaultPrefix = '+380';

    // Ensure the initial value starts with the default prefix
    if (!phoneInput.value.startsWith(defaultPrefix)) {
        phoneInput.value = defaultPrefix;
    }

    // Add input event listener for additional validation
    phoneInput.addEventListener('input', handleInput);

    // Add keypress event listener to cancel non-numeric input
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
