﻿using clean_architecture.Application.Common.Interfaces;
using System;

namespace clean_architecture.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
