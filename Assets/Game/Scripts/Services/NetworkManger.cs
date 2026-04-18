using UnityEngine;

namespace Game
{
    public class NetworkManager : MonoBehaviour
    {
        public static NetworkManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                InitializeNetwork();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void InitializeNetwork()
        {
            // Здесь инициализация Photon Fusion или другого сетевого решения
            Debug.Log("Network initialized");
        }

        public void Connect()
        {
            // Логика подключения к серверу
            Debug.Log("Connecting to server...");
        }

        public void Disconnect()
        {
            // Логика отключения от сервера
            Debug.Log("Disconnecting from server...");
        }

        public bool IsConnected()
        {
            // Возвращает статус подключения
            return false; // заменить на реальную проверку
        }

        // Добавьте методы для отправки и получения сетевых сообщений, синхронизации и т.п.
    }
}