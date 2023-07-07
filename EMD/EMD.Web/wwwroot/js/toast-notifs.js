document.addEventListener('DOMContentLoaded', function () {
    var successToast = document.querySelector('.toast.bg-success');
    var errorToast = document.querySelector('.toast.bg-danger');

    var successToastInstance = new bootstrap.Toast(successToast);
    var errorToastInstance = new bootstrap.Toast(errorToast);

    var successToastShown = successToast && successToast.dataset.shown === 'true';

    if (successToast && !successToastShown) {
        successToastInstance.show();
        successToast.dataset.shown = 'true';
    }

    var errorToastShown = errorToast && errorToast.dataset.shown === 'true';

    if (errorToast && !errorToastShown) {
        errorToastInstance.show();
        errorToast.dataset.shown = 'true';
    }
});