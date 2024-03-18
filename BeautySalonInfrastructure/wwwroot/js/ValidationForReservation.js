document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('reservationForm');

    const firstName = document.getElementById('Client_FirstName');
    const lastName = document.getElementById('Client_LastName');
    const phone = document.getElementById('Client_Phone');
    const email = document.getElementById('Client_Email');

    const defaultPrefix = '+380';

    if (!phone.value.startsWith(defaultPrefix)) {
        phone.value = defaultPrefix;
    }

    firstName.addEventListener('input', function () {
        this.value = this.value.replace(/[^А-ЯЄІЇҐЎа-яєіїґў']+/g, '');
    });

    lastName.addEventListener('input', function () {
        this.value = this.value.replace(/[^А-ЯЄІЇҐЎа-яєіїґў']+/g, '');
    });

    phone.addEventListener('input', function () {
        if (this.value.length < defaultPrefix.length) {
            this.value = defaultPrefix;
        } else if (this.value.length > 13) { // +38 and 9 digits for Ukrainian numbers
            this.value = this.value.slice(0, 13);
        }
    });

    phone.addEventListener('keypress', function (e) {
        if (!/\d/.test(e.key)) {
            e.preventDefault();
        }
    });

    // Завантаження даних з бд якщо користувач вводить пошту яка вже існує
    email.addEventListener('input', function () {
        const emailValue = email.value.trim();

        if (isValidEmail(emailValue)) {
            $.ajax({
                url: '/Reservations/GetClientData',
                type: 'GET',
                data: { email: emailValue },
                success: function (data) {
                    if (data) {
                        firstName.setAttribute('readonly', true);
                        lastName.setAttribute('readonly', true);
                        phone.setAttribute('readonly', true);

                        firstName.value = data.firstName;
                        lastName.value = data.lastName;
                        phone.value = data.phone;
                    } else {
                        firstName.removeAttribute('readonly');
                        lastName.removeAttribute('readonly');
                        phone.removeAttribute('readonly');

                        firstName.value = '';
                        lastName.value = '';
                        phone.value = defaultPrefix;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.error('Error:', errorThrown);
                }
            });
        } else {
            firstName.removeAttribute('readonly');
            lastName.removeAttribute('readonly');
            phone.removeAttribute('readonly');

            firstName.value = '';
            lastName.value = '';
            phone.value = defaultPrefix;
        }
    });

    const fields = [firstName, lastName, phone];
    const message = "Ви не можете змінювати ці дані";


    fields.forEach(field => {
        const errorSpan = document.createElement('span');
        errorSpan.style.color = 'red';
        field.parentNode.insertBefore(errorSpan, field.nextSibling);

        field.addEventListener('click', function () {
            fields.forEach(f => {
                if (f.nextSibling && f.nextSibling.tagName === 'SPAN') {
                    f.nextSibling.textContent = '';
                }
            });


            if (field.hasAttribute('readonly')) {
                errorSpan.textContent = message;
            }
        });
    });

    // Очищення повідомлення про помилку при зміні пошти
    email.addEventListener('input', function () {
        fields.forEach(field => {
            const errorSpan = field.nextSibling;
            errorSpan.textContent = '';
        });
    });



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
        } else {
            setSuccess(firstName);
        }

        if (lastNameValue === '') {
            setError(lastName, 'Прізвище є обов\'язковим полем');
            isValid = false;
        } else {
            setSuccess(lastName);
        }

        if (phoneValue === '' || phoneValue === defaultPrefix) {
            setError(phone);
            isValid = false;
        } else if (phoneValue.length < 13) {
            setError(phone);
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
