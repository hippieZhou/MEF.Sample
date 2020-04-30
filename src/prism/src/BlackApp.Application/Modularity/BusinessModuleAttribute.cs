using System;

namespace BlackApp.Application.Modularity
{
    /// <summary>
    /// 对模块进行层级划分
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class BusinessModuleAttribute : Attribute
    {
        /// <summary>
        /// 模块显示的优先级
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// 模块归属的菜单：请使用 MainOwnedContracts 中的相应值
        /// </summary>
        public string Belong { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        public string FriendlyName { get; set; }
    }
}
