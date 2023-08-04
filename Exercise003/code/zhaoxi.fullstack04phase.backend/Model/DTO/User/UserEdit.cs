﻿namespace Model.DTO.User
{
    public class UserEdit
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string NickName { get; set; }

        public string Password { get; set; }

        //public int UserType { get; set; }

        public bool IsEnable { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }
    }
}
