using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using UnityEngine;

namespace DBUtility
{
    public static class XmlHelper
    {
        
        public static void OpenXml()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("大地图.tmx");    //加载Xml文件  
            XmlElement rootElem = doc.DocumentElement;   //获取根节点  
            XmlNodeList personNodes = rootElem.GetElementsByTagName("layer"); //获取person子节点集合  
            foreach (XmlNode node in personNodes)
            {
                string strName = ((XmlElement)node).GetAttribute("name");   //获取name属性值  
                Debug.Log(strName);
                XmlNodeList subAgeNodes = ((XmlElement)node).GetElementsByTagName("data");  //获取age子XmlElement集合  
                if (subAgeNodes.Count == 1)
                {
                    string strAge = subAgeNodes[0].InnerText;
                    Debug.Log(strAge);
                }
            }
        }
    }
}