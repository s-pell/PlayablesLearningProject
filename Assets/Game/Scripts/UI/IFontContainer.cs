using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public interface IFontContainer
    {
        public const string FontAssetName = "darla";//"liberationSans"
        UniTask ApplyFontToUI();
    }
}