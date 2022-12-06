import { BasicResponse } from "../types/_common/basic/http";

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
	const data: BasicResponse = await response.json();
	//console.log(data);
	//if (data.ResponseNo=='0000') {
	//	const res: any = data.ResponseData;
	//	console.log(res);
	//	const token = res.Token;
	//	if (token) {
	//		localStorage.setItem('user', JSON.stringify(token));
	//	}
	//}
	return data;
};

export const isAuthenticated = () => {
	const user = localStorage.getItem('user');
	if (!user) {
		return {}
	}
	return JSON.parse(user);
};
