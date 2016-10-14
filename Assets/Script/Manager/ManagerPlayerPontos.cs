using UnityEngine;
using System.Collections;

public class ManagerPlayerPontos : MonoBehaviour
{
    public static ManagerPlayerPontos managerPontos;

    string strength = "Strength";
    string dexterity = "Dexterity";
    string stamina = "Stamina";
    string charisma = "Charisma";
    string intelligence = "Intelligence";
    string spirit = "Spirit";

    void Awake()
    {
        managerPontos = this;
    }

    public void SetStrength(string nome, int value)
    {
        PlayerPrefs.SetInt((nome + strength), PlayerPrefs.GetInt((nome + strength)) + value);
    }

    public int GetStrength(string nome)
    {
        return PlayerPrefs.GetInt((nome + strength));
    }

    public void SetDexterity(string nome, int value)
    {
        PlayerPrefs.SetInt((nome + dexterity), PlayerPrefs.GetInt((nome + dexterity)) + value);
    }

    public int GetDexterity(string nome)
    {
        return PlayerPrefs.GetInt((nome + dexterity));
    }

    public void SetStamina(string nome, int value)
    {
        PlayerPrefs.SetInt((nome + stamina), PlayerPrefs.GetInt((nome + stamina)) + value);
    }

    public int GetStamina(string nome)
    {
        return PlayerPrefs.GetInt((nome + stamina));
    }

    public void SetCharisma(string nome, int value)
    {
        PlayerPrefs.SetInt((nome + charisma), PlayerPrefs.GetInt((nome + charisma)) + value);
    }

    public int GetCharisma(string nome)
    {
        return PlayerPrefs.GetInt((nome + charisma));
    }

    public void SetIntelligence(string nome, int value)
    {
        PlayerPrefs.SetInt((nome + intelligence), PlayerPrefs.GetInt((nome + intelligence)) + value);
    }

    public int GetIntelligence(string nome)
    {
        return PlayerPrefs.GetInt((nome + intelligence));
    }

    public void SetSpirit(string nome, int value)
    {
        PlayerPrefs.SetInt((nome + spirit), PlayerPrefs.GetInt((nome + spirit)) + value);
    }

    public int GetSpirit(string nome)
    {
        return PlayerPrefs.GetInt((nome + spirit));
    }
}