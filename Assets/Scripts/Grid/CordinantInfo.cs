using System;

namespace TetrisBlast.Grid
{
    [Serializable]
    public class CordinantInfo
    {
        public int key;
        public int orderX;

        public CordinantInfo(int key, int x)
        {
            this.key = key;
            orderX = x;
        }
    }
}