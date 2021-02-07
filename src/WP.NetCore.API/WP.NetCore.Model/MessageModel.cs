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
        public bool Result { get; set; }
      
        /// <summary>
        /// 返回信息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 返回数据集合
        /// </summary>
        public object Data { get; set; }


        public ResponseResult Success(object data=null)
        {
            return new ResponseResult() { Result=true,Data= data };
        }

        public ResponseResult Error(string msg)
        {
            return new ResponseResult() { Result = false,Msg= msg };
        }
    }
}
