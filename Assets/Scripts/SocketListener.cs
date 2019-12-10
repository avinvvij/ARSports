using UnityEngine;
using socket.io;
using System;
using Newtonsoft.Json;





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
    

    private void Start() {

        Debug.Log("Trying connection");

        var socket = Socket.Connect(url);


        socket.On(SystemEvents.connect, () =>
       {
           Debug.Log("Connection successful");
       });

        socket.On("myMsg", (string data) => {
            data = data.Replace("\\", "");
            data = data.Substring(1, data.Length - 2);
            Commands commands = JsonConvert.DeserializeObject<Commands>(data);
            Debug.Log(commands.commentary);
            playAnimation.handlePlayerActions(commands);
            //playAnimation.playShotAnimation(commands.cmd2.Split(' ')[1]);
        });
    }

    private void Update() {
        
    }

}