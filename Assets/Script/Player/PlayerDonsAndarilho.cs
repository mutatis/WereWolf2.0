using UnityEngine;
using System.Collections;

public class PlayerDonsAndarilho : MonoBehaviour
{
    public float[] cost;

    public PlayerStats controller;

    public PlayerAttackController playerAttack;

    public PlayerDano playerDano;

    public PlayerAnimation playerAnim;

    public PlayerMovment playerMov;

    public PlayerDom player;

    public GameObject invoque, eletrecute, pack, pacman, flash;

    public Transform posInvoque;

    [FMODUnity.EventRef]
    public string uivo;

    FMOD.Studio.EventInstance audioInstanceCreator;

    public string nome;

    GameObject[] obj;

    void Update()
    {
		if (Time.timeScale != 0) 
		{
			if (!playerDano.stun && !playerMov.isGrab && !playerAttack.presa && !playerMov.jump && !controller.crinos) 
			{
				switch (player)
				{
					case PlayerDom.Player1:
						if (Input.GetKey (KeyCode.Joystick1Button5)) 
						{
							if (Input.GetKeyDown (KeyCode.Joystick1Button0)) 
							{
								PressButtonA ("P1");
							} 
							else if (Input.GetKeyDown (KeyCode.Joystick1Button1)) 
							{
								PressButtonB ("P1");
							} 
							else if (Input.GetKeyDown (KeyCode.Joystick1Button2)) 
							{
								PressButtonX ("P1");
							} 
							else if (Input.GetKeyDown (KeyCode.Joystick1Button3)) 
							{
								PressButtonY ("P1");
							}
						}
                        if (Input.GetKey(KeyCode.LeftShift))
                        {
                            if (Input.GetKeyDown(KeyCode.Space))
                            {
                                PressButtonA("P1");
                            }
                            else if (Input.GetKeyDown(KeyCode.C))
                            {
                                PressButtonB("P1");
                            }
                            else if (Input.GetKeyDown(KeyCode.Z))
                            {
                                PressButtonX("P1");
                            }
                            else if (Input.GetKeyDown(KeyCode.X))
                            {
                                PressButtonY("P1");
                            }
                        }
					break;
					case PlayerDom.Player2:
						if (Input.GetKey (KeyCode.Joystick2Button5)) 
						{
							if (Input.GetKeyDown (KeyCode.Joystick2Button0)) 
							{
								PressButtonA ("P2");
							} 
							else if (Input.GetKeyDown (KeyCode.Joystick2Button1)) 
							{
								PressButtonB ("P2");
							} 
							else if (Input.GetKeyDown (KeyCode.Joystick2Button2)) 
							{
								PressButtonX ("P2");
							} 
							else if (Input.GetKeyDown (KeyCode.Joystick2Button3)) 
							{
								PressButtonY ("P2");
							}
						}
					break;
				}
			}
		}
    }

    public void FlashSlam()
    {
        Instantiate(flash);
    }

    void CalloftheWyld()
    {
        playerDano.stun = true;
        playerAnim.anim.SetTrigger("Uivo");
        audioInstanceCreator = FMODUnity.RuntimeManager.CreateInstance(uivo);
        audioInstanceCreator.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
        audioInstanceCreator.start();
        obj = GameObject.FindGameObjectsWithTag("Player");

        for(int i = 0; i < obj.Length; i ++)
        {
            if(obj[i].GetComponent<PlayerStats>().nome != nome)
            {
                obj[i].GetComponent<PlayerStatus>().call = true;
                obj[i].GetComponent<PlayerStats>().call = true;
            }
        }
        StartCoroutine("Call");
    }

    IEnumerator Call()
    {
        yield return new WaitForSeconds(3);
        for (int i = 0; i < obj.Length; i++)
        {
            if (obj[i].GetComponent<PlayerStats>().nome != nome)
            {
                obj[i].GetComponent<PlayerStatus>().call = false;
                obj[i].GetComponent<PlayerStats>().call = false;
            }
        }
        StopCoroutine("Call");
    }

    void FabricoftheMind()
	{
		if (pacman == null) 
		{
			controller.gnose -= cost [1];
			playerDano.stun = true;
			playerAnim.anim.SetTrigger ("Summon");
			pacman = Instantiate (invoque, posInvoque.position, transform.rotation) as GameObject;
			pacman.GetComponent<Pacman> ().obj = gameObject;
		}
    }

    void Eletrecute()
    {
        GameObject tempObj;
        tempObj = Instantiate(eletrecute, transform.position, transform.rotation) as GameObject;
        if(gameObject.transform.localScale.x < 0)
        {
            tempObj.transform.localScale = new Vector3(tempObj.transform.localScale.x * -1, tempObj.transform.localScale.y, tempObj.transform.localScale.z);
        }
    }

    void ArrancarPelos()
    {
        pack.SetActive(true);
        pack.GetComponent<PackAtiva>().qual = 1;
    }

    void PressButtonA(string player)
    {
        if(PlayerPrefs.GetInt(nome + player + "ButtonA") == 0 && controller.gnose >= cost[0])
        {
            CalloftheWyld();
            controller.gnose -= cost[0];
        }
        else if (PlayerPrefs.GetInt(nome + player + "ButtonA") == 1 && controller.gnose >= cost[1])
        {
            FabricoftheMind();
        }
        else if (PlayerPrefs.GetInt(nome + player + "ButtonA") == 2 && controller.gnose >= cost[2])
        {
            Eletrecute();
            controller.gnose -= cost[2];
        }
        else if (PlayerPrefs.GetInt(nome + player + "ButtonA") == 3 && controller.gnose >= cost[3] && Manager.manager.player.Length > 1)
        {
            ArrancarPelos();
            controller.gnose -= cost[3];
        }
    }

    void PressButtonB(string player)
    {
        if (PlayerPrefs.GetInt(nome + player + "ButtonB") == 0 && controller.gnose >= cost[0])
        {
            CalloftheWyld();
            controller.gnose -= cost[0];
        }
        else if (PlayerPrefs.GetInt(nome + player + "ButtonB") == 1 && controller.gnose >= cost[1])
        {
            FabricoftheMind();
        }
        else if (PlayerPrefs.GetInt(nome + player + "ButtonB") == 2 && controller.gnose >= cost[2])
        {
            Eletrecute();
            controller.gnose -= cost[2];
        }
        else if (PlayerPrefs.GetInt(nome + player + "ButtonB") == 3 && controller.gnose >= cost[3] && Manager.manager.player.Length > 1)
        {
            ArrancarPelos();
            controller.gnose -= cost[3];
        }
    }

    void PressButtonX(string player)
    {
        if (PlayerPrefs.GetInt(nome + player + "ButtonX") == 0 && controller.gnose >= cost[0])
        {
            CalloftheWyld();
            controller.gnose -= cost[0];
        }
        else if (PlayerPrefs.GetInt(nome + player + "ButtonX") == 1 && controller.gnose >= cost[1])
        {
            FabricoftheMind();
        }
        else if (PlayerPrefs.GetInt(nome + player + "ButtonX") == 2 && controller.gnose >= cost[2])
        {
            Eletrecute();
            controller.gnose -= cost[2];
        }
        else if (PlayerPrefs.GetInt(nome + player + "ButtonX") == 3 && controller.gnose >= cost[3] && Manager.manager.player.Length > 1)
        {
            ArrancarPelos();
            controller.gnose -= cost[3];
        }
    }

    void PressButtonY(string player)
    {
        if (PlayerPrefs.GetInt(nome + player + "ButtonY") == 0 && controller.gnose >= cost[0])
        {
            CalloftheWyld();
            controller.gnose -= cost[0];
        }
        else if (PlayerPrefs.GetInt(nome + player + "ButtonY") == 1 && controller.gnose >= cost[1])
        {
            FabricoftheMind();
        }
        else if (PlayerPrefs.GetInt(nome + player + "ButtonY") == 2 && controller.gnose >= cost[2])
        {
            Eletrecute();
            controller.gnose -= cost[2];
        }
        else if (PlayerPrefs.GetInt(nome + player + "ButtonY") == 3 && controller.gnose >= cost[3] && Manager.manager.player.Length > 1)
        {
            ArrancarPelos();
            controller.gnose -= cost[3];
        }
    }
}

public enum PlayerDom
{
    Player1,
    Player2,
    Player3,
    Player4
}