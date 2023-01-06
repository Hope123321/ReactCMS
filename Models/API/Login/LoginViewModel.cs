using System;
using System.Collections.Generic;

namespace ReactCMS.Models.API.Login
{
    public class LoginRequestViewModel
    {
        public string UserNo { get; set; }
        public string UserPwd { get; set; }
        public bool Remember { get; set; }
    }
    public class LoginResponseViewModel
    {
        public string ResultNo { get; set; }
        public string ResultNa { get; set; }

        public LoginResponseViewModel_SysUserLogin SysUserLogin { get; set; }
    }
    public class LoginResponseViewModel_SysUserLogin
    {
        public string UserNo { get; set; }
        public string UserNa { get; set; }
        public string EmpNo { get; set; }
        public string EmpNa { get; set; }
        public string ShopNo { get; set; }
        public object ShopNa { get; set; }
        public string SpGpNo { get; set; }
        public string UserGUID { get; set; }
        public string MenuList { get; set; }

        /// <summary>
        /// �K�X�j���ܧ�̫����
        /// </summary>
        public string PWDCHGDATE { get; set; }

        /// <summary>
        /// �D�D�˦�
        /// </summary>
        public string THEMENO { get; set; }
        /// <summary>
        /// ����Token
        /// </summary>
        public string Token { get; set; }

        public LoginResponse_SysSetDet[] SysSetDet { get; set; }
        public LoginResponse_RxSysCompNo[] RxSysCompNo { get; set; }
        public LoginResponse_RxSysChaNo[] RxSysChaNo { get; set; }
        public LoginResponse_RxSysBraNo[] RxSysBraNo { get; set; }
        public LoginResponse_RxManageBrand[] RxManageBrand { get; set; }
    }

    public class LoginResponse_SysSetDet
    {
        public string SysBraNo { get; set; }
        public string SetTopNo { get; set; }
        public string SetNo { get; set; }
        public string SetNa { get; set; }
        public string SetValue { get; set; }
    }

    public class LoginResponse_RxSysCompNo
    {
        public string SysCompNo { get; set; }
    }

    public class LoginResponse_RxSysChaNo
    {
        public string SysChaNo { get; set; }
    }

    public class LoginResponse_RxSysBraNo
    {
        public string SysBraNo { get; set; }
    }

    public class LoginResponse_RxManageBrand
    {
        public string SysBraNo { get; set; }
    }
}
