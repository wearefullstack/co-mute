import { Modal } from "antd";
import { useState } from "react";
import Button from "../Button";
import Divider from "../Divider";
import CreateCPO from "../modals/CreateCPO";


export default
function CreatedCPOs(){
    const [ showCreator, setShowCreator ] = useState(false);

    return <div className="tab-container">
        <Button onClick={()=> setShowCreator(true)} title="Create"/>
        <Divider/>
        <Modal title="Basic Modal" open={showCreator}  onCancel={()=> setShowCreator(false)} footer={<></>}>
            <CreateCPO/>
      </Modal>
        </div>
}