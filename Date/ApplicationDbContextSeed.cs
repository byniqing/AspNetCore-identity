using IdentityDemo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace IdentityDemo.Date
{
    public class ApplicationDbContextSeed
    {
        private UserManager<ApplicationUser> _userManger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="server">依赖注入的容器，这里可以获取依赖注入</param>
        /// <returns></returns>
        public async Task AsyncSpeed(ApplicationDbContext context, IServiceProvider server)
        {
            try
            {
                _userManger = server.GetRequiredService<UserManager<ApplicationUser>>();
                var logger = server.GetRequiredService<ILogger<ApplicationDbContext>>();
                logger.LogInformation("speed Init");

                //如果没有用户，则创建一个
                if (!_userManger.Users.Any())
                {
                    var defaultUser = new ApplicationUser
                    {
                        UserName = "Admin",
                        Email = "cnblogs@163.com"
                    };
                    var userResult = await _userManger.CreateAsync(defaultUser, "123456");
                    if(!userResult.Succeeded)
                    {
                        logger.LogError("创建失败");
                        //logger.LogInformation("初始化用户失败");
                        userResult.Errors.ToList().ForEach(e => {
                            logger.LogError(e.Description);
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("初始化用户失败");
            }
        }
    }
}
