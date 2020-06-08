using UnityEngine;

namespace Events
{
   public class EventStateHandeler : ScriptableObject
   {
      private SelectionController _selectionController;
      private void Awake()
      {
         _selectionController= FindObjectOfType<SelectionController>();
      }

      public void EnterState()
      {
         _selectionController.enabled = false;
         Time.timeScale = 0f;
      }
      public void ExitState()
      {
         _selectionController.enabled = true;
         Time.timeScale = 1f;
      }
   
   }
}
