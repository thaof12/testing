let realPassword = "123456";
let mainPasswordVisible = false;
let editPasswordVisible = false;

const maskedPassword = document.getElementById('masked-password');
const toggleMainPassword = document.getElementById('toggle-main-password');
toggleMainPassword.onclick = () => {
    mainPasswordVisible = !mainPasswordVisible;
    maskedPassword.textContent = mainPasswordVisible ? realPassword : '••••••';
    toggleMainPassword.className = mainPasswordVisible ? 'fas fa-eye-slash' : 'fas fa-eye';
};

const maskedPasswordEdit = document.getElementById('masked-password-edit');
const toggleEditPassword = document.getElementById('toggle-edit-password');
toggleEditPassword.onclick = () => {
    editPasswordVisible = !editPasswordVisible;
    maskedPasswordEdit.textContent = editPasswordVisible ? realPassword : '••••••';
    toggleEditPassword.className = editPasswordVisible ? 'fas fa-eye-slash' : 'fas fa-eye';
};

document.getElementById("logo").onclick =
    document.getElementById("home-link").onclick = () => {
        document.getElementById('edit-tab').style.display = 'none';
        document.getElementById('security-tab').style.display = 'none';
        document.getElementById('info-tab').style.display = 'block';
    };

function isValidEmail(email) {
    return /^[^\s@]+@gmail\.com$/.test(email);
}

function isValidPhone(phone) {
    return /^0\d{9}$/.test(phone);
}

function showEditTab() {
    document.getElementById('info-tab').style.display = 'none';
    document.getElementById('security-tab').style.display = 'none';
    document.getElementById('edit-tab').style.display = 'block';

    document.getElementById('edit-name').value = document.getElementById('name').textContent;
    document.getElementById('edit-phone').value = document.getElementById('phone').textContent;
    document.getElementById('edit-email').value = document.getElementById('email').textContent;
    document.getElementById('edit-username').value = document.getElementById('username').textContent;

    maskedPasswordEdit.textContent = '••••••';
    toggleEditPassword.className = 'fas fa-eye';
    editPasswordVisible = false;
}

function cancelEdit() {
    document.getElementById('edit-tab').style.display = 'none';
    document.getElementById('info-tab').style.display = 'block';
}

function saveEdit() {
    const name = document.getElementById('name').textContent.trim();
    const phone = document.getElementById('phone').textContent.trim();
    const email = document.getElementById('email').textContent.trim();
    const username = document.getElementById('username').textContent.trim();

    const newName = document.getElementById('edit-name').value.trim();
    const newPhone = document.getElementById('edit-phone').value.trim();
    const newEmail = document.getElementById('edit-email').value.trim();
    const newUsername = document.getElementById('edit-username').value.trim();

    const errors = [];
    if (!isValidPhone(newPhone)) errors.push("Số điện thoại không hỗ trợ");
    if (!isValidEmail(newEmail)) errors.push("Email không hợp lệ");
    if (newUsername.length > 25) errors.push("Tên tài khoản không được quá 25 ký tự");

    if (errors.length > 0) {
        alert(errors.join("\n"));
        return;
    }

    const isChanged = newName !== name || newPhone !== phone || newEmail !== email || newUsername !== username;

    document.getElementById('name').textContent = newName;
    document.getElementById('phone').textContent = newPhone;
    document.getElementById('email').textContent = newEmail;
    document.getElementById('username').textContent = newUsername;

    document.getElementById('edit-tab').style.display = 'none';
    document.getElementById('info-tab').style.display = 'block';

    alert(isChanged ? "Đã lưu thay đổi" : "Đã lưu");
}

function showSecurityTab() {
    document.getElementById('info-tab').style.display = 'none';
    document.getElementById('edit-tab').style.display = 'none';
    document.getElementById('security-tab').style.display = 'block';
}

function confirmPasswordChange() {
    const newPassword = document.getElementById('new-password').value.trim();
    const confirmPassword = document.getElementById('confirm-password').value.trim();

    if (newPassword.length <= 5) return alert("Mật khẩu phải trên 5 ký tự");
    if (newPassword !== confirmPassword) return alert("Mật khẩu mới không trùng khớp");

    realPassword = newPassword;
    alert("Đã đổi mật khẩu");
    document.getElementById('security-tab').style.display = 'none';
    document.getElementById('edit-tab').style.display = 'block';
}

function cancelPasswordChange() {
    if (confirm("Xác nhận hủy")) {
        document.getElementById('security-tab').style.display = 'none';
        document.getElementById('edit-tab').style.display = 'block';
    }
}

