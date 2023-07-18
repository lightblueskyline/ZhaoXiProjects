using Model.Other;

namespace ExeWebApi.Config
{
    public class ResultHelper
    {
        public static ApiResult Success(object data)
        {
            return new ApiResult() { IsSuccess = true, Result = data, Msg = System.String.Empty };
        }

        public static ApiResult Error(string message)
        {
            return new ApiResult() { IsSuccess = false, Result = null, Msg = message };
        }
    }
}
