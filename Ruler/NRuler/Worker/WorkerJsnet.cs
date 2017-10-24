using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NRuler.Model;
using Noesis.Javascript;
using System.ComponentModel;
using System.Reflection;

namespace NRuler.Worker
{
    /// <summary>
    /// 具体命令执行者
    /// </summary>
    class WorkerJsnet : IWorker
    {
        #region 声明属性
        private static JavascriptContext context = new JavascriptContext();
        private static int usedCount = 0;
        private const int reGenerateJSContextPeek = 1500;
        public List<ParamInfo> paramList = new List<ParamInfo>();
        private object result = null;   // 运行后结果
        #endregion

        #region 共有函数

        public List<ParamInfo> SetParam(string paramName, object paramValue) 
        {
            paramList.Add(new ParamInfo { ParamName=paramName, ParamValue=paramValue });
            return paramList;
        }
        public List<ParamInfo> SetParams(params ParamInfo[] paramArray)
        {
            paramList = paramArray.ToList();
            return paramList;
        }
        
        private static void ResourceEnsurace()
        {
            usedCount++;

            if (usedCount >= reGenerateJSContextPeek)
            {
                if (context != null)
                {
                    context.Dispose();
                    context = null;
                }

                context = new JavascriptContext();

                usedCount = 1;
            }
        }

        public string GenerateJS(string jsCode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.GeneratePreservedJS());
            sb.AppendLine();
            sb.AppendLine("function _GET_RETURN()");
            sb.AppendLine("{");
            sb.AppendLine(jsCode);
            sb.AppendLine("}");
            sb.AppendLine();
            sb.AppendLine("_RESULT_RETURN=_GET_RETURN();");

            return sb.ToString();
        }

        public object Run(string externalCode, params ParamInfo[] parameters)
        {
            ResourceEnsurace();

            foreach (var item in parameters)
                context.SetParameter(item.ParamName, item.ParamValue);

            string realJs = GenerateJS(externalCode);

            context.Run(realJs);

            result = context.GetParameter("_RESULT_RETURN");
            return result;
        }

        public T GetResult<T>() 
        {
            object obj = result;
            return (T)obj;
        }

        public object GetResult()
        {
            if (result==null)
                return null;
            object obj = result;
            Type type = obj.GetType();
            return ConvertObject(obj, type);
        }

        #endregion

        #region 私有函数

        private string _js = string.Empty;
        private string GeneratePreservedJS()
        {
            if (!string.IsNullOrEmpty(_js))
                return _js;

            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Scripts", "underscore-min.js");
            _js = File.ReadAllText(path);

            return _js;
        }

        /// <summary>
        /// 将一个对象转换为指定类型
        /// </summary>
        /// <param name="obj">待转换的对象</param>
        /// <param name="type">目标类型</param>
        /// <returns>转换后的对象</returns>
        private object ConvertObject(object obj, Type type)
        {
            if (type == null) return obj;
            if (obj == null) return type.IsValueType ? Activator.CreateInstance(type) : null;
 
            Type underlyingType = Nullable.GetUnderlyingType(type);
            if (type.IsAssignableFrom(obj.GetType())) // 如果待转换对象的类型与目标类型兼容，则无需转换
            {
                return obj;
            }
            else if ((underlyingType ?? type).IsEnum) // 如果待转换的对象的基类型为枚举
            {
                if (underlyingType != null && string.IsNullOrEmpty(obj.ToString())) // 如果目标类型为可空枚举，并且待转换对象为null 则直接返回null值
                {
                    return null;
                }
                else
                {
                    return Enum.Parse(underlyingType ?? type, obj.ToString());
                }
            }
            else if (typeof(IConvertible).IsAssignableFrom(underlyingType ?? type)) // 如果目标类型的基类型实现了IConvertible，则直接转换
            {
                try
                {
                    return Convert.ChangeType(obj, underlyingType ?? type, null);
                }
                catch
                {
                    return underlyingType == null ? Activator.CreateInstance(type) : null;
                }
            }
            else
            {
                TypeConverter converter = TypeDescriptor.GetConverter(type);
                if (converter.CanConvertFrom(obj.GetType()))
                {
                    return converter.ConvertFrom(obj);
                }
                ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
                if (constructor != null)
                {
                    object o = constructor.Invoke(null);
                    PropertyInfo[] propertys = type.GetProperties();
                    Type oldType = obj.GetType();
                    foreach (PropertyInfo property in propertys)
                    {
                        PropertyInfo p = oldType.GetProperty(property.Name);
                        if (property.CanWrite && p != null && p.CanRead)
                        {
                            property.SetValue(o, ConvertObject(p.GetValue(obj, null), property.PropertyType), null);
                        }
                    }
                    return o;
                }
            }
            return obj;
        }

        #endregion
    }
}
