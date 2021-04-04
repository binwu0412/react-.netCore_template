import React, { useEffect, useState } from "react";
import { Grid, Button, TextField, IconButton } from "@material-ui/core";
import { WorkOutline, Business, PermIdentity, PeopleOutline, AddCircleOutline, FiberNew } from "@material-ui/icons";

import FormInput from "../components/form/FormInput";
import FormChipInput from "../components/form/FormChipInput";
import CostDonutPlot from "./CostDonutPlot";

import * as BenefitCostService from "../services/BenefitCostService";
import * as ToastService from "../services/ToastService";
import Employee from "../models/Employee";

const EmployeeBenefitCostPage = ({ location }) => {
  const [benefitCostPreview, setBenefitCostPreview] = useState(null);
  const originEmployee = location.state.employee ?? new Employee();
  const [employee, setEmployee] = useState(location.state.employee ?? new Employee());
  const handleChange = (property) => (event) => {
    let newEmployee = {...employee};
    newEmployee[property] = event.target.value;
    setEmployee(newEmployee);
  }

  const [canEdit, setCanEdit] = useState(false);
  const handleSwitchEdit = () => {
    if (canEdit) setEmployee(originEmployee);
    setCanEdit(!canEdit);
  }

  const handleLoadBenefitCost = async () => {
    const benefitCost = await BenefitCostService.getBenefitCost(location.state.employee.id);
    setBenefitCostPreview(benefitCost);
  }

  const [newDependendName, setNewDependendName] = useState("");
  const [dependends, setDependends] = useState(
    location.state.employee && location.state.employee.dependends 
      ? location.state.employee.dependends 
      : []);
  const handleUpdateDependendName = (event) => setNewDependendName(event.target.value);
  
  const handleAddNewDependend = () => {
    let newDependends = [...dependends];
    newDependends.push({ id: 0, name: newDependendName, employeeId: 4 });
    setDependends(newDependends);
    setNewDependendName("");
  }

  const removeDependend = (index) => () => {
    let newDependends = [...dependends];
    newDependends.splice(index, 1);
    setDependends(newDependends);
  }

  useEffect(() => {
    handleLoadBenefitCost();
  }, []);

  return (
    <div className="page">
      <Grid container justify="center" alignItems="center" className="height-100per">
        <Grid item xs={5} className="height-100per">
          <CostDonutPlot benefitCostPreview={benefitCostPreview}/>
        </Grid>
        <Grid item xs={3} className="height-100per" container direction="column" justify="center" >
          <FormInput
            icon={<PermIdentity/>}
            label="Employee Name"
            required
            disabled={!canEdit}
            schema="required"
            value={employee.name}
            onChange={handleChange("name")}
          />
          <FormInput
            icon={<WorkOutline/>}
            label="Employee Title"
            required
            disabled={!canEdit}
            schema="required"
            value={employee.title}
            onChange={handleChange("title")}
          />
          <FormInput
            icon={<Business/>}
            label="Employee Department"
            required
            disabled={!canEdit}
            schema="required"
            value={employee.department}
            onChange={handleChange("department")}
          />
          <FormChipInput
            label="Dependends"
            values={dependends} 
            renderName="name"
            disabled={!canEdit}
            handleRemoveChip={removeDependend}
            renderInputSection={() => (
              <>
                <div className="mr-1 vertical-center">
                  <FiberNew color="primary"/>
                </div>
                <TextField 
                  focused 
                  value={newDependendName}
                  className="mr-2" 
                  onChange={handleUpdateDependendName}
                  placeholder="Dependend Name"/>
                <IconButton size="small"
                  onClick={handleAddNewDependend}>
                  <AddCircleOutline/>
                </IconButton>
              </>
            )}
            icon={<PeopleOutline/>}
          />
          <Grid container item className="p-3" justify="flex-end" alignItems="center">
            <div className="vertical-center">
              { canEdit
                ? <Button color="secondary" variant="contained" className="mr-1"
                    onClick={handleSwitchEdit}>
                    Cancel</Button>
                : null }
              <Button color="primary" variant="contained"
                onClick={() => canEdit ? ToastService.showErrorToast("Not Authorized") : handleSwitchEdit()}>
                {canEdit ? "Save Info" : "Edit Info"}
              </Button>
            </div>
          </Grid>
        </Grid>
      </Grid>
    </div>
  );
}

export default EmployeeBenefitCostPage;