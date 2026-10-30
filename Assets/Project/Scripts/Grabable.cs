using UnityEngine;

public class Grabable : MonoBehaviour
{

    public float resistance;
    Rigidbody rb;
    Vector3 throwedDirection;
    Vector3 rawDirection;
    GameObject grabbedGameObject=null;
    public virtual void OnGrab()
    {
        throwedDirection = Vector3.zero;
        grabbedGameObject = this.gameObject;
        rb=GetComponent<Rigidbody>();

        if (this is not Enemy) 
        {
            transform.position += (Vector3.up * 0.5f);
        }
        
        rb.constraints = RigidbodyConstraints.FreezeAll;
       
    }
    public virtual void OnThrow(Vector3 directionImpulse,Vector3 rawdirection)
    {
        throwedDirection = directionImpulse;
        rawDirection= rawdirection;
        rb.constraints = RigidbodyConstraints.None;

        rb.AddForce(directionImpulse - ((directionImpulse * resistance) / 100), ForceMode.Impulse);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.collider.gameObject.GetComponent<Grabable>()&& collision.collider.gameObject != grabbedGameObject) 
        {
            Rigidbody grabableobj=collision.collider.gameObject.GetComponent<Rigidbody>();
            grabableobj.constraints = RigidbodyConstraints.FreezeRotation;
            grabableobj.AddForce(rawDirection-((rawDirection*resistance)/100),ForceMode.Impulse);
        }
    }
}
