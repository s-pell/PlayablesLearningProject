using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game
{
    public static class TilesExtensions
    {
        public static void Deconstruct(this Vector2Int vect, out int x, out int y)
        {
            x = vect.x;
            y = vect.y;
        }

        public static void Release(this GameObject tile)
        {
            Addressables.ReleaseInstance(tile);
            Debug.Log("Префаб выгружен");
        }
    }
}