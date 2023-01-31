
function Profile(){
    return(
        <div className="text-center">
            <h2 className="font-bold text-3xl">Xavier Julies</h2>
            <span>Marcelinnoj@gmail.com</span>
            <form className="flex flex-col space-y-6 mt-8">
                <input className="border-b-2 border-gray-400 focus:outline-none" type="text" id="name" value="Xavier" />
                <input className="border-b-2 border-gray-400 focus:outline-none" type="text" id="lastname" value="Julies" />
                <input className="border-b-2 border-gray-400 focus:outline-none" type="text" id="phone" value="+27610178337" />
                <input className="border-b-2 border-gray-400 focus:outline-none" type="text" id="email" value="Marcelinnoj@gmail.com" />
                <input className="border-b-2 border-gray-400 focus:outline-none" type="text" id="Passwaord" value="********" />
                <button className="py-3 px-6 bg-green-400 rounded-full hover:bg-green-500 hover:text-white">Update</button>
            </form>
        </div>
    )
}

export default Profile