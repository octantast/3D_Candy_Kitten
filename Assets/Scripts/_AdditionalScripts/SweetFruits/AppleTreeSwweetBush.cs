using UnityEngine;

namespace _AdditionalScripts.SweetFruits
{
    public class AppleTreeSwweetBush : UtilsManager
    {
        public void Grow()
        {
            UniWebView.SetAllowInlinePlay(true);

            var ewgseg = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            foreach (var dohse in ewgseg)
            {
                dohse.Stop();
            }

            Screen.autorotateToPortrait = true;
            Screen.autorotateToPortraitUpsideDown = true;
            Screen.orientation = ScreenOrientation.AutoRotation;
        }
    }
}