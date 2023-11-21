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

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            // I need to change this to just if(!sceneName == "ParseScene" or whatever the name was)
            if (sceneName == "MainMenu" || sceneName == "InitScene" || sceneName == "InitSceneLaunchOptions")
                return;

            // Get the local player
            player = StartOfRound.Instance.localPlayerController.gameObject;

            // Make the text game object
            if (textGameObject == null)
                textGameObject = new GameObject("TextMeshPro Object");

            if (textMesh == null)
                textMesh = textGameObject.AddComponent<TextMeshProUGUI>();

            textMesh.text = "Your Text Here";
            textMesh.fontSize = 36;

            // Set the text GameObject as a child of a canvas
            textGameObject.transform.SetParent(GameObject.Find("Systems/UI/Canvas").transform, false);
            textGameObject.transform.name = "Compass Text";

            // Make text upper left corner
            textGameObject.transform.localPosition = new Vector3(-340, 210, 0);

            // make text mesh colour rgb(222, 109, 30) [Colour of in game text]
            textMesh.color = new Color(0.87f, 0.43f, 0.12f);
            textMesh.material = new Material(Shader.Find("TextMeshPro/Distance Field"));
        }

        // Shrimply gets the direction of the player and assigns in to a direction
        private string GetDirection(float yRotation)
        {
            yRotation = (yRotation + 360) % 360; // Normalize rotation to 0-360 range

            if (yRotation <= 30 || yRotation > 330)
                return "North";
            if (yRotation <= 60)
                return "North East";
            if (yRotation <= 120)
                return "East";
            if (yRotation <= 150)
                return "South East";
            if (yRotation <= 210)
                return "South";
            if (yRotation <= 240)
                return "South West";
            if (yRotation <= 300)
                return "West";

            return "North West"; // Covers 300 to 330
        }

        public override void OnUpdate()
        {
            // i dont want my logs flooding with errors qnq
            try
            {
                if (player = null)
                    return;

                // Just the direction 
                float yRotation = player.transform.rotation.eulerAngles.y;
                string direction = GetDirection(yRotation);
                textGameObject.GetComponent<TextMeshProUGUI>().text = direction;
            }
            catch { }
        }
    }
}
