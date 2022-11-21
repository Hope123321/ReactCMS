
export const login = async (username: string, password: string) => {

	const response = await fetch(`/Login`,
		{
			method: 'POST',
			headers: {
				"Content-Type": "application/json"
			},
			body: JSON.stringify({
				UserNo: username,
				UserPwd: password
			})
		}
	);
	const data = await response.json();
	console.log(data);

	const token = data.token;
	if (token) {
		localStorage.setItem('user', JSON.stringify(data));
	}

	return data;
};

export const isAuthenticated = () => {
	const user = localStorage.getItem('user');
	if (!user) {
		return {}
	}
	return JSON.parse(user);
};
