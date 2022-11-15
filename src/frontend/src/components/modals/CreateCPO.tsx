import { InfoCircleOutlined } from '@ant-design/icons';
import { Button, Form, Input, InputNumber, Radio, TimePicker, notification} from 'antd';
import TextArea from 'antd/lib/input/TextArea';
import React, { useState } from 'react';
import APIManager from '../../Managers/APIManager';
import { IError } from '../../Utils';
import DayPicker from '../DayPicker';


interface IForm {
  origin: string,
  departure_time: any,
  destination: string,
  expected_arrival_time: string
  days_available: string[],
  available_seats: number,
  notes: string
}

const EMPTY_FORM: IForm = {
  origin: "", departure_time: "", destination: "", expected_arrival_time: "", days_available: [], available_seats: 1, notes: ""
}


export default
function CreateCPO({ onCreate}: any){
  const [form, setForm] = useState<IForm>(EMPTY_FORM);
  const [errors, setErrors] = useState<IError<IForm>>({ });
  const [isLoading, setIsLoading]=  useState(false);

  function onTextChange(key: keyof IForm){
    return ({ target: { value }}: any) => {
      console.log(key, value);
      setForm((_form) => ({..._form, [key]: value }));
    }
  }


  function onTimeChange(key: keyof IForm){
    return (value: any) => {
      console.log(key, value.format("HH:mm:ss"));
      setForm((_form) => ({..._form, [key]: value?.format("HH:mm:ss") || "" }));
    }
  }

  function onInputChange(key: keyof IForm){
    return (value: any) => {
      console.log(key, value);
      setForm((_form) => ({..._form, [key]: value }));
    }
  }

  function createCPO(){
    if(isLoading) return;

    const _errors = validateForm(form);
    if(!_errors){
      setIsLoading(true);
      APIManager.getInstance()
      .createCPO(form)
      .then(result => onCreate(result.result))
      .catch(error => {
        console.log("....", error);
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
      <Form.Item label="Origin" required validateStatus={ errors.origin && "error" } help={ errors.origin }>
        <Input placeholder="e.g CBD Durban" onChange={onTextChange("origin")} />
      </Form.Item>
      <Form.Item required label="Departure Time"  validateStatus={ errors.departure_time && "error" } help={ errors.departure_time }>
        <TimePicker onChange={onTimeChange("departure_time")}  />
      </Form.Item>
      <Form.Item label="Destination" required validateStatus={ errors.destination && "error" } help={ errors.destination }>
        <Input placeholder="e.g Glenwood Durban"  onChange={onTextChange("destination")}   />
      </Form.Item>
      <Form.Item required label="Expected Arrival Time" validateStatus={ errors.expected_arrival_time && "error" } help={ errors.expected_arrival_time }>
        <TimePicker onChange={onTimeChange("expected_arrival_time")}  />
      </Form.Item>
      <Form.Item required label="Days Available" validateStatus={ errors.days_available && "error" } help={ errors.days_available }>
        <DayPicker   selectedDays={form.days_available} onChange={onInputChange("days_available")}/>
      </Form.Item>
      <Form.Item label="Available Seats" validateStatus={ errors.available_seats && "error" } help={ errors.available_seats }>
        <InputNumber min={1}  value={form.available_seats} onChange={onInputChange("available_seats")} />
      </Form.Item>
      <Form.Item label="Notes" validateStatus={ errors.notes && "error" } help={ errors.notes }>
        <TextArea rows={4} placeholder="notes" onChange={onTextChange("notes")} />
      </Form.Item>
      <Form.Item>
        <Button loading={ isLoading } type="primary" onClick={createCPO}>Submit</Button>
      </Form.Item>
    </Form>
  );
};


function validateForm(form: IForm): IError<IForm> | null{
  const error: IError<IForm> = {};

  if(form.origin.length ===0) error.origin = "required";
  if(form.departure_time.length ===0) error.departure_time = "required";
  if(form.destination.length ===0) error.destination = "required";
  if(form.expected_arrival_time.length ===0) error.expected_arrival_time = "required";
  if(form.days_available.length ===0) error.origin = "required";
 
  return Object.keys(error).length === 0? null : error;
}