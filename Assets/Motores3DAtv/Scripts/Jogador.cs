using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Jogador : MonoBehaviour
{
    [SerializeField] float velocidade = 5f;
    [SerializeField] float sensibilidadeMouse = 2f; // Sensibilidade do movimento do mouse
    [SerializeField] float limiteRotacao = 80f; // Limita a rotação vertical da câmera

    Rigidbody rb;
    Camera cam;

    Vector2 direcaoMovimento;
    Vector3 ultimaPosicao;

    float rotacaoX = 0f; // Armazena a rotação vertical

    bool podeAndar = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked; // Bloqueia o cursor no centro da tela
        Cursor.visible = false; // Esconde o cursor
    }

    void Update()
    {
        direcaoMovimento = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Movimento da câmera com base no movimento do mouse
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadeMouse * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadeMouse * Time.deltaTime;

        rotacaoX -= mouseY;
        rotacaoX = Mathf.Clamp(rotacaoX, -limiteRotacao, limiteRotacao);

        cam.transform.localRotation = Quaternion.Euler(rotacaoX, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void FixedUpdate()
    {
        if (!podeAndar) return;
        rb.MovePosition(rb.position + new Vector3(direcaoMovimento.x, 0, direcaoMovimento.y) * velocidade * Time.fixedDeltaTime);
        ultimaPosicao = transform.position;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Chao"))
        {
            podeAndar = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Chao"))
        {
            podeAndar = false;
            transform.position = ultimaPosicao;
        }
    }
}
