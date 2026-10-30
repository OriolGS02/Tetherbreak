using UnityEngine;

public class Enemy : Grabable
{


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnGrab()
    {
        base.OnGrab();


    }

    public enum EnemyType 
    {
        Flying
    }
}
