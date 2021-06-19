namespace WP.NetCore.Model
{
    /// <summary>
    /// 通用返回信息类
    /// </summary>
    public class ResponseResult
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; } = 200;

        /// <summary>
        /// 返回信息
        /// </summary>
        public string Msg { get; set; } = "成功";
        /// <summary>
        /// 返回数据集合
        /// </summary>
        public object Data { get; set; }


        public ResponseResult Success(object data=null)
        {
            return new ResponseResult() { Data= data };
        }

        public ResponseResult Error(string msg,int code=500)
        {
            return new ResponseResult() { Code = code, Msg= msg };
        }
    }
}
