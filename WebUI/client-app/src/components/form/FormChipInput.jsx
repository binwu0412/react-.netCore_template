import React, { Component } from "react";
import { FormControl, InputLabel, Grid, Select, FilledInput, Chip } from "@material-ui/core";
import { CancelRounded, Face } from "@material-ui/icons";

export default class FormChipInput extends Component {
  render() {
    const { label, values, renderName, icon, GridProps, renderInputSection, handleRemoveChip, disabled } = this.props;
    return (
      <Grid item className="p-3" {...GridProps}>
        <FormControl fullWidth disabled>
          <InputLabel variant="filled" className="formInput_label">{ label }</InputLabel>
          <Select
            startAdornment={icon}
            open={false}
            multiple
            margin="dense"
            value={values}
            IconComponent={React.Fragment}
            input={<FilledInput className="formInput"/>}
            renderValue={(values) => (
              <div className="formMultiChip_inputChipBox">
                {values.map((item, index) => {
                  return (
                    <Chip key={index} label={item[renderName]} size="small"
                      icon={<Face />}
                      disabled={disabled}
                      deleteIcon={<CancelRounded />}
                      color="primary"
                      onDelete={handleRemoveChip(index)}
                      className="formMultiChip_inputChip"
                      variant="outlined"/>)
                })}
              </div>
            )}
          />
        </FormControl>
        { !disabled
          ? <Grid container justify="flex-start" className="p-4" style={{backgroundColor: 'rgba(63, 81, 181, 0.2)', borderBottomLeftRadius: 5, borderBottomRightRadius: 5}}>
              { renderInputSection && renderInputSection() }
            </Grid>
          : null
        }
        
      </Grid>
    )
  }
}