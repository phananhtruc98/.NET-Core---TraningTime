﻿using System;

namespace GrooveMessengerDAL.Models.CustomModel
{
    public class DialogDraftModel
    {
        public Guid Id { get; set; }
        public string Who { get; set; }
        public string Message { get; set; }
        public DateTime? Time { get; set; }
    }

    //[AttributeUsage(AttributeTargets.Property, Inherited = false)]
    //public class MapBy : Attribute
    //{
    //    public MapBy(Type enumType)
    //    {

    //    }
    //}
}
