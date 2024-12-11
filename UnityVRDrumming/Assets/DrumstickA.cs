using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;

public class DrumstickA : MonoBehaviour
{

    public float speed = 10.4f;

    // Definindo as fontes de �udio
    public AudioSource audioPlayerCymbal;
    public AudioSource audioPlayerFloorTom;
    public AudioSource audioPlayerHiHat;
    public AudioSource audioPlayerSnare;
    public AudioSource audioPlayerRackTomSmall;
    public AudioSource audioPlayerRackTomBig;
    public AudioSource audioPlayerBassDrum;

    // Refer�ncia para o receptor OSC e endere�os
    public OSCReceiver receiver;
    public OSCTransmitter transmitter; // Refer�ncia para o transmissor OSC
    public string addressHiHat = "/hihat";
    public string addressDrumBass = "/drumbass";
    public string feedbackAddress = "/drumfeedback"; // Endere�o OSC para feedback de colis�o
    public string genericAddress = "/example/1"; // Endere�o gen�rico para exibir todas as mensagens OSC

    // Start is called before the first frame update
    void Start()
    {
        // Inicializa��o do OSC e vincula��o de endere�os
        receiver.Bind(addressHiHat, ReceivedHiHatMessage);
        receiver.Bind(addressDrumBass, ReceivedDrumBassMessage);

        // Vincular um m�todo para receber todas as mensagens no endere�o gen�rico
        receiver.Bind(genericAddress, ReceivedGenericMessage);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // M�todo para receber a mensagem do HiHat
    private void ReceivedHiHatMessage(OSCMessage message)
    {
        if (message.ToInt(out int value) && value == 1)
        {
            audioPlayerHiHat.Play();
            Debug.Log("HiHat ativado via OSC");
        }
    }

    // M�todo para receber a mensagem do DrumBass
    private void ReceivedDrumBassMessage(OSCMessage message)
    {
        if (message.ToInt(out int value) && value == 1)
        {
            audioPlayerBassDrum.Play();
            Debug.Log("DrumBass ativado via OSC");
        }
    }

    // M�todo para receber mensagens gen�ricas OSC e imprimir no console
    private void ReceivedGenericMessage(OSCMessage message)
    {
        Debug.LogFormat("Received OSC Message - Address: {0} | Values: {1}", message.Address, message);
    }

    // M�todo chamado ao detectar colis�o
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colis�o detectada com: " + collision.gameObject.name);

        // Tocar �udio baseado no tag do objeto colidido
        if (collision.gameObject.tag == "CymbalCollisionTag")
        {
            audioPlayerCymbal.Play();
        }
        else if (collision.gameObject.tag == "FloorTomCollisionTag")
        {
            audioPlayerFloorTom.Play();
        }
        else if (collision.gameObject.tag == "HiHatCollisionTag")
        {
            audioPlayerHiHat.Play();
        }
        else if (collision.gameObject.tag == "SnareCollisionTag")
        {
            audioPlayerSnare.Play();
        }
        else if (collision.gameObject.tag == "RackTomSmallCollisionTag")
        {
            audioPlayerRackTomSmall.Play();
        }
        else if (collision.gameObject.tag == "RackTomBigCollisionTag")
        {
            audioPlayerRackTomBig.Play();
        }
        else if (collision.gameObject.tag == "BassDrumCollisionTag")
        {
            audioPlayerBassDrum.Play();
        }

        // Enviar mensagem OSC para o feedbackAddress
        if (transmitter != null)
        {
            var message = new OSCMessage(feedbackAddress);
            message.AddValue(OSCValue.Int(1)); // Envia valor 1 para o receptor
            transmitter.Send(message);

            Debug.Log($"Mensagem OSC enviada para {feedbackAddress} com valor 1.");
        }
        else
        {
            Debug.LogWarning("Transmissor OSC n�o configurado. Nenhuma mensagem foi enviada.");
        }
    }
}
