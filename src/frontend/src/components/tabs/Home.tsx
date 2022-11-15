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
            console.log(results)
            setCPOs(results.result);
        })
        .catch(error => {})
        .finally(()=>{ setLoading(false) });
    }, []);

    function onLeave(pos: number){
        console.log(pos);
        CPOs.splice(0,1);
        setCPOs([...CPOs]);
    }
 
    return <div className="tab-container">
        <Button onClick={()=> setShowSearcher(true)} title="Join/Discover"/>
        <Divider/>
        <List grid={{ gutter: 16, xs: 1,sm: 1, md: 2,lg: 3, xl:4, xxl:4 }} dataSource={CPOs} renderItem={renderItem(onLeave)}/>
        <Drawer title="Basic Drawer" placement="right" onClose={()=> setShowSearcher(false)} open={showSearcher}>
            <Discover onJoinCPO={ onJoinCPO }/>
</Drawer>
        </div>
}

function renderItem(_onLeave: any){

    return (item: any, pos: number) => {
        item.joined = "true";
        return (
            <List.Item>
                <CPO item={ item } _onLeave={()=> _onLeave(pos)}/>
          </List.Item>
        )
    }
   
}


 

