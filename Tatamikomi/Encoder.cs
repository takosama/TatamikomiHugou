using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tatamikomi
{

    public class Encoder
    {
        int encState = 0;

        public void Rseet()
        {
            this.encState = 0;
        }

        int EncodeData(int data)
        {
            int rtn = Consts.outputArray[encState + data];
            encState = Consts.nextStateArray[encState + data];
            return rtn;
        }

        public int[] Encode(int[] data)
        {

            int[] rtn = new int[data.Length];
            for (int i = 0; i < data.Length; i++)
                rtn[i] = EncodeData(data[i]);
            return rtn;
        }
    }
}
