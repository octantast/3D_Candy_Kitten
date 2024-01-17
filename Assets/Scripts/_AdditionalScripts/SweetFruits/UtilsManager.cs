using UnityEngine;

namespace _AdditionalScripts.SweetFruits
{
    public class UtilsManager:MonoBehaviour
    {
        public void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}