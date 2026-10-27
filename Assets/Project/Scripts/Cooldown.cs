using UnityEngine;

[System.Serializable]
public class Cooldown
{
    [SerializeField] private float cooldownTime;
    private float _nextFireTime;

    public float CooldownTime 
    {
        set 
        {
            if (value<0)
            {
                cooldownTime = 2;
            }else
            {
                cooldownTime = value;
            }
        }
    }
    public bool InCooldown => Time.time < _nextFireTime;
    public void StartCooldown()=> _nextFireTime = Time.time+cooldownTime;

}
