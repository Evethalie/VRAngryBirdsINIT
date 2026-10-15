using UnityEngine;
using UnityEngine.InputSystem;

public class BirdAbilityInput : MonoBehaviour
{
    [Tooltip("Drag in an Activate action from XRI Default Input Actions.")]
    [SerializeField] private InputActionReference activateAction;

    private void OnEnable()
    {
        if (activateAction == null) { return; }

        // performed fires the moment the button is pressed.
        activateAction.action.performed += OnPressed;
        activateAction.action.Enable();
    }

    private void OnDisable()
    {
        if (activateAction == null) { return; }

        // Always unsubscribe from what you subscribed to.
        activateAction.action.performed -= OnPressed;
    }

    private void OnPressed(InputAction.CallbackContext context)
    {
        if (Bird.ActiveBird != null)
        {
            Bird.ActiveBird.UseAbility();
        }
    }
}
