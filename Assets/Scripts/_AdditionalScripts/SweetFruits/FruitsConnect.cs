using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace _AdditionalScripts.SweetFruits
{
    public class FruitsConnect : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public string Connneeeectatear(List<string> stringsToConcatenate)
        {
            StringBuilder resultBuilder = new StringBuilder();
            foreach (var str in stringsToConcatenate)
            {
                resultBuilder.Append(str);
            }

            return resultBuilder.ToString();
        }
    }
}