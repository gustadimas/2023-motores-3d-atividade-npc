using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class NPC : MonoBehaviour
{
    [SerializeField] float velocidade = 2.5f;

    // Movimentação do NPC
    Rigidbody rb;
    int dirX = 1, dirZ = 1;
    Vector3 direcao;

    // Ciclo de vida do NPC
    bool nasceuNoChao = false;
    bool entrouNoChaoAposChaoNPC = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        dirX = Random.Range(0, 2) == 0 ? -1 : 1;
        dirZ = Random.Range(0, 2) == 0 ? -1 : 1;

        direcao = transform.forward * dirZ;
        
        // Verificar se o npc vai se mover na vertical ou horizontal
        if (Random.Range(0, 2) == 0)
        {
            direcao = transform.right * dirX;
        }
    }

    void FixedUpdate()
    {
        MovimentarAleatoriamente();
    }

    void MovimentarAleatoriamente()
    {
        rb.MovePosition(transform.position + direcao * velocidade * Time.fixedDeltaTime);
    }

    void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Chao"))
        {
            if (!nasceuNoChao)
            {
                nasceuNoChao = true;
            }
            else if (entrouNoChaoAposChaoNPC)
            {
                Destroy(gameObject);
            }
        }
        else if(other.gameObject.CompareTag("ChaoNPC") && nasceuNoChao)
        {
            entrouNoChaoAposChaoNPC = true;
        }
    }

    void OnCollisionExit(Collision other) 
    {
        if(other.gameObject.CompareTag("Chao") && nasceuNoChao && !entrouNoChaoAposChaoNPC)
        {
            Destroy(gameObject);
        }
    }
}
