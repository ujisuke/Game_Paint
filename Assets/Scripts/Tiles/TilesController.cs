using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Tiles
{
    public class TilesController : MonoBehaviour
    {
        private TilesFacade tilesFacade;
        [SerializeField] private Tilemap tilemap;

        private void Awake()
        {
            tilesFacade = new TilesFacade(tilemap);
        }
    }
}
