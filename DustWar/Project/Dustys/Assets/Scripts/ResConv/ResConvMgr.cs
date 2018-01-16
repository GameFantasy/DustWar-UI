using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public class ResConvMgr{
    public static void Save() {
        GameDataSet.Ins = null;

        ResCtrl.Ins.m_CharacterCtrl.m_CharacterRes.Save();

        ResConvMgr.SaveGoToFile<GameDataSet>(GameDataSet.Ins,
                 Application.dataPath + "/ResData" + "/GameData.dat");
    }

    public static void Load() {
        GameDataSet.Ins = ResConvMgr.LoadGoFromFile<GameDataSet>(Application.dataPath + "/ResData" + "/GameData.dat");

        ResCtrl.Ins.m_CharacterCtrl.m_CharacterRes.Load();

        GameDataSet.Ins = null;
    }
    //public static void AddGoFromExcel(Transform TranBagList, String excelString, String resString)
    //{
    //    FileStream stream = File.Open(excelString, FileMode.Open, FileAccess.Read);
    //    IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

    //    DataSet result = excelReader.AsDataSet();

    //    int columns = result.Tables[0].Columns.Count;
    //    int rows = result.Tables[0].Rows.Count;

    //    for (int i = 1; i < rows; i++)
    //    {
    //        string nvalue = result.Tables[0].Rows[i][0].ToString();
    //        //Debug.Log(nvalue);
    //        GameObject Preb0 = Resources.Load(resString + "/" + nvalue) as GameObject;
    //        GameObject Preb1 = GameObject.Instantiate(Preb0) as GameObject;
    //        Preb1.transform.parent = TranBagList;
    //    }
    //}

    public static void SaveGoToFile<T>(T go, String fileString) where T : new() {
        //文件的写入流信息  
        FileStream fs;
        FileInfo file = new FileInfo(fileString);
        if (!file.Exists)
        {
            //如果文件不存在则创建  
            fs = new FileStream(fileString, FileMode.Create);
        }
        else
        {
            // 删除文档
            File.Delete(fileString);
            // 重新打开
            fs = new FileStream(fileString, FileMode.Create);
        }
        BinaryFormatter formatter = new BinaryFormatter();
        try
        {
            formatter.Serialize(fs, go);
        }
        catch (SerializationException e)
        {
            Debug.Log("Failed to serialize. Reason:" + e.Message);
            throw;
        }
        finally
        {
            fs.Close();
        }
    }

    public static T LoadGoFromFile<T>(String fileString) where T : new()
    {
        //文件的写入流信息  
        FileStream fs;
        T go = new T();
        FileInfo file = new FileInfo(fileString);
        if (!file.Exists)
        {
            Debug.Log("Failed to open. :" + fileString);
            return go;
        }
        else
        {
            //如果存在则打开  
            fs = new FileStream(fileString, FileMode.Open);
        }
        BinaryFormatter formatter = new BinaryFormatter();
        try
        {
            go = (T)formatter.Deserialize(fs);
        }
        catch (SerializationException e)
        {
            Debug.Log("Failed to serialize. Reason:" + e.Message);
            throw;
        }
        finally
        {
            fs.Close();
        }
        return go;
    }
}
