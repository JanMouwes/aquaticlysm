using UnityEngine;
using UnityEngine.EventSystems;
using Util;

public class ActionController : MonoBehaviour
{
    private bool _priority;

    private void Awake()
    {
        GlobalStateMachine.instance.StateChanged += ToggleEnable;
    }

    private void OnDestroy()
    {
        GlobalStateMachine.instance.StateChanged -= ToggleEnable;
    }

    private void ToggleEnable(IState state) =>this.enabled = state is Play;

    // Update is called once per frame.
    void Update()
    {
        if (Input.GetButtonDown("RightMouseButton"))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                // Set the priority with the shift click.
                _priority = Input.GetButton("Shift");

                // Use a raycast to register a game object and send the data to the selected entities.
                if (MouseUtil.TryRaycastAtMousePosition(100, out RaycastHit hit))
                    foreach (Selectable selectable in SelectionController.selectedEntities)
                    {
                        IAction action = selectable.gameObject.GetComponent<IAction>();
                        if(action != null)
                            action.ActionHandler(hit, _priority);
                    }

            }
        }
    }
}