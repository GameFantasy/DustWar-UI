using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Utils {
    public class Singleton<T> where T: new(){
        private static T m_Ins;
        public static T Ins {
            get {
                if (m_Ins == null)
                {
                    m_Ins = new T();
                    return m_Ins;
                }
                return m_Ins;
            }
            set {
                m_Ins = value;
            }
        }
    }
}
