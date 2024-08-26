using UnityEngine;

namespace SwapTiles.Views
{
 public class TileImageView: MonoBehaviour
 {
  public class Factory
  {
   private readonly TileImageView _prototype;
   private readonly GridField _gridField;
   private readonly Sprite _sprite;
   private readonly int _rows;
   private readonly int _columns;

   public Factory(TileImageView prototype, GridField gridField, Sprite sprite, int columns, int rows)
   {
    _prototype = prototype;
    _gridField = gridField;
    _sprite = sprite;
    _rows = rows;
    _columns = columns;
   }
   public TileImageView Create(Vector3Int pos)
   {
    var instance = Instantiate(_prototype);
    instance.Construct(_sprite, pos, _rows, _columns, _gridField.CellSize);
    return instance;
   }
  }
  
  private static readonly int BaseMap = Shader.PropertyToID("_BaseMap");
  private static readonly int BaseMapSt = Shader.PropertyToID("_BaseMap_ST");
  
  [SerializeField] private MeshRenderer imageMeshRenderer;
  
  private MaterialPropertyBlock _materialPropertyBlock;

  private void Construct(Sprite sprite, Vector3Int position, int columns, int rows, Vector3 cellSize)
  {
   transform.localScale = new Vector3(cellSize.x, cellSize.y, 1);
   
   _materialPropertyBlock = new MaterialPropertyBlock();
   _materialPropertyBlock.SetTexture(BaseMap, sprite.texture);
   
   float w = 1f/ columns;
   float h = 1f/ rows;
   float x = w * position.x;
   float y = h * position.y;
   
   _materialPropertyBlock.SetVector(BaseMapSt, new Vector4(w,h, x, y));
   imageMeshRenderer.SetPropertyBlock(_materialPropertyBlock);
  }
 }
}