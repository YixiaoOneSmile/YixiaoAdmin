using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YixiaoAdmin.Common
{
    public class Response
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }

        public void Success()
        {
            Code = 0;
            Message = "请求成功";
        }
        public void Success(dynamic data,string msg = "")
        {
            Code = 0;
            Message = "请求成功!"+ msg;
            this.Data = data;
        }
        public void ItemNotFound(string msg = "")
        {
            Code = -1;
            Message = "没有找到该数据!" + msg;
        }
        public void OperationRepetition(string msg = "")
        {
            Code = -2;
            Message = "提交重复！" + msg;
        }
        public void DataBaseError(string msg = "")
        {
            Code = -3;
            Message = "数据错误!" + msg;
        }
        public void SystemError(string msg = "")
        {
            Code = -4;
            Message = "系统错误!" + msg;
        }
        public void OperationError(string msg = "")
        {
            Code = -5;
            Message = "请求错误!" + msg;
        }
    }

    public class PagesResponse
    {
        public int code { get; set; }
        public string msg { get; set; }
        public dynamic data { get; set; }
        public int count {get;set;}
        public void Success(dynamic data,int count=0, string msg = "")
        {
            code = 0;
            this.msg = "请求成功!" + msg;
            this.data = data;
            this.count = count;
        }
    }
}