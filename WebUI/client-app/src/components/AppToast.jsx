import React, { Component } from "react";
import { Snackbar } from '@material-ui/core';
import { Alert } from '@material-ui/lab';
import * as ToastService from '../services/ToastService';

export default class AppToast extends Component {
	state = {
    toastOpen: false,
    severity: 'success',
    alertMessage: ''
  };

  componentDidMount() {
    ToastService.setToast(this);
  }
  
	render() {
    const { toastOpen, severity, alertMessage } = this.state; 
		return (
      <Snackbar 
        open={toastOpen} 
        autoHideDuration={3000} 
        onClose={ this.handleToastClose } 
        anchorOrigin={{vertical: 'bottom', horizontal: 'left'}}
        className="toastBar"
      >
        <Alert variant="filled" onClose={this.handleToastClose} severity={severity} className="toastAlert">
          { alertMessage }
        </Alert>
      </Snackbar>
    );
  }
  
  handleToastOpen = (severity, alertMessage) => this.setState({ toastOpen: true, severity, alertMessage });
  handleToastClose = () => this.setState({ toastOpen: false });
}
