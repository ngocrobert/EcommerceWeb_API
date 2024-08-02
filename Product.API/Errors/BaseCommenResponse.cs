namespace Product.API.Errors
{
    public class BaseCommenResponse
    {

        

        public BaseCommenResponse(int stuatusCode, string message = null)
        {
            StatusCode = stuatusCode;
            Message = message ?? DefaultMessageForSatusCode(stuatusCode);
        }

        //public BaseCommenResponse(int stuatusCode)
        //{
        //    stuatusCode = stuatusCode;
        //}

        private string DefaultMessageForSatusCode(int stuatusCode)
         => stuatusCode switch
         {
             400 => "bad request",
             401 => "not authorize",
             404 => "resource not found",
             500 => "server error",
             _ => null
         };

        public int StatusCode { get; set; }
        public string Message { get; set; }

    }
}
