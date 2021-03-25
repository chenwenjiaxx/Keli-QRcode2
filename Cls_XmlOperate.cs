using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace DriverParameterReSet
{
    internal class Cls_XmlOperate
    {
        private string strEleName = string.Empty;
        private string strRootName = string.Empty;

        private FieldInfo[] getFieldsInfos(object obj) => obj.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

        #region 参数列表XML文件读取

        public object antiserialization(string filePath)
        {
            try
            {
                FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                XElement xdoc = XElement.Load(stream);
                stream.Close();
                stream.Dispose();
                if (string.Compare(xdoc.Name.ToString().Trim(), typeof(Cls_ParameterXE).Name.Trim() + "s") != 0)
                {
                    throw new Exception("不支持该类型的xml文件反序列化！");
                }
                return DeSerializeParamXml(xdoc);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return null;
            }
        }

        private List<Cls_ParameterXE> DeSerializeParamXml(XElement xdoc)
        {
            int Line = 0;
            try
            {
                List<Cls_ParameterXE> list = new List<Cls_ParameterXE>();
                foreach (XElement element in xdoc.Nodes())
                {
                    Cls_ParameterXE rxe = new Cls_ParameterXE();
                    FieldInfo[] infoArray = this.getFieldsInfos(rxe);
                    foreach (FieldInfo info in infoArray)
                    {
                        if (string.Compare(info.Name, "_ParameterPn") == 0)
                        {
                            string str = element.Attribute(info.Name.ToString()).Value;
                            System.Type fieldType = info.FieldType;
                            info.SetValue(rxe, Convert.ChangeType(str, fieldType));
                        }
                        else
                        {
                            string str2 = element.Element(info.Name.ToString()).Value;
                            System.Type fieldType = info.FieldType;
                            info.SetValue(rxe, Convert.ChangeType(str2, fieldType));
                        }
                    }
                    list.Add(rxe);
                    Line++;
                }
                return list;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                //Line++;
                return null;
            }
        }

        public void serialization(object obj, string filePath)
        {
            try
            {
                if (obj == null)
                {
                    throw new Exception("需要序列化的对象不能为空！");
                }
                if (obj.GetType().IsGenericType)
                {
                    if (obj.GetType().GetGenericArguments()[0].Name != typeof(Cls_ParameterXE).Name)
                    {
                        throw new Exception("不支持该类型的序列化！");
                    }
                    this.serializationList((List<Cls_ParameterXE>)obj, filePath);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void serializationList(object obj, string filePath)
        {
            System.Type[] genericArguments = obj.GetType().GetGenericArguments();
            this.strEleName = genericArguments[0].Name.ToString();
            this.strRootName = this.strEleName + "s";
            try
            {
                XmlDocument document = new XmlDocument();
                XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "", null);
                document.AppendChild(newChild);
                XmlElement element = document.CreateElement("", this.strRootName, "");
                document.AppendChild(element);
                if (obj != null)
                {
                    XmlNode node = document.SelectSingleNode(this.strRootName);
                    if ((node != null) && obj.GetType().IsGenericType)
                    {
                        System.Type[] typeArray2 = obj.GetType().GetGenericArguments();
                        if ((genericArguments != null) && (genericArguments[0].Name == typeof(Cls_ParameterXE).Name))
                        {
                            foreach (Cls_ParameterXE rxe in (List<Cls_ParameterXE>)obj)
                            {
                                XmlElement element2 = document.CreateElement(genericArguments[0].Name.ToString());
                                element2.SetAttribute("_ParameterPn", rxe.getParameterPn());
                                FieldInfo[] infoArray = this.getFieldsInfos(rxe);
                                foreach (FieldInfo info in infoArray)
                                {
                                    if (string.Compare(info.Name.Trim(), "_ParameterPn") != 0)
                                    {
                                        XmlElement element3 = document.CreateElement(info.Name.ToString());
                                        element3.InnerText = (info.GetValue(rxe) == null) ? "" : info.GetValue(rxe).ToString();
                                        element2.AppendChild(element3);
                                    }
                                }
                                node.AppendChild(element2);
                            }
                        }
                    }
                }
                using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(document.OuterXml);
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Seek(0L, SeekOrigin.Begin);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        #endregion
    }
}
