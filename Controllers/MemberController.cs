using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReactCMS.ClassLibrary;
using ReactCMS.Helpers;
using ReactCMS.Models;
using ReactCMS.Models.API.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ReactCMS.Controllers
{
    [ApiController]
    [Route("/API/Member/[action]")]
    public class MemberController : ControllerBase
    {

        private Utility _utillity;
        private JwtHelpers _jwtHelpers;
        public MemberController(JwtHelpers jwtHelpers)
        {
            _utillity = new Utility();
            _jwtHelpers = jwtHelpers;
        }

        [Authorize]
        [HttpGet]
        public string GetProfile()
        {

            BasicResponseViewModel res = new BasicResponseViewModel();
            try
            {
                string userNo = User.Identity.Name;

                res.ResponseNo = "0000";
                res.ResponseData = new GetProfileResponseViewModel()
                {
                    UserNo = userNo,
                    UserNa = userNo,
                    Mobile = "0912345678"
                };
                // string encryptUserPWD = _utillity.SHA256Encode(source.UserPwd);
                // //呼叫API取得人員資料
                // res = SysUserLogin(source.UserNo, encryptUserPWD);

                // if (res.ResponseNo == "0000")
                // {
                //     LoginResponseViewModel_SysUserLogin view = (LoginResponseViewModel_SysUserLogin)res.ResponseData;
                //     if (!string.IsNullOrEmpty(view.PWDCHGDATE) && view.PWDCHGDATE.CompareTo(DateTime.Now.ToString("yyyy/MM/dd")) == -1)
                //     {
                //         #region 成功但需要強制換密碼
                //         res.ResponseNo = "1111";
                //         res.ResponseNa = "此USER需要變更密碼後重新登入。";
                //         //回傳變更密碼所需的資料
                //         res.ResponseData = new LoginResponseViewModel_SysUserLogin() { UserNo = view.UserNo};
                //         #endregion
                //     }
                //     else
                //     {
                //         //如果有勾選Remember則Token效期變成3000分鐘失效，沒有則30分鐘失效
                //         int expireMinutes=source.Remember?3000:30;
                //         view.Token= _jwtHelpers.GenerateToken(view.UserNo,expireMinutes);
                //         //回傳給前台讓前台選取需要顯示的資料
                //         res.ResponseData = view;
                //     }
                // }
            }
            catch (Exception ex)
            {
                res.ResponseNo = "1000";
                res.ResponseNa = "API回應失敗、錯誤訊息：" + ex.Message;
            }
            return JsonSerializer.Serialize(res);
        }

        [Authorize]
        [HttpPost]
        public string UpdateProfile([FromBody] UpdateProfileRequestViewModel source)
        {

            BasicResponseViewModel res = new BasicResponseViewModel();
            try
            {
                string userNo = User.Identity.Name;

                res.ResponseNo = "0000";
                res.ResponseData = new UpdateProfileResponseViewModel()
                {
                    UpdateNo = "00"
                };
                // string encryptUserPWD = _utillity.SHA256Encode(source.UserPwd);
                // //呼叫API取得人員資料
                // res = SysUserLogin(source.UserNo, encryptUserPWD);

                // if (res.ResponseNo == "0000")
                // {
                //     LoginResponseViewModel_SysUserLogin view = (LoginResponseViewModel_SysUserLogin)res.ResponseData;
                //     if (!string.IsNullOrEmpty(view.PWDCHGDATE) && view.PWDCHGDATE.CompareTo(DateTime.Now.ToString("yyyy/MM/dd")) == -1)
                //     {
                //         #region 成功但需要強制換密碼
                //         res.ResponseNo = "1111";
                //         res.ResponseNa = "此USER需要變更密碼後重新登入。";
                //         //回傳變更密碼所需的資料
                //         res.ResponseData = new LoginResponseViewModel_SysUserLogin() { UserNo = view.UserNo};
                //         #endregion
                //     }
                //     else
                //     {
                //         //如果有勾選Remember則Token效期變成3000分鐘失效，沒有則30分鐘失效
                //         int expireMinutes=source.Remember?3000:30;
                //         view.Token= _jwtHelpers.GenerateToken(view.UserNo,expireMinutes);
                //         //回傳給前台讓前台選取需要顯示的資料
                //         res.ResponseData = view;
                //     }
                // }
            }
            catch (Exception ex)
            {
                res.ResponseNo = "1000";
                res.ResponseNa = "API回應失敗、錯誤訊息：" + ex.Message;
            }
            return JsonSerializer.Serialize(res);
        }

    }
}
