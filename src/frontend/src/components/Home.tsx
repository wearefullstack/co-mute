
import '../css/home.css'
import {Tabs} from 'antd';
import HomeTab from './tabs/Home';
import CreatedCPOs from './tabs/CreatedCPOs';
import ProfileTab from './tabs/Profile';
import { useParams } from 'react-router-dom';

const items = [
    { label: 'Home', key: 'home', children: <HomeTab/> },
    { label: 'Created Car Pools', key: 'created_car_pools', children: <CreatedCPOs/> },
    { label: 'Profile', key: 'profile', children: <ProfileTab/> },
  ];
 

export default
function Home(){
    const { id } = useParams();

    return (
        <div className="main-container">
            <div className="col" style={{ width: "100%", height: "100%"}}>
            <div className="row header">
                <h1 style={{ alignSelf: "start", padding: 0, margin: 0, fontFamily: "'Anton', sans-serif"}}>CO-MUTE</h1>
            </div>
            <Tabs color='red'  style={{ backgroundColor: "#d9d9f8", height: "100%", color: "#ffffff"}} items={items} />;
            </div>
        </div>
    )
}