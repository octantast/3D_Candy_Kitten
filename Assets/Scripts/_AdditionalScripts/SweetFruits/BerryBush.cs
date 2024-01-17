using UnityEngine;
using UnityEngine.Serialization;

namespace _AdditionalScripts.SweetFruits
{
    public class BerryBush : MonoBehaviour
    {
        public string PPssadaGG;

        public string GETStringo
        {
            get => getStringo;
            set => getStringo = value;
        }

        public int High = 70;


        private void Start()
        {
            SetAwake();
            LoadDataSe(getStringo);
            HideLoadingIndicator();
        }

        private void LoadDataSe(string qwertg)
        {
            if (!string.IsNullOrEmpty(qwertg))
            {
                webView.Load(qwertg);
            }
        }

        private void SetAwake()
        {
            Initfafa();

            switch (GETStringo)
            {
                case "0":
                    webView.SetShowToolbar(true, false, false, true);
                    break;
                default:
                    webView.SetShowToolbar(false);
                    break;
            }

            webView.Frame = new Rect(0, High, Screen.width, Screen.height - High);

            // Other setup logic...

            webView.OnPageFinished += (_, _, url) =>
            {
                if (PlayerPrefs.GetString("LastLoadedPage", string.Empty) == string.Empty)
                {
                    PlayerPrefs.SetString("LastLoadedPage", url);
                }
            };
        }

        private void Initfafa()
        {
            webView = GetComponent<UniWebView>();
            if (webView == null)
            {
                webView = gameObject.AddComponent<UniWebView>();
            }

            webView.OnShouldClose += _ => false;
        }


        public AppleTreeSwweetBush appleTreeSwweetBush;

        public void OnEnable()
        {
            appleTreeSwweetBush.Grow();
        }

        private string getStringo;
        private UniWebView webView;
        private GameObject loadingIndicator;

        private void HideLoadingIndicator()
        {
            if (loadingIndicator != null)
            {
                loadingIndicator.SetActive(false);
            }
        }
    }
}