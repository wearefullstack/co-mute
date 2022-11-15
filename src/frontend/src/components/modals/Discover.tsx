import { Input, List } from "antd";
import { useEffect, useState } from "react";
import APIManager from "../../Managers/APIManager";
import Button from "../Button";
import CPO from "../ListItems/CPO";


export default
function Discover(){
    const [ searchTerm, setSearchTerm] = useState("");
    const [ CPOs, setCPOs] = useState<any[]>([]);
    const [ isLoading, setIsLoading ] = useState(false);

    function search(){
        setIsLoading(true);

        APIManager.getInstance()
        .searchCPOs(searchTerm)
        .then(({ result }) => {
            setCPOs(result);
            console.log("::R", result)
        })
        .catch(error => { console.log("::E", error) })
        .finally(() => setIsLoading(false))
    }

    function onTextChange({ target : { value }}: any){
        setSearchTerm(value);
    }


    useEffect(() => {
        search();
    },[])

    
    
    return (
    <div>
        <div className="row">
            <Input style={{ marginRight: 8}} onChange={ onTextChange } value={ searchTerm} /><Button title="Search" onClick={ search }/>
        </div>
        <List loading={ isLoading } grid={{ gutter: 16 }} dataSource={CPOs} renderItem={renderItem}/>

    </div>
    )
}

function renderItem(item: any){
    return (
        <List.Item>
            <CPO item={ item }/>
        </List.Item>
    )
}