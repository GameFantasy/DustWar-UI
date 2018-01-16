using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;
using Excel;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public class GameDataSetUtil : MonoBehaviour
{

    void Start()
    {
        AddGoFromExcel4Bag(Application.dataPath + "/ResConv/Character" + "/BagList.xlsx");
        ResConvCtrl.SaveGoToFile(GameDataSet.Ins, Application.dataPath + "/ResData" + "/ConfigData.dat");
    }

    public static void AddGoFromExcel4Bag(String excelString)
    {
        FileStream stream = File.Open(excelString, FileMode.Open, FileAccess.Read);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

        DataSet result = excelReader.AsDataSet();

        int columns = result.Tables[0].Columns.Count;
        int rows = result.Tables[0].Rows.Count;

        for (int i = 1; i < rows; i++)
        {
            ItemData pd = new ItemData();
            pd.m_Id = int.Parse(result.Tables[0].Rows[i][0].ToString());
            pd.m_Name = result.Tables[0].Rows[i][1].ToString();
            pd.m_Index = int.Parse(result.Tables[0].Rows[i][2].ToString());
            pd.m_Type = (ITEMTYPE)int.Parse(result.Tables[0].Rows[i][3].ToString());
            GameDataSet.Ins.m_BagList.Add(pd);
        }
    }
}
