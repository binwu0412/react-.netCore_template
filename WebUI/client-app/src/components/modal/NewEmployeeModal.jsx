import React from 'react';

export const createNewEmploeeModalConfig = (onSave) => {
  renderTitle: () => ( <div>New Employee</div> ),
  onDefaultClick: () => onSave(),
  renderContent: () => ()
}
