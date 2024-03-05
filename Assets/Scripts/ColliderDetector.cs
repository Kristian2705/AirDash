using UnityEngine;
using UnityEngine.Events;

public class ColliderDetector : MonoBehaviour
{
    public UnityEvent<GameObject> onGateTrigger;
    public UnityEvent onCollisionEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Gate"))
        {
            onGateTrigger.Invoke(other.gameObject);
        }
    }

    //private void UpdateFlames(Collider other)
    //{
    //    other.transform.parent.GetComponent<Gate>().ToggleFlames(false);
    //    other.transform.parent.GetComponent<Animation>().Stop();

    //    int index = other.transform.parent.GetSiblingIndex();

    //    if (other.transform.parent.parent.childCount > index + 1)
    //    {
    //        index += 1;
    //        Transform nextGate = other.transform.parent.parent.GetChild(index);
    //        if (nextGate)
    //        {
    //            nextGate.GetComponent<Gate>().ToggleFlames(true);
    //            nextGate.GetComponent<Animation>().Play();
    //        }
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        onCollisionEnter.Invoke();
    }

    //private void FixedUpdate()
    //{
    //    RaycastHit hit;
    //    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
    //    {
    //        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
    //    }
    //}
}
