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
using Assets.ResData;

public class PoolDataSetUtil : MonoBehaviour
{

    void Start()
    {
        AddGoFromExcel(Application.dataPath + "/ResConv/PlayerData" + "/PlayerData.xlsx");
        AddGoFromExcel4CommonResData(Application.dataPath + "/ResConv/PlayerData" + "/EquipData.xlsx", PoolResDataSet.Ins.m_EquipDic);
        ResConvCtrl.SaveGoToFile<PoolResDataSet>(PoolResDataSet.Ins, Application.dataPath + "/ResData" + "/PoolData.dat");
    }
    
    public static void AddGoFromExcel(String excelString)
    {
        FileStream stream = File.Open(excelString, FileMode.Open, FileAccess.Read);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

        DataSet result = excelReader.AsDataSet();

        int columns = result.Tables[0].Columns.Count;
        int rows = result.Tables[0].Rows.Count;

        for (int i = 1; i < rows; i++)
        {
            PlayerResData pd = new PlayerResData();
            pd.m_Id = int.Parse(result.Tables[0].Rows[i][0].ToString());
            pd.m_PlayerName = result.Tables[0].Rows[i][1].ToString();
            for(int j = 2; j < columns; j++){
                pd.AddData(result.Tables[0].Rows[0][j].ToString(), result.Tables[0].Rows[i][j].ToString());
            }
            PoolResDataSet.Ins.m_PlayerDic.Add(pd.m_Id, pd);
        }
    }

    public static void AddGoFromExcel4CommonResData(String excelString, Dictionary<int, CommonResData> m_Dic) {
        FileStream stream = File.Open(excelString, FileMode.Open, FileAccess.Read);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

        DataSet result = excelReader.AsDataSet();

        int columns = result.Tables[0].Columns.Count;
        int rows = result.Tables[0].Rows.Count;

        for (int i = 1; i < rows; i++) {
            CommonResData pd = new CommonResData();
            pd.m_Id = int.Parse(result.Tables[0].Rows[i][0].ToString());
            pd.m_Name = result.Tables[0].Rows[i][1].ToString();
            for (int j = 2; j < columns; j++) {
                pd.AddData(result.Tables[0].Rows[0][j].ToString(), result.Tables[0].Rows[i][j].ToString());
            }
            m_Dic.Add(pd.m_Id, pd);
        }
    }
}
