using UnityEngine;

public class Arrow : MonoBehaviour
{
   public float m_speed = 2000.0f;
   public Transform m_Tip = null;
   
   private Rigidbody m_rigidbody = null;
   private bool m_IsStopped = true;
   private Vector3 m_LastPosition = Vector3.zero;

   private void Awake() {
       m_rigidbody = GetComponent<Rigidbody>();
   }

   private void FixedUpdate() {
       if(m_IsStopped)
        return;

        //rotate
        m_rigidbody.MoveRotation(Quaternion.LookRotation(m_rigidbody.velocity, transform.up));

        //collision check
        RaycastHit hit;
        if(Physics.Linecast(m_LastPosition, m_Tip.position, out hit))
        {
            Stop(hit.collider.gameObject);
        }

        //last position
        m_LastPosition = m_Tip.position;
   
   }
    void Start()
    {
        m_LastPosition = transform.position;
    }
   private void Stop(GameObject hitObject)
   {
       m_IsStopped = true;

       transform.parent = hitObject.transform;

       m_rigidbody.isKinematic = true;
       m_rigidbody.useGravity = false;

       //damage
       CheckForDamage(hitObject);
   }

    private void CheckForDamage(GameObject hitObject)
    {
        MonoBehaviour[] behaviours = hitObject.GetComponents<MonoBehaviour>();

        foreach (MonoBehaviour behaviour in behaviours)
        {
            if(behaviour is IDamageable)
            {
                IDamageable damageable = (IDamageable)behaviour;
                damageable.Damage(5);

                break;
            }
        }
    }

   public void Fire(float pullValue)
   {
       m_LastPosition = transform.position;

       m_IsStopped = false;
       transform.parent = null;

       m_rigidbody.isKinematic = false;
       m_rigidbody.useGravity = true;
       m_rigidbody.AddForce(transform.forward * (pullValue*m_speed));

       Destroy(gameObject, 5.0f);
   }
}
