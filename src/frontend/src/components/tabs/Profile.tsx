import { Form, Input, message, Space } from "antd";
import { useEffect, useRef, useState } from "react";
import { useNavigate } from 'react-router-dom'
import APIManager from "../../Managers/APIManager";
import UserManager from "../../Managers/UserManager";
import Button from "../Button";
import Divider from "../Divider";
import Labeled from "../Labeled";

interface IForm {
    name: string,
    phone: string,
    surname: string,
}


export default
function ProfileTab(){
    const [ updates, setUpdates ] = useState<IForm>({ name: "", surname: "", phone: ""});
    const updatesMeta = useRef(0);
    const navigate = useNavigate();
 
    function onChange(key: keyof IForm){
        return (({ target: { value }}: any)=> {
            updatesMeta.current++;
            setUpdates(_updates => ({..._updates, [key]: value }));
        })
    }

    function update(){
        const finalUpdates: any = {};

        if(updates.name.length > 0) finalUpdates.name = updates.name;
        if(updates.surname.length > 0) finalUpdates.surname = updates.surname;
        if(updates.phone.length > 0) finalUpdates.phone = updates.phone;

        APIManager.getInstance()
        .updateUser(finalUpdates)
        .then(({ result }) => {
            UserManager.getInstance().setActiveUser(result);
            message.success("Profile Updated");
            updatesMeta.current = 0;
        })
        .catch(error => {})
        

    }

    function logout(){
        UserManager.getInstance()
        .removeActiveUser();
        navigate("/");
    }

    useEffect(()=>{
        const user = UserManager.getInstance().getActiveUser();
         if(user){
            setUpdates({ name: user.name, surname: user.surname, phone: user.phone || ""})
         }else{
            logout();
         }
    }, []);

    const user = UserManager.getInstance().getActiveUser();
    const joined = user ? (new Date(user.date_created || "").toLocaleDateString("en-US")) : "";

    const canUpdate = updatesMeta.current > 0;

    return (
        <div className="tab-container col" style={{ width: "100%", alignItems: "center"}}>
             <div className="col" style={{ maxWidth: "500px"}}>
                <div className="text-logo">
                    <h4 >A</h4>
                </div>
            <Labeled title="Name">
                <Input placeholder="name" value={updates.name} onChange={onChange("name")}/>
            </Labeled>
            <Labeled title="Surname">
                <Input placeholder="surname" value={updates.surname} onChange={onChange("surname")}/>
            </Labeled>
            <Labeled title="Phone">
                <Input placeholder="name" value={updates.phone} onChange={onChange("phone")} />
            </Labeled>
            <Labeled title="Email Address">
                <h4 style={{ textAlign: "start"}}>{user?.email}</h4>
            </Labeled>
            <Labeled title="Joined">
                <h4 style={{ textAlign: "start"}}>{joined }</h4>
            </Labeled>
            <Divider/>
            <div className="row">
            <Button title="Logout" onClick={ logout }/>
            <Button title="Update" onClick={ update }/>
            </div>
            
            </div>
        </div>
    )
}