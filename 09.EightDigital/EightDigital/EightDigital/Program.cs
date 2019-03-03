﻿using System;

namespace EightDigital {
    /// <summary>
    /// 测试类 2018年9月25日13:51:32
    /// </summary>
    public class Program {
        /* 题目（八数码问题）：
           有 1 - 8 八个数字，放在 3 * 3 的九宫格里面，留下一个空格（代码实现时，我们将空格表示为 0 ）
                  3    4    1    
                  5    6    0    
                  8    2    7    
           空格可以和相邻的数字进行交换，如移动成 
                  1    2    3    
                  4    5    6    
                  7    8    0    
           则移动完成 */
        static void Main(string[] args) {
            int[,] arr = { { 3, 4, 1 }, { 5, 6, 0 }, { 8, 2, 7 } };

            //int[,] arr = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 0, 8 } };

            //int[,] arr = { { 4, 5, 1 }, { 8, 3, 0 }, { 2, 7, 6 } };//左 左 上 右 下 左 下 右 右 上 左 左 下 右 上 上 右 下 左 左 上 右 下 右 下

            //int[,] arr = { { 3, 4, 1 }, { 5, 0, 6 }, { 8, 2, 7 } };
            EightDigital e = new EightDigital(arr);

            //bool result = e.ExcuteDFS();

            #region 算法分析 DFS算法的缺陷 --> BFS 算法的引入
            /* 这里可以很明显的看出本次案例中 DFS 算法的弊端：即堆栈内存开销大、非最优解 */
            /* 上面简单的例子 可以一眼看出 0 向右移动一次即可成功变为最终状态 */
            /* 而运行 DFS 算法后 发现也确实输出为： True 右 */
            /* 但是 若将 DFS 的搜索方向优先级改为 DFSSearch(LEFT) || DFSSearch(RIGHT) || DFSSearch(UP) || DFSSearch(DOWN) */
            /* 则会发现 输出结果为：True    左 右 上 右 左 右 左 上 左 左 右 下 右 左 右 左 上 左 左 右 下 右 左 右 左 上 左
                                             左 右 下 右 左 右 左 上 左 左 右 下 右 左 右 左 上 左 左 右 下 右 下 左 右 上
                                            右 左 右 左 下 左 左 右 上 右 左 右 左 下 左 左 右 上 右 左 右 左 下 左 左 右 上
                                             右 左 右 左 下 左 左 右 上 右 左 右 左 下 左 上 下 左 左 右 上 右 左 右 左 上
                                            左 左 右 下 右 左 右 左 上 左 左 右 下 右 左 右 左 上 左 左 右 下 右 左 右 左 上
                                             左 左 右 下 右 左 右 左 上 左 左 右 下 右 下 左 右 上 右 左 右 左 下 左 左 右
                                            上 右 左 右 左 下 左 左 右 上 右 左 右 左 下 左 左 右 上 右 左 右 左 下 左 左 右
                                             上 右 左 右 左 下 左 上 下 左 左 右 上 右 左 右 左 上 左 左 右 下 右 左 右 左
                                            上 左 左 右 下 右 左 右 左 上 左 左 右 下 右 左 右 左 上 左 左 右 下 右 左 右 左
                                             上 左 左 右 下 右 下 左 右 上 右 左 右 左 下 左 左 右 上 右 左 右 左 下 左 左
                                            右 上 右 左 右 左 下 左 左 右 上 右 左 右 左 下 左 左 右 上 右 左 右 左 下 左 上
                                             下 左 左 右 上 右 左 右 左 上 左 左 右 下 右 左 右 左 上 左 左 右 下 右 左 右
                                            左 上 左 左 右 下 右 左 右 左 上 左 左 右 下 右 左 右 左 上 左 左 右 下 右 下 左
                                             右 上 右 左 右 左 下 左 左 右 上 右 左 右 左 下 左 左 右 上 右 左 右 左 下 左
                                            左 右 上 右 左 右 左 下 左 左 右 上 右 左 右 左 下 左 上 下 左 左 右 上 右 左 右
                                             左 上 左 左 右 下 右 左 右 左 上 左 左 右 下 右 左 右 左 上 左 左 右 下 右 左
                                            右 左 上 左 左 右 下 右 左 右 左 上 左 左 右 下 右 下 左 右 上 右 左 右 左 下 左
                                             左 右 上 右 左 右 左 下 左 左 右 上 右 左 右 左 下 左 左 右 上 右 左 右 左 下
                                            左 左 右 上 右 左 右 左 下 左 上 下 左 左 右 上 右 左 右 左 上 左 左 右 下 右 左
                                             右 左 上 左 左 右 下 右 左 右 左 上 左 左 右 下 右 左 右 左 上 左 左 右 下 右
                                            左 右 左 上 左 左 右 下 右 下 左 右 上 右 左 右 左 下 左 左 右 上 右 左 右 左 下
                                             左 左 右 上 右 左 右 左 下 左 左 右 上 右 左 右 左 下 左 左 右 上 右 左 右 左
                                            下 左 上 下 左 左 右 上 右 左 右 左 上 左 左 右 下 右 左 右 左 上 左 左 右 下 右
                                             左 右 左 上 左 左 右 下 右 左 右 左 上 左 左 右 下 右 左 右 左 上 左 左 右 下
                                            右 下 左 右 上 右 左 右 左 下 左 左 右 上 右 左 右 左 下 左 左 右 上 右 左 右 左
                                             下 左 左 右 上 右 左 右 左 下 左 左 右 上 右 左 右 左 下 左 上 下 */
            /* 如此路径。先不言其正确与否 即使其真的可以走通 但也可以确定的是 绝非最优路径 */
            /* 不仅如此 在搜索过程中 也肯定会有很多类似的情况最终因算法选择了"错误"的路径而一路错误的搜索下去 误入歧途 */
            /* 最终导致情况越来越复杂 甚至出现内存溢出等异常而终止 */
            /* DFS 算法的这种情况就类似与现实生活中的"钻牛角尖"  它会在一个方向一直搜索下去直到该方向走不通为止 */
            /* 如上题。因为算法认为左侧是可以走通且不违反规则 所以就会向左走 然后继续的一路遍历下去 殊不知如果第一步朝右 就已经胜利了 */

            /* 我们要避免 DFS 算法带来的这种"歧途"效果 */
            /* 首先 考虑到的应该就是 -- 是否可以每次对四个方向进行并行的搜索？然后择优？ */

            /* 如此思路 就牵扯到了 BFS 广度优先算法 */

            #endregion

            bool result = e.ExcuteBFS();
            Console.WriteLine(result.ToString());
            e.PrintCurrentPath();

            //e.PrintCurrentStatus();

            Console.ReadLine();
        }
    }
}
