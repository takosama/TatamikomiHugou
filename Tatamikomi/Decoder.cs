using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tatamikomi
{


    public class Decoder
    {
        List<int>[] route = new List<int>[4];
        int[] pathMetricArray =
       {
                int.MaxValue,
                int.MaxValue,
                int.MaxValue,
                int.MaxValue,
            };

        public Decoder()
        {
            Reset();
        }

        void Reset()
        {
            cnt = 0;
            for (int i = 0; i < route.Length; i++)
                route[i] = new List<int>();

            pathMetricArray = new int[]
            {
                    int.MaxValue,
                    int.MaxValue,
                    int.MaxValue,
                    int.MaxValue,
            };
        }

        int cnt = 0;

        int[] ComputeNode()
        {
            int min = int.MaxValue;
            int nodeID = 0;
            for (int i = 0; i < 4; i++)
            {
                if (pathMetricArray[i] < min)
                {
                    min = pathMetricArray[i];
                    nodeID = i;
                }
            }
            //nodeID = 0;
            int[] rtn = new int[route[0].Count + 1];
            rtn[route[0].Count] = nodeID;
            for (int i = route[0].Count - 1; i >= 0; i--)
            {
                rtn[i] = route[rtn[i + 1]][i];
            }
            rtn = rtn.Select(x => x % 2).ToArray();
            return rtn;
        }

        void Decode(int data, int sumcount)
        {

            if (cnt <= 1)
            {
                if (cnt == 0)
                {
                    pathMetricArray[0] = getlng(data, 00);
                    pathMetricArray[1] = getlng(data, 01);
                }
                else
                {
                    int p0 = pathMetricArray[0];
                    int p1 = pathMetricArray[1];
                    pathMetricArray[0] = p0 + getlng(data, 00);
                    pathMetricArray[1] = p0 + getlng(data, 01);
                    pathMetricArray[2] = p1 + getlng(data, 01);
                    pathMetricArray[3] = p1 + getlng(data, 00);

                    route[0].Add(0);


                    route[1].Add(0);


                    route[2].Add(1);

                    route[3].Add(1);
                }
            }
            else
            {
                int[] tmp = new int[8];
                tmp = Consts.outputArray.Select(x => getlng(x, data)).ToArray();
                for (int i = 0; i < 8; i++)
                {
                    tmp[i] += pathMetricArray[i / 2];
                }
                if (tmp[0] < tmp[4])
                {
                    route[0].Add(0);
                    pathMetricArray[0] = tmp[0];
                }
                else
                {
                    route[0].Add(2);
                    pathMetricArray[0] = tmp[4];
                }

                if (tmp[1] < tmp[5])
                {
                    route[1].Add(0);
                    pathMetricArray[1] = tmp[1];
                }
                else
                {
                    route[1].Add(2);

                    pathMetricArray[1] = tmp[5];
                }

                if (tmp[2] < tmp[6])
                {
                    route[2].Add(1);

                    pathMetricArray[2] = tmp[2];
                }
                else
                {
                    route[2].Add(3);
                    pathMetricArray[2] = tmp[6];
                }

                if (tmp[3] < tmp[7])
                {
                    route[3].Add(1);

                    pathMetricArray[3] = tmp[3];
                }
                else
                {
                    route[3].Add(3);

                    pathMetricArray[3] = tmp[7];
                }
            }
            cnt++;

        }

        int getlng(int data0, int data1)
        {
            int xor = data0 ^ data1;
            int rtn = (xor >> 1) + (xor & 0x01);
            return rtn;
        }

        public int[] Decode(int[] data)
        {
            foreach (var n in data)
                Decode(n, data.Length);
            var rtn = ComputeNode();

            return rtn;
        }
    }
}
