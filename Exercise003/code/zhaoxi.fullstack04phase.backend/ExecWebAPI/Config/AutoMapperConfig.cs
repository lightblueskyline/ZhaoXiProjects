using AutoMapper;
using Model.DTO.Menu;
using Model.DTO.Role;
using Model.DTO.User;
using Model.Entity;

namespace ExecWebAPI.Config
{
    /// <summary>
    /// 实体和数据传输对象之间的映射
    /// </summary>
    public class AutoMapperConfig : Profile
    {
        /*
         * 映射的多种方式
         * 1. for 循环，单个、逐个字段赋值
         * 2. AutoMapper
         * 3. 通过 ORM 提供的映射功能
         */

        public AutoMapperConfig()
        {
            #region 角色
            //CreateMap<Role, RoleResponse>();
            CreateMap<RoleAdd, Role>();
            CreateMap<RoleEdit, Role>();
            #endregion

            #region 用户
            //CreateMap<Users, UserResponse>();
            CreateMap<UserAdd, Users>();
            CreateMap<UserEdit, Users>();
            #endregion

            #region 菜单
            //CreateMap<Menu, MenuResponse>();
            CreateMap<MenuAdd, Menu>();
            CreateMap<MenuEdit, Menu>();
            #endregion
        }
    }
}
