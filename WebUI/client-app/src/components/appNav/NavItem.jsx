import React from 'react';
import { ListItem, Tooltip, IconButton } from '@material-ui/core';

const NavItem = ({ title, renderItemIcon, isSelected, onClick }) => {
  return (
    <ListItem dense disableGutters>
      <Tooltip title={title}>
        <IconButton onClick={onClick}>{renderItemIcon()}</IconButton>
      </Tooltip>
    </ListItem>
  );
}

export default NavItem;

