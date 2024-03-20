//document.addEventListener('DOMContentLoaded', function () {
//    const form = document.getElementById('reservationForm');
//    const firstName = document.getElementById('Client_FirstName');
//    const lastName = document.getElementById('Client_LastName');
//    const phone = document.getElementById('Client_Phone');
//    const email = document.getElementById('Client_Email');
//    const defaultPrefix = '+380';

//    if (!phone.value.startsWith(defaultPrefix)) {
//        phone.value = defaultPrefix;
//    }

//    firstName.addEventListener('input', function () {
//        this.value = this.value.replace(/[^А-ЯЄІЇҐЎа-яєіїґў']+/g, '');
//    });

//    lastName.addEventListener('input', function () {
//        this.value = this.value.replace(/[^А-ЯЄІЇҐЎа-яєіїґў']+/g, '');
//    });

//    phone.addEventListener('input', function () {
//        if (this.value.length < defaultPrefix.length) {
//            this.value = defaultPrefix;
//        } else if (this.value.length > 13) { // +38 and 9 digits for Ukrainian numbers
//            this.value = this.value.slice(0, 13);
//        }
//    });

//    phone.addEventListener('keypress', function (e) {
//        if (!/\d/.test(e.key)) {
//            e.preventDefault();
//        }
//    });

//    // Завантаження даних з бд якщо користувач вводить пошту яка вже існує

//    ////не стираються дані
//    //function handleEmailInput() {
//    //    const emailValue = email.value.trim();

//    //    if (emailValue.length === 0) {
//    //        resetFields(true);
//    //    } else if (isValidEmail(emailValue)) {
//    //        getClientData(emailValue);
//    //    }
//    //}

//    //function getClientData(emailValue) {
//    //    $.ajax({
//    //        url: '/Reservations/GetClientData',
//    //        type: 'GET',
//    //        data: { email: emailValue },
//    //        success: function (data) {
//    //            if (data) {
//    //                setFields(data);
//    //            } else {
//    //                resetFields();
//    //            }
//    //        },
//    //        error: function (jqXHR, textStatus, errorThrown) {
//    //            console.error('Error:', errorThrown);
//    //        }
//    //    });
//    //}

//    ////не стираються дані

//    //function handleEmailInput() {
//    //    const emailValue = email.value.trim();

//    //    if (emailValue.length === 0) {
//    //        resetFields(true);
//    //    } else if (isValidEmail(emailValue)) {
//    //        if (emailValue !== lastValidEmail) {
//    //            getClientData(emailValue, function (data) {
//    //                if (!data) {
//    //                    // Не скидати поля
//    //                    // resetFields(); // Закоментуйте цей рядок
//    //                }
//    //            });
//    //        }

//    //        lastValidEmail = emailValue;
//    //    }
//    //}

//    //function getClientData(emailValue, callback) {
//    //    $.ajax({
//    //        url: '/Reservations/GetClientData',
//    //        type: 'GET',
//    //        data: { email: emailValue },
//    //        success: function (data) {
//    //            if (data) {
//    //                setFields(data);
//    //                allowInvalidEmail = true;
//    //            } else {
//    //                // Не скидати поля
//    //                // allowInvalidEmail = true; // Закоментуйте цей рядок
//    //            }
//    //            callback(data);
//    //        },
//    //        error: function (jqXHR, textStatus, errorThrown) {
//    //            console.error('Error:', errorThrown);
//    //        },
//    //    });
//    //}



//    ////не стираються дані
//    //function handleEmailInput() {
//    //    const emailValue = email.value.trim();

//    //    if (emailValue.length === 0) {
//    //        resetFields(true);
//    //    } else if (isValidEmail(emailValue)) {
//    //        getClientData(emailValue, function (data) {
//    //            if (data) {
//    //                setFields(data);
//    //            } else if (emailValue !== lastValidEmail) {
//    //                // Скинути поля, коли адреса електронної пошти змінена
//    //                resetFields();
//    //            }
//    //            lastValidEmail = emailValue;
//    //        });
//    //    }
//    //}

//    //function getClientData(emailValue, callback) {
//    //    $.ajax({
//    //        url: '/Reservations/GetClientData',
//    //        type: 'GET',
//    //        data: { email: emailValue },
//    //        success: function (data) {
//    //            if (data) {
//    //                allowInvalidEmail = true;
//    //            }
//    //            callback(data);
//    //        },
//    //        error: function (jqXHR, textStatus, errorThrown) {
//    //            console.error('Error:', errorThrown);
//    //        },
//    //    });
//    //}



//    //скидаються всі дані. не скидаються дані якщо введено існуючу пошту з клавіатури
 

//    //function getClientData(emailValue) {
//    //    $.ajax({
//    //        url: '/Reservations/GetClientData',
//    //        type: 'GET',
//    //        data: { email: emailValue },
//    //        success: function (data) {
//    //            if (data) {
//    //                setFields(data);
//    //                email.dataset.previousValue = emailValue;
//    //            } else {
//    //                resetFields();
//    //            }
//    //        },
//    //        error: function (jqXHR, textStatus, errorThrown) {
//    //            console.error('Error:', errorThrown);
//    //        }
//    //    });
//    //}


//    //function resetFields(keepData = false) {
//    //    firstName.removeAttribute('readonly');
//    //    lastName.removeAttribute('readonly');
//    //    phone.removeAttribute('readonly');

//    //    if (!keepData) {
//    //        if (!firstName.hasAttribute('readonly')) {
//    //            firstName.value = '';
//    //        }
//    //        if (!lastName.hasAttribute('readonly')) {
//    //            lastName.value = '';
//    //        }
//    //        if (!phone.hasAttribute('readonly')) {
//    //            phone.value = defaultPrefix;
//    //        }
//    //    }
//    //}



//    //let lastValidPhone = '';

//    //function handlePhoneInput(phoneValue) {
//    //    // Перевіряємо, чи є введене значення телефону в базі даних
//    //    $.ajax({
//    //        url: '/Reservations/GetClientData',
//    //        type: 'GET',
//    //        data: { phone: phoneValue },
//    //        success: function (data) {
//    //            if (data) {
//    //                // Якщо телефон існує, ми встановлюємо поля з даними клієнта
//    //                setFields(data);
//    //                phone.dataset.previousValue = phoneValue;
//    //                lastValidPhone = phoneValue;
//    //            } else {
//    //                // Якщо телефон не існує, ми не скидаємо поля
//    //                resetFields(true);
//    //            }
//    //        },
//    //        error: function (jqXHR, textStatus, errorThrown) {
//    //            console.error('Error:', errorThrown);
//    //        }
//    //    });
//    //}

//    //phone.addEventListener('input', function () {
//    //    let phoneValue = this.value;
//    //    if (phoneValue === defaultPrefix) {
//    //        // Якщо користувач видалив телефон, ми скидаємо поля тільки якщо телефон був в базі даних
//    //        if (lastValidPhone === phone.dataset.previousValue) {
//    //            resetFields();
//    //        }
//    //    } else if (phoneValue !== phone.dataset.previousValue) {
//    //        // Якщо користувач ввів новий телефон, ми обробляємо його
//    //        handlePhoneInput(phoneValue);
//    //    }
//    //});

//    //phone.addEventListener('input', clearErrorMessages);

//    //function resetFields(keepData = false) {
//    //    firstName.removeAttribute('readonly');
//    //    lastName.removeAttribute('readonly');
//    //    email.removeAttribute('readonly');

//    //    if (!keepData) {
//    //        if (!firstName.hasAttribute('readonly')) {
//    //            firstName.value = '';
//    //        }
//    //        if (!lastName.hasAttribute('readonly')) {
//    //            lastName.value = '';
//    //        }
//    //        if (!email.hasAttribute('readonly')) {
//    //            email.value = '';
//    //        }
//    //    }
//    //}



//    //function setFields(data) {
//    //    firstName.setAttribute('readonly', true);
//    //    lastName.setAttribute('readonly', true);
//    //    email.setAttribute('readonly', true);

//    //    firstName.value = data.firstName;
//    //    lastName.value = data.lastName;
//    //    email.value = data.email;
//    //}




//    ////function resetFields() {
//    ////    firstName.removeAttribute('readonly');
//    ////    lastName.removeAttribute('readonly');
//    ////    phone.removeAttribute('readonly');

//    ////    firstName.value = '';
//    ////    lastName.value = '';
//    ////    phone.value = defaultPrefix;
//    ////}

//    //function handleFieldClick(field) {
//    //    fields.forEach(f => {
//    //        if (f.nextSibling && f.nextSibling.tagName === 'SPAN') {
//    //            f.nextSibling.textContent = '';
//    //        }
//    //    });

//    //    if (field.hasAttribute('readonly')) {
//    //        const errorSpan = field.nextSibling;
//    //        errorSpan.textContent = message;
//    //    }
//    //}

//    //function clearErrorMessages() {
//    //    fields.forEach(field => {
//    //        const errorSpan = field.nextSibling;
//    //        errorSpan.textContent = '';
//    //    });
//    //}

//    //const fields = [firstName, lastName, phone];
//    //const message = "Ви не можете змінювати ці дані оскільки використовуєте пошту яка вже зареєстрована.";

//    //fields.forEach(field => {
//    //    const errorSpan = document.createElement('span');
//    //    errorSpan.style.color = 'red';
//    //    field.parentNode.insertBefore(errorSpan, field.nextSibling);

//    //    field.addEventListener('click', function () {
//    //        handleFieldClick(field);
//    //    });
//    //});

//    //email.addEventListener('input', handleEmailInput);
//    //email.addEventListener('input', clearErrorMessages);




//    form.addEventListener('submit', function (e) {
//        if (!validateInputs()) {
//            e.preventDefault();
//        }
//    });

//    const setError = (element, message) => {
//        const inputGroup = element.closest('.form-group');
//        const errorDisplay = inputGroup.querySelector('.text-danger');

//        errorDisplay.innerText = message;
//        inputGroup.classList.add('error');
//        inputGroup.classList.remove('success');
//    };

//    const setSuccess = (element) => {
//        const inputGroup = element.closest('.form-group');
//        const errorDisplay = inputGroup.querySelector('.text-danger');

//        errorDisplay.innerText = '';
//        inputGroup.classList.add('success');
//        inputGroup.classList.remove('error');
//    };


//    const isValidEmail = (email) => {
//        const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
//        return re.test(String(email).toLowerCase());
//    };

//    const isAlpha = (text) => {
//        const re = /^[А-ЯЄІЇҐЎа-яєіїґў']+$/;
//        return re.test(String(text));
//    };


//    const validateInputs = () => {
//        const firstNameValue = firstName.value.trim();
//        const lastNameValue = lastName.value.trim();
//        const phoneValue = phone.value.trim();
//        const emailValue = email.value.trim();

//        let isValid = true;

//        if (firstNameValue === '') {
//            setError(firstName);
//            isValid = false;
//        } else if (firstNameValue.length < 2) {
//            setError(firstName);
//            isValid = false;
//        } else {
//            setSuccess(firstName);
//        }

//        if (lastNameValue === '') {
//            setError(lastName);
//            isValid = false;
//        } else if (lastNameValue.length < 2) {
//            setError(lastName);
//            isValid = false;
//        } else {
//            setSuccess(lastName);
//        }

//        if (phoneValue === '' || phoneValue === defaultPrefix) {
//            setError(phone);
//            isValid = false;
//        } else if (phoneValue.length < 13) {
//            setError(phone);
//            isValid = false;
//        } else {
//            setSuccess(phone);
//        }

//        if (emailValue === '') {
//            setError(email);
//            isValid = false;
//        } else if (!isValidEmail(emailValue)) {
//            setError(email);
//            isValid = false;
//        } else {
//            setSuccess(email);
//        }

//        return isValid;
//    };


//});
