using System;
namespace IdGenerator.Obfuscat
{
    /// <summary>
    /// id混淆器, 模糊含有规则的id值, 比如数据库自增id
    /// </summary>
    public class IdObfuscator
    {
        private Feistel feistel;
        private Base62 base62Transfer;

        public IdObfuscator()
        {
            this.feistel = new Feistel();
            this.base62Transfer = new Base62();
        }
        /// <summary>
        /// 通过Feistel加密算法, 隐藏id特征
        /// https://my.oschina.net/u/2485991/blog/613920
        /// </summary>
        /// <param name="id">无符号长整型id</param>
        /// <returns>返回经过加密后的id</returns>
        public ulong Permute(ulong id)
        {
            return this.feistel.Permute(id);
        }

        /// <summary>
        /// 通过Feistel加密, 并转换为62进制
        /// </summary>
        /// <param name="id">无符号长整型id</param>
        /// <returns>返回62进制字符串</returns>
        public string PermuteToBase62(ulong id)
        {
            return this.base62Transfer.Parse(this.Permute(id));
        }

        /// <summary>
        /// 通过Feistel加密算法, 隐藏id特征
        /// https://my.oschina.net/u/2485991/blog/613920
        /// </summary>
        /// <param name="id">无符号整型id</param>
        /// <returns>返回经过加密后的id</returns>
        public uint Permute(uint id)
        {
            return this.feistel.Permute(id);
        }

        /// <summary>
        /// 通过Feistel加密, 并转换为62进制
        /// </summary>
        /// <param name="id">无符号长整型id</param>
        /// <returns>返回62进制字符串</returns>
        public string PermuteToBase62(uint id)
        {
            return this.base62Transfer.Parse(this.Permute(id));
        }
    }
}
