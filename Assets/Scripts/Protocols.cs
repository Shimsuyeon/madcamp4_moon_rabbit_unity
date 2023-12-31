using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protocols
{
    public class Packets
    {
        public class req_Login
        {
            public string id;
            public string pw;
            public string status;
            public int[] shop;
            public int[] cookie;
            public int score;
            public int money;
            public int[] cafe;
        }



        public class req_Register
        {
            public string id;
            public string pw;
        }
        public class res_Register
        {
            public bool result;
        }



        public class req_GetCookie {
            public string id;
        }
        public class res_GetCookie {
            public int[] cookie;
        }


        public class req_UpdateCookie {
            public string id;
            public int[] cookie;
            public int score;
        }


        public class req_GetRank {
            public RankUnit[] rank;
        }
        public class req_MakeCookie
        {
            public string id;
            public int[] cookie;
            public int[] shop;
        }

        public class req_cellCookies
        {
            public string id;
            public int[] shop;
            public int money;
        }


        public class req_buyCafe
        {
            public int[] cafe;
            public int money;
            public string id;
        }
    }
}