using System;
using System.Collections.Generic;
using UnityEngine;

namespace _AdditionalScripts.SweetFruits.DataScripts
{
    public class DictionaryDB : MonoBehaviour
    {
        [SerializeField] private List<string> _dictFruits1;
        [SerializeField] private List<string> _dictFruits2;

        public List<string> DictFruits1 => _dictFruits1;
        public List<string> DictFruits2 => _dictFruits2;
    }
}