document.addEventListener('DOMContentLoaded', (event) => {
    const phoneInput = document.getElementById('phoneInput');
    const defaultPrefix = '+380';
    phoneInput.value = defaultPrefix;
    phoneInput.addEventListener('input', handleInput);

    function handleInput(e) {
        const { value } = e.target;
        if (!value.startsWith(defaultPrefix) || value.length < defaultPrefix.length) {
            e.target.value = defaultPrefix;
        } else if (value.length > 13) { // +38 and 9 digits for Ukrainian numbers
            e.target.value = value.slice(0, 13);
        }
    }
});
