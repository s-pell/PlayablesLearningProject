using UnityEngine;
using UnityEngine.UIElements;

namespace Game
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        private UIDocument uiDocument;
        private VisualElement root;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                InitializeUI();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void InitializeUI()
        {
            uiDocument = GetComponent<UIDocument>();
            if (uiDocument == null)
            {
                Debug.LogError("UIManager requires a UIDocument component on the same GameObject.");
                return;
            }

            root = uiDocument.rootVisualElement;

            // Здесь можно инициализировать основные UI элементы, подписываться на события и т.д.
        }

        /// <summary>
        /// Пример метода для показа или скрытия UI элемента по имени
        /// </summary>
        public void SetElementVisibility(string elementName, bool visible)
        {
            var element = root.Q<VisualElement>(elementName);
            if (element != null)
            {
                element.style.display = visible ? DisplayStyle.Flex : DisplayStyle.None;
            }
            else
            {
                Debug.LogWarning($"UI element '{elementName}' not found.");
            }
        }

        // Добавляйте методы для управления UI по вашему проекту
    }
}