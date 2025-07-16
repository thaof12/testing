// ==== Tab switching for menu.html ====
document.addEventListener('DOMContentLoaded', function() {
  // Tab switching
  document.querySelectorAll('.tab-btn').forEach(btn => {
    btn.addEventListener('click', function() {
      document.querySelectorAll('.tab-btn').forEach(b => {
        b.classList.remove('border-indigo-500', 'text-indigo-600');
        b.classList.add('border-transparent', 'text-gray-500');
      });
      this.classList.remove('border-transparent', 'text-gray-500');
      this.classList.add('border-indigo-500', 'text-indigo-600');
      const tabId = this.getAttribute('data-tab');
      document.querySelectorAll('.tab-content').forEach(content => {
        content.classList.remove('active');
      });
      const tabContent = document.getElementById(tabId);
      if (tabContent) tabContent.classList.add('active');
    });
  });

  // ==== Modal handling for menu.html ====
  const itemModal = document.getElementById('itemModal');
  const addItemBtn = document.getElementById('addItemBtn');
  const closeModalBtn = document.getElementById('closeModalBtn');
  const cancelBtn = document.getElementById('cancelBtn');
  if (addItemBtn && itemModal) {
    addItemBtn.addEventListener('click', () => {
      itemModal.classList.remove('hidden');
    });
  }
  if (closeModalBtn && itemModal) {
    closeModalBtn.addEventListener('click', () => {
      itemModal.classList.add('hidden');
    });
  }
  if (cancelBtn && itemModal) {
    cancelBtn.addEventListener('click', () => {
      itemModal.classList.add('hidden');
    });
  }
  if (itemModal) {
    window.addEventListener('click', (e) => {
      if (e.target === itemModal) {
        itemModal.classList.add('hidden');
      }
    });
  }
  const itemForm = document.getElementById('itemForm');
  if (itemForm && itemModal) {
    itemForm.addEventListener('submit', function(e) {
      e.preventDefault();
      alert('Món ăn đã được lưu thành công!');
      itemModal.classList.add('hidden');
      this.reset();
    });
  }

  // ==== Modal handling for combo.html ====
  const comboModal = document.getElementById('comboModal');
  const addComboBtn = document.getElementById('addComboBtn');
  const closeComboModalBtn = document.getElementById('closeComboModalBtn');
  const cancelComboBtn = document.getElementById('cancelComboBtn');
  if (addComboBtn && comboModal) {
    addComboBtn.addEventListener('click', () => {
      comboModal.classList.remove('hidden');
    });
  }
  if (closeComboModalBtn && comboModal) {
    closeComboModalBtn.addEventListener('click', () => {
      comboModal.classList.add('hidden');
    });
  }
  if (cancelComboBtn && comboModal) {
    cancelComboBtn.addEventListener('click', () => {
      comboModal.classList.add('hidden');
    });
  }
  if (comboModal) {
    window.addEventListener('click', (e) => {
      if (e.target === comboModal) {
        comboModal.classList.add('hidden');
      }
    });
  }
  const comboForm = document.getElementById('comboForm');
  if (comboForm && comboModal) {
    comboForm.addEventListener('submit', function(e) {
      e.preventDefault();
      alert('Combo đã được lưu thành công!');
      comboModal.classList.add('hidden');
      this.reset();
    });
  }

  // ==== Combo name suggestion and price calculation for combo.html ====
  const comboItemA = document.getElementById('comboItemA');
  const comboItemB = document.getElementById('comboItemB');
  const comboName = document.getElementById('comboName');
  const comboDiscount = document.getElementById('comboDiscount');
  const comboPrice = document.getElementById('comboPrice');
  function suggestComboName() {
    if (comboItemA && comboItemB && comboName) {
      const a = comboItemA.value;
      const b = comboItemB.value;
      if (a && b) {
        comboName.value = a + ' + ' + b;
      }
    }
  }
  function calcComboPrice() {
    if (comboItemA && comboItemB && comboDiscount && comboPrice) {
      const priceA = parseInt(comboItemA.selectedOptions[0]?.getAttribute('data-price') || '0', 10);
      const priceB = parseInt(comboItemB.selectedOptions[0]?.getAttribute('data-price') || '0', 10);
      const discount = parseFloat(comboDiscount.value) || 0;
      if (priceA && priceB) {
        let total = priceA + priceB;
        if (discount > 0 && discount < 100) {
          total = Math.round(total * (1 - discount / 100));
        }
        comboPrice.value = total;
      }
    }
  }
  if (comboItemA) comboItemA.addEventListener('change', () => { suggestComboName(); calcComboPrice(); });
  if (comboItemB) comboItemB.addEventListener('change', () => { suggestComboName(); calcComboPrice(); });
  if (comboDiscount) comboDiscount.addEventListener('input', calcComboPrice);
});
