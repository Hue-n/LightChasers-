using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void ChangePlayerState(PlayerStates newState);

public class UIManager : MonoBehaviour
{
    public static ChangePlayerState stateCaster;

    //User Variables
    private bool cursorLocked = true;
    public bool inChat = false;

    //Connect Menu Dependencies
    [Header("Connect Menu Dependencies")]
    public static UIManager instance;
    public GameObject startMenu;
    public InputField usernameField;

    //Chatbox Dependencies, Data Structs, and Veriables
    [Header("Chatbox Dependencies")]
    public GameObject unconnectedPanel;
    public GameObject chatBoxPanel;

    public GameObject chatPanel;
    public GameObject textObject;

    public Text userIDText;
    public InputField chatField;

    [Header("Chatbox Data Structs")]
    [SerializeField]
    List<Message> messageList = new List<Message>();

    [Header("Chatbox Variables")]
    public int maxMessages = 100;
    public string username;
    public string message;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Debug.Log("A UIManager already exists! Destroying object.");
            Destroy(this);
        }

        userIDText.text = "";
    }

    public void ConnectToServer()
    {
        startMenu.SetActive(false);
        usernameField.interactable = false;
        username = usernameField.text;
        Client.instance.ConnectToServer();
    }

    private void Update()
    {
        ChatboxCheck();
        ChatBoxActive();
    }

    public void ChangePlayerState(PlayerStates newState)
    {
        //stateCaster.Invoke(newState);
    }

    #region Chatbox
    public void ReceivedMessage(string _message)
    {
        if (messageList.Count >= maxMessages)
        {
            Destroy(messageList[0].textObject.gameObject);
            messageList.Remove(messageList[0]);
        }

        Message newMessage = new Message();
        newMessage.text = _message;

        GameObject newText = Instantiate(textObject, chatPanel.transform);
        newMessage.textObject = newText.GetComponent<Text>();
        newMessage.textObject.text = newMessage.text;
        messageList.Add(newMessage);
    }

    public void ReceivedMessage(string _message, Color _color)
    {
        if (messageList.Count >= maxMessages)
        {
            Destroy(messageList[0].textObject.gameObject);
            messageList.Remove(messageList[0]);
        }

        Message newMessage = new Message();
        newMessage.text = _message;

        GameObject newText = Instantiate(textObject, chatPanel.transform);
        Debug.Log("Test");
        newMessage.textObject = newText.GetComponent<Text>();
        newMessage.textObject.text = newMessage.text;

        newMessage.textObject.color = _color;
    }

    public void SetMessage(string message)
    {
        this.message = username + ": " + message;
    }

    public void SendUserMessage(string message)
    {
        ReceivedMessage(message);
        ClientSend.PlayerChatMessage(message);
    }

    public void ChatboxCheck()
    {
        if (Client.instance.isConnected)
        {
            chatBoxPanel.gameObject.SetActive(true);
            unconnectedPanel.gameObject.SetActive(false);
        }

        else
        {
            chatBoxPanel.gameObject.SetActive(false);
            unconnectedPanel.gameObject.SetActive(true);
        }

        if (!inChat && Input.GetKeyDown(KeyCode.Return))
        {
            inChat = true;
            chatField.enabled = true;
            chatField.ActivateInputField();
            ChangePlayerState(PlayerStates.chat);
        }

        else if (inChat)
        {
            if (chatField.text != "" && Input.GetKeyDown(KeyCode.Return))
            {
                SetMessage(chatField.text); //Sets the message
                SendUserMessage(message); //Sends the message to the server
                chatField.text = "";
                chatField.DeactivateInputField();
                chatField.enabled = false;
                inChat = false;
                ChangePlayerState(PlayerStates.moving);
            }

            else if (Input.GetKeyDown(KeyCode.Return))
            {
                chatField.DeactivateInputField();
                chatField.enabled = false;
                inChat = false;
                ChangePlayerState(PlayerStates.moving);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (cursorLocked)
            {
                Cursor.lockState = CursorLockMode.None;
                cursorLocked = false;
            }

            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                cursorLocked = true;
            }
        }
    }

    private void ChatBoxActive()
    {
        if (inChat)
        {
            chatBoxPanel.GetComponent<Image>().color = new Color(0, 0, 0, 15);
            //GameManager.players[Client.instance.myId].GetComponent<PlayerController>().UpdatePlayerState(PlayerStates.menu);
        }

        else
        {
            chatBoxPanel.GetComponent<Image>().color = new Color(0, 0, 0, 255);
            //GameManager.players[Client.instance.myId].GetComponent<PlayerController>().UpdatePlayerState(PlayerStates.moving);
        }
    }
    #endregion
}

[System.Serializable]
public class Message
{
    public string text;
    public Text textObject;
}

