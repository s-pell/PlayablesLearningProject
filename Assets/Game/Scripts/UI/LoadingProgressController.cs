using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game
{
    public class LoadingProgressController : MonoBehaviour, IFontContainer
    {
        public UIDocument uiDocument;
        private ProgressBar progressBar;
        private AssetLoader<Font> fontLoader = null;



        void OnEnable()
        {
            if (fontLoader == null)
            {
                fontLoader = new AssetLoader<Font>("font");
                var root = uiDocument.rootVisualElement;
                progressBar = root.Q<ProgressBar>("loadingProgressBar");
                progressBar.value = 0f;
            }

            ApplyFontToUI().Forget();
        }

        private void OnDisable()
        {
            fontLoader.Release();
            Debug.Log("Выгрузить шрифт");
        }


        public void SetProgress(float progress)
        {
            progressBar.value = Mathf.Clamp01(progress);
        }

        public async UniTask ApplyFontToUI()
        {
            Debug.Log("Загрузить шрифт");
            var font = await fontLoader.LoadAsync();
            progressBar.style.unityFont = font;
        }
    }
}