import React from 'react'
import "../css/labeled.css"


export default
function Labeled({ children, title, error }: any): JSX.Element{

    return (
        <div className='l-container col'>
            <div className='l-container row'>
                <h4 className='title'>{title}</h4><h4 className="error">{error}</h4>
            </div>
            { children }
        </div>

    );
}