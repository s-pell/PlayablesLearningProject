using UnityEngine;
using UnityEngine.UIElements;

namespace Game
{
    public class LoadingProgressController : MonoBehaviour
    {
        public UIDocument uiDocument;
        private ProgressBar progressBar;

        void OnEnable()
        {
            var root = uiDocument.rootVisualElement;
            progressBar = root.Q<ProgressBar>("loadingProgress");
            progressBar.value = 0f;
        }

        public void SetProgress(float progress)
        {
            progressBar.value = Mathf.Clamp01(progress);
        }
    }
}

