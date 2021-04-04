import React from "react";
import { Drawer, List } from "@material-ui/core";
import { ViewList, Home } from "@material-ui/icons";
import { useHistory } from "react-router-dom";
import NavItem from "./NavItem";

const AppNav = () => {
  let history = useHistory();
  const handleNavigate = (path) => () => {
    history.push(path);
  }

  return (
    <div style={{height: '100vh', width: 48}}>
      <Drawer variant="permanent" anchor="left">
        <List dense disablePadding>
          <NavItem 
            title="Home"
            renderItemIcon={() => <Home/>}
            onClick={handleNavigate('/')}/>
          {/* <NavItem 
            title="Benefit Cost"
            renderItemIcon={() => <ViewList/>}
            onClick={handleNavigate('/')}/> */}
        </List>
      </Drawer>
    </div>
  );
}

export default AppNav;

