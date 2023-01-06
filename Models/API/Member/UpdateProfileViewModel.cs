using System;
using System.Collections.Generic;

namespace ReactCMS.Models.API.Member
{
    public class UpdateProfileRequestViewModel
    {
        public string UserNo { get; set; }
        public string UserNa { get; set; }
        public string Mobile { get; set; }
    }
    public class UpdateProfileResponseViewModel
    {
        public string UpdateNo { get; set; }
        public string UpdateNa { get; set; }
    }
}
