using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;

public class DeepCopy : MonoBehaviour {
    //需要silverlight支持
    public static T Do<T>(T obj) {
        object retval;
        using (MemoryStream ms = new MemoryStream()) {
            DataContractSerializer ser = new DataContractSerializer(typeof(T));
            ser.WriteObject(ms, obj);
            ms.Seek(0, SeekOrigin.Begin);
            retval = ser.ReadObject(ms);
            ms.Close();
        }
        return (T)retval;
    }
}
