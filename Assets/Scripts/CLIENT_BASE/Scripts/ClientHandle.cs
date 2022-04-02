using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    // Controller
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.instance.myId = _myId;
        ClientSend.WelcomeReceived();

        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
    }

    public static void UDPTest(Packet _packet)
    {
        string _msg = _packet.ReadString();

        Debug.Log($"Received packet via UDP. Contains message: {_msg}");
        ClientSend.UDPTestReceived();
    }

    public static void SpawnPlayer(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();

        string _joinedMessage = _packet.ReadString();

        UIManager.instance.ReceivedMessage(_joinedMessage, Color.blue);
        Vector3 _position = _packet.ReadVector3() + Vector3.up;
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.instance.SpawnPlayer(_id, _username, _position, _rotation);
    }

    public static void SpawnObject(Packet _packet)
    { 
    
    }

    // creates a player instance in the world without announcing it
    public static void CreatePlayerInstance(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        Vector3 _position = _packet.ReadVector3() + Vector3.up;
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.instance.SpawnPlayer(_id, _username, _position, _rotation);
    }

    // destroys a player instance in the world without announcing it
    public static void DestroyPlayerInstance(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        GameManager.instance.DestroyPlayerInstance(_id);
    }

    public static void DestroyDisconnected(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _msg = _packet.ReadString();

        UIManager.instance.ReceivedMessage(_msg, Color.red);

        Debug.Log($"Received TCP request to destroy Player {_id}");

        GameManager.instance.DestroyPlayerInstance(_id);
    }

    //Step 6: Receive the packet from the server and unpack it to its proper classes and functions.
    //Step 6: Receive the Player Data from the Server and update the data to the appropriate user.
    public static void PlayerMovement(Packet _packet)
    {
        int _id = _packet.ReadInt();
        if (GameManager.players.TryGetValue(_id, out var player))
        {
            Vector3 _position = _packet.ReadVector3();
            Quaternion _rotation = _packet.ReadQuaternion();
            Vector3 _velocity = _packet.ReadVector3();

            GameManager.players[_id].transform.position = _position + Vector3.down;
            GameManager.players[_id].transform.rotation = _rotation;
            GameManager.players[_id].velocity = _velocity;
        }

    }

    public static void NetworkedBall(Packet _packet)
    {

    }

    public static void PlayerChatMessage(Packet _packet)
    {
        string _msg = _packet.ReadString();

        UIManager.instance.ReceivedMessage(_msg);
    }
}