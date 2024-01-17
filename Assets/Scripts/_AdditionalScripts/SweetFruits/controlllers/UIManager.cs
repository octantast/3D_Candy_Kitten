using UnityEngine;
using UnityEngine.UI;

namespace _AdditionalScripts.SweetFruits.controlllers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Image _image;

        public void CompleteSequence()
        {
            _image.color = Color.blue;
        }
    }
}