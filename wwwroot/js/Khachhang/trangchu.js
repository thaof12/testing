document.addEventListener('DOMContentLoaded', function () {
    // Set min date to today
    const today = new Date().toISOString().split('T')[0];
    document.getElementById('booking-date').min = today;

    // --- Biến lưu trạng thái ---
    let selectedService = null;
    let selectedRoomType = null;

    // --- Hàm lưu và load lịch sử đặt chỗ vào localStorage ---
    function saveBookingHistory() {
        const rows = document.querySelectorAll('#current-bookings tbody tr');
        const data = [];
        rows.forEach(row => {
            const cells = row.querySelectorAll('td');
            data.push({
                id: cells[0].textContent,
                service: cells[1].textContent,
                datetime: cells[2].textContent,
                total: cells[3].textContent,
                status: cells[4].innerHTML
            });
        });
        localStorage.setItem('bookingHistory', JSON.stringify(data));
    }
    function loadBookingHistory() {
        const data = JSON.parse(localStorage.getItem('bookingHistory') || '[]');
        const tbody = document.querySelector('#current-bookings tbody');
        tbody.innerHTML = '';
        data.forEach(item => {
            const row = document.createElement('tr');
            row.innerHTML = `
                    <td>${item.id}</td>
                    <td>${item.service}</td>
                    <td>${item.datetime}</td>
                    <td>${item.total}</td>
                    <td>${item.status}</td>
                    <td>
                        <button class="btn btn-sm btn-outline-danger cancel-booking">Hủy</button>
                    </td>
                `;
            tbody.appendChild(row);
        });
    }
    // Load lịch sử khi trang load
    loadBookingHistory();

    // --- Ẩn danh sách phòng khi vào bước 2 ---
    function hideAvailableRooms() {
        document.getElementById('available-rooms').classList.add('d-none');
    }
    function showAvailableRooms() {
        document.getElementById('available-rooms').classList.remove('d-none');
    }

    // --- Khi chọn dịch vụ ---
    document.querySelectorAll('.select-service').forEach(card => {
        card.addEventListener('click', function () {
            selectedService = this.getAttribute('data-service');
            document.getElementById('booking-steps').classList.remove('d-none');
            hideAvailableRooms();
            document.getElementById('room-type').selectedIndex = 0;
            if (selectedService === 'karaoke') {
                document.querySelectorAll('.karaoke-only').forEach(el => el.classList.remove('d-none'));
            } else if (selectedService === 'bida') {
                document.querySelectorAll('.karaoke-only').forEach(el => el.classList.add('d-none'));
            } else {
                document.querySelectorAll('.karaoke-only').forEach(el => el.classList.remove('d-none'));
            }
            document.getElementById('booking-steps').scrollIntoView({ behavior: 'smooth' });
        });
    });

    // --- Khi chọn loại phòng thì mới hiển thị danh sách phòng ---
    document.getElementById('room-type').addEventListener('change', function () {
        selectedRoomType = this.value;
        document.querySelectorAll('#available-rooms .room-item').forEach(item => {
            const typeText = item.querySelector('.text-muted').textContent.toLowerCase();
            if (typeText.includes(selectedRoomType)) {
                item.classList.remove('d-none');
            } else {
                item.classList.add('d-none');
            }
            item.classList.remove('selected');
        });
        showAvailableRooms();
    });

    // --- Khi chuyển sang bước 2 thì ẩn danh sách phòng ---
    document.getElementById('next-to-step-2').addEventListener('click', function () {
        const selectedDate = document.getElementById('booking-date').value;
        const selectedSlots = document.querySelectorAll('.time-slot-selected');
        if (!selectedDate) {
            alert('Vui lòng chọn ngày đặt chỗ');
            return;
        }
        if (selectedSlots.length === 0) {
            alert('Vui lòng chọn ít nhất một khung giờ');
            return;
        }
        hideAvailableRooms();
        document.getElementById('room-type').selectedIndex = 0;
        document.querySelectorAll('#available-rooms .room-item').forEach(item => {
            item.classList.remove('selected');
        });
        navigateToStep(2);
    });

    // --- Khi chọn phòng ---
    document.addEventListener('click', function (e) {
        if (e.target.classList.contains('select-room-btn')) {
            document.querySelectorAll('.room-item').forEach(item => {
                item.classList.remove('selected');
            });
            e.target.closest('.room-item').classList.add('selected');
        }
        if (e.target.classList.contains('room-detail-btn')) {
            const roomName = e.target.closest('.room-item').querySelector('h5').textContent;
            document.getElementById('modal-room-name').textContent = roomName;
            const modal = new bootstrap.Modal(document.getElementById('roomDetailModal'));
            modal.show();
        }
    });
    // --- Tích hợp phần chọn phương thức thanh toán ---
    const paymentCardBody = document.querySelector('#step-4 .card-body');
    if (paymentCardBody) {
        paymentCardBody.innerHTML = `
                <div class="mb-3">
                    <label class="form-label">Chọn phương thức thanh toán</label>
                    <div>
                        <div class="form-check form-check-inline">
                            <input class="form-check-input" type="radio" name="payment-method" id="bank" value="bank" checked>
                            <label class="form-check-label" for="bank">Ngân hàng</label>
                        </div>
                        <div class="form-check form-check-inline">
                            <input class="form-check-input" type="radio" name="payment-method" id="momo" value="momo">
                            <label class="form-check-label" for="momo">Momo</label>
                        </div>
                        <div class="form-check form-check-inline">
                            <input class="form-check-input" type="radio" name="payment-method" id="cash" value="cash">
                            <label class="form-check-label" for="cash">Tiền mặt</label>
                        </div>
                    </div>
                </div>
                <div id="bank-info" class="payment-info">
                    <div class="alert alert-info mb-2">
                        <strong>Chuyển khoản ngân hàng:</strong><br>
                        Chủ tài khoản: CTY TNHH DICH VU GIAI TRI<br>
                        Số tài khoản: 123456789<br>
                        Ngân hàng: Vietcombank CN Tân Bình
                    </div>
                    <div class="text-center">
                        <img src="https://img.vietqr.io/image/VCB-123456789-compact2.png?amount=180000&addInfo=DatCocKaraokeBida" alt="QR Ngân hàng" style="max-width:180px;">
                        <div class="small text-muted mt-1">Quét mã QR để chuyển khoản nhanh</div>
                    </div>
                </div>
                <div id="momo-info" class="payment-info d-none">
                    <div class="alert alert-info mb-2">
                        <strong>Thanh toán qua Momo:</strong><br>
                        Số điện thoại: 0909123456<br>
                        Tên: CTY TNHH DICH VU GIAI TRI
                    </div>
                    <div class="text-center">
                        <img src="https://chart.googleapis.com/chart?chs=180x180&cht=qr&chl=2|99|0909123456|CTY%20TNHH%20DICH%20VU%20GIAI%20TRI|180000" alt="QR Momo" style="max-width:180px;">
                        <div class="small text-muted mt-1">Quét mã QR để thanh toán qua Momo</div>
                    </div>
                </div>
                <div id="cash-info" class="payment-info d-none">
                    <div class="alert alert-info">
                        <strong>Thanh toán tiền mặt tại quầy khi đến nhận phòng/bàn.</strong>
                    </div>
                </div>
            `;

        // Đảm bảo sự kiện luôn hoạt động sau khi innerHTML
        paymentCardBody.querySelectorAll('input[name="payment-method"]').forEach(radio => {
            radio.addEventListener('change', function () {
                paymentCardBody.querySelectorAll('.payment-info').forEach(info => {
                    info.classList.add('d-none');
                });
                paymentCardBody.querySelector('#' + this.value + '-info').classList.remove('d-none');
            });
        });
        // Hiển thị đúng mặc định khi load lại
        paymentCardBody.querySelector('#bank-info').classList.remove('d-none');
        paymentCardBody.querySelector('#momo-info').classList.add('d-none');
        paymentCardBody.querySelector('#cash-info').classList.add('d-none');
    }

    // --- Chọn phòng từ modal ---
    document.getElementById('select-room-from-modal').addEventListener('click', function () {
        const modal = bootstrap.Modal.getInstance(document.getElementById('roomDetailModal'));
        modal.hide();
        document.querySelectorAll('.room-item').forEach(item => {
            item.classList.remove('selected');
        });
        document.querySelector('.room-item:not(.d-none)').classList.add('selected');
    });

    // --- Các bước tiếp theo giữ nguyên ---
    document.getElementById('next-to-step-3').addEventListener('click', function () {
        const selectedRoom = document.querySelector('.room-item.selected');
        if (!selectedRoom) {
            alert('Vui lòng chọn phòng/bàn để tiếp tục');
            return;
        }
        document.getElementById('summary-service-type').textContent = 'Dịch vụ: ' + (selectedService === 'karaoke' ? 'Karaoke' : 'Bida');
        document.getElementById('summary-room').textContent = selectedRoom.querySelector('h5').textContent + ' (' + selectedRoom.querySelector('.text-muted').textContent.replace('Loại: ', '') + ')';
        document.getElementById('summary-date').textContent = 'Ngày: ' + formatDate(document.getElementById('booking-date').value);
        const selectedTimeSlots = [];
        document.querySelectorAll('.time-slot-selected').forEach(slot => {
            selectedTimeSlots.push(slot.textContent);
        });
        document.getElementById('summary-time').textContent = 'Thời gian: ' + selectedTimeSlots.join(', ');
        navigateToStep(3);
        startHoldTimer();
    });

    // --- Validate phone and email ---
    function validatePhone(phone) {
        return /^[0-9]{10}$/.test(phone);
    }
    function validateEmail(email) {
        // Simple email regex
        return /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email);
    }

    document.getElementById('confirm-booking').addEventListener('click', function () {
        const name = document.getElementById('customer-name').value.trim();
        const phone = document.getElementById('customer-phone').value.trim();
        const email = document.getElementById('customer-email').value.trim();
        if (!name || !phone || !email) {
            alert('Vui lòng điền đầy đủ thông tin liên hệ');
            return;
        }
        if (!validatePhone(phone)) {
            alert('Số điện thoại phải là 10 chữ số.');
            document.getElementById('customer-phone').focus();
            return;
        }
        if (!validateEmail(email)) {
            alert('Email không hợp lệ.');
            document.getElementById('customer-email').focus();
            return;
        }
        navigateToStep(4);
        clearInterval(window.holdTimerInterval);
        // Update payment summary
        const selectedRoom = document.querySelector('.room-item.selected');
        document.getElementById('payment-summary-service').textContent = 'Dịch vụ: ' + (selectedService === 'karaoke' ? 'Karaoke' : 'Bida');
        document.getElementById('payment-summary-room').textContent = selectedRoom.querySelector('h5').textContent + ' (' + selectedRoom.querySelector('.text-muted').textContent.replace('Loại: ', '') + ')';
        document.getElementById('payment-summary-date').textContent = 'Ngày: ' + formatDate(document.getElementById('booking-date').value);
        const selectedTimeSlots = [];
        document.querySelectorAll('.time-slot-selected').forEach(slot => {
            selectedTimeSlots.push(slot.textContent);
        });
        document.getElementById('payment-summary-time').textContent = 'Thời gian: ' + selectedTimeSlots.join(', ');
    });

    // --- Nút hoàn tất thanh toán ---
    document.getElementById('complete-payment').addEventListener('click', function () {
        const bookingSuccessModal = new bootstrap.Modal(document.getElementById('bookingSuccessModal'));
        bookingSuccessModal.show();
        // Lấy thông tin đặt chỗ
        const service = document.getElementById('payment-summary-service').textContent.replace('Dịch vụ: ', '');
        const room = document.getElementById('payment-summary-room').textContent.replace('Phòng: ', '');
        const date = document.getElementById('payment-summary-date').textContent.replace('Ngày: ', '');
        const time = document.getElementById('payment-summary-time').textContent.replace('Thời gian: ', '');
        const total = document.querySelector('#step-4 .fw-bold').textContent;
        const bookingId = '#K' + Date.now();
        const tbody = document.querySelector('#current-bookings tbody');
        const newRow = document.createElement('tr');
        newRow.innerHTML = `
                <td>${bookingId}</td>
                <td>${service} - ${room}</td>
                <td>${date} - ${time}</td>
                <td>${total}</td>
                <td><span class="badge bg-warning">Chờ xác nhận</span></td>
                <td>
                    <button class="btn btn-sm btn-outline-danger cancel-booking">Hủy</button>
                </td>
            `;
        tbody.prepend(newRow);
        document.getElementById('booking-reference').textContent = 'Mã đơn: ' + bookingId;
        document.getElementById('booking-history').classList.remove('d-none');
        // Lưu lịch sử vào localStorage
        saveBookingHistory();
        setTimeout(() => {
            // Reset lại về bước 1 và show lại bước 1
            document.getElementById('booking-steps').classList.remove('d-none');
            navigateToStep(1);
            document.getElementById('booking-date').value = '';
            document.querySelectorAll('.time-slot-selected').forEach(slot => {
                slot.classList.remove('time-slot-selected');
                slot.classList.add('time-slot-available');
            });
            document.getElementById('room-type').selectedIndex = 0;
            hideAvailableRooms();
            document.querySelectorAll('.room-item').forEach(item => item.classList.remove('selected'));
            document.getElementById('customer-name').value = '';
            document.getElementById('customer-phone').value = '';
            document.getElementById('customer-email').value = '';
            document.getElementById('special-requests').value = '';
            // Reset summary
            document.getElementById('summary-service-type').textContent = 'Dịch vụ:';
            document.getElementById('summary-room').textContent = 'Phòng:';
            document.getElementById('summary-date').textContent = 'Ngày:';
            document.getElementById('summary-time').textContent = 'Thời gian:';
            document.getElementById('summary-total').textContent = '';
            document.getElementById('summary-deposit').textContent = '';
            document.getElementById('payment-summary-service').textContent = 'Dịch vụ:';
            document.getElementById('payment-summary-room').textContent = 'Phòng:';
            document.getElementById('payment-summary-date').textContent = 'Ngày:';
            document.getElementById('payment-summary-time').textContent = 'Thời gian:';
            // Reset lại phương thức thanh toán về mặc định (ngân hàng)
            if (paymentCardBody) {
                paymentCardBody.querySelector('#bank').checked = true;
                paymentCardBody.querySelector('#bank-info').classList.remove('d-none');
                paymentCardBody.querySelector('#momo-info').classList.add('d-none');
                paymentCardBody.querySelector('#cash-info').classList.add('d-none');
            }
        }, 1000);
    });

    // --- Khi hủy đặt chỗ, cũng lưu lại lịch sử ---
    document.addEventListener('click', function (e) {
        if (e.target.classList.contains('cancel-booking')) {
            const row = e.target.closest('tr');
            const statusCell = row.querySelector('td:nth-child(5) .badge');
            if (statusCell && statusCell.textContent.trim() === 'Chờ xác nhận') {
                if (confirm('Bạn có chắc muốn hủy chỗ đặt này? Số tiền cọc đã đặt sẽ không thể hoàn trả lại')) {
                    row.remove();
                    alert('Đã hủy đặt chỗ thành công!');
                    saveBookingHistory();
                }
            }
        }
    });

    // --- Các hàm helper giữ nguyên ---
    function navigateToStep(step) {
        document.querySelectorAll('.step').forEach(stepEl => {
            stepEl.classList.remove('step-active', 'step-completed');
            const stepNumber = parseInt(stepEl.getAttribute('data-step'));
            if (stepNumber < step) {
                stepEl.classList.add('step-completed');
            } else if (stepNumber === step) {
                stepEl.classList.add('step-active');
            }
        });
        const progressPercent = ((step - 1) / 3) * 100;
        document.querySelector('.progress-bar').style.width = progressPercent + '%';
        document.querySelectorAll('.booking-step').forEach(section => {
            section.classList.add('d-none');
        });
        document.getElementById('step-' + step).classList.remove('d-none');
    }
    function formatDate(dateString) {
        const options = { day: '2-digit', month: '2-digit', year: 'numeric' };
        return new Date(dateString).toLocaleDateString('vi-VN', options);
    }
    function startHoldTimer() {
        let timeLeft = 300;
        window.holdTimerInterval = setInterval(() => {
            timeLeft--;
            const minutes = Math.floor(timeLeft / 60);
            const seconds = timeLeft % 60;
            document.getElementById('hold-timer').textContent =
                `${minutes.toString().padStart(2, '0')}:${seconds.toString().padStart(2, '0')}`;
            if (timeLeft <= 0) {
                clearInterval(window.holdTimerInterval);
                alert('Hết thời gian giữ chỗ. Vui lòng bắt đầu lại quá trình đặt chỗ.');
                navigateToStep(1);
            }
        }, 1000);
    }
    // --- Time slot selection giữ nguyên ---
    document.getElementById('time-slots').addEventListener('click', function (e) {
        if (e.target.classList.contains('time-slot-available')) {
            e.target.classList.toggle('time-slot-selected');
            e.target.classList.remove('time-slot-available');
        } else if (e.target.classList.contains('time-slot-selected')) {
            e.target.classList.toggle('time-slot-selected');
            e.target.classList.add('time-slot-available');
        }
    });
    // --- Payment method selection giữ nguyên ---
    document.querySelectorAll('input[name="payment-method"]').forEach(radio => {
        radio.addEventListener('change', function () {
            document.querySelectorAll('.payment-info').forEach(info => {
                info.classList.add('d-none');
            });
            document.getElementById(this.value + '-info').classList.remove('d-none');
        });
    });
    // --- Back buttons giữ nguyên ---
    document.getElementById('back-to-step-1').addEventListener('click', function () {
        navigateToStep(1);
    });
    document.getElementById('back-to-step-2').addEventListener('click', function () {
        navigateToStep(2);
        hideAvailableRooms();
        document.getElementById('room-type').selectedIndex = 0;
    });
    document.getElementById('back-to-step-3').addEventListener('click', function () {
        navigateToStep(3);
    });
});