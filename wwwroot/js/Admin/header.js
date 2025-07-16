
        // Fullscreen functionality
        document.getElementById('fullscreenBtn').addEventListener('click', function() {
            if (!document.fullscreenElement) {
                document.documentElement.requestFullscreen().catch(err => {
                    console.error(`Error attempting to enable fullscreen: ${err.message}`);
                });
            } else {
                if (document.exitFullscreen) {
                    document.exitFullscreen();
                }
            }
        });
        
        // Change icon when in fullscreen
        document.addEventListener('fullscreenchange', function() {
            const fullscreenBtn = document.getElementById('fullscreenBtn');
            if (document.fullscreenElement) {
                fullscreenBtn.innerHTML = '<i class="fas fa-compress"></i>';
            } else {
                fullscreenBtn.innerHTML = '<i class="fas fa-expand"></i>';
            }
        });

        // Responsive adjustments for small screens
        function handleResponsive() {
            const navButtons = document.querySelector('.nav-buttons-container');
            const availableWidth = window.innerWidth;
            
            // Implement responsive logic if needed
            // Could add dropdown for buttons if space is limited
        }

        // ...existing code...

// Tự động active nav-button theo URL
// ...existing code...
function setActiveNavButton() {
    const navButtons = document.querySelectorAll('.nav-buttons-container .nav-button');
    const currentPath = window.location.pathname.toLowerCase();

    navButtons.forEach(btn => {
        const btnHref = btn.getAttribute('href');
        if (!btnHref) return;
        // Lấy thư mục cha của href (ví dụ: /bia/)
        const btnDir = btnHref.toLowerCase().split('/').filter(Boolean)[0];
        // Kiểm tra nếu currentPath chứa /bia/ hoặc /kra/ hoặc /menu/ hoặc /nhanvien/ hoặc /revenue/
        if (currentPath.includes(`/${btnDir}/`)) {
            btn.classList.add('active');
        } else {
            btn.classList.remove('active');
        }
    });
}
// Gọi lại hàm này khi header đã được include vào DOM
        document.addEventListener('DOMContentLoaded', setActiveNavButton);
setActiveNavButton();
// ...existing code...
        window.addEventListener('resize', handleResponsive);
        document.addEventListener('DOMContentLoaded', handleResponsive);

