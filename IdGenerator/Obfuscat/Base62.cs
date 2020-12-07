using System;
using System.Collections.Generic;

namespace IdGenerator.Obfuscat
{
    /// <summary>
    /// 10进制转62进制
    /// </summary>
    public class Base62
    {
        /// <summary>
        /// 实际应用中可以调整元素的顺序
        /// </summary>
        private char[] _base62Chars = new char[]
        {
            'g',
            'D',
            's',
            'p',
            'f',
            'w',
            'X',
            '4',
            'x',
            'Q',
            'o',
            '5',
            'S',
            'F',
            'b',
            't',
            'c',
            'd',
            '8',
            'W',
            'P',
            'q',
            'M',
            'L',
            'V',
            'Y',
            'E',
            'N',
            'H',
            'I',
            'a',
            '3',
            'm',
            'A',
            'i',
            'e',
            'r',
            'C',
            '7',
            '9',
            'Z',
            'z',
            'G',
            'K',
            'n',
            'T',
            'y',
            'j',
            'B',
            'J',
            'O',
            'l',
            'U',
            'k',
            'v',
            '6',
            '1',
            'h',
            'u',
            'R',
            '2',
            '0',
       };
       
        /// <summary>
        /// 计算62进制,支持最大的无符号长整型
        /// </summary>
        /// <param name="number">原数值</param>
        /// <returns>转为62进制后的数组</returns>
        private  int[] RadixTransform(ulong number)
        {
            ulong rest = number;
            List<int> list = new List<int>();
            while (rest != 0)
            {
                list.Add((int)(rest - (rest / 62) * 62));
                rest = rest / 62;
            }
            int[] values = new int[list.Count];
            int j = values.Length - 1;
            for (int i = 0; i < list.Count; i++)
            {
                values[j] = list[i];
                --j;
                if (j < 0) break;
            }
            return values;
        }

        /// <summary>
        /// 将原无符号整型/无符号长整型转换为62进制字符串
        /// </summary>
        /// <param name="number">原数值</param>
        /// <returns>62进制字符串</returns>
        public string Parse(ulong number)
        {
            int[] values = this.RadixTransform(number);
            char[] resultArr = new char[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                resultArr[i] = this._base62Chars[values[i]];
            }
            return string.Join("", resultArr);
        }

    }
}
