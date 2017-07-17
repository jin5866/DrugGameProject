using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Source.Interface
{
    interface ICharacterBuff
    {
        void GetBuff(float degree);
        void GetDebuff(float degree);
        void ResetBuff();
    }
}
