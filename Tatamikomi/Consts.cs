using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tatamikomi
{
    static class Consts
    {
        public static readonly int[] nextStateArray =
                 {
            0,
            2,
            4,
            6,
            0,
            2,
            4,
            6
        };
        public static readonly int[] outputArray =
              {
            0,
            1,
            1,
            0,
            2,
            3,
            3,
            2
        };
    }
}
