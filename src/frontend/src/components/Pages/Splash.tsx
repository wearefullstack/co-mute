import { Space, Spin } from 'antd';
import { LoadingOutlined } from '@ant-design/icons';
import { useEffect } from 'react';
import UserManager from '../../Managers/UserManager';
import { useNavigate } from 'react-router-dom'
const antIcon = <LoadingOutlined style={{ fontSize: 64, color: "#3d3d3d" }} spin />;



export default
function Splash(){
      let navigate = useNavigate();


    useEffect(()=> {
        const user = UserManager.getInstance().getActiveUser();

        if(user){
            navigate("/home")
        }else{
            navigate("/login")
        }
    }, []);
    
    return (
        <div className="main-container">
            <Spin size='large' indicator={antIcon}/>
        </div>
    );
}