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

    }
}