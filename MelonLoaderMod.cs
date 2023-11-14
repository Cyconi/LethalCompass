using MelonLoader;
using UnityEngine;
using System.Collections;
using System;


namespace LethalCompanyCompass
{
    using TMPro;
    public static class BuildInfo
    {
        public const string Name = "LethalCompanyCompass"; // Name of the Mod.  (MUST BE SET)
        public const string Author = "squidypal"; // Author of the Mod.  (Set as null if none)
        public const string Company = null; // Company that made the Mod.  (Set as null if none)
        public const string Version = "1.0.0"; // Version of the Mod.  (MUST BE SET)
        public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)
    }

    public class LethalCompanyCompass : MelonMod
    {
        private GameObject player;
        private GameObject textGameObject;
        private TextMeshProUGUI textMesh;
        private float fadeDuration = 1f;
        private bool isFadingIn;
        private bool fade = false;
        private Transform playerContainer;
        
        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            if (sceneName == "MainMenu" || sceneName == "InitScene" || sceneName == "InitSceneLaunchOptions")
                return;
            if (!GameObject.Find("PlayersContainer")) return;
            
            if(textGameObject == null)
            textGameObject = new GameObject("TextMeshPro Object");
            playerContainer = GameObject.Find("PlayersContainer").transform;
            if(textMesh == null)
            textMesh = textGameObject.AddComponent<TextMeshProUGUI>();
            textMesh.text = "Your Text Here";
            textMesh.fontSize = 36;

            // Set the text GameObject as a child of a canvas
            textGameObject.transform.SetParent(GameObject.Find("Systems/UI/Canvas").transform, false);
            textGameObject.transform.name = "Compass Text";
            
            textGameObject.transform.localPosition = new Vector3(-340, 210, 0);
            // make text mesh color rgb(222, 109, 30)
            textMesh.color = new Color(0.87f, 0.43f, 0.12f);
            textMesh.material = new Material(Shader.Find("TextMeshPro/Distance Field"));
        }

        private string GetDirection(float yRotation)
        {
            yRotation = (yRotation + 360) % 360; // Normalize rotation to 0-360 range

            if (yRotation <= 30 || yRotation > 330) return "North";
            if (yRotation <= 60) return "North East";
            if (yRotation <= 120) return "East";
            if (yRotation <= 150) return "South East";
            if (yRotation <= 210) return "South";
            if (yRotation <= 240) return "South West";
            if (yRotation <= 300) return "West";
            return "North West"; // Covers 300 to 330
        }

       public override void OnUpdate()
       {
           if (playerContainer != null && playerContainer.childCount > 5)
           {
               player = playerContainer.GetChild(5).gameObject;
           }
           else
           {
               TriggerFade(false);
               return;
           }

           if (player == null)
            {
                if(!fade || textMesh.color.a > 0)
                    TriggerFade(false);
            }
            else
            {
                if(textMesh.color.a < 1)
                    TriggerFade(true);
                float yRotation = player.transform.rotation.eulerAngles.y;
                string direction = GetDirection(yRotation);
                textGameObject.GetComponent<TextMeshProUGUI>().text = direction;
            }
        }
       
       public void TriggerFade(bool fadeIn)
       {
           isFadingIn = fadeIn;
           MelonCoroutines.Start(FadeText());
       }

       private IEnumerator FadeText()
       {
           fade = true;
           float targetAlpha = isFadingIn ? 1.0f : 0.0f;
           Color currentColor = textMesh.color;
           float startAlpha = currentColor.a;

           for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / fadeDuration)
           {
               Color newColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(startAlpha, targetAlpha, t));
               textMesh.color = newColor;
               yield return null;
           }
           // Ensure the final alpha is set correctly
           textMesh.color = new Color(currentColor.r, currentColor.g, currentColor.b, targetAlpha);
           fade = false;
       }
    }
}
