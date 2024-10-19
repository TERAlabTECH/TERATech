using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonMusica : MonoBehaviour
{
    [SerializeField] AudioSource music11;
    [SerializeField] AudioSource music21;
    [SerializeField] AudioSource music31;
    [SerializeField] AudioSource music41;
    [SerializeField] AudioSource music12;
    [SerializeField] AudioSource music22;
    [SerializeField] AudioSource music32;
    [SerializeField] AudioSource music42;
    [SerializeField] AudioSource music13;
    [SerializeField] AudioSource music23;
    [SerializeField] AudioSource music33;
    [SerializeField] AudioSource music43;
    [SerializeField] AudioSource music14;
    [SerializeField] AudioSource music24;
    [SerializeField] AudioSource music34;
    [SerializeField] AudioSource music44;

    bool play11 = false;
    bool play21 = false;
    bool play31 = false;
    bool play41 = false;
    bool play12 = false;
    bool play22 = false;
    bool play32 = false;
    bool play42 = false;
    bool play13 = false;
    bool play23 = false;
    bool play33 = false;
    bool play43 = false;
    bool play14 = false;
    bool play24 = false;
    bool play34 = false;
    bool play44 = false;

    public void onMusic11()
    { 
        music21.volume = Mathf.Clamp(music21.volume - 1,0,1);
        music31.volume = Mathf.Clamp(music31.volume - 1,0,1);
        music41.volume = Mathf.Clamp(music41.volume - 1,0,1);
        play21 = false;
        play31 = false;
        play41 = false;
        if (play11)
        {
            music11.volume = Mathf.Clamp(music11.volume - 1, 0, 1); ;
            play11 = false;
        }
        else
        {
            music11.volume = Mathf.Clamp(music11.volume + 1, 0, 1);
            play11 = true;
        }
    }
    public void onMusic21()
    {
        music11.volume = Mathf.Clamp(music11.volume - 1, 0, 1);
        music31.volume = Mathf.Clamp(music31.volume - 1, 0, 1);
        music41.volume = Mathf.Clamp(music41.volume - 1,0,1);
        play11 = false;
        play31 = false;
        play41 = false;

        if (play21)
        {
            music21.volume = Mathf.Clamp(music21.volume - 1, 0, 1);
            play21 = false;
        }
        else
        {
            music21.volume = Mathf.Clamp(music21.volume + 1, 0, 1);
            play21 = true;
        }
    }
    public void onMusic31()
    {
        music11.volume = Mathf.Clamp(music11.volume - 1, 0, 1);
        music21.volume = Mathf.Clamp(music21.volume - 1, 0, 1);
        music41.volume = Mathf.Clamp(music41.volume - 1,0,1);
        play11 = false;
        play21 = false;
        play41 = false;

        if (play31)
        {
            music31.volume = Mathf.Clamp(music31.volume - 1, 0, 1);
            play31 = false;
        }
        else
        {
            music31.volume = Mathf.Clamp(music31.volume + 1, 0, 1);
            play31 = true;
        }
    }

    public void onMusic41()
    { 
        music11.volume = Mathf.Clamp(music11.volume - 1, 0, 1);
        music21.volume = Mathf.Clamp(music21.volume - 1,0,1);
        music31.volume = Mathf.Clamp(music31.volume - 1,0,1);
        play11 = false;
        play21 = false;
        play31 = false;
        if (play41)
        {
            music41.volume = Mathf.Clamp(music41.volume - 1, 0, 1); ;
            play41 = false;
        }
        else
        {
            music41.volume = Mathf.Clamp(music41.volume + 1, 0, 1);
            play41 = true;
        }
    }
    public void onMusic12()
    {
        music22.volume = Mathf.Clamp(music22.volume - 1, 0, 1);
        music32.volume = Mathf.Clamp(music32.volume - 1, 0, 1);
        music42.volume = Mathf.Clamp(music42.volume - 1,0,1);
        play22 = false;
        play32 = false;
        play42 = false;

        if (play12)
        {
            music12.volume = Mathf.Clamp(music12.volume - 1, 0, 1);
            play12 = false;
        }
        else
        {
            music12.volume = Mathf.Clamp(music12.volume + 1, 0, 1);
            play12 = true;
        }
    }
    public void onMusic22()
    {
        music12.volume = Mathf.Clamp(music12.volume - 1, 0, 1);
        music32.volume = Mathf.Clamp(music32.volume - 1, 0, 1);
        music42.volume = Mathf.Clamp(music42.volume - 1,0,1);
        play12 = false;
        play32 = false;
        play42 = false;
        if (play22)
        {
            music22.volume = Mathf.Clamp(music22.volume - 1, 0, 1);
            play22 = false;
        }
        else
        {
            music22.volume = Mathf.Clamp(music22.volume + 1, 0, 1);
            play22 = true;
        }
    }
    public void onMusic32()
    {
        music12.volume = Mathf.Clamp(music12.volume - 1, 0, 1);
        music22.volume = Mathf.Clamp(music22.volume - 1, 0, 1);
        music42.volume = Mathf.Clamp(music42.volume - 1,0,1);
        play12 = false;
        play22 = false;
        play42 = false;

        if (play32)
        {
            music32.volume = Mathf.Clamp(music32.volume - 1, 0, 1);
            play32 = false;
        }
        else
        {
            music32.volume = Mathf.Clamp(music32.volume + 1, 0, 1);
            play32 = true;
        }
    }
    public void onMusic42()
    {
        music12.volume = Mathf.Clamp(music12.volume - 1, 0, 1);
        music22.volume = Mathf.Clamp(music22.volume - 1, 0, 1);
        music32.volume = Mathf.Clamp(music32.volume - 1,0,1);
        play12 = false;
        play22 = false;
        play32 = false;

        if (play42)
        {
            music42.volume = Mathf.Clamp(music42.volume - 1, 0, 1);
            play42 = false;
        }
        else
        {
            music42.volume = Mathf.Clamp(music42.volume + 1, 0, 1);
            play42 = true;
        }
    }

    public void onMusic13()
    {
        music23.volume = Mathf.Clamp(music23.volume - 1, 0, 1);
        music33.volume = Mathf.Clamp(music33.volume - 1, 0, 1);
        music43.volume = Mathf.Clamp(music43.volume - 1,0,1);
        play23 = false;
        play33 = false;
        play43 = false;

        if (play13)
        {
            music13.volume = Mathf.Clamp(music13.volume - 1, 0, 1);
            play13 = false;
        }
        else
        {
            music13.volume = Mathf.Clamp(music13.volume + 1, 0, 1);
            play13 = true;
        }
    }
    public void onMusic23()
    {
        music13.volume = Mathf.Clamp(music13.volume - 1, 0, 1);
        music33.volume = Mathf.Clamp(music33.volume - 1, 0, 1);
        music43.volume = Mathf.Clamp(music43.volume - 1,0,1);
        play13 = false;
        play33 = false;
        play43 = false;

        if (play23)
        {
            music23.volume = Mathf.Clamp(music23.volume - 1, 0, 1);
            play23 = false;
        }
        else
        {
            music23.volume = Mathf.Clamp(music23.volume + 1, 0, 1);
            play23 = true;
        }
    }
    public void onMusic33()
    {
        music23.volume = Mathf.Clamp(music23.volume - 1, 0, 1);
        music13.volume = Mathf.Clamp(music13.volume - 1, 0, 1);
        music43.volume = Mathf.Clamp(music43.volume - 1,0,1);
        play13 = false;
        play23 = false;
        play43 = false;

        if (play33)
        {
            music33.volume = Mathf.Clamp(music33.volume - 1, 0, 1); music33.Stop();
            play33 = false;
        }
        else
        {
            music33.volume = Mathf.Clamp(music33.volume + 1, 0, 1);
            play33 = true;
        }
    }
    public void onMusic43()
    {
        music23.volume = Mathf.Clamp(music23.volume - 1, 0, 1);
        music13.volume = Mathf.Clamp(music13.volume - 1, 0, 1);
        music33.volume = Mathf.Clamp(music33.volume - 1,0,1);
        play13 = false;
        play23 = false;
        play33 = false;

        if (play43)
        {
            music43.volume = Mathf.Clamp(music43.volume - 1, 0, 1);
            play43 = false;
        }
        else
        {
            music43.volume = Mathf.Clamp(music43.volume + 1, 0, 1);
            play43 = true;
        }
    }
    public void onMusic14()
    {
        music24.volume = Mathf.Clamp(music24.volume - 1, 0, 1);
        music34.volume = Mathf.Clamp(music34.volume - 1, 0, 1);
        music44.volume = Mathf.Clamp(music44.volume - 1,0,1);
        play24 = false;
        play34 = false;
        play44 = false;

        if (play14)
        {
            music14.volume = Mathf.Clamp(music14.volume - 1, 0, 1);
            play14 = false;
        }
        else
        {
            music14.volume = Mathf.Clamp(music14.volume + 1, 0, 1);
            play14 = true;
        }
    }
    public void onMusic24()
    {
        music14.volume = Mathf.Clamp(music14.volume - 1, 0, 1);
        music34.volume = Mathf.Clamp(music34.volume - 1, 0, 1);
        music44.volume = Mathf.Clamp(music44.volume - 1,0,1);
        play14 = false;
        play34 = false;
        play44 = false;

        if (play24)
        {
            music24.volume = Mathf.Clamp(music24.volume - 1, 0, 1);
            play24 = false;
        }
        else
        {
            music24.volume = Mathf.Clamp(music24.volume + 1, 0, 1);
            play24 = true;
        }
    }
    public void onMusic34()
    {
        music24.volume = Mathf.Clamp(music24.volume - 1, 0, 1);
        music14.volume = Mathf.Clamp(music14.volume - 1, 0, 1);
        music44.volume = Mathf.Clamp(music44.volume - 1,0,1);
        play24 = false;
        play14 = false;
        play44 = false;

        if (play34)
        {
            music34.volume = Mathf.Clamp(music34.volume - 1, 0, 1);
            play34 = false;
        }
        else
        {
            music34.volume = Mathf.Clamp(music34.volume + 1, 0, 1);
            play34 = true;
        }
    }
    public void onMusic44()
    {
        music24.volume = Mathf.Clamp(music24.volume - 1, 0, 1);
        music14.volume = Mathf.Clamp(music14.volume - 1, 0, 1);
        music34.volume = Mathf.Clamp(music34.volume - 1,0,1);
        play24 = false;
        play14 = false;
        play34 = false;

        if (play44)
        {
            music44.volume = Mathf.Clamp(music44.volume - 1, 0, 1);
            play44 = false;
        }
        else
        {
            music44.volume = Mathf.Clamp(music44.volume + 1, 0, 1);
            play44 = true;
        }
    }

}
