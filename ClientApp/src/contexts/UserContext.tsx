import React, { useState, useEffect, createContext } from 'react';
import { isAuthenticated } from '../services/AuthService';

import Login from '../pages/Login/Login';
import { User } from '../types/_common/login/login';
import { Props } from '../types/_common/props';

const UserContext = createContext({});

export const UserProvider = ({ children }: Props) => {
	try {
		const [currentUser, setCurrentUser] = useState<User | null>(null);

		useEffect(() => {
			const checkLoggedIn = async () => {
				let cuser:User | null = isAuthenticated();
				console.log('cuser', cuser);
				console.log(cuser.Token);
				if (cuser === null || cuser.Token === undefined) {
					console.log('empty!');
					localStorage.removeItem('user');
					cuser = null;
				}

				setCurrentUser(cuser);
			};

			checkLoggedIn();
		}, []);

		console.log('usercontext', currentUser);

		return (
			<UserContext.Provider value={[currentUser, setCurrentUser]}>
				{currentUser ? children : <Login />}
			</UserContext.Provider>
		);
	} catch (ex) {
		console.error(ex);
	}
};


export default UserContext;
