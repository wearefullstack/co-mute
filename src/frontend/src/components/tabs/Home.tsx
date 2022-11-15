import { Drawer, List, Modal } from "antd";
import { useEffect, useState } from "react";
import APIManager from "../../Managers/APIManager";
import Button from "../Button";
import Divider from "../Divider";
import CPO from "../ListItems/CPO";
import CreateCPO from "../modals/CreateCPO";
import Discover from "../modals/Discover";


export default
function Home(){
    const [isLoading, setLoading] = useState(false);
    const [ showSearcher, setShowSearcher ] = useState(false);
    const [ CPOs, setCPOs] = useState<any[]>([]);

    function onJoinCPO(cpo: any) {
        setShowSearcher(false);
        cpo.joined = true;
        setCPOs((cpos) => ([...cpos, cpo]));
    }

    useEffect(() => {
        setLoading(true);
        APIManager.getInstance()
        .getJoinedCPO()
        .then(results => {
            console.log(":::", results.result);
            setCPOs(results.result);
        })
        .catch(error => {})
        .finally(()=>{ setLoading(false) });
    }, []);

    return <div className="tab-container">
        <Button onClick={()=> setShowSearcher(true)} title="Join/Discover"/>
        <Divider/>
        <List grid={{ gutter: 16, column: 4 }} dataSource={CPOs} renderItem={renderItem}/>
        <Drawer title="Basic Drawer" placement="right" onClose={()=> setShowSearcher(false)} open={showSearcher}>
            <Discover onJoinCPO={ onJoinCPO }/>
</Drawer>
        </div>
}

function renderItem(item: any){
    return (
        <List.Item>
            <CPO item={ item } />
      </List.Item>
    )
}


 

