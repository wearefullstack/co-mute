import { InfoCircleOutlined } from '@ant-design/icons';
import { Button, Form, Input, InputNumber, Radio, TimePicker } from 'antd';
import TextArea from 'antd/lib/input/TextArea';
import React, { useState } from 'react';
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
function CreateCPO(){
  const [form, setForm] = useState<IForm>(EMPTY_FORM);
  const [errors, setErrors] = useState<IError<IForm>>({ origin:  "sasa"});

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
        <InputNumber min={1}  defaultValue={3} onChange={onInputChange("available_seats")} />
      </Form.Item>
      <Form.Item label="Notes" validateStatus={ errors.notes && "error" } help={ errors.notes }>
        <TextArea rows={4} placeholder="notes" onChange={onTextChange("notes")} />
      </Form.Item>
      <Form.Item>
        <Button type="primary">Submit</Button>
      </Form.Item>
    </Form>
  );
};