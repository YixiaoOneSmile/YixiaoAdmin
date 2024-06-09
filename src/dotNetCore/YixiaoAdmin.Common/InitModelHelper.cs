using System;

namespace YixiaoAdmin.Common
{
    /// <summary>
    /// 唯一用户标识帮助类
    /// </summary>
    public class GuidHelper
    {
        /// <summary>
        /// 统一生成GUID（仅包含大写字母和数字）
        /// </summary>
        /// <returns></returns>
        public static string NewGUID()
        {
            return Guid.NewGuid().ToString("N").ToUpper();
        }
    }
    public class InitModel
    {
        /// <summary>
        /// 模型初始化
        /// </summary>
        /// <param name="model">传入的模型</param>
        /// <param name="flag">标志 如果为true则初始化，如果为false则只修改ModifyTime和ModifyUser</param>
        /// <returns></returns>
        public static dynamic Init(dynamic model, string userId, bool flag = false)
        {
            if (flag)
            {
                model.Id = GuidHelper.NewGUID();
                model.CreateUsername = userId;
                model.CreateTime = DateTime.Now;

            }
            model.ModificationUsername = userId;
            model.ModificationTime = DateTime.Now;
            return model;
        }
    }
}
