﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrackingApp.Business
{
    public class ServiceResult<T>
    {
        public T Result { get; set; }

        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public bool Succeed { get; set; }

        public static ServiceResult<T> Success(T data)
        {
            return new ServiceResult<T> { Succeed = true, Result = data };
        }

        public static ServiceResult<T> Failed(T data, string message, int code)
        {
            return new ServiceResult<T> { Succeed = false, Result = default(T), ErrorMessage = message, ErrorCode = code };
        }
    }
}
