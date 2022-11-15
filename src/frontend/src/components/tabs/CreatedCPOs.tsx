import { List, Modal } from "antd";
import { useEffect, useState } from "react";
import APIManager from "../../Managers/APIManager";
import Button from "../Button";
import Divider from "../Divider";
import CPO from "../ListItems/CPO";
import CreateCPO from "../modals/CreateCPO";


export default
function CreatedCPOs(){
    const [isLoading, setLoading] = useState(false);
    const [ showCreator, setShowCreator ] = useState(false);
    const [ CPOs, setCPOs] = useState<any[]>([]);

    function onCreateCPO(cpo: any) {
        setShowCreator(false);
        cpo.joined_users = 0;
        setCPOs((cpos) => ([...cpos, cpo]));
    }

    useEffect(() => {
        setLoading(true);
        APIManager.getInstance()
        .getCreatedCPOs()
        .then(results => {
            console.log(":::", results.result);
            setCPOs(results.result);
        })
        .catch(error => {})
        .finally(()=>{ setLoading(false) });
    }, []);

    return <div className="tab-container">
        <Button onClick={()=> setShowCreator(true)} title="Create"/>
        <Divider/>
        <List grid={{ gutter: 16, column: 4 }} dataSource={CPOs} renderItem={renderItem}/>
        <Modal title="Basic Modal" open={showCreator}  onCancel={()=> setShowCreator(false)} footer={<></>}>
            <CreateCPO onCreate={ onCreateCPO }/>
        </Modal>
        </div>
}

function renderItem(item: any){
    return (
        <List.Item>
            <CPO item={ item } />
      </List.Item>
    )
}