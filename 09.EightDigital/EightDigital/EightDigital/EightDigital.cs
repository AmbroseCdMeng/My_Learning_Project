using System;
using System.Collections.Generic;

namespace EightDigital {
    /// <summary>
    /// 八数码问题初始化及操作方法实现 2018年9月25日13:51:47
    /// </summary>
    public class EightDigital {
        #region 定义基本常量、变量

        /* 定义九宫格的最终状态 为了方便 直接使用string类型常量 */
        public const string STATUS = "123456780";

        /* 定义移动的方向 */
        public const int LEFT = 1;
        public const int RIGHT = 2;
        public const int UP = 3;
        public const int DOWN = 4;

        /* 定义九宫格 3 * 3 的二维数组表示 */
        public int[,] arr;

        /* 定义 x y 变量 存储 0 在二维数组中的位置 */
        public int x, y;

        #endregion

        #region 定义算法中使用的变量

        /* 定义集合 存储当前移动的路径 -- DFS使用 */
        public List<int?> movePath = new List<int?>();

        /* 定义集合（队列） 存储每一步搜索的对象 也是下一步要搜索的状态集合 -- BFS使用 */
        public List<BFSSearchItem> searchItemList = new List<BFSSearchItem>();

        /* 定义集合 存储已经搜索过的九宫格状态 */
        public List<string> statusList = new List<string>();

        #endregion

        #region 构造函数初始化 搜索空格位置

        /* 构造函数：初始化对象时遍历二维数组 搜索 0 所在的位置并对 x y 赋值 */
        public EightDigital(int[,] arr) {
            this.arr = arr;
            GetPositionOfSpace();
        }

        /* 返回空格（即 0）所在的 X Y 坐标 */
        private void GetPositionOfSpace() {
            for (int i = 0; i < arr.GetLength(0); i++) {
                for (int j = 0; j < arr.GetLength(1); j++) {
                    if (arr[i, j] == 0) {
                        x = i;
                        y = j;
                    }
                }
            }
        }

        #endregion

        #region 移动、回退操作
        /// <summary>
        /// 移动空格位置
        /// </summary>
        /// <param name="direction">移动的方向</param>
        /// <returns></returns>
        public bool Move(int direction) {
            /* 定义局部变量 canMove 存储指定方向是否可以进行移动操作 */
            bool canMove = false;
            /* 判断是否可以向指定方向移动 如可以 则继续 如不可以 则终止 */
            switch (direction) {
                case LEFT:
                    canMove = y > 0;
                    break;
                case RIGHT:
                    canMove = y < 2;
                    break;
                case UP:
                    canMove = x > 0;
                    break;
                case DOWN:
                    canMove = x < 2;
                    break;
                default:
                    break;
            }
            /* 如果不满足移动条件 则直接终止跳出方法 */
            if (!canMove) {
                return false;
            }
            /* 执行移动操作 */
            switch (direction) {
                case LEFT:
                    arr[x, y] = arr[x, y] ^ arr[x, y - 1];
                    arr[x, y - 1] = arr[x, y] ^ arr[x, y - 1];
                    arr[x, y] = arr[x, y] ^ arr[x, y - 1];
                    /* 移动成功后 重新定位 0 的位置 */
                    y = y - 1;
                    break;
                case RIGHT:
                    arr[x, y] = arr[x, y] ^ arr[x, y + 1];
                    arr[x, y + 1] = arr[x, y] ^ arr[x, y + 1];
                    arr[x, y] = arr[x, y] ^ arr[x, y + 1];
                    /* 移动成功后 重新定位 0 的位置 */
                    y = y + 1;
                    break;
                case UP:
                    arr[x, y] = arr[x, y] ^ arr[x - 1, y];
                    arr[x - 1, y] = arr[x, y] ^ arr[x - 1, y];
                    arr[x, y] = arr[x, y] ^ arr[x - 1, y];
                    /* 移动成功后 重新定位 0 的位置 */
                    x = x - 1;
                    break;
                case DOWN:
                    arr[x, y] = arr[x, y] ^ arr[x + 1, y];
                    arr[x + 1, y] = arr[x, y] ^ arr[x + 1, y];
                    arr[x, y] = arr[x, y] ^ arr[x + 1, y];
                    /* 移动成功后 重新定位 0 的位置 */
                    x = x + 1;
                    break;
                default:
                    break;
            }
            /* 移动操作完成后 将本次移动方向追加到路径集合中 */
            movePath.Add(direction);
            return true;
        }

        /// <summary>
        /// 回退空格位置 当移动后九宫格的状态与之前出现过的状态重复或不合理时 用于回溯操作
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public bool BackMove(int direction) {
            switch (direction) {
                case LEFT:
                    Move(RIGHT);
                    break;
                case RIGHT:
                    Move(LEFT);
                    break;
                case UP:
                    Move(DOWN);
                    break;
                case DOWN:
                    Move(UP);
                    break;
                default:
                    break;
            }
            /* 回退成功后将上次移动的方向从路径集合中移除 */
            movePath.RemoveAt(movePath.Count - 1);
            return true;
        }
        #endregion

        #region 获取九宫格状态、当前移动路径操作
        /// <summary>
        /// 获取当前九宫格的数字状态 依次按照顺序强转为int型 方便与常量STATUS的比较和存储
        /// </summary>
        /// <returns></returns>
        public string GetCurrentStatus() {
            string num = string.Empty;
            for (int i = 0; i < arr.GetLength(0); i++) {
                for (int j = 0; j < arr.GetLength(1); j++) {
                    num += arr[i, j].ToString();
                }
            }
            return num;
        }

        /// <summary>
        /// 恢复当前九宫格的状态到int类型的二维数组 并重新定位 0 的坐标
        /// </summary>
        /// <param name="currentStatus"></param>
        public void RecoverStatus(string currentStatus) {
            /* 恢复九宫格数组 */
            for (int i = 0; i < currentStatus.Length; i++) {
                /* 注意：currentStatus[i] 是一个字符串 需要将其转成int 否则 会自动转成对应的ASCII码 那样的话 "1" 可能就会被转为49 */
                arr[i / 3, i % 3] = Convert.ToInt32(currentStatus[i].ToString());
            }
            /* 重定位空格位置 */
            GetPositionOfSpace();
        }

        /// <summary>
        /// 输出当前九宫格的字符状态
        /// </summary>
        public void PrintCurrentStatus() {
            for (int i = 0; i < arr.GetLength(0); i++) {
                for (int j = 0; j < arr.GetLength(1); j++) {
                    Console.Write(arr[i, j].ToString() + "\t");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// 输出当前路径
        /// </summary>
        public void PrintCurrentPath() {
            for (int i = 0; i < movePath.Count; i++) {
                Console.Write(ConvertDirectionToString(movePath[i]) + " ");
            }
        }

        /// <summary>
        /// 方向转换成汉字形式显示
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public string ConvertDirectionToString(int? direction) {
            switch (direction) {
                case LEFT:
                    return "左";
                case RIGHT:
                    return "右";
                case UP:
                    return "上";
                case DOWN:
                    return "下";
                default:
                    return null;
            }
        }
        #endregion

        #region BFS 广度优先中获取某一状态形成的路径
        /// <summary>
        /// 根据指定的状态结点包含的信息（当前状态、上一次移动方向、上一结点）逆向追溯其形成的路径
        /// </summary>
        /// <param name="item"></param>
        public void GetRoute(BFSSearchItem item) {
            /* 清空 movePath 准备存储最终路径 （因movePath是两种算法共用存储路径的，所以有可能在移动过程中存储到一些无效路径） */
            movePath.Clear();
            /* 追溯过程直到当前结点无上一结点 即当前结点为首结点 为止 */
            while (null != item.LASTITEM) {
                /* 路径集合中在倒序插入方向 */
                movePath.Insert(0, item.DIRECTION);
                /* 追溯上一结点 */
                item = item.LASTITEM;
            }
        }
        #endregion

        #region 调用算法 返回执行结果
        public bool ExcuteDFS() {
            /* 记录初始状态 */
            statusList.Add(GetCurrentStatus());
            /* 分别向四个方向开始搜索 */
            return DFSSearch(LEFT) || DFSSearch(RIGHT) || DFSSearch(UP) || DFSSearch(DOWN);
        }

        public bool ExcuteBFS() {
            /* 记录初始状态 */
            statusList.Add(GetCurrentStatus());
            /* 初始状态加入搜索队列 准备搜索 */
            searchItemList.Add(new BFSSearchItem(GetCurrentStatus(), null, null));
            return BFSSearch();
        }
        #endregion

        #region 算法实现

        #region 深度优先算法 Depth First Search

        /* 深度优先算法的特点：
                适合获取所有结果    
                占用内存空间少 
                代码量少 
                常常与递归结合使用
                堆栈内存开销大
                效率相对较慢
        */

        /* 实现基本思想：暴力搜索 + 剪枝 + 回溯 + 递归 */

        /* 实现基本原理：
         *  1、利用递归算法 向任一方向持续搜索 直到该方向无法走通或者为已搜索过的状态
         *  2、在某一点 对四个方向进行搜索 如果四个方向均无法通过或均为已搜索过的状态 回溯到上一状态 改变方向继续搜索 
         *  3、当回溯到起始状态依然无有效可通过路径供选择时 认为该题目无解 
         *  4、当某次搜索完成后状态与指定状态相同时 则认为搜索成功 算法结束 */

        /// <summary>
        /// 深度优先算法实现
        /// </summary>
        /// <param name="direction">移动方向</param>
        /// <returns></returns>
        public bool DFSSearch(int direction) {
            /* 因某些路径可能十分复杂无法确定是否可以走通 最终导致内存溢出 所以新增判断 如果路径 > 100 则放弃该路径 */
            //if (movePath.Count > 100) { return false; }

            /* 根据传入的方向 执行移动操作（包含是否可移动的判断 */
            /* 如果移动失败（包含无法移动）结束本次操作 返回false */
            if (!Move(direction)) {
                return false;
            }

            /* 移动成功后 获取当前九宫格状态 */
            string currentStatus = GetCurrentStatus();

            /* 判断当前九宫格状态 如果为最终状态 则结束搜索 */
            if (STATUS == currentStatus) {
                return true;
            }

            /* 判断当前九宫格状态 如果为已经搜索过的状态 则回溯到上一步 并结束本次查找 */
            if (statusList.Contains(currentStatus)) {
                BackMove(direction);
                return false;
            }

            /* 若上述条件都不满足 则说明该状态是一个新状态 将其追加到已搜索状态集合中 */
            statusList.Add(currentStatus);

            /* 递归调用DFS算法 继续向移动后空格所在位置的四个方向搜索 */
            bool search = DFSSearch(LEFT) || DFSSearch(RIGHT) || DFSSearch(UP) || DFSSearch(DOWN);

            /* 如果四个方向都搜索失败 则回溯到上一步 并删除该路径 */
            if (!search) {
                BackMove(direction);
                return false;
            }
            return true;
        }
        #endregion

        #region 广度优先算法 Breadth First Search

        /* 广度优先算法的特点：
                适合获取最优解 
                占用内存空间较大 
                代码较为复杂 
                效率相对较高 
        */

        /* 实现基本思想：多方向并行搜索 + 队列存储 */

        /* 实现基本原理：
         *  1、定义一个链表结点 存储九宫格当前状态、上次移动的方向、移动之前的结点。（单向链表的结构）
         *  2、定义一个队列 存储搜索的结果。同时搜索某一点的四个方向 将未重复的搜索结果状态加入到队列中存储
         *  3、将当前状态、上次移动的方向、移动之前的项次对象 以结点的形式存储到链表中
         *  4、循环搜索队列中所有存储的状态的下一步（包括四个方向）直到出现最终胜利状态 输出对应链表中的路径
         *          备注：这里搜索队列中存储的状态需要将存储的string类型状态恢复为int的二维数组
         *  5、当队列为空 但依然未搜索到最终状态时 认为此题无解*/

        /// <summary>
        /// 广度优先算法代码实现
        /// </summary>
        public bool BFSSearch() {
            /* 如果队列中还存在未搜索的状态 */
            while (searchItemList.Count > 0) {
                /* 从队首开始搜索 */
                BFSSearchItem item = searchItemList[0];
                /* 从队列中移除本次取出的结点 */
                searchItemList.Remove(item); //searchItemList.RemoveAt(0);
                /* 如果该结点的当前状态为最终状态 则获取到达该状态的路径 并结束搜索 */
                if (item.STATUS == STATUS) {
                    /* 获取移动的路径到movePath集合中 */
                    GetRoute(item);
                    /* 搜索成功 结束 */
                    return true;
                }
                /* 如果当前结点非最终状态 恢复该状态为数组形式(包含重定位空格位置) */
                RecoverStatus(item.STATUS);
                /* 分别向4个方向继续搜索 将搜索结果继续保存 */
                for (int i = 1; i <= 4; i++)    // 1:LEFT   2:RIGHT    3:UP    4:DOWN
                {
                    /* 移动操作 如果不满足移动条件或者移动失败 结束本次移动 继续其他方向的搜索 */
                    if (!Move(i)) { continue; }
                    /* 移动成功后 获取当前九宫格状态 */
                    string currentStatus = GetCurrentStatus();
                    /* 如果该状态已经搜索过 */
                    if (statusList.Contains(currentStatus)) {
                        /* 回退到上一步 */
                        if (!BackMove(i)) { throw new Exception("回退操作异常"); }
                        /* 放弃该状态在当前方向的搜索 继续搜索其他方向 */
                        continue;
                    }
                    /* 如果该状态未被搜索过 则加入待搜索的队列 并且同时标记该状态已搜索（待搜索中也属于已经搜索过） */
                    searchItemList.Add(new BFSSearchItem(currentStatus, i, item));
                    statusList.Add(currentStatus);
                    /* 恢复为本次移动之前的状态 尝试向其他方向移动 */
                    BackMove(i);
                }
            }
            /* 当队列中所待搜索状态都已经搜索完成 仍未找到出口时 认为该题无解 结束算法 */
            return false;
        }

        #endregion

        #endregion
    }

    /// <summary>
    /// BFS 算法存储每一步信息的链表结点类
    /// </summary>
    public class BFSSearchItem {
        /// <summary>
        /// 当前九宫格状态
        /// </summary>
        public string STATUS { get; set; }
        /// <summary>
        /// 上一次移动的方向
        /// </summary>
        public int? DIRECTION { get; set; }
        /// <summary>
        /// 上一次移动的结点对象
        /// </summary>
        public BFSSearchItem LASTITEM { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="status"></param>
        /// <param name="dir"></param>
        /// <param name="item"></param>
        public BFSSearchItem(string status, int? dir, BFSSearchItem item) {
            STATUS = status;
            DIRECTION = dir;
            LASTITEM = item;
        }
    }
}
