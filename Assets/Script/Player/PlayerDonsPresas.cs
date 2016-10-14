using UnityEngine;
using System.Collections;

public class PlayerDonsPresas : MonoBehaviour
{
    public float[] cost;

    public PlayerStats controller;

    public PlayerAttackController playerAttack;

    public PlayerDano playerDano;

    public PlayerAnimation playerAnim;

    public PlayerMovment playerMov;

    public PlayerDomPresas player;

    public PlayerStatus playerStatus;

    public GameObject granite, pack;

    public Transform posGranite;

    public string nome;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            print("Apertando");
        }

        if (Time.timeScale != 0)
        {
            if (!playerDano.stun && !playerMov.isGrab && !playerAttack.presa && !playerMov.jump && !controller.crinos)
            {
                switch (player)
                {
                    case PlayerDomPresas.Player1:
                        if (Input.GetKey(KeyCode.Joystick1Button5))
                        {
                            if (Input.GetKeyDown(KeyCode.Joystick1Button0))
                            {
                                PressButtonA("P1");
                            }
                            else if (Input.GetKeyDown(KeyCode.Joystick1Button1))
                            {
                                PressButtonB("P1");
                            }
                            else if (Input.GetKeyDown(KeyCode.Joystick1Button2))
                            {
                                PressButtonX("P1");
                            }
                            else if (Input.GetKeyDown(KeyCode.Joystick1Button3))
                            {
                                PressButtonY("P1");
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
                    case PlayerDomPresas.Player2:
                        if (Input.GetKey(KeyCode.Joystick2Button5))
                        {
                            if (Input.GetKeyDown(KeyCode.Joystick2Button0))
                            {
                                PressButtonA("P2");
                            }
                            else if (Input.GetKeyDown(KeyCode.Joystick2Button1))
                            {
                                PressButtonB("P2");
                            }
                            else if (Input.GetKeyDown(KeyCode.Joystick2Button2))
                            {
                                PressButtonX("P2");
                            }
                            else if (Input.GetKeyDown(KeyCode.Joystick2Button3))
                            {
                                PressButtonY("P2");
                            }
                        }
                        break;
                }
            }
        }
    }

    void SlamDunk()
    {
        playerAnim.anim.SetBool("SlamDunk", true);
        playerAnim.anim.SetTrigger("StartSlam");
    }

    void LunarArmor()
    {
        playerStatus.lunar = true;
        controller.lunar = true;
        StartCoroutine("Lunar");
    }

    IEnumerator Lunar()
    {
        yield return new WaitForSeconds(3);
        playerStatus.lunar = false;
        controller.lunar = false;
        StopCoroutine("Lunar");
    }

    void WallofGranite()
    {
        playerDano.stun = true;
        playerAnim.anim.SetTrigger("SummonGranite");
        Instantiate(granite, posGranite.position, transform.rotation);
    }

    void PressButtonA(string player)
    {
        if (PlayerPrefs.GetInt(nome + player + "ButtonA") == 0 && controller.gnose >= cost[0])
        {
            LunarArmor();
            controller.gnose -= cost[0];
        }
        else if (PlayerPrefs.GetInt(nome + player + "ButtonA") == 1 && controller.gnose >= cost[1])
        {
            WallofGranite();
            controller.gnose -= cost[1];
        }
        else if (PlayerPrefs.GetInt(nome + player + "ButtonA") == 2 && controller.gnose >= cost[2] && Manager.manager.player.Length > 1)
        {
            SlamDunk();
            controller.gnose -= cost[2];
        }
    }

    void PressButtonB(string player)
    {
        if (PlayerPrefs.GetInt(nome + player + "ButtonB") == 0 && controller.gnose >= cost[0])
        {
            LunarArmor();
            controller.gnose -= cost[0];
        }
        else if (PlayerPrefs.GetInt(nome + player + "ButtonB") == 1 && controller.gnose >= cost[1])
        {
            WallofGranite();
            controller.gnose -= cost[1];
        }
        else if (PlayerPrefs.GetInt(nome + player + "ButtonB") == 2 && controller.gnose >= cost[2] && Manager.manager.player.Length > 1)
        {
            SlamDunk();
            controller.gnose -= cost[2];
        }
    }

    void PressButtonX(string player)
    {
        if (PlayerPrefs.GetInt(nome + player + "ButtonX") == 0 && controller.gnose >= cost[0])
        {
            LunarArmor();
            controller.gnose -= cost[0];
        }
        else if (PlayerPrefs.GetInt(nome + player + "ButtonX") == 1 && controller.gnose >= cost[1])
        {
            WallofGranite();
            controller.gnose -= cost[1];
        }
        else if (PlayerPrefs.GetInt(nome + player + "ButtonX") == 2 && controller.gnose >= cost[2] && Manager.manager.player.Length > 1)
        {
            SlamDunk();
            controller.gnose -= cost[2];
        }
    }

    void PressButtonY(string player)
    {
        if (PlayerPrefs.GetInt(nome + player + "ButtonY") == 0 && controller.gnose >= cost[0])
        {
            LunarArmor();
            controller.gnose -= cost[0];
        }
        else if (PlayerPrefs.GetInt(nome + player + "ButtonY") == 1 && controller.gnose >= cost[1])
        {
            WallofGranite();
            controller.gnose -= cost[1];
        }
        else if (PlayerPrefs.GetInt(nome + player + "ButtonY") == 2 && controller.gnose >= cost[2] && Manager.manager.player.Length > 1)
        {
            SlamDunk();
            controller.gnose -= cost[2];
        }
    }
}

public enum PlayerDomPresas
{
    Player1,
    Player2,
    Player3,
    Player4
}