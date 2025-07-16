document.addEventListener('DOMContentLoaded', function() {
    const modal = document.getElementById('employeeModal');
    const addEmployeeBtn = document.querySelector('.btn-add-employee');
    const closeModalBtn = document.getElementById('closeModal');

    if (addEmployeeBtn && modal) {
        addEmployeeBtn.addEventListener('click', () => {
            modal.classList.remove('hidden');
            document.querySelector('#employeeModal h3').textContent = 'Thêm nhân viên mới';
        });
    }

    if (closeModalBtn && modal) {
        closeModalBtn.addEventListener('click', () => {
            modal.classList.add('hidden');
        });
    }

    window.addEventListener('click', (event) => {
        if (event.target === modal) {
            modal.classList.add('hidden');
        }
    });

    // Edit buttons
    document.querySelectorAll('button.text-indigo-600').forEach(btn => {
        btn.addEventListener('click', () => {
            modal.classList.remove('hidden');
            document.querySelector('#employeeModal h3').textContent = 'Chỉnh sửa nhân viên';
            // In a real app, you would populate the form with employee data here
        });
    });

    // Delete buttons
    document.querySelectorAll('button.text-red-600').forEach(btn => {
        btn.addEventListener('click', (event) => {
            if (confirm('Bạn có chắc chắn muốn xóa nhân viên này?')) {
                const row = event.target.closest('tr');
                if (row) row.remove();
            }
        });
    });
});
