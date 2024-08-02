﻿namespace Product.API.Errors
{
    public class ApiValidationErrorResponse : BaseCommenResponse
    {
        public ApiValidationErrorResponse() : base(400)
        {
        }
        public IEnumerable<string> Errors { get; set; }
    }
}