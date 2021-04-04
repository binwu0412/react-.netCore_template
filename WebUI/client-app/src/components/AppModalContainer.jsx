import React, { useState, useEffect } from 'react';
import { Dialog, DialogTitle, DialogContent, DialogActions, Button } from '@material-ui/core';
import { CancelOutlined, CheckCircleOutlined } from '@material-ui/icons';
import * as ModalService from '../services/ModalService';

const AppModalContainer = () => {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const handleCloseModal = () => setIsModalOpen(false);

  const [renderTitle, setRenderTitle] = useState(null);
  const [renderContent, setRenderContent] = useState(null);
  const [renderActions, setRenderActions] = useState(null);
  const [onDefaultOkClick, setDefaultOkClick] = useState(null);
  const renderDefaultActions = () => (
    <>
      <Button 
        color="secondary" 
        size="small" 
        startIcon={<CancelOutlined/>} 
        onClick={handleCloseModal}>Cancel</Button>
      <Button
        variant="contained"
        color="primary"
        size="small"
        startIcon={<CheckCircleOutlined/>}
        onClick={ Boolean(onDefaultOkClick) ? onDefaultOkClick() : null }>Ok</Button>
    </>
  )
  // const resetModal = () => {
  //   setRenderTitle("");
  //   setRenderContent(null);
  //   setRenderActions(null);
  // }

  const handleOpenModal = ({ renderTitle, renderContent, renderActions, onDefaultOkClick }) => {
    setRenderTitle(renderTitle);
    setRenderContent(renderContent);
    setRenderActions(renderActions);
    setDefaultOkClick(onDefaultOkClick);
    setIsModalOpen(true);
  }

  useEffect(() => { ModalService.setModal({
    handleCloseModal, handleOpenModal
  }) }, []);

  return (
    <React.Fragment>
      { <Dialog open={isModalOpen} onClose={handleCloseModal}>
          <DialogTitle>{ Boolean(renderTitle) ? renderTitle() : null }</DialogTitle>
          <DialogContent>{ Boolean(renderContent) ? renderContent() : null }</DialogContent>
          <DialogActions>
            { Boolean(renderActions) 
              ? renderActions()
              : renderDefaultActions()}
          </DialogActions>
        </Dialog> }
    </React.Fragment>
  );
}

export default AppModalContainer;