using UnityEngine;

namespace Game
{
    public class AnalyticsManager : MonoBehaviour
    {
        public static AnalyticsManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                InitializeAnalytics();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void InitializeAnalytics()
        {
            // Инициализация выбранного аналитического сервиса
            Debug.Log("Analytics initialized");
        }

        /// <summary>
        /// Отправка события с именем и параметрами
        /// </summary>
        public void SendEvent(string eventName, params (string key, object value)[] parameters)
        {
            // Пример: формирование словаря параметров
            var eventParams = new System.Collections.Generic.Dictionary<string, object>();
            foreach (var param in parameters)
            {
                eventParams[param.key] = param.value;
            }

            // Здесь вызов API аналитики, например:
            // UnityEngine.Analytics.Analytics.CustomEvent(eventName, eventParams);

            Debug.Log($"Analytics event sent: {eventName} with params: {eventParams.Count}");
        }
    }
}