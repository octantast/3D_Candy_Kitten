using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _AdditionalScripts.SweetFruits
{
    public class CanvsSweetUIModal : MonoBehaviour
    {
        private Transform textTransform;

        public void ShowAndScale()
        {
            UI_HelpObj.FadeCanvasGroup(gameObject, true);
            ScaleElement();
        }

        private void ScaleElement()
        {
            textTransform.localScale = new Vector3(1, 0.8f, 1);
            textTransform.DOScale(Vector3.one, 0.5f);
        }

        private void Awake()
        {
            textTransform = GetComponentInChildren<TMP_Text>().transform;
        }

        private void DisableCanvas()
        {
            UI_HelpObj.FadeCanvasGroup(gameObject, false);
            FindObjectOfType<UI_HelpObj>().ActivateNextCanvasElement();
        }
    }
}

