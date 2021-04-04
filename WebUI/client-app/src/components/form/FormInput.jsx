import React, { Component } from 'react'
import { TextField, Grid, InputAdornment, IconButton } from '@material-ui/core';
import { ClearRounded } from '@material-ui/icons';
import Joi from 'joi-full';
import classNames from 'classnames';

export const fieldSchema = {
  required: Joi.string().required().label("Required field"),
  email: Joi.string().allow('').email({ minDomainAtoms: 2 }).label("Email"),
  emailReuired: Joi.string().required().email({ minDomainAtoms: 2 }).label("password"),                               
  password: Joi.string().regex(/^[\w]{8,30}$/).required(),
  phone: Joi.string().length(10).regex(/^[0-9]+$/),
  fax: Joi.string().length(10).regex(/^[0-9]+$/),
  date: Joi.date().format('YYYY-MM-DD'),
  time: Joi.date().format('hh:mm A'),
  greaterThan0: Joi.number().greater(0).required().label("This field")
};

export default class FormInput extends Component {
  state = {
    error: null
  }

  render() {
    const { error } = this.state;
    const { label, value, icon, onChange, GridProps, InputProps, disabled, options, helperText, ...others } = this.props;
    const inputClass = this.getInputClass(error, disabled);
    return (
      <Grid item className="p-3" {...GridProps}>
        <TextField
          label={label}
          value={value}
          variant="filled"
          size="small"
          fullWidth
          focused={!disabled}
          error={Boolean(error)}
          disabled={disabled}
          InputProps={{
            ...InputProps,
            className: inputClass,
            startAdornment: 
              <InputAdornment className="mr-1" >{icon}</InputAdornment>,
            endAdornment: !disabled
              ? <InputAdornment positon="end">
                  <IconButton id="clearButton" size="small" onClick={this.handleClear}>
                    <ClearRounded/>
                  </IconButton>
                </InputAdornment>
              : null
          }}
          InputLabelProps={{ className: "formInput_label" }}
          helperText={!error ? helperText : error}
          onChange={this.handleChange}
          FormHelperTextProps={{ className: 'formInput_helperText' }}
          onBlur={this.handleValidate}
          {...others}
        />
      </Grid>
    )
  }

  getInputClass = (error, disabled) => {
    return classNames({
      formInput_err: Boolean(error),
      formInput: !Boolean(error),
      formInput_disabled: Boolean(disabled)
    });
  }

  handleChange = (event) => this.props.onChange(event);

  handleValidate = () => {
    const { value, schema } = this.props;
    if (!schema) return;

    const result = this.validateValue(value, schema);
    this.handleSetError(result);

    return result.error;
  }

  validateValue = (value, schema) => {
    return Joi.validate({ [schema]: value}, {[schema]: fieldSchema[schema]});
  }

  handleClear = () => {
    const { schema } = this.props;
    this.props.onChange({target: { value: '' }});
    if (!schema) return;

    const result = this.validateValue('', schema);
    this.handleSetError(result);
  }

  handleSetError = (validateResult) => {
    if (Boolean(validateResult.error)) this.setState({ error: validateResult.error.details[0].message })
    else this.setState({ error: null });
  }
}
