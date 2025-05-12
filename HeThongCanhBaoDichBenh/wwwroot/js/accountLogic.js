document.getElementById('registerForm').addEventListener('submit', function (e) {
    e.preventDefault();

    const username = document.getElementById('regUsername').value.trim();
    const password = document.getElementById('regPassword').value;
    const confirmPassword = document.getElementById('regConfirmPassword').value;

    if (password !== confirmPassword) {
        alert("Mật khẩu không khớp.");
        return;
    }

    fetch('/Account/Register', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ username: username, password: password })
    })
        .then(response => response.json())
        .then(data => {
            alert(data.message);
            if (data.success) {
                document.getElementById('btn-login').click();

                setTimeout(() => {
                    const loginUsername = document.getElementById('login-username');
                    const loginPassword = document.getElementById('login-password');

                    loginUsername.value = username;

                    loginUsername.setCustomValidity("");
                    loginUsername.checkValidity();

                    loginPassword.focus();
                }, 100);
            }
        })
        .catch(error => {
            console.error('Lỗi:', error);
            alert("Có lỗi xảy ra!");
        });
});

document.getElementById('loginForm').addEventListener('submit', function (e) {
    e.preventDefault();

    const username = document.getElementById('login-username').value;
    const password = document.getElementById('login-password').value;

    fetch('/Account/Login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ username, password })
    })
        .then(response => response.json())
        .then(data => {
            alert(data.message);
            if (data.success) {
                window.location.href = data.redirectUrl;
            }
        })
        .catch(error => {
            console.error('Lỗi:', error);
            alert("Có lỗi xảy ra!");
        });
});

