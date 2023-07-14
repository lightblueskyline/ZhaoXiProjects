using AutoMapper;
using Model.DTO.Menu;
using Model.DTO.Role;
using Model.DTO.User;
using Model.Entity;

namespace ExeWebApi.Config
{
    /// <summary>
    /// 数据传输对象的映射
    /// </summary>
    public class AutoMapperConfigs : Profile
    {
        public AutoMapperConfigs()
        {
            // 角色
            CreateMap<RoleAdd, Role>();
            CreateMap<RoleEdit, Role>();

            // 用户
            CreateMap<UserAdd, Users>();
            CreateMap<UserEdit, Users>();

            // 菜单
            CreateMap<MenuAdd, Menu>();
            CreateMap<MenuEdit, Menu>();
        }
    }
}
