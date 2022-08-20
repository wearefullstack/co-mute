import React, {Component} from 'react';
import 'antd/dist/antd.css';
import './index.css';
import { Table } from 'antd';
import { Button } from 'antd';
import { UserContext } from './UserContext';


// import 'react-table/react-table.css';

class CarList extends Component{
    constructor(props){
        super(props);
        this.state = { cars: [] };
        this.commute = {};
    }

    static contextType=UserContext


     teste = (item) =>  {
        console.log("I am here", item);
        const user = this.context
        console.log('carlistuser', user)
        const req = {email: user.email, carpool: item}
        console.log('req', req)
    fetch('http://localhost:8080/joinpool', {
    method: 'POST', // or 'PUT'
    headers: {
        'Content-Type': 'application/json',
    },
    body: JSON.stringify(req),
    })
    .then((response) => response.json())
    .then((data) => {
    console.log('Success:', data);
    })
    .catch((error) => {
    console.error('Error:', error);
    });
        }
    componentDidMount() {


        fetch('http://localhost:8080/' + 'carpools')
            .then((response) => response.json())
            .then((responseData) =>  {
            console.log('cars  responseData:', responseData);
                
                this.setState({

                    cars: responseData
                });
                console.log(responseData)
            })
            .catch(err => console.error(err));

        console.log('cars  this.setState:', this.state);

    }

    render(){

        const test = () => {
            console.log("I am here");
        }
            
        const columns = [
            {
              title: 'Origin',
              dataIndex: 'origin',
              key: 'origin',
            },
            {
              title: 'DepartureTime',
              dataIndex: 'departureTime',
              key: 'departureTime',
            },
            {
              title: 'Destination',
              dataIndex: 'destination',
              key: 'destination',
            },
            {
              title: 'ExpectedArrivalTime',
              dataIndex: 'expectedArrivalTime',
              key: 'expectedArrivalTime',
            },
            {
              title: 'Available Seats',
              dataIndex: 'availableSeats',
              key: 'availableSeats',
            },
            {
                title: "Owner",
                dataIndex: ['owner', 'firstname'],
                // key:'owner',
                
            },
            {
                render: (record) => <Button onClick={() => this.teste(record)} type="primary">Join Carpool</Button>,
                
            }
            
          ];
          
        // const tableRows = this.state.cars.map((car, index) =>
        // <tr key={index}>
        //
        // </tr>
        return(
            <div className="App">
           <Table
    columns={columns}
    expandable={{
      expandedRowRender: (record) => (
        <p
          style={{
            margin: 0,
          }}
        >
          {record.description}
        </p>
      ),
      rowExpandable: (record) => record.name !== 'Not Expandable',
    }}
    dataSource={this.state.cars}
  />
            </div>
        );
    }
}
export default CarList;