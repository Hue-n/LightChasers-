using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameManagerBase : MonoBehaviour
{
    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();

    [Header("NETWORK BASE")]
    public GameObject localPlayerPrefab;
    public GameObject playerPrefab;
    [Header("==========")]
    public bool nothing;

    public void DestroyDisconnected(int _id)
    {
        Debug.Log($"Destroying Player {_id}");
        Destroy(players[_id].gameObject);
        players.Remove(_id);
    }

    public void SpawnPlayer(int _id, string _username, Vector3 _position, Quaternion _rotation)
    {
        GameObject _player;

        if (_id == Client.instance.myId)
        {
            _player = Instantiate(localPlayerPrefab, _position, _rotation);
            _player.GetComponent<PlayerManager>().id = _id;
            _player.GetComponent<PlayerManager>().username = _username;

            UIManager.instance.username = _username;
            UIManager.instance.userIDText.text = _username + " - " + _id;
        }
        else
        {
            _player = Instantiate(playerPrefab, _position, _rotation);
            _player.GetComponent<PlayerManager>().id = _id;
            _player.GetComponent<PlayerManager>().username = _username;
        }

        players.Add(_id, _player.GetComponent<PlayerManager>());
    }
}
