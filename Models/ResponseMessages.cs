namespace PiyatMandli
{
    public class ResponseMessages
    {
        //1 = SUCCESS
        public static string SuccessCode = "1";
        public static string SuccessMsg = "Successfull Transaction";

        //2 = ERROR (Server side)
        public static string ErrorCode = "2";
        public static string ErrorMsg = "Error on server side";

        //3 = AUTHENTICATION FAILED
        public static string AuthenticationFailedCode = "3";
        public static string AuthenticationFailedMsg = "Authentication Failed";

        //4 = NO DATA FOUND
        public static string NoDataCode = "4";
        public static string NoDataMsg = "No Data Found";

        public static string UnsuccessCode = "5";
        public static string UnsuccessMsg = "Unsuccessful Transaction";

        public ResponseMessages()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
