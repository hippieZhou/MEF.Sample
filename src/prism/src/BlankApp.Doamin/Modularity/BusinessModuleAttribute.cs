using System;

namespace BlankApp.Doamin.Modularity
{
    /// <summary>
    /// 对模块进行层级划分
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class BusinessModuleAttribute : Attribute
    {
        /// <summary>
        /// 模块加载的优先级
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// 一级菜单
        /// </summary>
        public string MainMenu { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        public string FriendlyName { get; set; }
    }
}
