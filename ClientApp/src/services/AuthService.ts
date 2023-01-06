import { BasicResponse } from "../types/_common/basic/http";
// 登入方法(API)
export const login = async (username: string, password: string, remember: boolean) => {

	const response = await fetch(`/API/Login/LoginAuth`,
		{
			method: 'POST',
			headers: {
				"Content-Type": "application/json"
			},
			body: JSON.stringify({
				UserNo: username,
				UserPwd: password,
				Remember:remember
			})
		}
	);
	const data: BasicResponse = await response.json();
	return data;
};

const userKey='user'
// 確認是否登入
export const isAuthenticated = () => {
	const user = localStorage.getItem(userKey);
	if (!user) {
		return null
	}
	return JSON.parse(user);
};

//設定登入資訊
export const SetUser = (user:string) => {
	localStorage.setItem(userKey,user);
};
//取得登入資訊
export const GetUser = () => {
	let str=localStorage.getItem(userKey);
	if(str!==null)
		return JSON.parse(str);
	return null;
};
//清除登入資訊
export const ClearUser = () => {
	localStorage.removeItem(userKey);
};

