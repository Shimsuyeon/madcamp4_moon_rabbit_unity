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
        }

    }
}