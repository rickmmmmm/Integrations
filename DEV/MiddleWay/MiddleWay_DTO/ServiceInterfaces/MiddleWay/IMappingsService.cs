﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.ServiceInterfaces.MiddleWay
{
    public interface IMappingsService
    {
        List<U> Map<T, U>(List<T> item) where U : new();
    }
}
