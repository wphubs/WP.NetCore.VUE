using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.Repository.EFCore;
using WP.NetCore.IServices;
using WP.NetCore.Model.EntityModel;
using System.Linq;
using System.Linq.Expressions;
using WP.NetCore.Model.Dto;
using AutoMapper;
using WP.NetCore.Model;
using Microsoft.EntityFrameworkCore;
using WP.NetCore.Model.ViewModel;
using WP.NetCore.Common.Helper;
using WP.NetCore.Common;

namespace WP.NetCore.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IBaseRepository<User> baseRepository;
        private readonly IBaseRepository<UserRole> userRoleRepository;
        private readonly IUnitOfWork uow;
        private readonly IRedisCacheManager cache;
        private readonly IMapper mapper;

        public UserService(IBaseRepository<User> baseRepository, 
            IBaseRepository<UserRole> userRoleRepository,
            IUnitOfWork uow,
             IRedisCacheManager cache,
            IMapper mapper)
        {
            this.baseRepository = baseRepository;
            this.userRoleRepository = userRoleRepository;
            this.uow = uow;
            this.cache = cache;
            this.baseDal = baseRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public async Task<User> CheckUserAsync(string userName, string passWord)
        {
            var objUser=await baseRepository.LoadNoTrackingAsync(x => x.UserName == userName && x.Password == passWord&&x.IsDelete == false);
            objUser = objUser.Include(x => x.UserRoles);
            return await objUser.FirstOrDefaultAsync();
            //return await baseRepository.FirstAsync(x => x.UserName == userName && x.Password == passWord);
        }


        public async Task<PageModel<UserViewModel>> GetUserListAsync(int pageIndex, int pageSize)
        {
            var users= await baseRepository.GetPageAsync(x => x.IsDelete == false, x => x.CreateTime, pageIndex, pageSize);
            var listUser= users.Data.Include(x => x.UserRoles).ThenInclude(x=>x.Role);
          
            var objReturn = new PageModel<UserViewModel>() 
            { 
                Data = mapper.Map<List<UserViewModel>>(listUser.ToList()), 
                Total = users.Total, 
                PageIndex = pageIndex,
                PageSize = pageSize };
            return objReturn;
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="listRoles"></param>
        /// <returns></returns>
        public async Task AddUserAsync(User objUser, List<long> listRoles)
        {
            try
            {

                var listUser = await baseRepository.GetPageAsync(x => x.IsDelete == false, x => x.CreateTime, 1, 11);
                var idWork = new Snowflake();
                await uow.BeginAsync();
                objUser.Id = idWork.NextId();
                List<UserRole> listAdd = new List<UserRole>();
                listRoles.ForEach(item =>
                {
                    listAdd.Add(new UserRole() { Id = idWork.NextId(), RoleId = item, UserId = objUser.Id });
                });
                await uow.DbContext.AddAsync(objUser);
                await uow.DbContext.AddRangeAsync(listAdd.AsEnumerable());
                await uow.CommitAsync();
            }
            catch (Exception ex)
            {
                await uow.RollbackAsync();
                throw;
            }
        }


        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="listRoles"></param>
        /// <returns></returns>
        public async Task EditUserAsync(User objUser, List<long> listRoles)
        {
            try
            {
                await cache.RemovePattern(JobConstant.RoleKey);
                await uow.BeginAsync();
                List<UserRole> listAdd = new List<UserRole>();
                listRoles.ForEach(item =>
                {
                    listAdd.Add(new UserRole() { Id = new Snowflake().NextId(), RoleId = item, UserId = objUser.Id });
                });
                await baseRepository.UpdateAsync(objUser);
                var listUserRole = await userRoleRepository.GetAllAsync(x => x.UserId == objUser.Id);
                await userRoleRepository.RemoveAsync(listUserRole);
                await userRoleRepository.AddRangeAsync(listAdd.AsEnumerable());
                await uow.CommitAsync();
            }
            catch (Exception ex)
            {
                await uow.RollbackAsync();
            }
        }
    }
}
