
import '../css/home.css'
import {Tabs} from 'antd';
import HomeTab from './tabs/Home';
import CreatedCPOs from './tabs/CreatedCPOs';
import ProfileTab from './tabs/Profile';

const items = [
    { label: 'Home', key: 'item-1', children: <HomeTab/> }, // remember to pass the key prop
    { label: 'Created Car Pools', key: 'item-2', children: <CreatedCPOs/> },
    { label: 'Profile', key: 'item-3', children: <ProfileTab/> },
  ];
 

export default
function Home(){
    //let { id } = useParma();

    return (
        <div className="main-container">
            <div className="col" style={{ width: "100%", height: "100%"}}>
            <div className="row header">
                <h1 style={{ alignSelf: "start"}}>CO-MUTE</h1>
            </div>
            <Tabs style={{ backgroundColor: "#dcd7ff", height: "100%"}} items={items} />;
            </div>
        </div>
    )
}