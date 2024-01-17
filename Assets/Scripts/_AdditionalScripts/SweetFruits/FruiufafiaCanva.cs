using UnityEngine;
using UnityEngine.Serialization;

namespace _AdditionalScripts.SweetFruits
{
    [System.Serializable]
    public class FruiufafiaCanva
    {
        [FormerlySerializedAs("canvasElementReference")] [SerializeField]
        private CanvsSweetUIModal canvsSweetUIModal;

        public void Activate()
        {
            canvsSweetUIModal.ShowAndScale();
        }
    }
}