using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RongCloud_SDK
{
    public class Model
    {
        /// <summary>
        /// 开发者平台分配的 App Key
        /// </summary>
        public static string AppKey { get; set; }

        /// <summary>
        /// 开发者平台分配的 App Secret
        /// </summary>
        public static string AppSecret{get;set;}

        public class User
        {
            public class TokenResult
            {
                public string code { get; set; }
                public string token { get; set; }
                public string userId { get; set; }
            }
            public class RefreshResult
            {
                public string code { get; set; }
            }
            public class CheckOnlineResult
            {
                public string code { get; set; }
                public string status { get; set; }
            }
            public class Unblock
            {
                public class BlockResult
                {
                    public string code { get; set; }
                }
                public class UnblockResult
                {
                    public string code { get; set; }
                }
                public class QueryResult
                {
                    public string code { get; set; }
                    public List<user> users { get; set; }
                    public class user
                    {
                        public string userId { get; set; }
                        public string blockEndTime { get; set; }
                    }
                }
            }
            public class Blacklist
            {
                public class AddResult
                {
                    public string code { get; set; }
                }
                public class RemoveResult
                {
                    public string code { get; set; }
                }
                public class QueryResult
                {
                    public string code { get; set; }
                    public List<string> users { get; set; }
                }
            }
        }
    }
}
