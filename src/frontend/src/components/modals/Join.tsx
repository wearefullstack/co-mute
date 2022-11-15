import { InfoCircleOutlined } from '@ant-design/icons';
import { Button, Form, Input, InputNumber, Radio, TimePicker, notification} from 'antd';
import TextArea from 'antd/lib/input/TextArea';
import React, { useState } from 'react';
import APIManager from '../../Managers/APIManager';
import { IError } from '../../Utils';
import DayPicker from '../DayPicker';


interface IForm {
  on_which_days: string[],
}

const EMPTY_FORM: IForm = {
    on_which_days: []
}


export default
function JoinCPO({ onCreate, cpo_id, validDays}: any){
  const [form, setForm] = useState<IForm>(EMPTY_FORM);
  const [errors, setErrors] = useState<IError<IForm>>({ });
  const [isLoading, setIsLoading]=  useState(false);


  function onInputChange(key: keyof IForm){
    return (value: any) => {
      console.log(key, value);
      setForm((_form) => ({..._form, [key]: value }));
    }
  }

  function join(){
    if(isLoading) return;

    const _errors = validateForm(form);
    if(!_errors){
      setIsLoading(true);
      APIManager.getInstance()
      .join(form.on_which_days, cpo_id)
      .then(result => {
        console.log(":R:R", result)
        onCreate(result.result)})
      .catch(error => {
        if(error.statusCode === 403){
          notification.error({
            message: error.message,
            description: "",
            placement: "top",
            duration: 0
          });
        }
      })
      .finally(()=> setIsLoading(false));
    }else{
      setErrors(_errors);
    }
  }

  return (
    <Form style={{ overflowY: "auto", maxHeight: "60vh"}} layout="vertical"  >
      <Form.Item required label="On Which days" validateStatus={ errors.on_which_days && "error" } help={ errors.on_which_days }>
        <DayPicker validDays={validDays}   selectedDays={form.on_which_days} onChange={onInputChange("on_which_days")}/>
      </Form.Item>
      <Form.Item>
        <Button loading={ isLoading } type="primary" onClick={join}>Submit</Button>
      </Form.Item>
    </Form>
  );
};


function validateForm(form: IForm): IError<IForm> | null{
  const error: IError<IForm> = {};

  if(form.on_which_days.length ===0) error.on_which_days = "required";
 
  return Object.keys(error).length === 0? null : error;
}