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

namespace WP.NetCore.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IBaseRepository<User> baseRepository;
        private readonly IBaseRepository<UserRole> userRoleRepository;
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public UserService(IBaseRepository<User> baseRepository, 
            IBaseRepository<UserRole> userRoleRepository,
            IUnitOfWork uow,
            IMapper mapper)
        {
            this.baseRepository = baseRepository;
            this.userRoleRepository = userRoleRepository;
            this.uow = uow;
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
            var objUser=await baseRepository.LoadAsync(x => x.UserName == userName && x.Password == passWord);
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

                await uow.BeginAsync();
                objUser.Id = new Snowflake().GetId();
                List<UserRole> listAdd = new List<UserRole>();
                listRoles.ForEach(item =>
                {
                    listAdd.Add(new UserRole() { Id = new Snowflake().GetId(), RoleId = item, UserId = objUser.Id });
                });
                await uow.DbContext.AddAsync(objUser);
                await uow.DbContext.AddRangeAsync(listAdd.AsEnumerable());
                await uow.CommitAsync();
            }
            catch (Exception ex)
            {
                await uow.RollbackAsync();
            }
        }


        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="listRoles"></param>
        /// <returns></returns>
        public async Task EditUserAsync(User objUser, List<long> listRoles)
        {
            try
            {
                await uow.BeginAsync();
                List<UserRole> listAdd = new List<UserRole>();
                listRoles.ForEach(item =>
                {
                    listAdd.Add(new UserRole() { Id = new Snowflake().GetId(), RoleId = item, UserId = objUser.Id });
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
