import React, { useState, useEffect, createContext } from "react";
import { GetUser, isAuthenticated } from "../services/AuthService";

import Login from "../pages/Login/Login";
import { User } from "../types/_common/login/login";
import { Props } from "../types/_common/props";
import AlertDialog from "../components/AlertDialog/AlertDialog";
//全系統使用Context(使用者資訊)
const UserContext = createContext({});

export const UserProvider = ({ children }: Props) => {
    //登入資料
    const [currentUser, setCurrentUser] = useState<User | null>(GetUser());

    const [ShowMessage, setShowMessage] = useState("");
    const [WindowOpen, setWindowOpen] = useState(false);

    // 第一次載入執行
    useEffect(() => {
      const checkLoggedIn = async () => {
        let cuser: User | null = isAuthenticated();

        if (cuser === null || cuser.Token === "") {
          cuser = null;
        }
        setCurrentUser(cuser);
      };

      checkLoggedIn();
    }, []);

    // 當值變更執行
    useEffect(() => {
      console.log("currentUser=");
      console.log(currentUser);

      // 登出
      if (!currentUser) {
        localStorage.removeItem("user");
        if (ShowMessage !== "") {
          setWindowOpen(true);
        }
      }
    });

    return (
      <UserContext.Provider
        value={[currentUser, setCurrentUser, setShowMessage]}
      >
        {currentUser ? children : <Login />}
        <AlertDialog
          message={ShowMessage}
          caption={""}
          Open={WindowOpen}
          SetOpen={setWindowOpen}
          IsCancel={false}
          ClickConfirm={() => {
            setCurrentUser(null);
          }}
        />
      </UserContext.Provider>
    );
  
};

export default UserContext;
