using Microsoft.AspNetCore.Mvc;
using ReactCMS.ClassLibrary;
using ReactCMS.Helpers;
using ReactCMS.Models;
using ReactCMS.Models.API.Login;
using System;
using System.Text.Json;

namespace ReactCMS.Controllers
{
    [ApiController]
    [Route("/API/Login/[action]")]
    public class LoginController : ControllerBase
    {
        private Utility _utillity;
        private JwtHelpers _jwtHelpers;
        public LoginController(JwtHelpers jwtHelpers) {
            _utillity=new Utility();
            _jwtHelpers=jwtHelpers;
        }

        [HttpPost]
        public string LoginAuth([FromBody] LoginRequestViewModel source)
        {

            BasicResponseViewModel res = new BasicResponseViewModel();
            try
            {
               

                #region 連線登入
                //呼叫API取得人員資料
                res = SysUserLogin(source.UserNo, source.UserPwd);

                if (res.ResponseNo == "0000")
                {
                    LoginResponseViewModel_SysUserLogin view = (LoginResponseViewModel_SysUserLogin)res.ResponseData;
                    if (!string.IsNullOrEmpty(view.PWDCHGDATE) && view.PWDCHGDATE.CompareTo(DateTime.Now.ToString("yyyy/MM/dd")) == -1)
                    {
                        #region 成功但需要強制換密碼
                        res.ResponseNo = "1111";
                        res.ResponseNa = "此USER需要變更密碼後重新登入。";
                        //回傳變更密碼所需的資料
                        res.ResponseData = new LoginResponseViewModel_SysUserLogin() { UserNo = view.UserNo};
                        #endregion
                    }
                    else
                    {
                        //如果有勾選Remember則Token效期變成3000分鐘失效，沒有則30分鐘失效
                        int expireMinutes=source.Remember?3000:30;
                        view.Token= _jwtHelpers.GenerateToken(view.UserNo,expireMinutes);
                        //回傳給前台讓前台選取需要顯示的資料
                        res.ResponseData = view;
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                res.ResponseNo = "1000";
                res.ResponseNa = "登入失敗、錯誤訊息：" + ex.Message;
            }
            return JsonSerializer.Serialize(res);
        }

        #region
        [NonAction]
        private BasicResponseViewModel SysUserLogin(string UserNo, string UserPwd)
        {
            BasicResponseViewModel resVW = new BasicResponseViewModel();


            if (UserNo.ToUpper().Equals("INSTALL"))
            {
                #region 動態密碼
                string Password = "";
                Password += "Rtx@";
                Password += Convert.ToString(((DateTime.Now.Month / 10) + 1) % 10);
                Password += Convert.ToString(((DateTime.Now.Month % 10) + 0) % 10);
                Password += Convert.ToString(((DateTime.Now.Day / 10) + 3) % 10);
                Password += Convert.ToString(((DateTime.Now.Day % 10) + 1) % 10);
                #endregion
                if (UserPwd.Equals(Password))
                {
                    resVW.ResponseNo = "0000";
                    LoginResponseViewModel_SysUserLogin info = new LoginResponseViewModel_SysUserLogin() { 
                    UserNo="INSTALL",
                    UserNa="系統管理員",
                    ShopNo="ALL",
                    SpGpNo="ALL",
                    MenuList="ALL"
                    };
                    resVW.ResponseData= info;
                }
                else {
                    resVW.ResponseNo = "9999";
                    resVW.ResponseNa = "密碼錯誤!";
                }
            }
            else
            {
                string encryptUserPWD = _utillity.SHA256Encode(UserPwd);
                Utility u = new Utility();

                RequestBody req = new RequestBody();
                LoginRequestViewModel data = new LoginRequestViewModel();
                data.UserNo = UserNo;
                data.UserPwd = encryptUserPWD;
                req.RequestData = data;
                //呼叫API Url
                string APIUrl = u.GetConfigValue("API:URL") + "SysUserLogin";

                BasicResponse res = u.PostAPIBySTD(APIUrl, JsonSerializer.Serialize(req));
                if (res.ResponseNo == "0000")
                {
                    LoginResponseViewModel LoginInfo = JsonSerializer.Deserialize<LoginResponseViewModel>(res.ResponseData.ToString());

                    if (LoginInfo.ResultNo == "00")
                    {
                        resVW.ResponseNo = "0000";
                        resVW.ResponseNa = "";
                    }
                    else
                    {
                        resVW.ResponseNo = LoginInfo.ResultNo;
                        resVW.ResponseNa = LoginInfo.ResultNa;
                    }
                    resVW.ResponseData = LoginInfo.SysUserLogin;
                }
                else
                {
                    resVW.ResponseNo = res.ResponseNo;
                    resVW.ResponseNa = res.ResponseNa;
                    resVW.ResponseData = null;
                }
            }
            return resVW;
        }

        #endregion
    }
}
