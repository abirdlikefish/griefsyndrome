using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHomuraBullet
{
    public void CreateCartridge_handgun();
    public void CreateCartridge_minimi();

    public void Discard_RPG();
    public void Discard_minimi();
    public void Discard_minimiB();

    public void Fire_RPG();
    public void Fire_minimi();
    public void Fire_handgun();
    public void Fire_grenade();
    public void Fire_grenade_front();
    // public void Fire_mortar();

    public void Place_mortar();
}
