using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading.Tasks;

public class PlayerThrowable : MonoBehaviour
{
    public BoxScript currBox = null;
    [SerializeField] private float range;
    [SerializeField] private float throwForce;
    [SerializeField] private float pickupRange;
    [SerializeField] private GameObject rangeIndicator;
    private Vector2 throwDir = Vector2.zero;
    public PlayerMovement pm;

    private void Start()
    {
        pm = GetComponent<PlayerMovement>();
    }

    public void performAim(InputAction.CallbackContext context)
    {
        if (currBox != null)
        {
            rangeIndicator.SetActive(true);
        }
    }
    public void performThrow(InputAction.CallbackContext context)
    {
        if (currBox != null)
        {
            throwDir = (Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position);
            var x = currBox;
            currBox.clearAttached();
            x.attachCD = 0.2f;
            x.Knockback(throwDir.normalized, throwForce);
            x.gameObject.layer += 1;
            x.called = false;
            x.collisionCD = 0.2f;
            rangeIndicator.SetActive(false);
            pm.isHolding(false);
        }
    }


    public void performPickup(InputAction.CallbackContext context)
    {
        var g = GameObject.FindGameObjectsWithTag("Box");
        var currPos = transform.position;
        var closest = Vector2.positiveInfinity;
        GameObject closestBox = null;
        foreach (GameObject box in g)
        {
            Vector2 x = box.transform.position - currPos;
            if (x.magnitude < closest.magnitude)
            {
                closest = x;
                closestBox = box;
            }
        }
        if (closest.magnitude < pickupRange)
        {
            closestBox.GetComponent<BoxScript>().pickMeUp();
            pm.isHolding(true);
        }
    }
}
