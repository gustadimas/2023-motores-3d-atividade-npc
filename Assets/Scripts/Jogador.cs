using UnityEngine;

public class Jogador : MonoBehaviour
{
    [SerializeField] float velocidade = 5f;
    [SerializeField] float sensibilidadeMouse = 2f;

    Rigidbody rb;
    Camera cam;

    Vector2 direcaoMovimento;

    Vector3 ultimaPosicao;

    bool podeAndar = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        CapturarEntradas();
    }

    private void CapturarEntradas()
    {
        direcaoMovimento = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        float mouseX = Input.GetAxis("Mouse X") * sensibilidadeMouse * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);
    }

    private void FixedUpdate()
    {
        MoverJogador();

        ultimaPosicao = transform.position;
    }

    private void MoverJogador()
    {
        if (!podeAndar) 
            return;

        Vector3 movimento = (cam.transform.forward * direcaoMovimento.y + cam.transform.right * direcaoMovimento.x).normalized * velocidade * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movimento);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Chao"))
            podeAndar = true;
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Chao"))
        {
            LidarComSaidaDeColisao();
            transform.position = ultimaPosicao;
        }
    }

    private void LidarComSaidaDeColisao()
    {
        podeAndar = false;
    }
}
