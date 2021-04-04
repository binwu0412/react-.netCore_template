import React from "react";
import Adapter from "@wojtekmaj/enzyme-adapter-react-17";
import { shallow, configure } from "enzyme";
import FormInput from "../../components/form/FormInput";
import { TextField, InputBase } from "@material-ui/core";

import Joi from 'Joi-full';

configure({ adapter: new Adapter() });

const mockOnChange = jest.fn();
let defaultProps = {
  label: "test",
  value: "",
  onChange: mockOnChange,
  schema: "required"
}
let defaultWrapper;

beforeEach(() => {
  mockOnChange.mockClear();
  defaultWrapper = shallow(<FormInput {...defaultProps} />);
  defaultWrapper.setProps(defaultProps);
});

describe("handleChange", () => {
  it("callTextFieldOnChangeProp__expectHandleChangeTriggered", () => {
    const testEvent = { target: { value: "new test value"}};
    const inputInstance = defaultWrapper.find(TextField).prop("onChange")(testEvent);

    expect(mockOnChange).toHaveBeenCalledWith(testEvent);
  });
});

describe("validateValue", () => {
  const validateMock = jest.fn();
  Joi.validate = validateMock;
  it("callWithValue&Schema&JoiReturnTrue__expectTrue", () => {
    validateMock.mockReturnValue(true);
    const testValue = "test";
    const testSchema = "testSchema";
    
    const isValidate = defaultWrapper.instance().validateValue(testValue, testSchema);

    expect(isValidate).toBe(true);
  });

  it("callWithValue&Schema&JoiReturnfalse__expectTrue", () => {
    validateMock.mockReturnValue(false);
    const testValue = "test";
    const testSchema = "testSchema";
    
    const isValidate = defaultWrapper.instance().validateValue(testValue, testSchema);

    expect(isValidate).toBe(false);
  });  
});

describe("handleValidate", () => {
  it("callWithNoSchema__expectUndefined", () => {
    defaultWrapper.setProps({ ...defaultProps, schema: null });
    const isValidate = defaultWrapper.instance().handleValidate();

    expect(isValidate).toBeFalsy();
  });

  it("callWithSchema&validateValue()ReturnError__expectUndefined", () => {
    const wrapper = shallow(<FormInput {...defaultProps} />);
    const instance = wrapper.instance();
    const validateMock = jest.fn();
    instance.validateValue = validateMock.mockReturnValue({ error: { details: [{message: "error test"}] }});
    instance.forceUpdate();

    const isValidate = instance.handleValidate();

    expect(validateMock).toHaveBeenCalled();
    expect(wrapper.state('error')).toBe("error test");
    expect(isValidate).toStrictEqual({ details: [{message: "error test"}] });
  });

  it("callWithSchema&validateValue()ReturNull__expectNull", () => {
    const wrapper = shallow(<FormInput {...defaultProps} />);
    const instance = wrapper.instance();
    const validateMock = jest.fn();
    instance.validateValue = validateMock.mockReturnValue({ error: null });
    instance.forceUpdate();

    const isValidate = instance.handleValidate();

    expect(validateMock).toHaveBeenCalled();
    expect(wrapper.state('error')).toBe(null);
    expect(isValidate).toStrictEqual(null);
  });
});

describe("handleClear", () => {
  it("calledWithNoSchema__expectReturnUndefined", () => {
    let props = { ...defaultProps, schema: undefined };
    const wrapper = shallow(<FormInput {...props} />);
    const instance = wrapper.instance();
    instance.forceUpdate();

    const result = instance.handleClear();

    expect(mockOnChange).toHaveBeenCalledWith({ target: { value: "" }});
    expect(result).toBe(undefined);
  });

  it("calledWithSchema&validateValue()ReturnFalse__expectvalidateValue()Called&handleSetError()Called", () => {
    const instance = shallow(<FormInput {...defaultProps}/>).instance();
    const validateValueMock = instance.validateValue = jest.fn().mockReturnValue(false);
    const handlelSetErrorSpy = jest.spyOn(instance, "handleSetError");
    instance.forceUpdate();

    instance.handleClear();

    expect(mockOnChange).toHaveBeenCalledWith({ target: { value: "" }});
    expect(validateValueMock).toHaveBeenCalledWith("", defaultProps.schema);
    expect(handlelSetErrorSpy).toHaveBeenCalledWith(false);
  });
});

