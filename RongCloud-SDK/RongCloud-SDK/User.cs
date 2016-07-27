using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RongCloud_SDK
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public class User
    {
        /// <summary>
        /// 基本用户服务
        /// </summary>
        public class Baisc
        {
            /// <summary>
            /// 获取 Token 方法
            /// </summary>
            /// <param name="userId">用户 Id，最大长度 64 字节。是用户在 App 中的唯一标识码，必须保证在同一个 App 内不重复，重复的用户 Id 将被当作是同一用户</param>
            /// <param name="name">用户名称，最大长度 128 字节。用来在 Push 推送时显示用户的名称</param>
            /// <param name="portraitUri">用户头像 URI，最大长度 1024 字节。用来在 Push 推送时显示用户的头像</param>
            /// <returns></returns>
            public async static Task<Model.User.TokenResult> GetToken(string userId,string name,string portraitUri)
            {
                var data = new Dictionary<string, string>();
                data.Add("userId", userId);
                data.Add("name", name);
                data.Add("portraitUri", portraitUri);
                var json = await tool.Http.Post("https://api.cn.ronghub.com/user/getToken.json", data);
                return tool.Json.DataContractJsonDeSerialize<Model.User.TokenResult>(json);
            }

            /// <summary>
            /// 刷新用户信息 方法
            /// </summary>
            /// <param name="userId">用户 Id，最大长度 64 字节。是用户在 App 中的唯一标识码，必须保证在同一个 App 内不重复，重复的用户 Id 将被当作是同一用户</param>
            /// <param name="name">用户名称，最大长度 128 字节。用来在 Push 推送时显示用户的名称</param>
            /// <param name="portraitUri">用户头像 URI，最大长度 1024 字节。用来在 Push 推送时显示用户的头像</param>
            /// <returns></returns>
            public async static Task<Model.User.RefreshResult> Refresh(string userId, string name, string portraitUri)
            {
                var data = new Dictionary<string, string>();
                data.Add("userId", userId);
                data.Add("name", name);
                data.Add("portraitUri", portraitUri);
                var json = await tool.Http.Post("https://api.cn.ronghub.com/user/refresh.json", data);
                return tool.Json.DataContractJsonDeSerialize<Model.User.RefreshResult>(json);
            }

            /// <summary>
            /// 检查用户在线状态 方法
            /// </summary>
            /// <param name="userId">用户 Id，最大长度 64 字节。是用户在 App 中的唯一标识码，必须保证在同一个 App 内不重复，重复的用户 Id 将被当作是同一用户</param>
            /// <returns></returns>
            public async static Task<Model.User.CheckOnlineResult> CheckOnline(string userId)
            {
                var data = new Dictionary<string, string>();
                data.Add("userId", userId);
                var json = await tool.Http.Post("https://api.cn.ronghub.com/user/refresh.json", data);
                return tool.Json.DataContractJsonDeSerialize<Model.User.CheckOnlineResult>(json);
            }
        }

        /// <summary>
        /// 用户封禁服务
        /// </summary>
        public class Unblock
        {
            /// <summary>
            /// 封禁用户 方法
            /// </summary>
            /// <param name="userId">用户 Id</param>
            /// <param name="minute"> 封禁时长,单位为分钟，最大值为43200分钟 </param>
            /// <returns></returns>
            public async static Task<Model.User.Unblock.BlockResult> Block(string userId,string minute)
            {
                var data = new Dictionary<string, string>();
                data.Add("userId", userId);
                data.Add("minute", minute);
                var json = await tool.Http.Post("https://api.cn.ronghub.com/user/block.json", data);
                return tool.Json.DataContractJsonDeSerialize<Model.User.Unblock.BlockResult>(json);
            }

            /// <summary>
            /// 解除用户封禁 方法
            /// </summary>
            /// <param name="userId">用户 Id</param>
            /// <returns></returns>
            public async static Task<Model.User.Unblock.UnblockResult> UnBlock(string userId)
            {
                var data = new Dictionary<string, string>();
                data.Add("userId", userId);
                var json = await tool.Http.Post("https://api.cn.ronghub.com/user/unblock.json", data);
                return tool.Json.DataContractJsonDeSerialize<Model.User.Unblock.UnblockResult>(json);
            }

            /// <summary>
            /// 获取被封禁用户 方法
            /// </summary>
            /// <returns></returns>
            public async static Task<Model.User.Unblock.QueryResult> Query()
            {
                var data = new Dictionary<string, string>();
                var json = await tool.Http.Post("https://api.cn.ronghub.com/user/block/query.json", data);
                return tool.Json.DataContractJsonDeSerialize<Model.User.Unblock.QueryResult>(json);
            }
        }

        /// <summary>
        /// 用户黑名单服务
        /// </summary>
        public class Blacklist
        {
            /// <summary>
            /// 添加用户到黑名单 方法
            /// </summary>
            /// <param name="userId">用户 Id</param>
            /// <param name="blackUserId">被加黑的用户Id</param>
            /// <returns></returns>
            public async static Task<Model.User.Blacklist.AddResult> Add(string userId,string blackUserId)
            {
                var data = new Dictionary<string, string>();
                data.Add("userId", userId);
                data.Add("blackUserId", blackUserId);
                var json = await tool.Http.Post("https://api.cn.ronghub.com/user/blacklist/add.json", data);
                return tool.Json.DataContractJsonDeSerialize<Model.User.Blacklist.AddResult>(json);
            }

            /// <summary>
            /// 从黑名单中移除用户 方法
            /// </summary>
            /// <param name="userId">用户 Id</param>
            /// <param name="blackUserId">被加黑的用户Id</param>
            /// <returns></returns>
            public async static Task<Model.User.Blacklist.RemoveResult> Remove(string userId, string blackUserId)
            {
                var data = new Dictionary<string, string>();
                data.Add("userId", userId);
                data.Add("blackUserId", blackUserId);
                var json = await tool.Http.Post("https://api.cn.ronghub.com/user/blacklist/remove.json", data);
                return tool.Json.DataContractJsonDeSerialize<Model.User.Blacklist.RemoveResult>(json);
            }

            /// <summary>
            /// 获取某用户的黑名单列表 方法
            /// </summary>
            /// <param name="userId">用户 Id</param>
            /// <returns></returns>
            public async static Task<Model.User.Blacklist.QueryResult> Query(string userId)
            {
                var data = new Dictionary<string, string>();
                data.Add("userId", userId);
                var json = await tool.Http.Post("https://api.cn.ronghub.com/user/blacklist/query.json", data);
                return tool.Json.DataContractJsonDeSerialize<Model.User.Blacklist.QueryResult>(json);
            }
        }
    }
}
