﻿namespace Product.API.Errors
{
    public class ApiException : BaseCommenResponse
    {
       
        private readonly string details;

        public ApiException(int stuatusCode, string message = null, string Details=null) : base(stuatusCode, message)
        {
           
            details = Details;
        }

        public string Details { get; set; }
    }
}
