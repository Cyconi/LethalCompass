using MelonLoader;
using UnityEngine;
using System.Collections;
using System;
using Unity.Netcode;
using UnityEngine.Networking;
using TMPro;


namespace LethalCompanyCompass
{
    public static class BuildInfo
    {
        public const string Name = "LethalCompanyCompass";
        public const string Author = "squidypal (Cyconi)";
        public const string Company = null;
        public const string Version = "1.1.0";
        public const string DownloadLink = null;
    }

    public class LethalCompanyCompass : MelonMod
    {
        private GameObject player;
        private GameObject textGameObject;
        private TextMeshProUGUI textMesh;
        private float fadeDuration = 1f;
        private bool isFadingIn;
        private bool fade = false;

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            if (sceneName == "MainMenu" || sceneName == "InitScene" || sceneName == "InitSceneLaunchOptions") 
                return;

            player = StartOfRound.Instance.localPlayerController.gameObject;

            if (textGameObject == null)
                textGameObject = new GameObject("TextMeshPro Object");

            TextMeshProUGUI textMesh = textGameObject.GetComponent<TextMeshProUGUI>() ?? textGameObject.AddComponent<TextMeshProUGUI>();

            textMesh.text = "Cumpiss";
            textMesh.fontSize = 36f;
            textMesh.color = new Color(0.87f, 0.43f, 0.12f);
            textMesh.material = new Material(Shader.Find("TextMeshPro/Distance Field"));

            textGameObject.transform.SetParent(GameObject.Find("Systems/UI/Canvas").transform, false);
            textGameObject.transform.name = "Compass Text";
            textGameObject.transform.localPosition = new Vector3(-340f, 210f, 0f);
        }


        private string GetDirection(float yRotation)
        {
            yRotation = (yRotation + 360f) % 360f;
            if (yRotation <= 30f || yRotation > 330f) 
                return "North";
            if (yRotation <= 60f) 
                return "North East";
            if (yRotation <= 120f) 
                return "East";
            if (yRotation <= 150f) 
                return "South East";
            if (yRotation <= 210f) 
                return "South";
            if (yRotation <= 240f) 
                return "South West";
            if (yRotation <= 300f) 
                return "West";
            
            return "North West";
        }

        public override void OnUpdate()
        {
            if (player == null && (!fade || textMesh.color.a > 0f))
                    TriggerFade(false);
            else
            {
                if (textMesh.color.a < 1f)
                    TriggerFade(true);

                float y = player.transform.rotation.eulerAngles.y;
                string direction = GetDirection(y);
                textMesh.text = direction;
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
            float targetAlpha = (isFadingIn ? 1f : 0f);
            Color currentColor = textMesh.color;
            float startAlpha = currentColor.a;
            for (float i = 0f; i < 1f; i += Time.deltaTime / fadeDuration)
            {
                textMesh.color = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(startAlpha, targetAlpha, i));
                yield return null;
            }
            textMesh.color = new Color(currentColor.r, currentColor.g, currentColor.b, targetAlpha);
            fade = false;
        }
    }
}
