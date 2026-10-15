using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Bird : MonoBehaviour
{
    public static Bird ActiveBird;
    
    protected Rigidbody body;
    
    private bool abilityUsed;
    
    protected virtual void Awake()
    {
        body = GetComponent<Rigidbody>();
    }
    
    public void Launched()
    {
        ActiveBird = this;
        abilityUsed = false;
    }
    
    public void UseAbility()
    {
        if (abilityUsed) { return; }
        abilityUsed = true;
        
        Ability();
    }
    
    protected virtual void Ability()
    {
        
    }
}
