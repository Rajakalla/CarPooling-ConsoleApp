using System;
using System.Collections.Generic;
using System.Text;

namespace CarPooling.Data.Models
{
    public class User
    {
        public int UserId { get; set; }
        public String UserName { get; set; }
        public String MobileNumber { get; set; }


        //public User(String userName,String mobileNumber)
        //{
        //    UserId = UserIdGenerator++;
        //    UserName = userName;
        //    MobileNumber = mobileNumber;
        //}
    }
}
