﻿using System;
using System.Collections.Generic;

namespace LSG.GenericCrud.Models
{
    public class Changeset
    {
        public Guid EventId { get; set; }
        public string EventName { get; internal set; }
        public DateTime EventDate { get; set; }
        public string UserId { get; set; }

        public List<Change> Changes { get; set; }

    }
}