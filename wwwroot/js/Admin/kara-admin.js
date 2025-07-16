// Section navigation
function showSection(sectionId) {
  document.querySelectorAll('.section-content').forEach(el => {
    el.classList.add('hidden');
  });
  document.querySelectorAll('button').forEach(btn => {
    btn.classList.remove('active-tab');
  });
  const section = document.getElementById(`${sectionId}-section`);
  if (section) section.classList.remove('hidden');
  if (event && event.target) event.target.classList.add('active-tab');
}

// Room modal controls
function openRoomModal() {
  const modal = document.getElementById('roomModal');
  if (modal) modal.classList.remove('hidden');
}
function closeRoomModal() {
  const modal = document.getElementById('roomModal');
  if (modal) modal.classList.add('hidden');
}

// Role modal controls (for pricing.html if needed)
function openRoleModal() {
  const modal = document.getElementById('roleModal');
  if (modal) modal.classList.remove('hidden');
}
function closeRoleModal() {
  const modal = document.getElementById('roleModal');
  if (modal) modal.classList.add('hidden');
}

// Initialize drag and drop rooms (for room map)
function initDraggableRooms() {
  const roomMap = document.getElementById('roomMap');
  if (!roomMap) return;
  roomMap.innerHTML = '';
  const rooms = [
    { id: 'KR101', name: 'VIP Room 1', x: 20, y: 20, status: 'available', capacity: 8, type: 'VIP' },
    { id: 'KR102', name: 'VIP Room 2', x: 140, y: 20, status: 'occupied', capacity: 10, type: 'VIP' },
    { id: 'KR201', name: 'Deluxe Room', x: 260, y: 20, status: 'reserved', capacity: 6, type: 'Deluxe' },
    { id: 'KR202', name: 'Deluxe Room', x: 20, y: 120, status: 'available', capacity: 6, type: 'Deluxe' },
    { id: 'KR301', name: 'Party Room', x: 140, y: 120, status: 'available', capacity: 15, type: 'Party' }
  ];
  rooms.forEach(room => {
    const roomEl = document.createElement('div');
    roomEl.className = `draggable-room ${room.status === 'available' ? 'bg-green-50' : 
                       room.status === 'occupied' ? 'bg-red-50' : 'bg-yellow-50'}`;
    roomEl.style.left = `${room.x}px`;
    roomEl.style.top = `${room.y}px`;
    roomEl.innerHTML = `
      <div class="flex items-center mb-1">
        <span class="status-indicator ${room.status === 'available' ? 'status-available' : room.status === 'occupied' ? 'status-occupied' : 'status-reserved'} mr-2"></span>
        <span class="font-bold">${room.id}</span>
      </div>
      <div class="text-xs font-medium mb-1">${room.name}</div>
      <div class="text-xs text-gray-500">Sức chứa: ${room.capacity}</div>
      <div class="text-xs text-gray-500">Loại: ${room.type}</div>
    `;
    roomEl.setAttribute('draggable', 'true');
    roomEl.title = `Mã phòng: ${room.id}
Tên: ${room.name}
Trạng thái: ${room.status === 'available' ? 'Trống' : room.status === 'occupied' ? 'Đang sử dụng' : 'Đã đặt'}
Sức chứa: ${room.capacity}
Loại phòng: ${room.type}`;
    roomEl.addEventListener('dragstart', function(e) {
      e.dataTransfer.setData('text/plain', room.id);
      setTimeout(() => {
        this.classList.add('opacity-50');
      }, 0);
    });
    roomEl.addEventListener('dragend', function() {
      this.classList.remove('opacity-50');
    });
    roomMap.addEventListener('dragover', function(e) {
      e.preventDefault();
    });
    roomMap.addEventListener('drop', function(e) {
      e.preventDefault();
      const data = e.dataTransfer.getData('text/plain');
      const draggedElement = document.querySelector(`[draggable]`);
      if (draggedElement) {
        draggedElement.style.left = `${e.clientX - roomMap.getBoundingClientRect().left - 50}px`;
        draggedElement.style.top = `${e.clientY - roomMap.getBoundingClientRect().top - 40}px`;
      }
    });
    roomMap.appendChild(roomEl);
  });
}

// Page initialization
document.addEventListener('DOMContentLoaded', function() {
  // Only call if function and element exist
  if (typeof showSection === 'function' && document.querySelector('.section-content')) {
    showSection('rooms');
  }
  if (typeof initDraggableRooms === 'function' && document.getElementById('roomMap')) {
    initDraggableRooms();
  }
});
