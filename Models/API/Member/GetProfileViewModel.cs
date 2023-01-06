using System;
using System.Collections.Generic;

namespace ReactCMS.Models.API.Member
{
    public class GetProfileRequestViewModel
    {
        public string UserNo { get; set; }
    }
    public class GetProfileResponseViewModel
    {
        public string UserNo { get; set; }
        public string UserNa { get; set; }
        public string Mobile { get; set; }
    }
}
