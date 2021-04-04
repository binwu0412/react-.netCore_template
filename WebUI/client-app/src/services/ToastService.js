let toast;

export const setToast = (elementRef) => toast = elementRef;
export const showToast = (severity, message) => toast.handleToastOpen(severity, message);
export const closeToast = () => toast.handleToastClose();

export const showErrorToast = (message = "error") => toast.handleToastOpen("error", message);
export const showInfoToast = (message = "info") => toast.handleToastOpen("info", message);
export const showSuccessToast = (message = "success") => toast.handleToastOpen("success", message);
export const showWarningToast = (message = "warning") => toast.handleToastOpen("warning", message);