
namespace SwapTiles.Models.Interaction
{
 public class InputLocker
 {
  private ILockingSource _lockSource;

  public bool CanInteract(ILockingSource source)
  {
   return _lockSource == null || (_lockSource != null && source == _lockSource);
  }
  
  public bool TryLock(ILockingSource lockSource)
  {
   if (_lockSource != null)
   {
    return _lockSource == lockSource;
   }
   
   _lockSource = lockSource;
   return true;
  }
  
  public void Unlock(ILockingSource lockSource)
  {
   if(_lockSource != lockSource)
    return;
   
   _lockSource = null;
  }

  public bool IsBlockedBy(ILockingSource lockingSource)
  {
   return _lockSource == lockingSource;
  }
 }
}