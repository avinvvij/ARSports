using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimateBall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void animateBall(string cmd3)
    {
        int units = int.Parse(cmd3.Split(' ')[1]);
        string direction = cmd3.Split(' ')[2];
        string movementType = cmd3.Split(' ')[3];
        if(units > 0)
        {
            Vector3 currentPosition = transform.position;
            float destination_x = currentPosition.x;
            float destination_z = currentPosition.z;
            switch (direction)
            {
                case "North":
                    destination_z = destination_z + units;
                    break;
                case "South":
                    destination_z = destination_z - units;
                    break;
                case "East":
                    destination_x = destination_x + units;
                    break;
                case "West":
                    destination_x = destination_x - units;
                    break;
            }
            if(movementType == "PARABOLIC")
            {
                transform.DOMoveY(currentPosition.y + 100.0f, units / 300).SetEase(Ease.Linear).OnComplete(() =>
                {
                transform.DOMoveY(currentPosition.y, units / 300).SetEase(Ease.Linear);
                });
            }
            transform.DOMoveX(destination_x, units / 150).SetEase(Ease.Linear);
            transform.DOMoveZ(destination_z, units / 150).SetEase(Ease.Linear);
        }
    }
}
