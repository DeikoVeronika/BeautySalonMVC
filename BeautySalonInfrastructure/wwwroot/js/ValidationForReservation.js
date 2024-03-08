document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('reservationForm');

    const firstName = document.getElementById('Client_FirstName');
    const lastName = document.getElementById('Client_LastName');
    const phone = document.getElementById('Client_Phone');
    const email = document.getElementById('Client_Email');

    form.addEventListener('submit', function (e) {
        if (!validateInputs()) {
            e.preventDefault();
        }
    });

    const setError = (element, message) => {
        const inputGroup = element.closest('.form-group');
        const errorDisplay = inputGroup.querySelector('.text-danger');

        errorDisplay.innerText = message;
        inputGroup.classList.add('error');
        inputGroup.classList.remove('success');
        element.value = '';
    };

    const setSuccess = (element) => {
        const inputGroup = element.closest('.form-group');
        const errorDisplay = inputGroup.querySelector('.text-danger');

        errorDisplay.innerText = '';
        inputGroup.classList.add('success');
        inputGroup.classList.remove('error');
    };

    const isValidEmail = (email) => {
        const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(String(email).toLowerCase());
    };

    const isAlpha = (text) => {
        const re = /^[А-ЯЄІЇҐЎа-яєіїґў']+$/;
        return re.test(String(text));
    };

    const validateInputs = () => {
        const firstNameValue = firstName.value.trim();
        const lastNameValue = lastName.value.trim();
        const phoneValue = phone.value.trim();
        const emailValue = email.value.trim();

        let isValid = true;

        if (firstNameValue === '') {
            setError(firstName, 'Ім\'я є обов\'язковим полем');
            isValid = false;
        } else if (!isAlpha(firstNameValue)) {
            setError(firstName, 'Введіть лише букви');
            isValid = false;
        } else {
            setSuccess(firstName);
        }

        if (lastNameValue === '') {
            setError(lastName, 'Прізвище є обов\'язковим полем');
            isValid = false;
        } else if (!isAlpha(lastNameValue)) {
            setError(lastName, 'Введіть лише букви');
            isValid = false;
        } else {
            setSuccess(lastName);
        }

        if (phoneValue === '') {
            setError(phone, 'Телефон є обов\'язковим полем');
            isValid = false;
        } else {
            setSuccess(phone);
        }

        if (emailValue === '') {
            setError(email, 'Email є обов\'язковим полем');
            isValid = false;
        } else if (!isValidEmail(emailValue)) {
            setError(email, 'Введіть дійсний email');
            isValid = false;
        } else {
            setSuccess(email);
        }

        return isValid;
    };
});
