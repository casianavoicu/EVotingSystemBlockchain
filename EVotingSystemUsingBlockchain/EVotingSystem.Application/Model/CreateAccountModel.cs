﻿using System;

namespace EVotingSystem.Application.Model
{
    public class CreateAccountModel
    {
        public string AccountAddress { get; set; }

        public string PublicKey { get; set; }

        public int Balance { get; set; }
    }
}
