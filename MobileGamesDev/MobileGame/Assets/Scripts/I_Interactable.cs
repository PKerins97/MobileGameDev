using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface I_Interactable
{
    void processTap();

    void processDrag(Ray ray);

    void deselectedObject();

    void processScale();

    void processRotate();

}