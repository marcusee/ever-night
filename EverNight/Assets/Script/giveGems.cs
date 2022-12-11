using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class giveGems : MonoBehaviour {

    public void add1()
    {
        Player.Instance.gems += 100;
    }
    public void add2()
    {
        Player.Instance.gems += 1000;
    }
    public void add3()
    {
        Player.Instance.gems += 2000;
    }
}