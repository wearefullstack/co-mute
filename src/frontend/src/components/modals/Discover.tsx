import { Input, List, Modal } from "antd";
import { useEffect, useState } from "react";
import APIManager from "../../Managers/APIManager";
import { handleSystemError } from "../../Utils";
import Button from "../Button";
import CPO from "../ListItems/CPO";
import JoinCPO from "./Join";


export default
function Discover({ onJoinCPO}: any){
    const [ searchTerm, setSearchTerm] = useState("");
    const [ CPOs, setCPOs] = useState<any[]>([]);
    const [ joinCPO, setJoinCPO] = useState<any | null>(null);
    const [ isLoading, setIsLoading ] = useState(false);

    function search(){
        setIsLoading(true);

        APIManager.getInstance()
        .searchCPOs(searchTerm)
        .then(({ result }) => {
            setCPOs(result);
        })
        .catch(handleSystemError)
        .finally(() => setIsLoading(false))
    }

    function onTextChange({ target : { value }}: any){
        setSearchTerm(value);
    }



    function onClickAction(item: any){
        setJoinCPO(item);
    };

    useEffect(() => {
        search();
    },[])

    function onCreate(cpo: any){
        setJoinCPO(null);
        onJoinCPO(cpo)
    }

    console.log(joinCPO);
    
    return (
    <div>
        <div className="row">
            <Input style={{ marginRight: 8}} onChange={ onTextChange } value={ searchTerm} /><Button title="Search" onClick={ search }/>
        </div>
        <List loading={ isLoading } grid={{ gutter: 16 }} dataSource={CPOs} renderItem={renderItem(onClickAction)}/>
        <Modal title="Basic Modal" open={joinCPO !== null}  onCancel={()=> setJoinCPO(null)} footer={<></>}>
            <JoinCPO validDays={joinCPO?.days_available.split(",")} onCreate={ onCreate } cpo_id={ joinCPO?.id }/>
        </Modal>

    </div>
    )
}

function renderItem(onClickAction: any){
    return (item: any) => {
        return (
            <List.Item>
                <CPO item={ item } onClickAction={ (joined: any) => onClickAction(item, joined) }/>
            </List.Item>
        )
    }
}