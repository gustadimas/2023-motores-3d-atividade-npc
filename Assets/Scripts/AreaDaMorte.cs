using UnityEngine;

public class AreaDaMorte : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.GetComponent<NPC>() != null)
        {
            print("NPC morreu");
            Destroy(other.gameObject);
        }
    }
}
