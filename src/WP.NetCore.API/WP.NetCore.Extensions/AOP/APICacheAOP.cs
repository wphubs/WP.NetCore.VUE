using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WP.NetCore.Common;

namespace WP.NetCore.Extensions
{
    public class APICacheAOP : CacheAOPbase
    {
        private readonly IRedisCacheManager _cache;
        public APICacheAOP(IRedisCacheManager cache)
        {
            _cache = cache;
        }
        //Intercept方法是拦截的关键所在，也是IInterceptor接口中的唯一定义
        public override void Intercept(IInvocation invocation)
        {
            var method = invocation.MethodInvocationTarget ?? invocation.Method;
            //对当前方法的特性验证
            //如果需要验证
            if (method.GetCustomAttributes(true).FirstOrDefault(x => x.GetType() == typeof(CachingAttribute)) is CachingAttribute qCachingAttribute)
            {
                //获取自定义缓存键
                var cacheKey = string.IsNullOrEmpty(qCachingAttribute.PrefixKey)?"": qCachingAttribute.PrefixKey+"_" + CustomCacheKey(invocation);
                //根据key获取相应的缓存值
                var cacheValue = _cache.GetValue(cacheKey).Result;
                if (cacheValue != null)
                {
                    Type returnType;
                    if (typeof(Task).IsAssignableFrom(method.ReturnType))
                    {
                        returnType = method.ReturnType.GenericTypeArguments.FirstOrDefault();
                    }
                    else
                    {
                        returnType = method.ReturnType;
                    }

                    dynamic _result = Newtonsoft.Json.JsonConvert.DeserializeObject(cacheValue, returnType);
                    invocation.ReturnValue = (typeof(Task).IsAssignableFrom(method.ReturnType)) ? Task.FromResult(_result) : _result;
                    return;
                }
                //去执行当前的方法
                invocation.Proceed();
                //存入缓存
                if (!string.IsNullOrWhiteSpace(cacheKey))
                {

                    object response;
                    //Type type = invocation.ReturnValue?.GetType();
                    var type = invocation.Method.ReturnType;
                    if (typeof(Task).IsAssignableFrom(type))
                    {
                        var resultProperty = type.GetProperty("Result");
                        response = resultProperty.GetValue(invocation.ReturnValue);
                    }
                    else
                    {
                        response = invocation.ReturnValue;
                    }
                    if (response == null) response = string.Empty;

                    _cache.Set(cacheKey, response, TimeSpan.FromMinutes(qCachingAttribute.AbsoluteExpiration));
                }
            }
            else
            {
                invocation.Proceed();//直接执行被拦截方法
            }
        }

    }
}
