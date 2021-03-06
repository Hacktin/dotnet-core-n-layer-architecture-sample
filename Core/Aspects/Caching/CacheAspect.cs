﻿using System;
using System.Linq;
using Castle.DynamicProxy;
using Core.Aspects.Base;
using Core.CrossCuttingCornces.Caching;
using Core.Ioc;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Caching
{
    public class CacheAspect:MethodInterceptors
    {

        private int _duration;
        private ICacheManager _cacheManager;
        public CacheAspect(int duration)
        {
            this._duration = duration;
            _cacheManager = ServiceCollectionTool.serviceProvider.GetService<ICacheManager>();
        }
        public override void Intercept(IInvocation invocation)
        {
            string methodName = string.Format($"{invocation.Method.Name}");
            var arguments = invocation.Arguments.ToList();

            string key = $"{methodName}(${string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))}";

            Type type = invocation.GetConcreteMethod().ReturnType;
            
            

            if (_cacheManager.IsAdd(key))
            {
                invocation.ReturnValue = _cacheManager.Get(key,type);
                return;
            }

            invocation.Proceed();
            _cacheManager.Add(key,invocation.ReturnValue,_duration);


        }
    }
}
