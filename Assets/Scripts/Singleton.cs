using System.Linq;
using UnityEngine;

namespace utility.singleton {
    
    public class Singleton<T> : MonoBehaviour where T : Component {
        private static object m_Lock = new object();
        private static T m_Instance;

        public static T I {
            get {
                lock (m_Lock) {
                
                    if (m_Instance == null) {
                   
                        m_Instance = (T)FindObjectOfType(typeof(T));
                    
                        if (m_Instance == null) {
                        
                            var singletonObject = new GameObject();
                            m_Instance = singletonObject.AddComponent<T>();
                            singletonObject.name = typeof(T).ToString() + " (Singleton)";
                        }
                    }
                    return m_Instance;
                }
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void Init() {
            m_Instance = null;
        }

    }

}