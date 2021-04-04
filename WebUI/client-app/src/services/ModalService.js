let modalRef;

export const setModal = (ref) => {
  modalRef = ref;
}

export const openModal = (modalDetail) => {
  modalRef && modalRef.handleOpenModal(modalDetail);
}

export const closeModal = () => {
  modalRef && modalRef.handleCloseModal();
}
