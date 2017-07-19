using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Source.Interface
{
    public interface INPCAction
    {
        void Move(Vector3 f, Vector3 r);
        void Attack();
        void Die();
    }
}
