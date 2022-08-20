
import React, { Component } from 'react'

export const UserContext = React.createContext()

export default class UserContextProvider extends Component {
	state = {
		firstName: '',
		lastName: '',
		email: '',
		password: ''
	}

	// handleUserChange = (event) => {
	// 	const { value: todo } = event.target
	// 	//console.info('handleUserChange todo', todo)
	// 	this.setState({ todo })
	// }

	setUser = (userInfo) => {
		this.setState(userInfo)
	}

	render() {
		const { handleUserChange, setUser } = this
		return <UserContext.Provider value={{ ...this.state, setUser }}>{this.props.children}</UserContext.Provider>
	}
}