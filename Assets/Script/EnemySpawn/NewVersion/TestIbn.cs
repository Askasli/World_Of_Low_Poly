using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TestIbn : MonoBehaviour
{
    private ItestTwo itest;

    [Inject]
    public void Constuct(ItestTwo itest)
    {
        this.itest = itest; 
    }

    private void Update()
    {
        itest.poof();
    }
}
