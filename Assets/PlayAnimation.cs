using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;




//animType: 0 for CHIP , 1 for DRIVE , 2 PUTT 

public class PlayAnimation : MonoBehaviour
{
	Animator animator;
    Transform mytransform;
    Commands mycommands = null;

    // Start is called before the first frame update
    void Start()
    {
		animator = gameObject.GetComponent<Animator>();
        mytransform = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(animator.GetAnimatorTransitionInfo(0).duration);
        if (animator.GetCurrentAnimatorStateInfo(0).length > animator.GetCurrentAnimatorStateInfo(0).normalizedTime && animator.GetCurrentAnimatorStateInfo(0).IsName("chip"))
        {
            // Avoid any reload.
            Debug.Log("Here playing chip animation");
        }
        else
        {
            animator.SetBool("CHIP", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).length > animator.GetCurrentAnimatorStateInfo(0).normalizedTime && animator.GetCurrentAnimatorStateInfo(0).IsName("drive"))
        {
            // Avoid any reload.
            Debug.Log("Here playing drive animation");
        }
        else
        {
            animator.SetBool("DRIVE", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).length > animator.GetCurrentAnimatorStateInfo(0).normalizedTime && animator.GetCurrentAnimatorStateInfo(0).IsName("putt"))
        {
            // Avoid any reload.
            Debug.Log("Here playing putt animation");
        }
        else
        {
            animator.SetBool("PUTT", false);
        }

    }

    public void handlePlayerActions(Commands commands)
    {
        mycommands = commands;
         if(int.Parse(commands.cmd1.Split(' ')[1]) > 0)
        {
            rotatePlayer(commands.cmd1.Split(' ')[2]);
            movePlayer(int.Parse(commands.cmd1.Split(' ')[1]), commands.cmd1.Split(' ')[2]);
        }
    }

    //rotate the player as per direction
    private void rotatePlayer(string direction)
    {
        switch (direction)
        {
            case "North":
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                break;
            case "South":
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));                
                break;
            case "East":
                transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));                
                break;
            case "West":
                transform.rotation = Quaternion.Euler(new Vector3(0, 270, 0));             
                break;
        }
        
    }

    private void movePlayer(int units , string direction)
    {
        animator.SetBool("walking", true);
        Vector3 currentPosition = transform.position;
        float destination_x = currentPosition.x;
        float destination_z = currentPosition.z;
        switch (direction)
        {
            case "North":
                destination_z = destination_z + units;
                break;
            case "South":
                destination_z = destination_z-units;
                break;
            case "East":
                destination_x = destination_x+ units;
                break;
            case "West":
                destination_x = destination_x -units;
                break;
        }
        transform.DOMove(new Vector3(destination_x, 0.1f, destination_z), units / 2).OnComplete(()=>
        {
            animator.SetBool("walking", false);
            Invoke("playShotAnimation", 0.5f);
        }
        );
        //DOTween.To(() => transform.position, x => transform.position = x, new Vector3(destination_x, 0.1f, destination_z), 20);
    }


    public Animator getAnimator()
	{
		return animator;
	}

    public void setAnimator(Animator anim)
	{
		 animator = anim;
	}

    public void playShotAnimation()
    {
        Debug.Log("Play Shot Here");
        animator.SetBool(mycommands.cmd2.Split(' ')[1], true);
    }


}
