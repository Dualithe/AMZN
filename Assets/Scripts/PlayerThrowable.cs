using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading.Tasks;

public class PlayerThrowable : MonoBehaviour
{
    [SerializeField] private float throwForce;
    [SerializeField] private float pickupRange;
    
    [Space]
    [SerializeField] private GameObject rangeIndicator;
    [SerializeField] private Transform hands;
    [SerializeField] private Transform boxHandler;
    public BoxScript pickedUpBox = null;

    private void Start() {
        hands.gameObject.SetActive(false);
    }

    private void OnEnable() { 
        InputHandler.Instance.event_Pickup.performed += performPickup;
        InputHandler.Instance.event_Throw.started += performAim;
        InputHandler.Instance.event_Throw.canceled += performThrow;
    }

    private void OnDisable() {
        InputHandler.Instance.event_Pickup.performed -= performPickup;
        InputHandler.Instance.event_Throw.started -= performAim;
        InputHandler.Instance.event_Throw.canceled -= performThrow;
    }

    public void PickupBox(BoxScript box) {
        pickedUpBox = box;
        hands.gameObject.SetActive(true);
        box.GetComponent<Collider2D>().enabled = false;
        box.body.velocity = Vector2.zero;
        Level.Current.MoveBox(box, boxHandler);
        box.transform.localPosition = Vector3.zero;
        box.body.isKinematic = true;
    }

    public void ThrowBox(Vector2 velocity) {
        hands.gameObject.SetActive(false);
        var box = pickedUpBox;
        box.body.isKinematic = false;
        pickedUpBox = null;
        Level.Current.ReturnBox(box);
        box.GetComponent<Collider2D>().enabled = true;
        box.body.velocity = velocity;

        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), box.GetComponent<Collider2D>(), true);
        Timer.StartOneshotTimer(this, 0.3f, () => {
            if (box != null) {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), box.GetComponent<Collider2D>(), false);
            }
        });
    }

    public void performAim(InputAction.CallbackContext context)
    {
        if (pickedUpBox != null) {
            rangeIndicator.SetActive(true);
        }
    }
    public void performThrow(InputAction.CallbackContext context)
    {
        if (pickedUpBox != null)
        {
            var throwDir = (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - (Vector2)boxHandler.position;
            var throwVelocity = throwDir.normalized * throwForce;

            ThrowBox(throwVelocity);
        }
    }

    public void performPickup(InputAction.CallbackContext context)
    {
        var nearestBox = Level.Current.FindNearestBox(transform.position);

        if (nearestBox != null && Level.Current.IsBoxPickable(nearestBox)) {
            var dis = (transform.position - nearestBox.transform.position).magnitude;
            if (dis <= pickupRange) {
                PickupBox(nearestBox);
            }
        }
    }
}
