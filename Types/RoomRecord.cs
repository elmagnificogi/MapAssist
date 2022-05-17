using MapAssist.Helpers;
using MapAssist.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MapAssist.Types
{
    public class RoomRecord
    {
        public string CharacterName;
        public string RoomName;
        public string RoomPassword;
        public DateTime time;

        public RoomRecord(string cname,string rname,string rpass)
        {
            CharacterName = cname;
            RoomName = rname;
            RoomPassword = rpass;
            time = DateTime.Now;
        }
    }
}
