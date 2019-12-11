using UnityEngine;
using socket.io;
using System;
using Newtonsoft.Json;
using UnityEngine.UI;


public class Commands
{
    public string cmd1 = "";
    public string cmd2 = "";
    public string cmd3 = "";
    public string commentary = "";
}

public class SocketListener : MonoBehaviour {

    public string url;
    public PlayAnimation playAnimation;
    public Text statusText;

    private void Start() {

        statusText.text = "Trying connection";

        var socket = Socket.Connect(url);


        socket.On(SystemEvents.connect, () =>
       {
           Debug.Log("Connection successful");
           statusText.text = "Connceted";
       });

        socket.On("myMsg", (string data) => {
            statusText.text = "Message Receved";
            data = data.Replace("\\", "");
            data = data.Substring(1, data.Length - 2);
            Commands commands = JsonConvert.DeserializeObject<Commands>(data);            
            playAnimation.handlePlayerActions(commands);            
            //playAnimation.playShotAnimation(commands.cmd2.Split(' ')[1]);
        });
    }

    private void Update() {
        
    }

}