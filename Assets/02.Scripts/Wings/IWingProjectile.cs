using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IWingProjectile
{
    //투사체를 발사하는 함수를 구현해야함.
    void Fire();
    IEnumerator FireCylce();
}
