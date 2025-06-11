using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class LocalizationManager : MonoBehaviour
    {
        public static LocalizationManager Instance;

        // Текущий язык (например, "en", "ru")
        public string CurrentLanguage { get; private set; } = "en";

        // Словарь ключ → перевод
        private Dictionary<string, string> localizedTexts = new Dictionary<string, string>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                LoadLocalization(CurrentLanguage);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Загрузка локализации для указанного языка
        /// </summary>
        public void LoadLocalization(string languageCode)
        {
            CurrentLanguage = languageCode;
            localizedTexts.Clear();

            // Здесь загрузите данные локализации из файлов, Addressables или другого источника
            // Пример заполнения словаря вручную:
            if (languageCode == "en")
            {
                localizedTexts.Add("welcome", "Welcome");
                localizedTexts.Add("start_game", "Start Game");
            }
            else if (languageCode == "ru")
            {
                localizedTexts.Add("welcome", "Добро пожаловать");
                localizedTexts.Add("start_game", "Начать игру");
            }
            else
            {
                Debug.LogWarning($"Localization for language {languageCode} not found.");
            }
        }

        /// <summary>
        /// Получить перевод по ключу
        /// </summary>
        public string GetText(string key)
        {
            if (localizedTexts.TryGetValue(key, out var value))
            {
                return value;
            }
            else
            {
                Debug.LogWarning($"Localization key '{key}' not found.");
                return key;
            }
        }
    }
}