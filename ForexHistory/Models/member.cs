//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ForexHistory.Models
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using System.Text;
    
    public partial class member
    {
        public member()
        {
            this.history1 = new HashSet<history>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string surName { get; set; }
        public string eMailAddress { get; set; }
        public string password { get; set; }
    
        public virtual ICollection<history> history1 { get; set; }
        private string md5(string data)
        {
            MD5 md5 = MD5.Create();
            byte[] hashData = md5.ComputeHash(Encoding.Default.GetBytes(data));
            StringBuilder returnValue = new StringBuilder();
            for (int i = 0; i < hashData.Length; i++)returnValue.Append(hashData[i].ToString());
            return returnValue.ToString();
        }
        public void setPassword(string data)
        {
            password = md5(data);
        }
    }
}
