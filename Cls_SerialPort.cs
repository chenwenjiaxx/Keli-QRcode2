using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DriverParameterReSet
{
    public class SerialPort : System.IO.Ports.SerialPort
    {
        public ArrayList SerialReturn = new ArrayList();
        public System.IO.Ports.SerialPort serialPort;
        public IContainer container;

        public SerialPort()
        {
            InitializeComponent();
        }

        public string[] GetCom() =>
            GetPortNames();

        //加载
        private void InitializeComponent()
        {
            container = new Container();
            serialPort = new System.IO.Ports.SerialPort(container);
            serialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
        }

        //接收数据
        private void GetDataReceive()
        {
            try
            {
                string str1 = "";
                string str2 = "";
                int bytestoRead = serialPort.BytesToRead;
                byte[] buffer = new byte[bytestoRead];

                serialPort.Read(buffer, 0, bytestoRead);

                foreach (byte b in buffer)
                {
                    str1 += Convert.ToChar(b).ToString();
                }

                char[] cArray = str1.ToCharArray();

                foreach (char c in cArray)
                {
                    str2 = str2 + Convert.ToString(Convert.ToInt32(c), 0x10) + " ";
                }

                if (str2 != "")
                {
                    string[] strArray = str2.Split(' ');

                    for (int i = 0; i < strArray.Length - 1; i++)
                    {
                        if (strArray[i].Length == 1)
                        {
                            strArray[i] = "0" + strArray[i];
                        }
                        SerialReturn.Add(strArray[i]);
                    }
                }
            }
            catch (Exception)
            { }
        }

        //事件接收数据
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            GetDataReceive();
        }

        //string转换byte[]
        private byte[] HexStrToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
            {
                hexString += " ";
            }
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
            {
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }
            return returnBytes;
        }

        //发送数据
        public void SendCommand(string str)
        {
            if (str == "")
            {
                return;
            }

            byte[] bSendData = HexStrToHexByte(str);
            int length = bSendData.Length;

            serialPort.Write(bSendData, 0, length);
            //TestCount.SendCount++;
        }

        /// <summary>
        /// 发送命令方法
        /// </summary>
        /// <param name="sendOrder">发送的命令(不含CRC)</param>
        /// <param name="sendCount">发送次数</param>
        /// <param name="readCount">每次读取次数</param>
        /// <param name="sleepTime">等待时间/ms</param>
        /// <param name="returnLong">返回数据长度</param>
        /// <returns>Result</returns>
        public Result Send(string sendOrder, int sendCount, int readCount, int sleepTime, int returnLong)
        {
            Result result = new Result();

            for (int i = 0; i < sendCount; i++)
            {
                this.SerialReturn.Clear();
                this.SendCommand(Cls_CRC.CalculateCRC(sendOrder));

                for (int j = 0; j < readCount; j++)
                {
                    Thread.Sleep(sleepTime);
                    ArrayList array = new ArrayList();
                    array.AddRange(this.SerialReturn);

                    if (array.Count == returnLong && Cls_CRC.VerdictCRC(StatusTool.ArrayToString(array)))
                    {
                        result.Code = 1;
                        result.Message = "发送成功";
                        return result;
                    }
                }
                //TestCount.FailCount++;
            }
            result.Code = -1;
            result.Message = "发送失败";
            return result;
        }
    }
}
