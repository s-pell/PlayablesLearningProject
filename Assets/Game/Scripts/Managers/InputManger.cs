using UnityEngine;

namespace Game
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // Пример обработки простого ввода (нажатие клавиши)
        public bool GetJumpPressed()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }

        // Пример получения осей движения
        public Vector2 GetMovement()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            return new Vector2(horizontal, vertical);
        }

        // Здесь можно добавить другие методы для обработки ввода
    }
}