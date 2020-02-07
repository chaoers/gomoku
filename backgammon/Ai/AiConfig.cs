﻿using System;
using System.Collections.Generic;
using System.Text;

namespace backgammon
{
    class AiConfig
    {
        public int searchDeep = 8;  //搜索深度
        public int countLimit = 20; //gen函数返回的节点数量上限，超过之后将会按照分数进行截断
        public int timeLimit = 100; // 时间限制，秒
        public int vcxDeep = 5;  //算杀深度
        public bool random = false;// 在分数差不多的时候是不是随机选择一个走
        // 下面几个设置都是用来提升搜索速度的
        public int spreadLimit = 1;// 单步延伸 长度限制
        public bool star = true; // 是否开启 starspread
        public bool cache = true; // 使用缓存, 其实只有搜索的缓存有用，其他缓存几乎无用。因为只有搜索的缓存命中后就能剪掉一整个分支，这个分支一般会包含很多个点。而在其他地方加缓存，每次命中只能剪掉一个点，影响不大。
        public bool window = false; // 启用期望窗口，由于用的模糊比较，所以和期望窗口是有冲突的

        public enum player {
            hum = 1,
            com = 2,
            empty = 0
        }
    }
}
