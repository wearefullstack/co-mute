import "../../css/cpo.css"
import { EnvironmentOutlined } from '@ant-design/icons';
import DayPicker from "../DayPicker";

export default
function CPO({ item: {days_available, notes, origin, destination, departure_time, expected_arrival_time, available_seats, joined_users, date_created} }: any){

    const date = new Date(date_created).toLocaleDateString("en-US");
    const selectedDays = days_available.split(",");
    return (
        <div className="col card">
            <Location title="Origin" info={ origin } time={ departure_time}/>
            <Location title="Destination" info={destination} time={ expected_arrival_time }/>
            <DayPicker selectedDays={selectedDays}/>
            <Info title="Seats" info={`${ joined_users} /${ available_seats }`}/>
            <Info title="Created" info= {date}/>
            <Info title="Notes" info={notes}/>

        </div>
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