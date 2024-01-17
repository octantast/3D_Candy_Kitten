using System.Collections.Generic;
using _AdditionalScripts.SweetFruits.controlllers;
using UnityEngine;

namespace _AdditionalScripts.SweetFruits
{
    public class UI_HelpObj : MonoBehaviour
    {
        private bool isSequenceCompleted;
        private int currentIndex;
        private int totalElements;

        [SerializeField] private List<FruiufafiaCanva> canvasElements;

        public void ActivateNextCanvasElement()
        {
            if (currentIndex < totalElements)
            {
                canvasElements[currentIndex].Activate();
                currentIndex++;
            }
            else
            {
                if (isSequenceCompleted)
                    return;

                FindObjectOfType<UIManager>().CompleteSequence();
                isSequenceCompleted = true;
            }
        }

        public static void FadeCanvasGroup(GameObject canvasObject, bool fadeIn)
        {
            canvasObject.SetActive(true);
            CanvasGroup canvasGroup = canvasObject.GetComponent<CanvasGroup>();
            float targetAlpha = fadeIn ? 1f : 0f;

            canvasObject.SetActive(false);
        }
    }
}