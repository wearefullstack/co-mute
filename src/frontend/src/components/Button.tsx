


export default
function Button({ title, onClick }: any){

    return (
        <h2 className="btn" onClick={onClick}>
            { title }
        </h2>
    )
}