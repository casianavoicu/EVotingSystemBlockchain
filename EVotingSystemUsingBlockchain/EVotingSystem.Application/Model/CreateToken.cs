﻿using System;

namespace EVotingSystem.Application.Model
{
    public class CreateToken
    {
        public int BlockIndex { get; set; }

        public DateTime TimeStamp { get; set; }

        public byte[] PreviousHash { get; set; }

        public string Data { get; set; }
    }
}
