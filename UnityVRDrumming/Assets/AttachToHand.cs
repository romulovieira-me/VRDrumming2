using UnityEngine;

public class AttachToHand : MonoBehaviour
{
    public Transform handAnchor; // Refer�ncia para a m�o (LeftHandAnchor ou RightHandAnchor)
    public Vector3 positionOffset = new Vector3(0f, 0.1f, 0.2f); // Inicialize com valores para mover a baqueta


    void Start()
    {
        if (handAnchor != null)
        {
            // Anexa o objeto � m�o
            AttachToHandAtStart();
        }
        else
        {
            Debug.LogError("Hand anchor is not assigned!");
        }
    }

    void AttachToHandAtStart()
    {
        // Define a DrumstickA como filha do RightHandAnchor
        transform.SetParent(handAnchor);

        // Ajusta a posi��o relativa aplicando o offset
        transform.localPosition = positionOffset;
        transform.localRotation = Quaternion.Euler(0f, 90f, 0f); // Ajuste a rota��o para alinhar a baqueta corretamente

        Debug.Log("DrumstickA Parent: " + transform.parent.name);
        Debug.Log("Drumstick Global Position: " + transform.position);

        // Configura o Rigidbody para interagir com colis�es
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false; // Permite f�sica
            rb.useGravity = false;  // Impede queda
        }
    }

    void LateUpdate()
    {
        Debug.Log("Drumstick Global Position Atualizada: " + transform.position);
    }


    void Update()
    {
        // Garante que a drumstick fique alinhada � m�o mesmo ap�s colis�es
        if (handAnchor != null)
        {
            transform.position = handAnchor.position;
            transform.rotation = handAnchor.rotation;
        }
    }



}
