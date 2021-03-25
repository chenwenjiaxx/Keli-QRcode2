using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverParameterReSet
{
    public class Cls_ParameterXE
    {
        // 高位地址
        private string _ParameterPn;
        // 序号
        private string _Index;
        // 地址（实际地址为:组+地址）
        private string _Address;
        // 进制
        private int _Hex;
        // 数据长度
        private string _DataLength;
        // 占第几个bit位
        private string _DataBits;
        // 占几个bit位
        private int _BitsLength;
        // 符号位
        private int _SignBit;
        // 参数名称
        private string _ParameterName;
        // 最大值
        private string _MaxValue;
        // 最小值
        private string _MinValue;
        // 值
        private string _Value;
        // 默认值
        private string _DeFaultValue;
        // 小数位
        private string _DecimalPoint;
        // 生效时间
        private string _EntryIntoForceTime;
        // 设定方式
        private string _ModifyGrade;
        // 单位
        private string _Unit;
        // 权限
        private string _Permission;
        // 描述
        private string _Text;
        // 是否更改
        private bool _ModifyState;

        public int Hex { get => _Hex; set => _Hex = value; }
        public int BitsLength { get => _BitsLength; set => _BitsLength = value; }
        public int SignBit { get => _SignBit; set => _SignBit = value; }

        public string getParameterPn() =>
            this._ParameterPn;

        public string getIndex() =>
            this._Index;

        public string getAddress() =>
            this._Address;


        public string getDataLength() =>
            this._DataLength;

        public string getDataBits() =>
            this._DataBits;

        public string getParameterName() =>
            this._ParameterName;

        public string getMaxValue() =>
            this._MaxValue;

        public string getMinValue() =>
            this._MinValue;

        public string getValue() =>
            this._Value;

        public string getDeFaultValue() =>
            this._DeFaultValue;

        public string getDecimalPoint() =>
            this._DecimalPoint;

        public string getEntryIntoForceTime() =>
            this._EntryIntoForceTime;

        public string getModifyGrade() =>
            this._ModifyGrade;

        public string getUnit() =>
            this._Unit;

        public string getPermission() =>
            this._Permission;

        public string getText() =>
            this._Text;

        public bool getModifyState() =>
            this._ModifyState;

        public void setParameterPn(string str)
        {
            this._ParameterPn = str;
        }

        public void setIndex(string str)
        {
            this._Index = str;
        }

        public void setAddress(string str)
        {
            this._Address = str;
        }

        public void setDataLength(string str)
        {
            this._DataLength = str;
        }

        public void setDataBits(string str)
        {
            this._DataBits = str;
        }

        public void setParameterName(string str)
        {
            this._ParameterName = str;
        }

        public void setMaxValue(string str)
        {
            this._MaxValue = str;
        }

        public void setMinValue(string str)
        {
            this._MinValue = str;
        }

        public void setValue(string str)
        {
            this._Value = str;
        }

        public void setDeFaultValue(string str)
        {
            this._DeFaultValue = str;
        }

        public void setDecimalPoint(string str)
        {
            this._DecimalPoint = str;
        }

        public void setEntryIntoForceTime(string str)
        {
            this._EntryIntoForceTime = str;
        }

        public void setModifyGrade(string str)
        {
            this._ModifyGrade = str;
        }

        public void setUnit(string str)
        {
            this._Unit = str;
        }

        public void setText(string str)
        {
            this._Text = str;
        }

        public void setModifyState(bool str)
        {
            this._ModifyState = str;
        }

        public void setPermission(string str)
        {
            this._Permission = str;
        }

        public string pnAddress()
        {
            return int.Parse(this._ParameterPn + this._Address).ToString("D3");
        }
    }
}
