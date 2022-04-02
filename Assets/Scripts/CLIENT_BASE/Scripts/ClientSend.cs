using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    // View
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }

    private static void SendUDPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.udp.SendData(_packet);
    }

    #region Packets
    public static void WelcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write(UIManager.instance.usernameField.text);

            SendTCPData(_packet);
        }
    }

    public static void UDPTestReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.udpTestReceived))
        {
            _packet.Write("Received a UDP Packet");

            SendUDPData(_packet);
        }
    }

    //Step 5: Define the packet you would like to send to the Server and send it.
    //Step 5: Pack up the Player Movement
    public static void PlayerMovement(Vector3 _position, Quaternion _rotation, Vector3 _velocity)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerMovement))
        {
            _packet.Write(_position);
            _packet.Write(_rotation);
            _packet.Write(_velocity);

            SendUDPData(_packet);
        }
    }

    public static void PlayerChatMessage(string message)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerChatMessage))
        {
            _packet.Write(UIManager.instance.message);

            SendTCPData(_packet);
        }
    }
    #endregion
}