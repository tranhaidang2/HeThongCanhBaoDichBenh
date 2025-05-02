document.addEventListener("DOMContentLoaded", function () {
    const reUsername = document.getElementById("regUsername");
    const rePassword = document.getElementById("regPassword");
    const ConfirmPassword = document.getElementById("regConfirmPassword");

    reUsername.oninvalid = function () {
        if (this.validity.valueMissing) {
            this.setCustomValidity("Nhập tên đăng nhập");
        }
    };
    reUsername.oninput = function () {
        this.setCustomValidity("");
    };

    rePassword.oninvalid = function () {
        if (this.validity.valueMissing) {
            this.setCustomValidity("Nhập mật khẩu");
        }
    };
    rePassword.oninput = function () {
        this.setCustomValidity("");
    };

    ConfirmPassword.oninvalid = function () {
        if (this.validity.valueMissing) {
            this.setCustomValidity("Nhập lại mật khẩu");
        }
    };
    ConfirmPassword.oninput = function () {
        this.setCustomValidity("");
    };

    const username = document.getElementById("login-username");
    const password = document.getElementById("login-password");

    username.oninvalid = function () {
        if (this.validity.valueMissing) {
            this.setCustomValidity("Nhập tên đăng nhập");
        }
    };
    username.oninput = function () {
        this.setCustomValidity("");
    };

    password.oninvalid = function () {
        if (this.validity.valueMissing) {
            this.setCustomValidity("Nhập mật khẩu");
        }
    };
    password.oninput = function () {
        this.setCustomValidity("");
    };
});