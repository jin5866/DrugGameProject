using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * 캐릭터별로 움직임 관리
 * 
 * 
 */ 
namespace Assets.Source.Interface
{
    interface ICharacterMover
    {
        void Move(float h, float v);
        float maxSpeed { set; }
    }
}
