import "../../css/cpo.css"
import { EnvironmentOutlined } from '@ant-design/icons';
import DayPicker from "../DayPicker";
import { Card, Popconfirm } from "antd";
import { useState } from "react";
import APIManager from "../../Managers/APIManager";

export default
function CPO({ onClickAction, item, canLeave = true, _onLeave }: any){
    const [{joined, days_available, notes, origin, destination, departure_time, expected_arrival_time, available_seats, joined_users, date_created}, setItem] = useState(item)

    const date = new Date(date_created).toLocaleDateString("en-US");
    const selectedDays = days_available.split(",");


    function onLeave(){
        setItem((i : any) => ({...i, joined: "false", joined_users: joined_users - 1}));
         _onLeave && _onLeave();
    }

    
    const action = canLeave ? [ <Action onLeave={ onLeave } joined={ joined } cpo_id={ item.id } onClick={() => {
      onClickAction(joined)
    }}/>] : [];
    
    return (
        <Card actions={action} >
            <Location title="Origin" info={ origin } time={ departure_time}/>
            <Location title="Destination" info={destination} time={ expected_arrival_time }/>
            <DayPicker selectedDays={selectedDays}/>
            <Info title="Seats" info={`${ joined_users} /${ available_seats }`}/>
            <Info title="Created" info= {date}/>
            <Info title="Notes" info={notes}/>

        </Card>
    )
}

function Location({ title,  info, time }: any){
    return <div className="location-container">
        <h2 className="time">{ time }</h2>
        <div className="row" style={{  width: "100%", alignItems: "center"}}>
            <EnvironmentOutlined style={{ fontSize: "1.5em", color: "#a0c4f1"}}/>
            <div className="simple-col  info-bar">
            <h4 className="loc-header">{title}</h4>
            <h6  className="loc-info" >{ info }</h6>

            </div>
        </div>      
    </div>
}

function Info({ title,  info }: any){
    return  (
              <div className="simple-row  info-bar">
            <h6  className="info-info" >{ info }</h6>
            <h4 className="info-header">{title}</h4>

          
     </div>
    );
}


function Action({ joined, cpo_id, onClick, onLeave }: any){
    const [open, setOpen] = useState(false);
    const [confirmLoading, setConfirmLoading] = useState(false);
  
    const showPopconfirm = () => {
        console.log("AAAAA");
    if(joined !== "true"){
        onClick()
    }else
      setOpen(true);
    };
  
    const handleOk = () => {
      setConfirmLoading(true);
  
      APIManager.getInstance()
      .leave(cpo_id)
      .then(()=>{
        setOpen(false);
        setConfirmLoading(false);
        onLeave();
      })
      .catch(e => {});
    };
  
    const handleCancel = () => {
      setOpen(false);
    };
  
    return (
      <Popconfirm
        title="Are you sure that you want to this Car Pool?"
        open={open}
        onConfirm={handleOk}
        okButtonProps={{ loading: confirmLoading }}
        onCancel={handleCancel}
      >
        <h4 onClick={ showPopconfirm }>{(joined == "false")? "Join" : "Leave"}</h4>

      </Popconfirm>
    );
  };