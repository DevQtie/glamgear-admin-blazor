window.showLiveToast = () => {
    const toastEl = document.getElementById('liveToast');
    if (toastEl) {
        const toast = bootstrap.Toast.getOrCreateInstance(toastEl);
        toast.show();

        toastEl.addEventListener('hidden.bs.toast', () => {
            window.location.reload();
        });
    }
};

window.showLiveToastRedirect = () => {
    const toastEl = document.getElementById('liveToast');
    if (toastEl) {
        const toast = bootstrap.Toast.getOrCreateInstance(toastEl);
        toast.show();

        toastEl.addEventListener('hidden.bs.toast', () => {
            window.location.replace('/users/verified-users');
        });
    }
};

window.showLiveToastError = () => {
    const toastEl = document.getElementById('liveToastError');
    if (toastEl) {
        const toast = bootstrap.Toast.getOrCreateInstance(toastEl);
        toast.show();
    }
};

window.goBack = function () {
    history.back();
};