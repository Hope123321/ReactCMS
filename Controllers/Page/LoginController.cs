using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReactCMS.ClassLibrary;
using ReactCMS.Helpers;
using ReactCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ReactCMS.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
                string encryptUserPWD = _utillity.SHA256Encode(source.UserPwd);
                //呼叫API取得人員資料
                res = SysUserLogin(source.UserNo, encryptUserPWD);

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
                        view.Token= _jwtHelpers.GenerateToken(view.UserNo);
                        //回傳給前台讓前台選取需要顯示的資料
                        res.ResponseData = view;
                    }
                }
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

            Utility u = new Utility();

            RequestBody req = new RequestBody();
            LoginRequestViewModel data = new LoginRequestViewModel();
            data.UserNo = UserNo;
            data.UserPwd = UserPwd;
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

            return resVW;
        }

        #endregion
    }
}
