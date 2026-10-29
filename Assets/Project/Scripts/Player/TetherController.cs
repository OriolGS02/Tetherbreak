using UnityEngine;

public class TetherController : MonoBehaviour
{
    
    PlayerInputReader playerInputs;
    [SerializeField]
    LineRenderer lineRenderer;

    public bool autoAim;
    Camera cam;

    [SerializeField]
    GameObject grabbedObject;

    [SerializeField]
    float SlowPersentage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInputs = GetComponent<PlayerInputReader>();
        lineRenderer.SetPosition(0, lineRenderer.gameObject.transform.parent.position);
        
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        TetherAim();
        lineRenderer.SetPosition(0, lineRenderer.gameObject.transform.parent.position);
        if (grabbedObject != null)
        {
            lineRenderer.SetPosition(1,Vector3.MoveTowards(lineRenderer.GetPosition(1), grabbedObject.transform.position,50*Time.deltaTime)/* grabbedObject.transform.position*/);
            lineRenderer.gameObject.SetActive(true);
        }
        else 
        {
            lineRenderer.SetPosition(1, lineRenderer.gameObject.transform.parent.position);
            lineRenderer.gameObject.SetActive(false);
        }

            TetherAim(); 
    }

    void TetherAim() 
    {
        Ray ray = cam.ScreenPointToRay(playerInputs.Aim);

        Debug.DrawRay(cam.transform.position, ray.direction);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f))
        {       
            
            
            if (playerInputs.Attack && grabbedObject != null) 
            {
                Vector3 hitpoint=hit.point;
                Rigidbody rb=grabbedObject.GetComponent<Rigidbody>();
                rb.constraints=RigidbodyConstraints.None;
                Vector3 dir = hitpoint+(Vector3.up*0.5f) - grabbedObject.transform.position;

                Vector3 directionValue = dir * 3;
                
                rb.AddForce(directionValue-((directionValue*SlowPersentage)/100),ForceMode.Impulse);

                grabbedObject=null;
            }

            if (playerInputs.Attack && grabbedObject == null&&hit.collider.CompareTag("grab")/*check if its grabable*/)
            {
                grabbedObject = hit.collider.gameObject;
                grabbedObject.transform.position += (Vector3.up * 0.5f);
                Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.FreezePositionY;

            }







        }
    }
}
