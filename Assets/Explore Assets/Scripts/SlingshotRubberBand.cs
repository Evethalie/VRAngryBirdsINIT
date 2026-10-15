using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(XRGrabInteractable))]
public class SlingshotBand : MonoBehaviour
{
    [Tooltip("The BirdSocket child. Drop a bird in here to load it.")]
    [SerializeField] private XRSocketInteractor birdSocket;

    [Tooltip("Its blue Z arrow is treated as downrange.")]
    [SerializeField] private Transform aimDirection;

    [SerializeField] private float snapBackSpeed = 15f;
    [SerializeField] private float maxStretch = 0.4f;
    [SerializeField] private float launchPower = 15f;

    private XRGrabInteractable grab;
    private Vector3 restPosition;
    private bool isHeld;
    private Collider launchedBirdCollider;

    private void Awake()
    {
        restPosition = transform.position;

        Rigidbody body = GetComponent<Rigidbody>();
        body.isKinematic = true;

        grab = GetComponent<XRGrabInteractable>();
        grab.trackRotation = false;
        grab.throwOnDetach = false;

        grab.selectEntered.AddListener(OnGrabbed);
        grab.selectExited.AddListener(OnReleased);
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        isHeld = true;
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        isHeld = false;

        Vector3 launchVelocity = -GetPullOffset() * launchPower;

        LaunchBird(launchVelocity);
    }

    private void LateUpdate()
    {
        if (isHeld)
        {
            transform.position = restPosition + GetPullOffset();
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position,
                                                              restPosition,
                                                              Time.deltaTime * snapBackSpeed);
        }
    }

    private Vector3 GetPullOffset()
    {
        Vector3 offset = transform.position - restPosition;

        if (aimDirection != null)
        {
            float forwardAmount = Vector3.Dot(offset, aimDirection.forward);

            if (forwardAmount > 0f)
            {
                offset -= aimDirection.forward * forwardAmount;
            }
        }

        return Vector3.ClampMagnitude(offset, maxStretch);
    }

    private void LaunchBird(Vector3 velocity)
    {
        if (birdSocket == null || birdSocket.hasSelection == false)
        {
            Debug.Log("Released with no bird loaded.");
            return;
        }

        IXRSelectInteractable bird = birdSocket.interactablesSelected[0];

        Rigidbody birdBody = bird.transform.GetComponent<Rigidbody>();
        if (birdBody == null) { return; }

        birdSocket.socketActive = false;
        birdSocket.interactionManager.SelectExit(birdSocket, bird);

        birdBody.isKinematic = false;
        birdBody.linearVelocity = velocity;

        launchedBirdCollider = bird.transform.GetComponent<Collider>();
        if (launchedBirdCollider != null)
        {
            launchedBirdCollider.enabled = false;
        }

        Invoke(nameof(EnableBirdCollider), 0.3f);
        Invoke(nameof(ReactivateSocket), 0.5f);
    }

    private void EnableBirdCollider()
    {
        if (launchedBirdCollider != null)
        {
            launchedBirdCollider.enabled = true;
        }
    }

    private void ReactivateSocket()
    {
        if (birdSocket != null)
        {
            birdSocket.socketActive = true;
        }
    }
}