using System;
namespace IdGenerator.Obfuscat
{
    /// <summary>
    /// Feistel加密
    /// </summary>
    public class Feistel
    {
        /// <summary>
        /// 伪随机数
        /// 必须是数学意义上的函数（x = y意味着f（x）= f（y））, 但不一定是可逆的。
        /// </summary>
        /// <param name="input"></param>
        /// <returns>返回值为0-1之间</returns>
        private double RoundFunction(ulong input)
        {
            return ((1369 * input + 150889) % 714025) / 714025.0;
        }

        /// <summary>
        /// 无符号长整型加密
        /// </summary>
        /// <param name="n">无符号长整型id</param>
        /// <returns>加密后的id</returns>
        public ulong Permute(ulong n)
        {
            ulong l1 = (n >> 32) & 4294967295l;
            ulong r1 = n & 4294967295l;
            ulong l2, r2;
            for (int i = 0; i < 3; i++)
            {
                l2 = r1;
                r2 = l1 ^ (ulong)(this.RoundFunction(r1) * 4294967295l);
                l1 = l2;
                r1 = r2;
            }
            return ((r1 << 32) + l1);
        }

        /// <summary>
        /// 无符号整型加密
        /// </summary>
        /// <param name="n">无符号整型id</param>
        /// <returns>加密后的id</returns>
        public uint Permute(uint n)
        {
            uint l1 = (n >> 16) & 65535;
            uint r1 = n & 65535;
            uint l2, r2;
            for (int i = 0; i < 3; i++)
            {
                l2 = r1;
                r2 = l1 ^ (uint)(RoundFunction(r1) * 65535);
                l1 = l2;
                r1 = r2;
            }
            return ((r1 << 16) + l1);
        }

       
    }
}
