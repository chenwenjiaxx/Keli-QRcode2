using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverParameterReSet
{
    /// <summary>
    /// _Code:判断参数 注：一般负数“-1”为失败，整数“1”为成功
    /// _Message:错误信息
    /// _Object:需要返回的数据
    /// </summary>
    public class Result
    {
        private int _Code = 0;

        private string _Message = "默认";

        private object _Object = null;

        public int Code { get => _Code; set => _Code = value; }

        public string Message { get => _Message; set => _Message = value; }

        public object Object { get => _Object; set => _Object = value; }
    }
}
