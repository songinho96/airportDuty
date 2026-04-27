#if UNITY_EDITOR
using AirportSurvival.Gameplay;
using AirportSurvival.UI;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AirportSurvival.EditorTools
{
    public static class VerticalSliceSceneBuilder
    {
        [MenuItem("Airport Survival/Build Vertical Slice 001 Scene")]
        public static void BuildScene()
        {
            Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            scene.name = "DayScene";

            AirportGameController gameController = CreateGameController();
            PlayerInteractor playerInteractor = CreatePlayer();
            CreateEnvironment();
            CreateTrashItems(gameController);
            CreateLightingAndCamera();
            CreateHud(gameController, playerInteractor);

            if (!AssetDatabase.IsValidFolder("Assets/Scenes"))
            {
                AssetDatabase.CreateFolder("Assets", "Scenes");
            }

            EditorSceneManager.SaveScene(scene, "Assets/Scenes/DayScene.unity");
            AssetDatabase.SaveAssets();
        }

        private static AirportGameController CreateGameController()
        {
            GameObject obj = new GameObject("GameController");
            return obj.AddComponent<AirportGameController>();
        }

        private static PlayerInteractor CreatePlayer()
        {
            GameObject player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            player.name = "Player";
            player.transform.position = new Vector3(0f, 1f, -6f);
            player.AddComponent<CharacterController>();
            player.AddComponent<SimplePlayerController>();
            return player.AddComponent<PlayerInteractor>();
        }

        private static void CreateEnvironment()
        {
            GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
            floor.name = "Airport Floor";
            floor.transform.position = Vector3.zero;
            floor.transform.localScale = new Vector3(24f, 0.2f, 18f);

            CreateStation("Restaurant Station", new Vector3(-8f, 0.8f, 4f), Color.yellow);
            CreateStation("Check-In Counter", new Vector3(0f, 0.8f, 5f), Color.cyan);
            CreateStation("Information Desk", new Vector3(8f, 0.8f, 4f), Color.green);
        }

        private static void CreateStation(string name, Vector3 position, Color color)
        {
            GameObject station = GameObject.CreatePrimitive(PrimitiveType.Cube);
            station.name = name;
            station.transform.position = position;
            station.transform.localScale = new Vector3(3f, 1.4f, 1.2f);
            station.GetComponent<Renderer>().sharedMaterial = CreateMaterial($"{name} Material", color);
        }

        private static void CreateTrashItems(AirportGameController gameController)
        {
            Vector3[] positions =
            {
                new Vector3(-5f, 0.4f, -1f),
                new Vector3(2f, 0.4f, 0f),
                new Vector3(6f, 0.4f, -3f),
                new Vector3(-2f, 0.4f, 4f)
            };

            for (int i = 0; i < positions.Length; i++)
            {
                GameObject trash = GameObject.CreatePrimitive(PrimitiveType.Cube);
                trash.name = $"Trash {i + 1}";
                trash.transform.position = positions[i];
                trash.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                trash.GetComponent<Renderer>().sharedMaterial = CreateMaterial("Trash Material", new Color(0.45f, 0.45f, 0.45f));
                TrashItem item = trash.AddComponent<TrashItem>();

                SerializedObject serializedTrash = new SerializedObject(item);
                serializedTrash.FindProperty("gameController").objectReferenceValue = gameController;
                serializedTrash.ApplyModifiedProperties();
            }
        }

        private static void CreateLightingAndCamera()
        {
            GameObject lightObj = new GameObject("Directional Light");
            Light light = lightObj.AddComponent<Light>();
            light.type = LightType.Directional;
            light.intensity = 1f;
            lightObj.transform.rotation = Quaternion.Euler(50f, -30f, 0f);

            GameObject cameraObj = new GameObject("Main Camera");
            Camera camera = cameraObj.AddComponent<Camera>();
            cameraObj.tag = "MainCamera";
            cameraObj.transform.position = new Vector3(0f, 12f, -14f);
            cameraObj.transform.rotation = Quaternion.Euler(55f, 0f, 0f);
            camera.clearFlags = CameraClearFlags.Skybox;
        }

        private static void CreateHud(AirportGameController gameController, PlayerInteractor playerInteractor)
        {
            GameObject eventSystemObj = new GameObject("EventSystem");
            eventSystemObj.AddComponent<EventSystem>();
            eventSystemObj.AddComponent<StandaloneInputModule>();

            Canvas canvas = CreateCanvas();
            AirportHudView hud = canvas.gameObject.AddComponent<AirportHudView>();
            DaySummaryView summary = canvas.gameObject.AddComponent<DaySummaryView>();

            Text stamina = CreateText(canvas.transform, "StaminaText", new Vector2(12f, -12f), "Stamina: 100");
            Text money = CreateText(canvas.transform, "MoneyText", new Vector2(12f, -42f), "Money: 0");
            Text scold = CreateText(canvas.transform, "ScoldText", new Vector2(12f, -72f), "Scolded: 0");
            Text cleaning = CreateText(canvas.transform, "CleaningText", new Vector2(12f, -102f), "Cleaning Lv 0 / Pts 0");
            Text timer = CreateText(canvas.transform, "TimerText", new Vector2(-160f, -12f), "Day: 120s", TextAnchor.UpperRight);
            Text prompt = CreateText(canvas.transform, "PromptText", new Vector2(0f, 80f), string.Empty, TextAnchor.LowerCenter);
            Text manager = CreateText(canvas.transform, "ManagerDialogueText", new Vector2(0f, -160f), string.Empty, TextAnchor.MiddleCenter);
            manager.gameObject.SetActive(false);
            Button endDayButton = CreateButton(canvas.transform, "EndDayButton", new Vector2(-100f, 54f), "End Day");

            GameObject summaryPanel = CreatePanel(canvas.transform, "SummaryPanel");
            Text summaryText = CreateText(summaryPanel.transform, "SummaryText", Vector2.zero, string.Empty, TextAnchor.MiddleCenter);
            summaryPanel.SetActive(false);

            SerializedObject hudObject = new SerializedObject(hud);
            hudObject.FindProperty("gameController").objectReferenceValue = gameController;
            hudObject.FindProperty("playerInteractor").objectReferenceValue = playerInteractor;
            hudObject.FindProperty("staminaText").objectReferenceValue = stamina;
            hudObject.FindProperty("moneyText").objectReferenceValue = money;
            hudObject.FindProperty("scoldText").objectReferenceValue = scold;
            hudObject.FindProperty("cleaningText").objectReferenceValue = cleaning;
            hudObject.FindProperty("timerText").objectReferenceValue = timer;
            hudObject.FindProperty("promptText").objectReferenceValue = prompt;
            hudObject.FindProperty("managerDialogueText").objectReferenceValue = manager;
            hudObject.FindProperty("endDayButton").objectReferenceValue = endDayButton;
            hudObject.ApplyModifiedProperties();

            SerializedObject summaryObject = new SerializedObject(summary);
            summaryObject.FindProperty("gameController").objectReferenceValue = gameController;
            summaryObject.FindProperty("panel").objectReferenceValue = summaryPanel;
            summaryObject.FindProperty("summaryText").objectReferenceValue = summaryText;
            summaryObject.ApplyModifiedProperties();
        }

        private static Canvas CreateCanvas()
        {
            GameObject canvasObj = new GameObject("HUD Canvas");
            Canvas canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObj.AddComponent<CanvasScaler>();
            canvasObj.AddComponent<GraphicRaycaster>();
            return canvas;
        }

        private static Text CreateText(Transform parent, string name, Vector2 anchoredPosition, string value, TextAnchor anchor = TextAnchor.UpperLeft)
        {
            GameObject obj = new GameObject(name);
            obj.transform.SetParent(parent, false);
            Text text = obj.AddComponent<Text>();
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            text.fontSize = 20;
            text.color = Color.white;
            text.alignment = anchor;
            text.text = value;

            RectTransform rect = text.rectTransform;
            rect.anchorMin = AnchorFromTextAnchor(anchor);
            rect.anchorMax = AnchorFromTextAnchor(anchor);
            rect.pivot = AnchorFromTextAnchor(anchor);
            rect.anchoredPosition = anchoredPosition;
            rect.sizeDelta = new Vector2(420f, 80f);
            return text;
        }

        private static Button CreateButton(Transform parent, string name, Vector2 anchoredPosition, string label)
        {
            GameObject obj = new GameObject(name);
            obj.transform.SetParent(parent, false);
            Image image = obj.AddComponent<Image>();
            image.color = new Color(0.15f, 0.18f, 0.22f, 0.95f);
            Button button = obj.AddComponent<Button>();

            RectTransform rect = obj.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(1f, 1f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(1f, 1f);
            rect.anchoredPosition = anchoredPosition;
            rect.sizeDelta = new Vector2(120f, 40f);

            Text text = CreateText(obj.transform, "Text", Vector2.zero, label, TextAnchor.MiddleCenter);
            text.rectTransform.anchorMin = Vector2.zero;
            text.rectTransform.anchorMax = Vector2.one;
            text.rectTransform.offsetMin = Vector2.zero;
            text.rectTransform.offsetMax = Vector2.zero;
            return button;
        }

        private static GameObject CreatePanel(Transform parent, string name)
        {
            GameObject obj = new GameObject(name);
            obj.transform.SetParent(parent, false);
            Image image = obj.AddComponent<Image>();
            image.color = new Color(0f, 0f, 0f, 0.82f);
            RectTransform rect = obj.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = Vector2.zero;
            rect.sizeDelta = new Vector2(520f, 360f);
            return obj;
        }

        private static Vector2 AnchorFromTextAnchor(TextAnchor anchor)
        {
            switch (anchor)
            {
                case TextAnchor.UpperRight:
                    return new Vector2(1f, 1f);
                case TextAnchor.LowerCenter:
                    return new Vector2(0.5f, 0f);
                case TextAnchor.MiddleCenter:
                    return new Vector2(0.5f, 0.5f);
                default:
                    return new Vector2(0f, 1f);
            }
        }

        private static Material CreateMaterial(string name, Color color)
        {
            Material material = new Material(Shader.Find("Standard"));
            material.name = name;
            material.color = color;
            return material;
        }
    }
}
#endif
