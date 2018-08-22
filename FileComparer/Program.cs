using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileComparer
{
    class Program
    {
        static void Main(string[] args)
        {
            var fs1 = new FileStream("W://MyGithub//FileComparer//.gitignore", FileMode.Open);
            var fs2 = new FileStream("W://MyGithub//RCommon//.gitignore", FileMode.Open);

            // 第一个文件的偏移字节数
            int pcount1 = 30 * 1024 * 1024 * 10;
            byte[] bytes = new byte[pcount1];
            fs1.Read(bytes, 0, pcount1);

            int i1, i2;
            // 一共的不同的字节数
            long totaldiff = 0;
            // 最大的连续不同的字节数
            long maxdiff = 0;
            // 当前的不同字节块
            long curdiff = 0;
            // 第一个不同字节的下标
            long firstDiffIndex = -1;
            // 最后一个不同字节的下标
            long lastDiffIndex = -1;
            long index = 0;
            while ((i1 = fs1.ReadByte()) != -1 && (i2 = fs2.ReadByte()) != -1)
            {
                var a = (byte)i1;
                var b = (byte)i2;
                if (a != b)
                {
                    if (firstDiffIndex == -1) firstDiffIndex = index;
                    lastDiffIndex = index;
                    curdiff++;
                    totaldiff++;
                }
                else
                {
                    if (curdiff > maxdiff) maxdiff = curdiff;
                    curdiff = 0;
                }
                index++;
            }

            Console.WriteLine("一共的不同的字节数：" + totaldiff);
            Console.WriteLine("最大的连续不同的字节数：" + maxdiff);
            Console.WriteLine("第一个不同字节的下标：" + firstDiffIndex);
            Console.WriteLine("最后一个不同字节的下标：" + lastDiffIndex);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }
    }
}
