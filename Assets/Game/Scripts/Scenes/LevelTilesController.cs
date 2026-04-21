using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Scripts.Scenes;
using Game.Services;
using UnityEngine;

namespace Game {
    public class LevelTilesController {
        private const float Sqrt3 = 1.732f;
        private LevelTilesConfig _config;
        private readonly IAddressablesService _addressablesService;

        private int _levelIndex = 1;
        private (int col, int row) _curTileIndexes;
        private GameObject _curTile;
        private Dictionary<(int, int), GameObject> Tiles = new Dictionary<(int, int), GameObject>(4);
        
        public LevelTilesController(LevelTilesConfig defaultConfig, IAddressablesService addressablesService) {
            _config = defaultConfig;
            _addressablesService = addressablesService;
        }

        private float Height => Sqrt3 * _config.TileSideSize;
        private float Width => 2 * _config.TileSideSize;
        private float HeightStep => Height * 0.5f;
        private float WidthStep => _config.TileSideSize * 3f;
        
        public async UniTask SetAnotherLevel(LevelTilesConfig config) {
            _config = config;
            _levelIndex = _config.LevelIndex;
            ClearAll();
            await ShowTile(0, 0);
            SelectTile((0, 0));
        }

        private void ClearAll() {
            foreach (var tile in Tiles.Values) {
                HideTile(tile).Forget();
            }
        }

        private void SelectTile((int, int) indexes) {
            _curTile = Tiles[indexes];
            _curTileIndexes = indexes;
        }

        public async UniTask ShowTile(int col, int row) {
            bool isEvenRow = _curTileIndexes.row % 2 == 0;
            var deltaCol = _curTileIndexes.col - col;
            var deltaRow = _curTileIndexes.row - row;

            UnloadOppositeTile();
            await LoadTile(); //.Forget(ex => Debug.LogError($"Ошибка загрузки плитки: {ex}"));

            async UniTask LoadTile() {
                var (x, z) = GetCoords(col, row);
                var tile = await _addressablesService.InstantiateAsync($"{_levelIndex}__{col}_{row}",
                    new Vector3(x, -_config.SpawnDepth, z));
                Tiles[(col, row)] = tile;
                await TweenTile(tile, true);
            }

            void UnloadOppositeTile() {
                if (deltaCol == 0) {
                    HideTile(_curTileIndexes.col, _curTileIndexes.row - deltaRow).Forget();
                }
                else {
                    if (deltaRow == 0) {
                        HideTile(_curTileIndexes.col - deltaCol,
                            (isEvenRow ? _curTileIndexes.row + 1 : _curTileIndexes.row - 1)).Forget();
                    }
                    else {
                        HideTile(_curTileIndexes.col - deltaCol, _curTileIndexes.row).Forget();
                    }
                }
            }
        }
        
        public async UniTaskVoid HideTile(int col, int row){
            if (Tiles.TryGetValue((col, row), out GameObject tile)){
                await HideTile(tile);
            }
        }

        private async UniTask HideTile(GameObject tile) {
            if (tile == null) {
                return;
            }

            await TweenTile(tile, false);
            tile.SetActive(false);
            tile.Release();
        }

        private (int col, int row) CheckPosition(Vector2 player, float tileSide) {
            var position = _curTile.transform.position;
            var deltaZ = player.y - position.z;
            var deltaX = player.x - position.x;

            if (Mathf.Abs(deltaZ) > 0.5f * Sqrt3 * tileSide)
                return (_curTileIndexes.col, _curTileIndexes.row + (deltaZ > 0 ? 1 : -1));

            //if (Mathf.Abs(deltaX) <= TileSideSize * 0.5f + (HalfHeight - Mathf.Abs(deltaZ)) * TileSideSize * 0.25f / HalfHeight)
            if (Mathf.Abs(deltaX) <= tileSide * 0.25f - Mathf.Abs(deltaZ) / (Sqrt3 * 2))
                return _curTileIndexes;

            return (_curTileIndexes.col + (deltaX > 0 ? 1 : -1), _curTileIndexes.row + (deltaZ > 0 ? 0 : -1));
        }

        private (float, float) GetCoords(int col, int row) {
            bool isEvenCol = col % 2 == 0;
            var result = (col * WidthStep / 2f, (isEvenCol ? (Sqrt3 * _config.TileSideSize * 0.5f) : 0) +
                                                row * Sqrt3 * _config.TileSideSize);
            //Debug.Log($"{col},{row} => {result}");
            return result;
        }

        private async UniTask TweenTile(GameObject tile, bool on) {
            Tweener tween =
                tile.transform.DOMoveY((on ? 0 : -1) * _config.SpawnDepth, _config.Duration);
            await tween.AwaitForComplete();
        }
    }
}