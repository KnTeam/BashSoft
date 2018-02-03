using System;

namespace SimpleJudge
{
    public static class SimpleJudge
    {
        public static void Main(string[] args)
        {
            Tester.CompareContent(@"D:\Git\BashSoft\UserOutput\actual.txt", @"D:\Git\BashSoft\UserOutput\expected.txt");
        }
    }
}
