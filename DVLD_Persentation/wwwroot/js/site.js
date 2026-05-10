(function () {
  // --- Phase 1: Splash Screen Logic (Robust) ---
  const handleSplash = () => {
    const splash = document.getElementById("splashScreen");
    const progress = document.getElementById("splashProgress");

    if (progress) progress.style.width = "100%";

    setTimeout(() => {
      if (splash) {
        splash.style.opacity = "0";
        setTimeout(() => {
          splash.style.visibility = "hidden";
          splash.style.display = "none"; // Ensure it's gone from layout
        }, 700);
        document.body.classList.remove("overflow-hidden");
        document.body.style.overflow = "auto";
      }
    }, 500);
  };

  // Run splash logic as early as possible
  if (document.readyState === "complete") {
    handleSplash();
  } else {
    window.addEventListener("load", handleSplash);
    // Fail-safe backup
    setTimeout(handleSplash, 2000);
  }

  const menuToggle = document.getElementById("menuToggle");
  const sidebar = document.getElementById("sidebar");
  const liveClock = document.getElementById("liveClock");
  const syncStatus = document.getElementById("syncStatus");
  const queueBar = document.getElementById("queueBar");
  const processingHealth = document.getElementById("processingHealth");

  if (menuToggle && sidebar) {
    menuToggle.addEventListener("click", function () {
      sidebar.classList.toggle("hidden");
    });
  }

  function tickClock() {
    if (!liveClock) return;
    liveClock.textContent = new Date().toLocaleString();
  }

  function updateStatus() {
    if (syncStatus) {
      const online = navigator.onLine;
      syncStatus.className = online
        ? "h-2 w-2 rounded-full bg-emerald-500 animate-pulse"
        : "h-2 w-2 rounded-full bg-red-500";
    }

    if (queueBar) {
      const pct = 30 + Math.floor(Math.random() * 60);
      queueBar.style.width = pct + "%";
    }

    if (processingHealth) {
      const healthy = navigator.onLine && Math.random() > 0.1;
      processingHealth.textContent = healthy
        ? "System Healthy"
        : "Status Degraded";
      processingHealth.className = healthy
        ? "text-sm font-semibold text-emerald-400 uppercase tracking-wide"
        : "text-sm font-semibold text-amber-400 uppercase tracking-wide";
    }
  }

  // Start background processes
  setInterval(tickClock, 1000);
  setInterval(updateStatus, 5000);
  tickClock();
  updateStatus();

  // Global Form Validation Observer
  document.querySelectorAll("form[data-validate]").forEach((form) => {
    const submitBtn = form.querySelector('button[type="submit"]');
    const inputs = form.querySelectorAll(
      "input[required], select[required], textarea[required]",
    );

    const validate = () => {
      let isValid = true;
      inputs.forEach((input) => {
        if (!input.value || input.value.trim() === "") isValid = false;
        if (
          input.type === "email" &&
          input.value &&
          !input.value.includes("@@")
        )
          isValid = false;
      });
      if (submitBtn) submitBtn.disabled = !isValid;
    };

    inputs.forEach((input) => {
      input.addEventListener("input", validate);
      input.addEventListener("change", validate);
    });
    validate();
  });

  // Phase 5: Global UI Helpers
  window.DVLD = {
    showToast: function (message, type = "success") {
      const container = document.getElementById("toastContainer");
      if (!container) return;

      const toast = document.createElement("div");
      const bgColor =
        type === "success"
          ? "bg-emerald-500"
          : type === "error"
            ? "bg-red-500"
            : "bg-amber-500";

      toast.className = `${bgColor} text-white px-6 py-3 rounded-lg shadow-2xl flex items-center gap-3 transform translate-x-full transition-all duration-300 opacity-0`;
      toast.innerHTML = `
        <svg class="h-5 w-5" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M13 16h-1v-4h-1m1-4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z"></path></svg>
        <span class="text-xs font-bold uppercase tracking-widest">${message}</span>
      `;

      container.appendChild(toast);

      // Animate In
      setTimeout(() => {
        toast.classList.remove("translate-x-full", "opacity-0");
      }, 100);

      // Auto Dismiss
      setTimeout(() => {
        toast.classList.add("translate-x-full", "opacity-0");
        setTimeout(() => toast.remove(), 300);
      }, 5000);
    },

    showModal: function (title, content, actions = []) {
      const backdrop = document.getElementById("modalBackdrop");
      const container = document.getElementById("modalContainer");
      if (!backdrop || !container) return;

      const actionButtons = actions
        .map(
          (a) => `
        <button onclick="${a.onClick}" class="${a.class || "btn-primary btn-sm"}">${a.label}</button>
      `,
        )
        .join("");

      container.innerHTML = `
        <div class="dvld-card w-full max-w-lg shadow-[0_0_50px_rgba(0,0,0,0.5)] border-slate-700 animate-in fade-in zoom-in duration-300">
            <div class="flex items-center justify-between border-b border-slate-700 pb-4 mb-6">
                <h3 class="text-lg font-bold text-white">${title}</h3>
                <button onclick="DVLD.closeModal()" class="text-slate-500 hover:text-white">
                    <svg class="h-6 w-6" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12"></path></svg>
                </button>
            </div>
            <div class="text-sm text-slate-400 mb-8 leading-relaxed">
                ${content}
            </div>
            <div class="flex items-center justify-end gap-3 pt-6 border-t border-slate-700">
                <button onclick="DVLD.closeModal()" class="btn-primary btn-secondary btn-sm">Close</button>
                ${actionButtons}
            </div>
        </div>
      `;

      backdrop.classList.remove("hidden");
    },

    closeModal: function () {
      const backdrop = document.getElementById("modalBackdrop");
      if (backdrop) backdrop.classList.add("hidden");
    },

    // Phase 2: Date Picker Helper
    initDatePicker: function (elementId) {
      const input = document.getElementById(elementId);
      if (!input) return;

      input.addEventListener("click", () => {
        input.showPicker();
      });

      if (!input.parentNode.querySelector(".calendar-icon")) {
        const icon = document.createElement("div");
        icon.className =
          "calendar-icon absolute right-3 top-1/2 -translate-y-1/2 text-slate-500 pointer-events-none";
        icon.innerHTML = `<svg class="h-4 w-4" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8 7V3m8 4V3m-9 8h10M5 21h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v12a2 2 0 002 2z"></path></svg>`;
        input.parentNode.classList.add("relative");
        input.parentNode.appendChild(icon);
      }
    },

    // Phase 2: Time Picker Helper
    initTimePicker: function (elementId) {
      const input = document.getElementById(elementId);
      if (!input) return;
      input.type = "time";
      input.addEventListener("click", () => input.showPicker());
    },

    // Phase 4: Debounce Helper
    debounce: function (func, wait) {
      let timeout;
      return function executedFunction(...args) {
        const later = () => {
          clearTimeout(timeout);
          func(...args);
        };
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
      };
    },

    // Phase 7: Multi-Step Wizard Logic
    initWizard: function (wizardId) {
      const wizard = document.getElementById(wizardId);
      if (!wizard) return;

      const steps = wizard.querySelectorAll(".wizard-step");
      const dots = wizard.querySelectorAll(".wizard-step-dot");
      let currentStep = 0;

      const showStep = (index) => {
        steps.forEach((step, i) => {
          step.classList.toggle("hidden", i !== index);
          if (i === index) step.classList.add("fade-in");
        });

        dots.forEach((dot, i) => {
          dot.classList.toggle("active", i === index);
          dot.classList.toggle("completed", i < index);
        });

        currentStep = index;
      };

      wizard.querySelectorAll("[data-wizard-next]").forEach((btn) => {
        btn.addEventListener("click", () => {
          if (currentStep < steps.length - 1) showStep(currentStep + 1);
        });
      });

      wizard.querySelectorAll("[data-wizard-prev]").forEach((btn) => {
        btn.addEventListener("click", () => {
          if (currentStep > 0) showStep(currentStep - 1);
        });
      });

      showStep(0);
    },

    // Phase 7: Empty State Generator
    createEmptyState: function (
      containerId,
      title,
      message,
      ctaLabel,
      onCtaClick,
    ) {
      const container = document.getElementById(containerId);
      if (!container) return;

      container.innerHTML = `
        <div class="empty-state-container fade-in">
          <div class="empty-state-illustration">
             <svg class="w-full h-full text-slate-700" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="1" d="M20 13V6a2 2 0 00-2-2H6a2 2 0 00-2 2v7m16 0v5a2 2 0 01-2 2H6a2 2 0 01-2-2v-5m16 0h-2.586a1 1 0 00-.707.293l-2.414 2.414a1 1 0 01-.707.293h-3.172a1 1 0 01-.707-.293l-2.414-2.414A1 1 0 006.586 13H4"></path></svg>
          </div>
          <h3 class="text-lg font-bold text-white mb-2">${title}</h3>
          <p class="text-xs text-slate-500 mb-8 max-w-xs mx-auto">${message}</p>
          ${ctaLabel ? `<button id="emptyCta" class="btn-primary py-2 px-8">${ctaLabel}</button>` : ""}
        </div>
      `;

      if (ctaLabel && onCtaClick) {
        document
          .getElementById("emptyCta")
          .addEventListener("click", onCtaClick);
      }
    },

    // Phase 7: Inline Editing Pattern
    initInlineEdit: function (containerId, onSave) {
      const container = document.getElementById(containerId);
      if (!container) return;

      const displayValue = container.querySelector("[data-inline-display]");
      const editInput = container.querySelector("[data-inline-input]");
      const editBtn = container.querySelector("[data-inline-trigger]");
      const saveBtn = container.querySelector("[data-inline-save]");
      const cancelBtn = container.querySelector("[data-inline-cancel]");

      if (!displayValue || !editInput || !editBtn) return;

      const setMode = (isEditing) => {
        displayValue.classList.toggle("hidden", isEditing);
        editBtn.classList.toggle("hidden", isEditing);
        editInput.classList.toggle("hidden", !isEditing);
        if (saveBtn) saveBtn.classList.toggle("hidden", !isEditing);
        if (cancelBtn) cancelBtn.classList.toggle("hidden", !isEditing);
        if (isEditing) editInput.focus();
      };

      editBtn.addEventListener("click", () => setMode(true));

      if (cancelBtn) {
        cancelBtn.addEventListener("click", () => {
          editInput.value = displayValue.textContent.trim();
          setMode(false);
        });
      }

      if (saveBtn) {
        saveBtn.addEventListener("click", () => {
          const newValue = editInput.value;
          displayValue.textContent = newValue;
          if (onSave) onSave(newValue);
          setMode(false);
        });
      }
    },

    // Phase 7: View Toggler Logic
    initViewToggler: function (containerId, controlsId) {
      const container = document.getElementById(containerId);
      const controls = document.getElementById(controlsId);
      if (!container || !controls) return;

      const buttons = controls.querySelectorAll("[data-view]");
      buttons.forEach((btn) => {
        btn.addEventListener("click", () => {
          const view = btn.getAttribute("data-view");

          // Update classes
          container.classList.remove("grid-view", "list-view", "card-view");
          container.classList.add(`${view}-view`);

          // Update buttons
          buttons.forEach((b) =>
            b.classList.toggle("text-dvld-teal", b === btn),
          );
          buttons.forEach((b) =>
            b.classList.toggle("bg-slate-700/50", b === btn),
          );
        });
      });
    },
  };

  // Global Search Logic with Debounce
  const globalSearch = document.getElementById("globalSearch");
  const suggestions = document.getElementById("searchSuggestions");
  const suggestionList = document.getElementById("suggestionList");

  if (globalSearch && suggestions) {
    const handleSearch = DVLD.debounce((e) => {
      const query = e.target.value.toLowerCase();
      if (query.length > 2) {
        // Mock Suggestions
        const mockData = [
          { title: "Ahmed Ragab", category: "Person", id: "102" },
          { title: "License #2841", category: "License", id: "2841" },
          {
            title: "Local Application #502",
            category: "Application",
            id: "502",
          },
        ];

        const filtered = mockData.filter((d) =>
          d.title.toLowerCase().includes(query),
        );

        if (filtered.length > 0) {
          suggestionList.innerHTML = filtered
            .map(
              (d) => `
            <div class="px-4 py-3 hover:bg-slate-700/50 cursor-pointer flex items-center justify-between group transition-colors">
              <div>
                <p class="text-xs font-bold text-dvld-light">${d.title}</p>
                <p class="text-[9px] uppercase font-bold text-slate-500">${d.category}</p>
              </div>
              <svg class="h-4 w-4 text-slate-600 group-hover:text-dvld-teal transition-colors" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 5l7 7-7 7"></path></svg>
            </div>
          `,
            )
            .join("");

          suggestions.classList.remove(
            "invisible",
            "opacity-0",
            "translate-y-2",
          );
        } else {
          suggestions.classList.add("invisible", "opacity-0", "translate-y-2");
        }
      } else {
        suggestions.classList.add("invisible", "opacity-0", "translate-y-2");
      }
    }, 300);

    globalSearch.addEventListener("input", handleSearch);

    // Close suggestions on blur (Robust)
    document.addEventListener("click", (e) => {
      if (globalSearch && suggestions) {
        if (
          !globalSearch.contains(e.target) &&
          !suggestions.contains(e.target)
        ) {
          suggestions.classList.add("invisible", "opacity-0", "translate-y-2");
        }
      }
    });
  }
})();
