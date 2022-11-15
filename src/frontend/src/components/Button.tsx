


export default
function Button({ title, onClick, style={} }: any){

    return (
        <h2 style={ style} className="btn" onClick={onClick}>
            { title }
        </h2>
    )
}