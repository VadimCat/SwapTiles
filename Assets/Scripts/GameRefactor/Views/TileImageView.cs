using Client.Views;
using UnityEngine;

namespace GameRefactor.Views
{
 public class TileImageView: MonoBehaviour
 {
  public class Factory
  {
   private readonly TileImageView _prototype;
   private readonly GridField _gridField;

   public Factory(TileImageView prototype, GridField gridField)
   {
    _prototype = prototype;
    _gridField = gridField;
   }
   public TileImageView Create(Sprite sprite, Vector3Int pos, int rows, int columns)
   {
    var instance = Instantiate(_prototype);
    instance.Construct(sprite, pos, rows, columns, _gridField.CellSize);
    return instance;
   }
  }
  
  private static readonly int BaseMap = Shader.PropertyToID("_BaseMap");
  private static readonly int BaseMapSt = Shader.PropertyToID("_BaseMap_ST");
  
  [SerializeField] private MeshRenderer imageMeshRenderer;
  
  private MaterialPropertyBlock _materialPropertyBlock;

  private void Construct(Sprite sprite, Vector3Int position, int rows, int columns, Vector3 cellSize)
  {
   transform.localScale = new Vector3(cellSize.x, cellSize.y, 1);
   
   _materialPropertyBlock = new MaterialPropertyBlock();
   _materialPropertyBlock.SetTexture(BaseMap, sprite.texture);
   
   float w = 1f/ rows;
   float h = 1f/ columns;
   float x = w * position.x;
   float y = h * position.y;
   
   _materialPropertyBlock.SetVector(BaseMapSt, new Vector4(w,h, x, y));
   imageMeshRenderer.SetPropertyBlock(_materialPropertyBlock);
  }
 }
}