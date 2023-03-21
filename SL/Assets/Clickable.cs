using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Hero;
using UnityEngine;

public class Clickable : MonoBehaviour
{
   [SerializeField] private Reticle _reticle;

   private void OnMouseDown()
   {
      _reticle.Selected(gameObject);
   }

   private void OnMouseUp()
   {
      _reticle.Deselect();
   }
}

public class Point : MonoBehaviour
{
   public bool flip;
}
