
// export default function LoginPage(){
//     const[email, setEmail] = use('')
//     const [password, setPassword] = useState('')

//     const data ={email: email, password: password};

//     const handleSubmit = e =>{
//         e.preventDefault();
    
    
//         fetch('http://localhost:8080/login', {
//             method: 'GET',
//             headers:{
//                 'Content-Type': 'application/json',
//             },
//             body:JSON.stringify(data),
//         })
//         .then((response) => response.json())
//         .then((data) => {
//             console.log('Logged In');
//         })
//         .catch((error) => {
//             console.error('Error', error);
//         });
//         navigate('/carpool');

//     }

//       return (
//     <form onSubmit={handleSubmit} >
//       <label>email</label>
//       <input
//         onChange={e => setFirstName(e.target.value)}
//         value={firstName}/><br/>
//       <label>password </label>
//       <input
//         onChange={e => setLastName(e.target.value)}
//         value={lastName}/><br/>
//       <label>Email </label>
//       <input
//         onChange={e => setEmail(e.target.value)}
//         value={email}/><br/>
//         <label>password </label>
//       <input
//         onChange={e => setPassword(e.target.value)}
//         value={password}/><br/>
//       <input type="submit" value="Press me"/>
//       <input type="cancel" value="Cancel me"/>
//     </form>
    
//   );

// }
