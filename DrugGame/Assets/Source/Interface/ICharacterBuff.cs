using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*
 * 2017-07-17 jin5866
 * 
 * 캐릭터의 버프 디버프 관리
 * 
 */ 
namespace Assets.Source.Interface
{
    interface ICharacterBuff
    {
        void GetBuff(float degree);
        void GetDebuff(float degree);
        void ResetBuff();
    }
}
