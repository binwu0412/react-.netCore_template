import React from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import AppNav from "./appNav/AppNav";
import EmployeeListPage from "../pages/EmployeeListPage";
import EmployeeBenefitCostPage from "../pages/EmployeeBenefitCostPage";
import NotFoundPage from "../pages/NotFoundPage";

const AppRoute = () => {
  return (
    <Router>
      <div className="app-route">
        <AppNav/>
        <Switch className="app-route__switch">
          <Route exact path="/" component={EmployeeListPage}/>
          <Route path="/employeecosts/:id" component={EmployeeBenefitCostPage}/>
          <Route path="*" component={NotFoundPage} />
        </Switch>
      </div>
    </Router>
  );
}

export default AppRoute;