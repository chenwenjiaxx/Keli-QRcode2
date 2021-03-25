using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverParameterReSet
{
    public class Cls_Servo
    {
        public List<Cls_ParameterXE> _ParameterList;

        public string ServoSeriaParameterNumber { get; set; }

        public void setParameterList(List<Cls_ParameterXE> parameterList)
        {
            _ParameterList = parameterList;
        }

        public List<Cls_ParameterXE> getParameterList() => _ParameterList;


    }
}
